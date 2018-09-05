/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ProGifPlayerPanel : MonoBehaviour
{
	public enum DisplayType
	{
		None = 0,
		Image,
		RawImage,
		Renderer,
	}

	[Header("[ Display Target ]")]
	public DisplayType m_DisplayType = DisplayType.RawImage;
	public Image m_GifImage;
	public Renderer m_GifRenderer;
	public RawImage m_GifRawImage;

	[Header("[ GIF Decode Settings ]")]
	/// <summary> Current selected gif decoder. </summary>
	public ProGifPlayerComponent.Decoder m_Decoder = ProGifPlayerComponent.Decoder.ProGif_QueuedThread;

	/// <summary> The max. number of gif frames to decode (ProGif decoder only). </summary>
	public int m_TargetDecodeFrameNum = -1;

	/// <summary> If 'True', save the gif to local storge. </summary>
	public bool m_ShouldSaveFromWeb = true;

	[Header("[ Debug ]")]
	public string m_Url = "";
	//Test
	//m_Url = "https://media.giphy.com/media/1jgLDGD1Bn27e/giphy.gif";
	//m_Url = "https://media.giphy.com/media/dLDpXLoJBICaI/giphy.gif";
	//m_Url = "https://media.giphy.com/media/KDyoY2v2MlDHy/giphy.gif"; //@@ lu
	//m_Url = "https://media.giphy.com/media/49Nw9UgF5ht7O/giphy.gif";
	//m_Url = "https://media.giphy.com/media/wMcFYStkhc6mA/giphy.gif"; //@@ kong
	//m_Url = "https://media.giphy.com/media/3ohc0YpD0LR5wRyz1S/giphy.gif";
	//m_Url = "https://media.giphy.com/media/xUPGcreVxpx1AXOHwQ/giphy.gif";
	//m_Url = "https://media.giphy.com/media/xUPGctftozEFipUic0/giphy.gif";
	//m_Url = "https://media.giphy.com/media/eDUHhtooZxyhi/giphy.gif";

	public enum DataPathTypeDebug
	{
		/// Get complete url/path from input field
		UrlInputField = 0,

		/// Combine Application.dataPath and fileName(name.gif) from input field.
		/// (To test your gif, put your gif in the Assets folder of the project, input the gif fileName in the url input field)
		DataPath,

		/// Combine Application.persistentDataPath and fileName(name.gif) from input field.
		/// (To test your gif, find the persistentDataPath on your machine by printing Application.persistentDataPath on debug console, put your gif in it, input the gif fileName in the url input field)
		PersistentDataPath
	}
	public DataPathTypeDebug localDebugDataPath = DataPathTypeDebug.UrlInputField;


	[Header("[ Others ]")]
	public GameObject containerGO;
	public GameObject playerControlBar;

	public Button btn_ToGallery;
	public InputField input_Url;

	public Action<float> _OnLoading = null;

	private string gifPath = "";


	/// <summary>
	/// Create an instance of ProGifPlayerPanel from provided prefab, and set parent.
	/// </summary>
	/// <param name="prefab">The Prefab of ProGifPlayerPanel.</param>
	/// <param name="parentT">The container/parent for this instance.</param>
	public static ProGifPlayerPanel Create(GameObject prefab, Transform parentT)
	{
		ProGifPlayerPanel gifPanel = ProGifManager.InstantiatePrefab<ProGifPlayerPanel>(prefab);
		gifPanel.transform.SetParent(parentT);
		gifPanel.transform.rotation = parentT.rotation;
		gifPanel.transform.localScale = Vector3.one;
		gifPanel.transform.localPosition = Vector3.zero;
		gifPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
		gifPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		return gifPanel;
	}

	// Use this for initialization
	public void Setup(string gifPath, Action<float> onLoading = null)
	{
		_OnLoading = onLoading;

		if(string.IsNullOrEmpty(gifPath))
		{
			gifPath = (string.IsNullOrEmpty(m_Url))? PlayerPrefs.GetString(ProGifManager.PP_LastGifPathKey, ""):m_Url;
		}

		SetInputText(gifPath);
		playerControlBar.SetActive(false);

		_Show();
	}

	public void OnInputValueChange(InputField input)
	{
		gifPath = input.text;
	}

	public void SetInputText(string path)
	{
		gifPath = path;
		input_Url.text = path;
	}

	public void OnToggleClick(Toggle toggle)
	{
		m_ShouldSaveFromWeb = toggle.isOn;
	}

	public void SaveToGallery()
	{
		if(string.IsNullOrEmpty(gifPath)) return;

		_SetButtonState(btn_ToGallery, false);

		FilePathName filePathName = new FilePathName();
		StartCoroutine(filePathName.LoadFileWWW(gifPath, 
			(data)=>{
				if(data != null)
				{
					string savePath = MobileMedia.SaveBytes(data, "Pro GIF", filePathName.GeFileNameWithoutExt(true), ".gif", true);

					#if UNITY_EDITOR
					Debug.Log("File saved : " + savePath);
					#endif
				}
			})
		);
	}

	private void _SetButtonState(Button button, bool enable)
	{
		button.enabled = enable;
		button.targetGraphic.color = (enable)? new Color(0f, 0.5f, 0f) : Color.gray;
	}

	public void Play()
	{
		if(string.IsNullOrEmpty(gifPath)) return;

		ProGifManager.Instance.SetAdvancedPlayerDecodeSettings(m_Decoder, m_TargetDecodeFrameNum, ProGifManager.Instance.m_FramePickingMethod, ProGifManager.Instance.m_OptimizeMemoryUsage);

		switch(localDebugDataPath)
		{
		case DataPathTypeDebug.DataPath:
			gifPath = System.IO.Path.Combine(Application.dataPath, gifPath);
			if(!gifPath.ToLower().Contains(".gif")) gifPath += ".gif";
			break;
		case DataPathTypeDebug.PersistentDataPath:
			gifPath = System.IO.Path.Combine(Application.dataPath, gifPath);
			if(!gifPath.ToLower().Contains(".gif")) gifPath += ".gif";
			break;
		}

		if(m_GifRawImage) m_GifRawImage.gameObject.SetActive(false);
		if(m_GifRenderer) m_GifRenderer.gameObject.SetActive(false);
		if(m_GifImage) m_GifImage.gameObject.SetActive(false);

		switch(m_DisplayType)
		{
		case DisplayType.Image:
			_PlayWithImage();
			break;
		case DisplayType.RawImage:
			_PlayWithRawImage();
			break;
		case DisplayType.Renderer:
			_PlayWithRenderer();
			break;

		default:
			if(m_GifRawImage != null)
			{
				_PlayWithRawImage();
			}
			else if(m_GifRenderer != null)
			{
				_PlayWithRenderer();
			}
			else if(m_GifImage != null)
			{
				_PlayWithImage();
			}
			break;
		}
	}

	private void _PlayWithImage()
	{
		if(m_GifImage) m_GifImage.gameObject.SetActive(true);

		ProGifManager.Instance.PlayGif(gifPath, m_GifImage, (progress)=>{
			//Set image scale/size if need:
			//m_GifImage.SetNativeSize();

			if(_OnLoading != null)
			{
				_OnLoading(progress);
			}
		}, m_ShouldSaveFromWeb);

		ProGifManager.Instance.m_GifPlayer.SetOnFirstFrameCallback((firstFrame)=>{
			DImageDisplayHandler displayHandler = m_GifImage.GetComponent<DImageDisplayHandler>();
			if(displayHandler != null)
			{
//				Texture2D temp = null;
//				firstFrame.gifTexture.SetColorsToTexture2D(ref temp);
//				Debug.Log(temp.width + " x " + temp.height);
//				displayHandler.SetImage(m_GifImage, temp);
				displayHandler.SetImage(m_GifImage, firstFrame.width, firstFrame.height);
			}

			_SetButtonState(btn_ToGallery, true);
		});
	}

	public void _PlayWithRawImage()
	{
		if(m_GifRawImage) m_GifRawImage.gameObject.SetActive(true);

		ProGifManager.Instance.PlayGif(gifPath, m_GifRawImage, (progress)=>{
			//Set rawImage scale/size if need:
			int gifWidth = ProGifManager.Instance.m_GifPlayer.width;
			int gifHeight = ProGifManager.Instance.m_GifPlayer.height;

			if(_OnLoading != null)
			{
				_OnLoading(progress);
			}
		}, m_ShouldSaveFromWeb);

		ProGifManager.Instance.m_GifPlayer.SetOnFirstFrameCallback((firstFrame)=>{
			DImageDisplayHandler displayHandler = m_GifRawImage.GetComponent<DImageDisplayHandler>();
			if(displayHandler != null)
			{
				displayHandler.SetRawImage(m_GifRawImage, firstFrame.width, firstFrame.height);
			}

			_SetButtonState(btn_ToGallery, true);
		});
	}

	public void _PlayWithRenderer()
	{

		if(m_GifRenderer) m_GifRenderer.gameObject.SetActive(true);

		ProGifManager.Instance.PlayGif(gifPath, m_GifRenderer, (progress)=>{
			//Set renderer transform scale/size if need:
			int gifWidth = ProGifManager.Instance.m_GifPlayer.width;
			int gifHeight = ProGifManager.Instance.m_GifPlayer.height;
			//m_GifRenderer.gameObject.GetComponent<Transform>().localScale = new Vector3(gifWidth/2, gifHeight/2, 
			//m_GifRenderer.gameObject.GetComponent<Transform>().localScale.z);

			if(_OnLoading != null)
			{
				_OnLoading(progress);
			}
		}, m_ShouldSaveFromWeb);

		ProGifManager.Instance.m_GifPlayer.SetOnFirstFrameCallback((firstFrame)=>{
			_SetButtonState(btn_ToGallery, true);
		});
	}

	public void Pause()
	{
		ProGifManager.Instance.PausePlayer();
	}

	public void Resume()
	{
		ProGifManager.Instance.ResumePlayer();
	}

	public void Stop()
	{
		ProGifManager.Instance.StopPlayer();
	}


	private void _Show()
	{
		//Show Gif image
		gameObject.SetActive(true);
		SDemoAnimation.Instance.Scale(containerGO, Vector3.zero, Vector3.one, 0.3f, SDemoAnimation.LoopType.None, ()=>{
			//Show control bar
			playerControlBar.SetActive(true);

			playerControlBar.transform.localPosition = new Vector3(0f, playerControlBar.transform.localPosition.y-280, 0f);
			float startY = playerControlBar.transform.localPosition.y;
			SDemoAnimation.Instance.Move(playerControlBar, new Vector3(0f, startY, 0f), new Vector3(0f, startY+280, 0f), 0.3f, SDemoAnimation.LoopType.None);
		});
	}

	public void Close()
	{
		_Close();
	}

	private void _Close()
	{
		//Hide control bar 
		float startY = playerControlBar.transform.localPosition.y;
		SDemoAnimation.Instance.Move(playerControlBar, new Vector3(0f, startY, 0f), new Vector3(0f, startY-280, 0f), 0.3f, SDemoAnimation.LoopType.None, ()=>{
			//Hide control bar
			playerControlBar.SetActive(false);

			//Hide Gif image
			SDemoAnimation.Instance.Rotate(containerGO, Vector3.zero, new Vector3(0f, 90f, 0f), 0.3f, SDemoAnimation.LoopType.None, ()=>{

				//Clear un-use resources in the recorder and player to avoid memory leak
				ProGifManager.Instance.Clear();

//				//Clear texture in preview image to avoid memory leak
//				if(m_GifImage.sprite != null && m_GifImage.sprite.texture != null)
//				{
//					Texture2D.Destroy(m_GifImage.sprite.texture);
//				}

				//Remove panel
				Destroy(gameObject);
			});
		});
	}

}
