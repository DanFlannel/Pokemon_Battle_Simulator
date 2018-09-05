using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gif Texture, holding a GIF frame contents (pixels, resolution, interval, and texture/sprite references etc.)
/// </summary>
public class GifTexture
{
	public Color32[] m_Colors = null;
	public int m_Width;
	public int m_Height;

	// Sprite
	public Sprite m_Sprite = null;
	// Texture
	public Texture2D m_texture2d = null;
	// Delay time until the next texture.
	public float m_delaySec = 0;


	private bool _hasCreateTexture = false;
	// Textures filter mode
	private FilterMode _filterMode = FilterMode.Point;
	// Textures wrap mode
	private TextureWrapMode _wrapMode = TextureWrapMode.Clamp;

	private bool _optimizeMemoryUsage = true;

	public GifTexture(Color32[] colors, int width, int height, float delaySec, FilterMode filterMode, TextureWrapMode wrapMode)
	{
		m_Colors = colors;
		m_Width = width;
		m_Height = height;
		m_delaySec = delaySec;
		_filterMode = filterMode;
		_wrapMode = wrapMode;
	}

	public GifTexture(Texture2D texture2d, float delaySec, bool optimizeMemoryUsgae = true)
	{
		_optimizeMemoryUsage = optimizeMemoryUsgae;
		if(optimizeMemoryUsgae)
		{
			m_Width = texture2d.width;
			m_Height = texture2d.height;
			_filterMode = texture2d.filterMode;
			_wrapMode = texture2d.wrapMode;
			m_Colors = texture2d.GetPixels32();
			Texture2D.Destroy(texture2d);
		}
		else
		{
			m_texture2d = texture2d;
		}
		m_delaySec = delaySec;
	}

	public GifTexture(Sprite sprite, float delaySec, bool optimizeMemoryUsgae = true)
	{
		_optimizeMemoryUsage = optimizeMemoryUsgae;
		if(optimizeMemoryUsgae)
		{
			m_Width = sprite.texture.width;
			m_Height = sprite.texture.height;
			_filterMode = sprite.texture.filterMode;
			_wrapMode = sprite.texture.wrapMode;
			m_Colors = sprite.texture.GetPixels32();
			Texture2D.Destroy(sprite.texture);
		}
		else
		{
			m_Sprite = sprite;
		}

		m_delaySec = delaySec;
	}

	/// Get the stored Texture2D, create a new one from m_Colors if not exist.
	/// The texture will take up certain memory until you clear it.
	/// (If just need to display the gif frame, use SetDisplay method instead)
	public Texture2D GetTexture2D()
	{
		if(m_texture2d != null)
		{
			return m_texture2d;
		}
		else
		{
			if(!_hasCreateTexture)
			{
				if(m_Colors != null)
				{
					m_texture2d = new Texture2D(m_Width, m_Height, TextureFormat.ARGB32, false);
					m_texture2d.filterMode = _filterMode;
					m_texture2d.wrapMode = _wrapMode;
					m_texture2d.SetPixels32(m_Colors);
					m_texture2d.Apply();
				}

				_hasCreateTexture = true;
				if(m_texture2d != null) return m_texture2d;
			}
			return GetSprite().texture;
		}
	}

	/// Get the stored Sprite, create a new one from the stored Texture2D or m_Colors if not exist.
	/// The texture in the sprite will take up certain memory until you clear it.
	/// (If just need to display the gif frame, use SetDisplay method instead)
	public Sprite GetSprite()
	{
		if(m_Sprite == null)
		{
			Texture2D tex = GetTexture2D();
			m_Sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
		}
		return m_Sprite;
	}

	public Sprite GetSprite_OptimizeMemoryUsage(ref Texture2D refTexture2d)
	{
		if(!_optimizeMemoryUsage)
		{
			return GetSprite();
		}

		if(m_Sprite == null)
		{
			SetColorsToTexture2D(ref refTexture2d);
			m_Sprite = Sprite.Create(refTexture2d, new Rect(0, 0, refTexture2d.width, refTexture2d.height), new Vector2(0.5f, 0.5f), 100);
		}
		else
		{
			SetColorsToTexture2D(ref refTexture2d);
		}
		return m_Sprite;
	}

	public void SetColorsToTexture2D(ref Texture2D refTexture2d)
	{
		if(!_optimizeMemoryUsage)
		{
			refTexture2d = GetTexture2D();
			return;
		}

		if(refTexture2d == null || refTexture2d.width != m_Width || refTexture2d.height != m_Height) 
		{
			refTexture2d = new Texture2D(m_Width, m_Height, TextureFormat.ARGB32, false);
		}

		refTexture2d.filterMode = _filterMode;
		refTexture2d.wrapMode = _wrapMode;
		refTexture2d.SetPixels32(m_Colors);
		refTexture2d.Apply();
	}

	/// Set the stored pixels(m_Colors) to diaplay on the target Image.
	/// (Reminder: Do NOT use the same refTexture2d for different GIF! As it will generate new texture each time.)
	public void SetDisplay(UnityEngine.UI.Image targetDisplay, ref Texture2D refTexture2d)
	{
		if(!_optimizeMemoryUsage)
		{
			refTexture2d = GetTexture2D();
			targetDisplay.sprite = GetSprite();
			return;
		}

		targetDisplay.sprite = GetSprite_OptimizeMemoryUsage(ref refTexture2d);
	}

	/// Set the stored pixels(m_Colors) to diaplay on the target RawImage.
	/// (Reminder: Do NOT use the same refTexture2d for different GIF! As it will generate new texture each time.)
	public void SetDisplay(RawImage targetDisplay, ref Texture2D refTexture2d)
	{
		if(!_optimizeMemoryUsage)
		{
			refTexture2d = GetTexture2D();
			return;
		}

		SetColorsToTexture2D(ref refTexture2d);
		targetDisplay.texture = refTexture2d;
	}

	/// Set the stored pixels(m_Colors) to diaplay on the target Renderer.
	/// (Reminder: Do NOT use the same refTexture2d for different GIF! As it will generate new texture each time.)
	public void SetDisplay(Renderer targetDisplay, ref Texture2D refTexture2d)
	{
		if(!_optimizeMemoryUsage)
		{
			refTexture2d = GetTexture2D();
			return;
		}

		SetColorsToTexture2D(ref refTexture2d);
		targetDisplay.material.mainTexture = refTexture2d;
	}

	/// Set the stored pixels(m_Colors) to diaplay on the target GUITexture.
	/// (Reminder: Do NOT use the same refTexture2d for different GIF! As it will generate new texture each time.)
	public void SetDisplay(GUITexture targetDisplay, ref Texture2D refTexture2d)
	{
		if(!_optimizeMemoryUsage)
		{
			refTexture2d = GetTexture2D();
			return;
		}

		SetColorsToTexture2D(ref refTexture2d);
		targetDisplay.texture = refTexture2d;
	}
}
