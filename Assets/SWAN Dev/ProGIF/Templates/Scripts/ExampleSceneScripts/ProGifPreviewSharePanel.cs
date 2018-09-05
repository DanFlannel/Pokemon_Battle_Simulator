/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ProGifPreviewSharePanel : MonoBehaviour
{
	public GameObject containerGO;
	public GameObject shareBarGO;
	public GameObject shareBarGO_V;
	public UnityEngine.UI.Image m_GifImage;
	public RectTransform m_PreviewBorderRectT;
	[HideInInspector]
	public string gifPath;

	/// <summary>
	/// Create an instance of ProGifPreviewSharePanel from provided prefab, and set parent.
	/// </summary>
	/// <param name="prefab">The Prefab of ProGifPreviewSharePanel.</param>
	/// <param name="parentT">The container/parent for this instance.</param>
	public static ProGifPreviewSharePanel Create(GameObject prefab, Transform parentT)
	{
		ProGifPreviewSharePanel gifPanel = ProGifManager.InstantiatePrefab<ProGifPreviewSharePanel>(prefab);
		gifPanel.transform.SetParent(parentT);
		gifPanel.transform.rotation = parentT.rotation;
		gifPanel.transform.localScale = Vector3.one;
		gifPanel.transform.localPosition = Vector3.zero;
		gifPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
		gifPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		return gifPanel;
	}

	// Use this for initialization
	public void Setup(string gifPath, bool loadFile = false, Action<float> onLoading = null)
	{
		this.gifPath = gifPath;

		if(loadFile)
		{
			//ProGifManager.Instance.m_OptimizeMemoryUsage = true;
			ProGifManager.Instance.PlayGif(gifPath, m_GifImage, (progress)=>{
				//Set the gif size when the first frame available and assigned to m_GifImage
				//Set image scale here:
				m_GifImage.SetNativeSize();

				//Set border size
				float width = m_GifImage.sprite.texture.width;
				float height = m_GifImage.sprite.texture.height;
				float border = width * 0.04f;
				m_PreviewBorderRectT.sizeDelta = new Vector2(width + border, height + border);

				if(onLoading != null)
				{
					onLoading(progress);
				}
			});
		}
		else
		{
			//ProGifManager.Instance.m_OptimizeMemoryUsage = true;
			ProGifManager.Instance.PlayGif(m_GifImage, (progress)=>{
				//Set the gif size when the first frame available and assigned to m_GifImage
				//Set image scale here:
				m_GifImage.SetNativeSize();

				//Set border size
				float width = m_GifImage.sprite.texture.width;
				float height = m_GifImage.sprite.texture.height;
				float border = width * 0.04f;
				m_PreviewBorderRectT.sizeDelta = new Vector2(width + border, height + border);

				_CheckAndSetRotation();

				if(onLoading != null)
				{
					onLoading(progress);
				}
			});
		}

		shareBarGO_V.SetActive(false);
		shareBarGO.SetActive(false);

		//Check screen orientation for selecting shareBar to show
		if(Screen.width > Screen.height)
		{
			shareBarGO = shareBarGO_V;
		}

		_Show();
	}

	public void ShareToFacebook()
	{
		ProGifManager.Instance.ShareFacebook(gifPath);
	}

	public void ShareToTwitter()
	{
		ProGifManager.Instance.ShareTwitter(gifPath);
	}

	private void _CheckAndSetRotation()
	{
        //ImageRotator.Rotation rotation = ProGifManager.Instance.m_GifRecorder.Rotation;
        ImageRotator.Rotation rotation = ProGifManager.Instance.m_GifPlayer.rotation;

		float angle = 0;
		switch(rotation)
		{
		case ImageRotator.Rotation.Left:
			angle = 90;
			break;
		case ImageRotator.Rotation.Right:
			angle = -90;
			break;
		case ImageRotator.Rotation.HalfCircle:
			angle = 180;
			break;
		}
		Vector3 previewObjectAngle = m_PreviewBorderRectT.localEulerAngles;
		m_PreviewBorderRectT.localEulerAngles = new Vector3(previewObjectAngle.x, previewObjectAngle.y, angle);
	}

	private void _Show()
	{
		//Show Gif image
		gameObject.SetActive(true);
		SDemoAnimation.Instance.Scale(containerGO, Vector3.zero, Vector3.one, 0.3f, SDemoAnimation.LoopType.None, ()=>{
			//Show preview & share bar
			shareBarGO.SetActive(true);
			SDemoAnimation.Instance.Scale(shareBarGO, Vector3.one * 3f, Vector3.one, 0.3f, SDemoAnimation.LoopType.None);
		});
	}

	public void Close()
	{
		_Close();
	}

	private void _Close()
	{
		//Hide preview & share bar 
		SDemoAnimation.Instance.Scale(shareBarGO, Vector3.one, Vector3.zero, 0.3f, SDemoAnimation.LoopType.None, ()=>{

			//Hide Gif image
			SDemoAnimation.Instance.Scale(containerGO, Vector3.one, Vector3.zero, 0.3f, SDemoAnimation.LoopType.None, ()=>{

				//Clear un-use resources in the recorder and player to avoid memory leak
				ProGifManager.Instance.Clear();

				//Clear texture in preview image to avoid memory leak
				if(m_GifImage.sprite != null && m_GifImage.sprite.texture != null)
				{
					Texture2D.Destroy(m_GifImage.sprite.texture);
				}

				//Remove panel
				Destroy(gameObject);
			});
		});
	}

}
