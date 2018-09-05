using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileMediaAndProGIF_Demo : MonoBehaviour
{
	public Transform m_PanelUILayer;
	public GameObject m_PlayerPrefab;

	public Image m_ImageDisplay;
	public Renderer[] m_TargetRenderers;

	public Toggle m_Toggle_UseNativePath; 	// Load media from native path is available on Android only
	public Toggle m_Toggle_UsePopup;		// Popup is available on iOS only
	public Slider m_ProgressSlider;
	public Text m_ProgressText;
	public Text m_Title;

	private string _defaultTitle;
	private void Start()
	{
		_defaultTitle = m_Title.text;
	}

	public void GetPreviewPhoto()
	{
		//Test
		int mediaType = Random.Range(0, 3);
		int mediaIndex = (Random.Range(0, 3) == 0)? -1:Random.Range(0, 10);
		int targetSize = (Random.Range(0, 2) == 0)? 0:100;

		MobileMedia.GetMediaPreviewPhoto_IOS(
			(previewPhotoPath)=>{

				// Receive the path of the newly generated preview photo (.PNG format)
				if(!string.IsNullOrEmpty(previewPhotoPath))
				{
					string extension = System.IO.Path.GetExtension(previewPhotoPath);
					m_Title.text = _defaultTitle + "\nFile type: " + extension.ToUpper() + " | mediaType: " + mediaType + 
						" | mediaIndex: " + mediaIndex + " | targetSize: " + targetSize + "\nPath : " + previewPhotoPath;

					_Play(previewPhotoPath, m_ImageDisplay, false);
				}
				else
				{
					m_Title.text = _defaultTitle + "\nPath is Null!" + " | mediaType: " + mediaType + " | mediaIndex: " + mediaIndex + " | targetSize: " + targetSize;
				}

			},
			mediaType,
			mediaIndex,
			targetSize,
			"preview"
		);
	}

	public void PickGifFromGallery(int rendererIndex)
	{
		if(Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
		{
			Debug.LogWarning("Trying to pick image in Editor. The native image picker work on Android & iOS device only. Please build your app and try it on devices!");
			return;
		}

		MobileMedia.PickImage((pickedPath)=>{

			if(pickedPath != null)
			{
				string extension = System.IO.Path.GetExtension(pickedPath);
				m_Title.text = _defaultTitle + "\nFile type: " + extension.ToUpper() + " : " + pickedPath;

				string gifPath = pickedPath;

				bool isGIF = pickedPath.ToLower().EndsWith(".gif");
				if(isGIF)
				{
					if(!m_Toggle_UseNativePath.isOn)
					{
						// Save a copy of the picked GIF to the application path
						FilePathName filePathName = new FilePathName();
						gifPath = filePathName.GetGifFullPath();
						filePathName.CopyFile(pickedPath, gifPath);
					}

					// Decode & Play the copy of the picked GIF, as the GIF is decoded as textures in the memory, you can even edit them if you want.
					if(rendererIndex >= 0 && rendererIndex < m_TargetRenderers.Length) // Display gif on Renderer
					{
						_Play(gifPath, m_TargetRenderers[rendererIndex], isGIF);
					}
					else if(rendererIndex == -1) // Display gif on Image
					{
						_Play(gifPath, m_ImageDisplay, isGIF);
					}
				}
				else
				{
					if(rendererIndex >= 0 && rendererIndex < m_TargetRenderers.Length)
					{
						_Play(gifPath, m_TargetRenderers[rendererIndex], isGIF);
					}
					else if(rendererIndex == -1)
					{
						_Play(gifPath, m_ImageDisplay, isGIF);
					}
				}
			}
			else
			{
				m_Title.text = _defaultTitle + "\nPath is Null!";
			}

		}, "Picking GIF (Pro GIF)", "image/*", m_Toggle_UsePopup.isOn, "temp");

	}

	public void ShowGifPlayer() 
	{
		ProGifPlayerPanel player = ProGifPlayerPanel.Create(m_PlayerPrefab, m_PanelUILayer);
		player.Setup("", (progress)=>{
			
			m_ProgressSlider.value = progress;
			m_ProgressText.text = "Progress : " + Mathf.CeilToInt(progress * 100) + "%";

		});
	}

	private void _Play(string filePath, Renderer targetRenderer, bool isGIF)
	{
		if(targetRenderer != null)
		{
			if(isGIF)
			{
				PGif.iPlayGif(filePath, targetRenderer, targetRenderer.name, (progress)=>{

					// Update decode progress of the GIF
					_OnProgress(progress, targetRenderer.name);

				}, shouldSaveFromWeb:false);
			}
			else
			{
				PGif.iClearPlayer(targetRenderer.name);

				string extension = System.IO.Path.GetExtension(filePath);
				List<string> supportImageTypes = new List<string>(){".jpg", ".jpeg", ".png"};
				if(supportImageTypes.Contains(extension.ToLower()))
				{
					FilePathName filePathName = new FilePathName();
					Texture2D tex2D = filePathName.LoadImage(filePath);
					if(tex2D != null)
					{
						targetRenderer.material.mainTexture = tex2D;
					}
				}
			}
		}
	}

	private void _Play(string filePath, Image targetImage, bool isGIF)
	{
		if(targetImage != null)
		{
			DImageDisplayHandler imageHandler = targetImage.gameObject.GetComponent<DImageDisplayHandler>();

			if(isGIF)
			{
				PGif.iPlayGif(filePath, targetImage, targetImage.name, (progress)=>{

					// Update decode progress of the GIF
					_OnProgress(progress, targetImage.name);

				}, shouldSaveFromWeb:false);

				PGif.iGetPlayer(targetImage.name).SetOnFirstFrameCallback((firstFrame)=>{
					if(imageHandler != null)
					{
						imageHandler.SetImage(targetImage, firstFrame.gifTexture.GetSprite());
					}
				});
			}
			else
			{
				PGif.iClearPlayer(targetImage.name);

				string extension = System.IO.Path.GetExtension(filePath);
				List<string> supportImageTypes = new List<string>(){".jpg", ".jpeg", ".png"};
				if(supportImageTypes.Contains(extension.ToLower()))
				{
					FilePathName filePathName = new FilePathName();
					Texture2D tex2D = filePathName.LoadImage(filePath);
					if(tex2D != null)
					{
						if(imageHandler != null)
						{
							imageHandler.SetImage(targetImage, tex2D);
						}
						else
						{
							m_ImageDisplay.sprite = filePathName.Texture2DToSprite(tex2D);
							m_ImageDisplay.SetNativeSize();
						}
					}
				}
			}
		}
	}

	private void _OnProgress(float progress, string playerName)
	{
		m_ProgressSlider.value = progress;
		m_ProgressText.text = "Progress : " + Mathf.CeilToInt(progress * 100) + "%";

		//Set the gif size when the first frame decode is finished and assigned to targetRenderer
		//Set renderer transform scale here:
		int gifWidth = PGif.iGetPlayer(playerName).width;
		int gifHeight = PGif.iGetPlayer(playerName).height;
	}
}
