using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GUITexture))]
public sealed class ProGifPlayerGuiTexture : ProGifPlayerComponent
{
	[HideInInspector] public GUITexture destinationGuiTexture;				// The GUITexture for displaying textures
	private List<GUITexture> m_ExtraGuiTextures = new List<GUITexture>();

	private Texture2D _displayTexture2D = null;

	void Awake()
	{
		if(destinationGuiTexture == null)
		{
			destinationGuiTexture = gameObject.GetComponent<GUITexture>();
		}
	}

	// Update gif frame for the Player (Update is called once per frame)
	void Update()
	{
		base.ThreadsUpdate();

		if(State == PlayerState.Playing && displayType == ProGifPlayerComponent.DisplayType.GuiTexture)
		{
			if(Time.time >= nextFrameTime)
			{
				spriteIndex = (spriteIndex >= gifTextures.Count - 1)? 0 : spriteIndex + 1;
				nextFrameTime = Time.time + interval;
			}

			if(spriteIndex < gifTextures.Count)
			{
				if(OnPlayingCallback != null) OnPlayingCallback(gifTextures[spriteIndex]);

				_SetDisplay(spriteIndex);

				if(m_ExtraGuiTextures != null && m_ExtraGuiTextures.Count > 0)
				{
					Texture2D tex = null;
					if(optimizeMemoryUsage)
					{
						tex = _displayTexture2D;
					}
					else
					{
						tex = gifTextures[spriteIndex].GetTexture2D();
					}

					for(int i = 0; i < m_ExtraGuiTextures.Count; i++)
					{
						if(m_ExtraGuiTextures[i] != null)
						{
							m_ExtraGuiTextures[i].texture = tex;
						}
						else
						{
							m_ExtraGuiTextures.Remove(m_ExtraGuiTextures[i]);
							m_ExtraGuiTextures.TrimExcess();
						}
					}
				}
			}
		}
	}

	public override void Play(RenderTexture[] gifFrames, int fps, bool isCustomRatio, int customWidth, int customHeight, bool optimizeMemoryUsage)
	{
		base.Play(gifFrames, fps, isCustomRatio, customWidth, customHeight, optimizeMemoryUsage);

		if(destinationGuiTexture == null) destinationGuiTexture = gameObject.GetComponent<GUITexture>();

		_SetDisplay(0);
	}

    protected override void _OnFrameReady(GifTexture gTex, bool isFirstFrame)
	{
		if(isFirstFrame) _SetDisplay(0);
	}

	private void _SetDisplay(int spriteIndex)
	{
		if(optimizeMemoryUsage) 
		{
			gifTextures[spriteIndex].SetColorsToTexture2D(ref _displayTexture2D); // Set Colors to single texture
		}

		if(destinationGuiTexture != null)
		{
			if(optimizeMemoryUsage)
			{
				destinationGuiTexture.texture = _displayTexture2D;
			}
			else
			{
				destinationGuiTexture.texture = gifTextures[spriteIndex].GetTexture2D();
			}
		}
	}

	public override void Clear()
	{
		//Your Code before base.Clear():
//		if(destinationGuiTexture != null && destinationGuiTexture.texture != null)
//		{
//			Texture2D.Destroy(destinationGuiTexture.texture);
//		}

		if(optimizeMemoryUsage && _displayTexture2D != null) 
		{
			Texture2D.Destroy(_displayTexture2D);
		}

		base.Clear();
	}


	public void ChangeDestination(GUITexture guiTexture)
	{
		destinationGuiTexture = guiTexture;
	}

	public void AddExtraDestination(GUITexture guiTexture)
	{
		if(!m_ExtraGuiTextures.Contains(guiTexture))
		{
			m_ExtraGuiTextures.Add(guiTexture);
		}
	}

	public void RemoveFromExtraDestination(GUITexture guiTexture)
	{
		if(m_ExtraGuiTextures.Contains(guiTexture))
		{
			m_ExtraGuiTextures.Remove(guiTexture);
			m_ExtraGuiTextures.TrimExcess();
		}
	}
}
