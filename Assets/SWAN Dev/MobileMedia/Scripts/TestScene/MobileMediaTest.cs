/// <summary>
/// By SwanDEV 2017
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class MobileMediaTest : DImageDisplayHandler
{
	public CanvasScaler canvasScaler;

	public Image displayImage;
	public MeshRenderer cubeMesh;
	public Text debugText;

//	[Space()]
//	[Header("[ Downloader Settings ]")]
//	public bool isWWW = false;
//	[Range(0, 3)] public int altDownloaderLevel = 0;


	void Start()
	{
		//Check screen orientation for setting canvas resolution
		if(Screen.width > Screen.height)
		{
			canvasScaler.referenceResolution = new Vector2(1920, 1080);
		}
		else
		{
			canvasScaler.referenceResolution = new Vector2(1080, 1920);
		}
	}

	public void PickImage()
	{
		MobileMedia.PickImage((imagePath)=>{
			// Implement your code to load & use the image using the returned image path:

			//FileInfo fileInfo = new FileInfo(imagePath);
			byte[] imageBytes = File.ReadAllBytes(imagePath);
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false); 
			texture2D.LoadImage(imageBytes);

			// Display image
			ShowImage(_ToSprite(texture2D));
			if(cubeMesh) cubeMesh.material.mainTexture = (Texture) texture2D;
		});
	}

	public void PickVideo()
	{
		MobileMedia.PickVideo((videoPath)=>{
			// Implement your code to load & play the video using the returned video path:

			string result = "Video Path: " + videoPath;
			Debug.Log(result);
			debugText.text = result;
		});
	}


	public void PickImages()
	{
//		MobileMedia.PickImages((imagePaths)=>{
//			// Implement your code to load & use the image(s) using the returned image paths:
//
//			string result = "Image Count: " + imagePaths.Length;
//			for(int i=0; i<imagePaths.Length; i++)
//			{
//				result += "\n" + imagePaths[i];
//			}
//			debugText.text = result;
//		});
	}

	public void PickVideos()
	{
//		MobileMedia.PickVideos((videoPaths)=>{
//			// Implement your code to load & play the video(s) using the returned video paths:
//
//			string result = "Video Count: " + videoPaths.Length;
//			for(int i=0; i<videoPaths.Length; i++)
//			{
//				result += "\n" + videoPaths[i];
//			}
//			debugText.text = result;
//		});
	}

	public void SaveJPG()
	{
		TakeScreenshot((tex2D)=>{
			MobileMedia.SaveImage(tex2D, "MobileMediaTest", new FilePathName().GetJpgFileName(), MobileMedia.ImageFormat.JPG);
			ShowImage(_ToSprite(tex2D));
		});
	}

	public void SavePNG()
	{
		TakeScreenshot((tex2D)=>{
			MobileMedia.SaveImage(tex2D, "MobileMediaTest", new FilePathName().GetPngFileName(), MobileMedia.ImageFormat.PNG);
			ShowImage(_ToSprite(tex2D));
		});
	}

	public void SaveGIF()
	{
		TakeScreenshot((tex2D)=>{
			MobileMedia.SaveImage(tex2D, "MobileMediaTest", new FilePathName().GetGifFileName(), MobileMedia.ImageFormat.GIF);
			ShowImage(_ToSprite(tex2D));
		});
	}

	public void SaveMP4()
	{
		string existingVideoPath = ""; // Path.Combine(Application.streamingAssetsPath, "dominos-pizza.mp4");
		List<string> mp4Paths = new FilePathName().GetFilePaths(Application.streamingAssetsPath, new List<string>{".mp4"});
		debugText.text = "mp4Paths: ";
		for(int i=0; i<mp4Paths.Count; i++)
		{
			debugText.text = debugText.text + "\n" + mp4Paths[i];
		}
		//debugText.text = existingVideoPath;
		Debug.Log("mp4Paths: " + debugText.text);

		if(mp4Paths == null) return;
		if(mp4Paths.Count == 0) return;

		existingVideoPath = mp4Paths[UnityEngine.Random.Range(0, mp4Paths.Count)];

		Debug.Log("existingVideoPath: " + existingVideoPath + " mp4Paths.Count: " + mp4Paths.Count);

//		if(isWWW) // Use WWWDownloader
//		{
//			_WWWDownloader(existingVideoPath);
//		}
//		else // Use WCDownloader
//		{
//			_WCDownloader(existingVideoPath);
//		}
	}


	public InputField downloaderInput;
	public void OnStartButtonClicked()
	{
		if(downloaderInput != null && !string.IsNullOrEmpty(downloaderInput.text))
		{
			string url = downloaderInput.text;

//			if(isWWW) // Use WWWDownloader
//			{
//				_WWWDownloader(url);
//			}
//			else // Use WCDownloader
//			{
//				_WCDownloader(url);
//			}
		}
	}

	private void _WWWDownloader(string url)
	{
//		WWWDownloader wwwdl = WWWDownloader.CreateDownloader(0);
//		wwwdl.m_AltWCDownloader = true;
//		wwwdl.m_AltDownloaderLevel = altDownloaderLevel;
//		wwwdl.Download(url,
//			(smartData)=>{
//
//				_OnDownloadComplete(smartData);
//
//			},
//			(index, progress)=>{
//
//			}
//		);
	}

	private void _WCDownloader(string url)
	{
//		WCDownloader wcdl = WCDownloader.CreateDownloader(0);
//		wcdl.DownloadAsync_Partially(url, Path.Combine(Application.persistentDataPath, "MyFileName52442.zip"), debugText);
//		Debug.Log("MyFileName52442");
//
//		WCDownloader wcdl = WCDownloader.CreateDownloader(0);
//		wcdl.m_AltWWWDownloader = true;
//		wcdl.m_AltDownloaderLevel = altDownloaderLevel;
//		wcdl.DownloadAsync(url,
//			(smartData)=>{
//
//				_OnDownloadComplete(smartData);
//
//			}, (index, progress)=>{
//
//			}
//		);

	}

