using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Pro GIF info. For getting the first frame of a GIF. Get the first frame texture, width, height, fps, total frame count.
/// </summary>
public class ProGifInfo : ProGifPlayerComponent
{
	public void GetInfo(string loadPath, Action<FirstGifFrame> onComplete, Decoder decoder = Decoder.ProGif_QueuedThread)
	{
		onComplete +=(firstFrame)=>{
			Clear();
			GameObject.Destroy(this.gameObject);
		};
		SetAdvancedDecodeSettings(decoder, 1);
		SetOnFirstFrameCallback(onComplete);
		LoadGifFromUrl(loadPath);
		this.loadPath = loadPath;
	}

	void Update()
	{
		base.ThreadsUpdate();
	}

    protected override void _OnFrameReady(GifTexture gTex, bool isFirstFrame){}
}
