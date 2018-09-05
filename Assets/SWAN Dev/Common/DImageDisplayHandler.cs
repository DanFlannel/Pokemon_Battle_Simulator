/// <summary>
/// By SwanDEV 2018
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DynamicUI - Image Display Handler for UGUI Image & RawImage.
/// How to use: (1) add as a base class (Inherits), (2) Drop this script in a GameObject and reference it.
/// Call the SetImage/SetRawImage method.
/// </summary>
public class DImageDisplayHandler : MonoBehaviour
{
	public enum BoundingTarget
	{
		/// <summary> Constraints the target image with the vector2 size(m_Size). </summary>
		Size,

		/// <summary> Constraints the target image with the sizeDelta of the RectTranform(m_RectTransform). </summary>
		RectTransform,

		/// <summary> Constraints the target image with the device screen size. </summary>
		Screen,
	}

	public enum BoundingType
	{
		SetNativeSize = 0,
		WidthAndHeight,
		Width,
		Height,
	}

	[Header("[ Image Display Handler ]")]
	public BoundingTarget m_BoundingTarget = BoundingTarget.Size;
	public RectTransform m_RectTransform;
	public Vector2 m_Size = new Vector2(512, 512);

	[Space()]
	public BoundingType m_BoundingType = BoundingType.SetNativeSize;

	[Space()]
	public float m_ScaleFactor = 1f;

	[Space()]
	public bool m_AutoClearTexture = true;


	public void SetImage(UnityEngine.UI.Image displayImage, Sprite sprite)
	{
		Clear(displayImage);
		displayImage.sprite = sprite;
		_SetSize(displayImage);
	}

	public void SetImage(UnityEngine.UI.Image displayImage, Texture2D texture2D)
	{
		Clear(displayImage);
		displayImage.sprite = _TextureToSprite(texture2D);
		_SetSize(displayImage);
	}

	public void SetRawImage(UnityEngine.UI.RawImage displayImage, Sprite sprite)
	{
		Clear(displayImage);
		displayImage.texture = (Texture) sprite.texture;
		_SetSize(displayImage);
	}

	public void SetRawImage(UnityEngine.UI.RawImage displayImage, Texture2D texture2D)
	{
		Clear(displayImage);
		displayImage.texture = (Texture) texture2D;
		_SetSize(displayImage);
	}

	public void SetRawImage(UnityEngine.UI.RawImage displayImage, Texture texture)
	{
		Clear(displayImage);
		displayImage.texture = texture;
		_SetSize(displayImage);
	}

	public void SetImage(UnityEngine.UI.Image displayImage, float width, float height)
	{
		displayImage.rectTransform.sizeDelta = _CalculateSize(new Vector2(width, height));
		_ApplyScaleFactor(displayImage.transform);
	}

	public void SetRawImage(UnityEngine.UI.RawImage displayImage, float width, float height)
	{
		displayImage.rectTransform.sizeDelta = _CalculateSize(new Vector2(width, height));
		_ApplyScaleFactor(displayImage.transform);
	}

	private void _SetSize(UnityEngine.UI.Image displayImage)
	{
		if(m_BoundingType == BoundingType.SetNativeSize)
		{
			displayImage.SetNativeSize();
		}
		else
		{
			displayImage.rectTransform.sizeDelta = _CalculateSize(new Vector2(displayImage.sprite.texture.width, displayImage.sprite.texture.height));
		}

		_ApplyScaleFactor(displayImage.transform);
	}

	private void _SetSize(UnityEngine.UI.RawImage displayImage)
	{
		if(m_BoundingType == BoundingType.SetNativeSize)
		{
			displayImage.SetNativeSize();
		}
		else
		{
			displayImage.rectTransform.sizeDelta = _CalculateSize(new Vector2(displayImage.texture.width, displayImage.texture.height));
		}

		_ApplyScaleFactor(displayImage.transform);
	}

	private void _ApplyScaleFactor(Transform displayImageT)
	{
		displayImageT.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, 1f);
	}

	private Vector2 _CalculateSize(Vector2 textureSize)
	{
		Vector2 boundarySize = Vector2.zero;

		switch(m_BoundingTarget)
		{
		case BoundingTarget.Size:
			boundarySize = m_Size;
			break;
		case BoundingTarget.RectTransform:
			boundarySize = m_RectTransform.GetComponent<RectTransform>().rect.size;
			break;
		case BoundingTarget.Screen:
			boundarySize = new Vector2(Screen.width, Screen.height);
			break;
		}

		float newWidth = textureSize.x;
		float newHeight = textureSize.y;
		float imageRatio = newWidth / newHeight;

		switch(m_BoundingType)
		{
		case BoundingType.WidthAndHeight:
			newWidth = boundarySize.x;
			newHeight = newWidth / imageRatio;

			if(newHeight > boundarySize.y)
			{
				newHeight = boundarySize.y;
				newWidth = newHeight * imageRatio;
			}
			break;

		case BoundingType.Width:
			newWidth = boundarySize.x;
			newHeight = newWidth / imageRatio;
			break;

		case BoundingType.Height:
			newHeight = boundarySize.y;
			newWidth = newHeight * imageRatio;
			break;

		default:
			newWidth = textureSize.x;
			newHeight = textureSize.y;
			break;
		}

		return new Vector2(newWidth, newHeight);
	}

	private Sprite _TextureToSprite(Texture2D texture)
	{
		if(texture == null) return null;

		Vector2 pivot = new Vector2(0.5f, 0.5f);
		float pixelPerUnit = 100;
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, pixelPerUnit);
	}

	public void Clear(UnityEngine.UI.Image displayImage)
	{
		if(m_AutoClearTexture && displayImage != null && displayImage.sprite != null && displayImage.sprite.texture != null)
		{
			Texture2D.Destroy(displayImage.sprite.texture);
			displayImage.sprite = null;
		}
	}

	public void Clear(UnityEngine.UI.RawImage displayImage)
	{
		if(m_AutoClearTexture && displayImage != null && displayImage.texture != null)
		{
			Texture.Destroy(displayImage.texture);
			displayImage.texture = null;
		}
	}

}