//	private void _OnDownloadComplete(SmartData smartData)
//	{
//		
//		string fileName = Path.GetFileNameWithoutExtension(smartData.m_FileNameWithExtension);
//		string fileExtension = Path.GetExtension(smartData.m_FileNameWithExtension);
//		Debug.Log("fileExtension: " + fileExtension + " fileNameWithExtension: " + smartData.m_FileNameWithExtension);
//
//		string savePath = "Save Path: ";
//		if(smartData.m_Data != null && smartData.m_Data.Length > 0)
//		{
//			bool isImage = (new List<string>{".jpg", ".jpeg", ".png", ".gif"}).Contains(fileExtension.ToLower());
//
//			// Save the byte array directly
//			savePath += MobileMedia.SaveBytes(smartData.m_Data, "TestFolder", Path.GetFileNameWithoutExtension(fileName), fileExtension, isImage);
//
//			// Convert to SmartObject: except for byte array, you can also 
//			// get Texture2D/AudioClip/AssetBundle/Text/MovieTexture if the download data if of that type.
//			smartData.ConvertToSmartObject((sObject)=>{
//				savePath += MobileMedia.SaveBytes(sObject.bytes, "TestFolder_SmartObject", Path.GetFileNameWithoutExtension(fileName), fileExtension, isImage);
//				debugText.text = "1. " + savePath;
//
//				Debug.Log("sObject.text: " + sObject.text);
//
//				sObject.Clear();
//			});
//
//		}
//		else
//		{
//			Debug.Log((smartData.m_Data == null)? "data is null!":"data.Length: " + smartData.m_Data.Length);
//		}
//		debugText.text = "2. " + savePath;
//	}

	private void ShowImage(Sprite sprite)
	{
		base.SetImage(displayImage, sprite);
	}


	#region ----- Others -----
	public void MoreAssetsAndDocuments()
	{
		Application.OpenURL("https://www.swanob2.com/assets");
	}

	public void TakeScreenshot(Action<Texture2D> onComplete)
	{
		StartCoroutine(_TakeScreenshot(onComplete));
	}

	private IEnumerator _TakeScreenshot(Action<Texture2D> onComplete)
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D readTex = new Texture2D(width, height, TextureFormat.RGB24, false);
		Rect rect = new Rect(0, 0, width, height);
		readTex.ReadPixels(rect, 0, 0);
		readTex.Apply();
		onComplete(readTex);
	}

	private Sprite _ToSprite(Texture2D texture)
	{
		if(texture == null) return null;

		Vector2 pivot = new Vector2(0.5f, 0.5f);
		float pixelPerUnit = 100;
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, pixelPerUnit);
	}
	#endregion

}
