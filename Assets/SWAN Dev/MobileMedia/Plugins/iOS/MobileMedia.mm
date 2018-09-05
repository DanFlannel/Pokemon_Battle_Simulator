// This plugin requires Unity 4.0 or later, iOS 8.0 or later //

#import <Foundation/Foundation.h>
#import <Photos/Photos.h>
#import <MobileCoreServices/UTCoreTypes.h>

#ifdef UNITY_4_0
#import "iPhone_View.h"
#elif UNITY_5_0
#import "iPhone_View.h"
#else
extern UIViewController* UnityGetGLViewController();
#endif


@interface iMobileMedia:NSObject
+ (int)checkPermission;
+ (int)requestPermission;
+ (int)canOpenSettings;
+ (void)openSettings;
+ (void)saveMedia:(NSString *)path albumName:(NSString *)album isImage:(BOOL)isImage;
+ (void)pickMedia:(BOOL)isImage savePath:(NSString *)imageTempPath usePopup:(BOOL)isPopup;
+ (int)isMediaPickerBusy;
@end


@implementation iMobileMedia

static NSString *unityImageTempPath;
static UIPopoverController *popup;
static UIImagePickerController *mediaPicker;
static int mediaPickerState = 0; // 0: none, 1: showing (always in this state on iPad), 2: finished

//#pragma clang diagnostic push
//#pragma clang diagnostic ignored "-Wdeprecated-declarations"
+ (int)checkPermission
{
    // Require iOS version >= 8 (Using photos framework)
    PHAuthorizationStatus status = [PHPhotoLibrary authorizationStatus];
    if(status == PHAuthorizationStatusAuthorized)
    {
        return 1;
    }
    else if(status == PHAuthorizationStatusNotDetermined )
    {
        return 2;
    }
    else
    {
        return 0;
    }
}
//#pragma clang diagnostic pop

+ (int)requestPermission
{
    // Require iOS version >= 8 (Using photos framework)
    PHAuthorizationStatus status = [PHPhotoLibrary authorizationStatus];
    
    if(status == PHAuthorizationStatusAuthorized)
    {
        return 1;
    }
    else if(status == PHAuthorizationStatusNotDetermined)
    {
        __block BOOL authorized = NO;
        
        dispatch_semaphore_t sema = dispatch_semaphore_create(0);
        [PHPhotoLibrary requestAuthorization:^(PHAuthorizationStatus status)
         {
             authorized = (status == PHAuthorizationStatusAuthorized);
             dispatch_semaphore_signal(sema);
         }];
        dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
        
        if(authorized)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    else
    {
        return 0;
    }
}

+ (int)canOpenSettings
{
    // If target iOS version is 8.0 or later, always return TRUE
    if(&UIApplicationOpenSettingsURLString != NULL)
    {
        return 1;
    }
    else
    {
        return 0;
    }
}

+ (void)openSettings
{
    // If target iOS version is 8.0 or later, always return TRUE
    if(&UIApplicationOpenSettingsURLString != NULL)
    {
        [[UIApplication sharedApplication] openURL:[NSURL URLWithString:UIApplicationOpenSettingsURLString]];
    }
}

+ (void)saveMedia:(NSString *)path albumName:(NSString *)album isImage:(BOOL)isImage
{
    // Require iOS version >= 8 (Using photos framework)
    void (^saveBlock)(PHAssetCollection *assetCollection) = ^void(PHAssetCollection *assetCollection)
    {
        [[PHPhotoLibrary sharedPhotoLibrary] performChanges:^ {
            PHAssetChangeRequest *assetChangeRequest;
            if (isImage)
            {
                assetChangeRequest = [PHAssetChangeRequest creationRequestForAssetFromImageAtFileURL:[NSURL fileURLWithPath:path]];
            }
            else
            {
                assetChangeRequest = [PHAssetChangeRequest creationRequestForAssetFromVideoAtFileURL:[NSURL fileURLWithPath:path]];
            }
            
            PHAssetCollectionChangeRequest *assetCollectionChangeRequest = [PHAssetCollectionChangeRequest changeRequestForAssetCollection:assetCollection];
            [assetCollectionChangeRequest addAssets:@[[assetChangeRequest placeholderForCreatedAsset]]];
            
        } completionHandler:^(BOOL success, NSError *error) {
            if (!success)
            {
                NSLog(@"Error creating asset: %@", error);
            }
            
            [[NSFileManager defaultManager] removeItemAtPath:path error:nil];
        }];
    };
    
    PHFetchOptions *fetchOptions = [[PHFetchOptions alloc] init];
    fetchOptions.predicate = [NSPredicate predicateWithFormat:@"localizedTitle = %@", album];
    PHFetchResult *fetchResult = [PHAssetCollection fetchAssetCollectionsWithType:PHAssetCollectionTypeAlbum subtype:PHAssetCollectionSubtypeAny options:fetchOptions];
    if(fetchResult.count > 0)
    {
        saveBlock(fetchResult.firstObject);
    }
    else
    {
        __block PHObjectPlaceholder *albumPlaceholder;
        [[PHPhotoLibrary sharedPhotoLibrary] performChanges:^ {
            PHAssetCollectionChangeRequest *changeRequest = [PHAssetCollectionChangeRequest creationRequestForAssetCollectionWithTitle:album];
            albumPlaceholder = changeRequest.placeholderForCreatedAssetCollection;
            
        } completionHandler:^(BOOL success, NSError *error) {
            if(success)
            {
                PHFetchResult *fetchResult = [PHAssetCollection fetchAssetCollectionsWithLocalIdentifiers:@[albumPlaceholder.localIdentifier] options:nil];
                if(fetchResult.count > 0)
                {
                    saveBlock(fetchResult.firstObject);
                }
                else
                {
                    [[NSFileManager defaultManager] removeItemAtPath:path error:nil];
                }
            }
            else
            {
                NSLog(@"Error creating album: %@", error);
                [[NSFileManager defaultManager] removeItemAtPath:path error:nil];
            }
        }];
    }
}

#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wdeprecated-declarations"
+ (void)TestSaveMedia:(NSString *)path albumName:(NSString *)albumName
{
    // On hold
}
#pragma clang diagnostic pop

+ (int)isMediaPickerBusy
{
    if(mediaPickerState == 2)
    {
        return 1;
    }
    
    if(mediaPicker != nil)
    {
        if(mediaPickerState == 1 || [mediaPicker presentingViewController] == UnityGetGLViewController())
        {
            return 1;
        }
        else
        {
            mediaPicker = nil;
            return 0;
        }
    }
    else
    {
        return 0;
    }
}

+ (void)pickMedia:(BOOL)isImage savePath:(NSString *)imageTempPath usePopup:(BOOL)isPopup
{
    mediaPicker = [[UIImagePickerController alloc] init];
    mediaPicker.delegate = self;
    mediaPicker.allowsEditing = NO;
    mediaPicker.sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
    
    if(isImage)
    {
        mediaPicker.mediaTypes = [NSArray arrayWithObject:(NSString *)kUTTypeImage];
    }
    else
    {
        mediaPicker.mediaTypes = [NSArray arrayWithObjects:(NSString *)kUTTypeMovie, (NSString *)kUTTypeVideo, nil];
    }
    
    unityImageTempPath = imageTempPath;
    
    mediaPickerState = 1;
    UIViewController *rootViewController = UnityGetGLViewController();
    if(!isPopup) //(UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone) // iPhone
    {
        [rootViewController presentViewController:mediaPicker animated:YES completion:^{ mediaPickerState = 0; }];
    }
    else // iPad
    {
        popup = [[UIPopoverController alloc] initWithContentViewController:mediaPicker];
        popup.delegate = self;
        [popup presentPopoverFromRect:CGRectMake( rootViewController.view.frame.size.width / 2, rootViewController.view.frame.size.height / 4, 0, 0 ) inView:rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
    }
}

+ (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info
{
    NSString *path;
    if([info[UIImagePickerControllerMediaType] isEqualToString:(NSString *)kUTTypeImage]) // image picked
    {
        NSURL * refUrl = [info objectForKey:UIImagePickerControllerReferenceURL];
        if(refUrl)
        {
            // Use the file name & extension in Photos
            //NSString * tempFilePathWithExtension = [[unityImageTempPath stringByAppendingString:@"/"] stringByAppendingString:refUrl.lastPathComponent];
            
            // Use the file extension in Photos
            NSString * tempFilePathWithExtension = [unityImageTempPath stringByAppendingString:[@"." stringByAppendingString:refUrl.pathExtension]];
            
            PHAsset * asset = [[PHAsset fetchAssetsWithALAssetURLs:@[refUrl] options:nil] lastObject];
            if(asset)
            {
                PHImageRequestOptions *options = [[PHImageRequestOptions alloc] init];
                options.synchronous = YES;
                options.networkAccessAllowed = NO;
                options.deliveryMode = PHImageRequestOptionsDeliveryModeHighQualityFormat;
                [[PHImageManager defaultManager] requestImageDataForAsset:asset options:options resultHandler:^(NSData * _Nullable imageData, NSString * _Nullable dataUTI, UIImageOrientation orientation, NSDictionary * _Nullable info)
                 {
                     NSNumber * isError = [info objectForKey:PHImageErrorKey];
                     NSNumber * isCloud = [info objectForKey:PHImageResultIsInCloudKey];
                     if([isError boolValue] || [isCloud boolValue] || ! imageData)
                     {
                         // fail
                         [iMobileMedia sendUnityMessageOnImagePicked:nil];
                     }
                     else
                     {
                         // success
                         // write the picked file data(imageData) to file in the provided path
                         [imageData writeToFile:tempFilePathWithExtension atomically:YES];
                         
                         // send the path back to Unity
                         //[iMobileMedia sendUnityMessageOnImagePicked:unityImageTempPath];
                         [iMobileMedia sendUnityMessageOnImagePicked:tempFilePathWithExtension];
                     }
                     
                     [picker dismissViewControllerAnimated:YES completion:nil];
                 }];
            }
        }
        
        // Save temp image as PNG
        //UIImage *image = info[UIImagePickerControllerOriginalImage];
        //if(image == nil)
        //{
        //Origin
        //path = nil;
        //Origin
        //}
        //else
        //{
        //Origin
        //[UIImagePNGRepresentation(image) writeToFile:unityImageTempPath atomically:YES];
        //path = unityImageTempPath;
        //Origin
        //}
    }
    else // video picked
    {
        NSURL *mediaUrl = info[UIImagePickerControllerMediaURL] ?: info[UIImagePickerControllerReferenceURL];
        if(mediaUrl == nil)
        {
            path = nil;
        }
        else
        {
            path = [mediaUrl path];
        }
        
        [iMobileMedia sendUnityMessageOnImagePicked:path];
        
        [picker dismissViewControllerAnimated:YES completion:nil];
    }
    
    /*
     if(path == nil)
     {
     path = @"";
     }
     
     const char *pathUTF8 = [path UTF8String];
     char *result = (char*) malloc(strlen(pathUTF8) + 1);
     strcpy(result, pathUTF8);
     
     popup = nil;
     mediaPicker = nil;
     mediaPickerState = 2;
     UnitySendMessage("MMPickerReceiver_iOS", "OnMediaReceived", result);
     
     [picker dismissViewControllerAnimated:YES completion:nil];
     */
}

+ (void)sendUnityMessageOnImagePicked:(NSString *)imageTempPath
{
    if(imageTempPath == nil)
    {
        imageTempPath = @"";
    }
    
    const char *pathUTF8 = [imageTempPath UTF8String];
    char *result = (char*) malloc(strlen(pathUTF8) + 1);
    strcpy(result, pathUTF8);
    
    popup = nil;
    mediaPicker = nil;
    mediaPickerState = 2;
    UnitySendMessage("MMPickerReceiver_iOS", "OnMediaReceived", result);
}

+ (void)imagePickerControllerDidCancel:(UIImagePickerController *)picker
{
    popup = nil;
    mediaPicker = nil;
    UnitySendMessage("MMPickerReceiver_iOS", "OnMediaReceived", "");
    
    [picker dismissViewControllerAnimated:YES completion:nil];
}

+ (void)popoverControllerDidDismissPopover:(UIPopoverController *) popoverController
{
    popup = nil;
    mediaPicker = nil;
    UnitySendMessage("MMPickerReceiver_iOS", "OnMediaReceived", "");
}


+ (void)getMediaPreviewPhoto:(int)mediaType mediaIndex:(int)mediaIndex targetSize:(int)size tempSavePath:(NSString *)unityImageTempPath
{
    PHFetchOptions *fetchOptions = [[PHFetchOptions alloc] init];
    fetchOptions.sortDescriptors = @[[NSSortDescriptor sortDescriptorWithKey:@"creationDate" ascending:NO]];
    
    //PHFetchResult *_allMedia = [PHAsset fetchAssetsWithOptions:fetchOptions]; // Image + Video + Audio + Unknown

    int assetIndex = 0; //Default get the first asset in the list(most recent file).
    PHAsset *asset;
    
    if(assetIndex == 0) // Photo
    {
        PHFetchResult *allPhotos = [PHAsset fetchAssetsWithMediaType:PHAssetMediaTypeImage options:fetchOptions];
        if(mediaIndex < 0) // Get the last one
        {
            assetIndex = (int)allPhotos.count - 1;
        }
        else if(mediaIndex < (int)allPhotos.count)
        {
            assetIndex = mediaIndex;
        }
        asset = [allPhotos objectAtIndex:assetIndex];
    }
    else if(assetIndex == 1) // Video
    {
        PHFetchResult *allVideos = [PHAsset fetchAssetsWithMediaType:PHAssetMediaTypeVideo options:fetchOptions];
        if(mediaIndex < 0) // Get the last one
        {
            assetIndex = (int)allVideos.count - 1;
        }
        else if(mediaIndex < (int)allVideos.count)
        {
            assetIndex = mediaIndex;
        }
        asset = [allVideos objectAtIndex:assetIndex];
    }
    else // Photo or Video
    {
        PHFetchResult *allPhotos = [PHAsset fetchAssetsWithMediaType:PHAssetMediaTypeImage options:fetchOptions];
        if(mediaIndex < 0) // Get the last one
        {
            assetIndex = (int)allPhotos.count - 1;
        }
        else if(mediaIndex < (int)allPhotos.count)
        {
            assetIndex = mediaIndex;
        }
        PHAsset *asset1 = [allPhotos objectAtIndex:assetIndex];
        
        PHFetchResult *allVideos = [PHAsset fetchAssetsWithMediaType:PHAssetMediaTypeVideo options:fetchOptions];
        if(mediaIndex < 0) // Get the last one
        {
            assetIndex = (int)allVideos.count - 1;
        }
        else if(mediaIndex < (int)allVideos.count)
        {
            assetIndex = mediaIndex;
        }
        PHAsset *asset2 = [allVideos objectAtIndex:assetIndex];
        
        if([asset1.creationDate timeIntervalSinceReferenceDate] > [asset2.creationDate timeIntervalSinceReferenceDate])
        {
            asset = asset1;
        }
        else
        {
            asset = asset2;
        }
    }
    
    int newWidth = size;
    int newHeight = size;
    if(size <= 0) // for size less or equal Zero, use the size of the asset instead.
    {
        //newSize = MIN((int)asset.pixelWidth, (int)asset.pixelHeight);
        newWidth = (int)asset.pixelWidth;
        if(newWidth <= 0) newWidth = 100;
        
        newHeight = (int)asset.pixelHeight;
        if(newHeight <= 0) newHeight = 100;
    }
    
    // Request an image for the asset from the PHCachingImageManager.
    PHImageManager *imageManager = [PHImageManager defaultManager];
    [imageManager requestImageForAsset:asset targetSize:CGSizeMake(newWidth, newHeight) contentMode:PHImageContentModeAspectFill options:nil resultHandler:^(UIImage *result, NSDictionary *info) {
        //UIImage *image = info[UIImagePickerControllerOriginalImage];
        
        if(result == nil)
        {
            [iMobileMedia sendUnityMessageOnPreviewPhotoPicked:nil];
        }
        else
        {
            NSString *filePathWithExtension = [unityImageTempPath stringByAppendingString:@".PNG"];
            [UIImagePNGRepresentation(result) writeToFile:filePathWithExtension atomically:YES];
            
            // send the path back to Unity
            [iMobileMedia sendUnityMessageOnPreviewPhotoPicked:filePathWithExtension];
        }
    }];
}

+ (void)sendUnityMessageOnPreviewPhotoPicked:(NSString *)imageTempPath
{
    if(imageTempPath == nil)
    {
        imageTempPath = @"";
    }
    
    const char *pathUTF8 = [imageTempPath UTF8String];
    char *result = (char*) malloc(strlen(pathUTF8) + 1);
    strcpy(result, pathUTF8);
    
    UnitySendMessage("MMPickerReceiver_iOS", "OnMediaReceived", result);
}

@end


// ---- Extern -----------------------------------------------------------
extern "C" int iCheckPermission()
{
    return [iMobileMedia checkPermission];
}

extern "C" int iRequestPermission()
{
    return [iMobileMedia requestPermission];
}

extern "C" int iCanOpenSettings()
{
    return [iMobileMedia canOpenSettings];
}

extern "C" void iOpenSettings()
{
    [iMobileMedia openSettings];
}

extern "C" void iSaveImage(const char* path, const char* album)
{
    [iMobileMedia saveMedia:[NSString stringWithUTF8String:path] albumName:[NSString stringWithUTF8String:album] isImage:YES];
}

extern "C" void iSaveVideo(const char* path, const char* album)
{
    [iMobileMedia saveMedia:[NSString stringWithUTF8String:path] albumName:[NSString stringWithUTF8String:album] isImage:NO];
}

extern "C" int iMediaPickerBusy()
{
    return [iMobileMedia isMediaPickerBusy];
}

extern "C" void iPickImage(const char* imageTempPath, bool isPopup)
{
    [iMobileMedia pickMedia:YES savePath:[NSString stringWithUTF8String:imageTempPath] usePopup:isPopup];
}

extern "C" void iPickVideo(bool isPopup)
{
    [iMobileMedia pickMedia:NO savePath:nil usePopup:isPopup];
}

extern "C" void iGetMediaPreviewPhoto(int mediaType, int mediaIndex, int targetSize, const char* imageTempPath)
{
    [iMobileMedia getMediaPreviewPhoto:mediaType mediaIndex:mediaIndex targetSize:targetSize tempSavePath:[NSString stringWithUTF8String:imageTempPath]];
}

