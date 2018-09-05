using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ProGifPlayer
{
	private ProGifPlayerComponent player = null;
    public ProGifPlayerComponent playerComponent
    {
        get
        {
            return player;
        }
    }

	//Advanced settings ------------------
	private ProGifPlayerComponent.Decoder decoder = ProGifPlayerComponent.Decoder.ProGif_Coroutines;
	private ProGifPlayerComponent.DecodeMode decodeMode = ProGifPlayerComponent.DecodeMode.Normal;
	private ProGifPlayerComponent.FramePickingMethod framePickingMethod = ProGifPlayerComponent.FramePickingMethod.ContinuousFromBeginning;
	private int targetDecodeFrameNum = -1;	//if targetDecodeFrameNum <= 0: decode & play all frames
	private bool optimizeMemoryUsage = true;
	//Advanced settings ------------------

	public ProGifPlayerComponent.PlayerState State
	{
		get{
			return (player == null)? ProGifPlayerComponent.PlayerState.None:player.State;
		}
	}

	/// <summary>
	/// Gif width (only available after the first gif frame is loaded)
	/// </summary>
	public int width
	{
		get{
			return (player == null)? 0:player.width;
		}
	}

	/// <summary>
	/// Gif height (only available after the first gif frame is loaded)
	/// </summary>
	public int height
	{
		get{
			return (player == null)? 0:player.height;
		}
	}

	/// <summary>
	/// Decoded gif texture list (get all the gif textures at the decoding process finished)
	/// </summary>
	public List<GifTexture> gifTextures
	{
		get{
			return (player == null)? null:player.gifTextures;
		}
	}

    public ImageRotator.Rotation rotation = ImageRotator.Rotation.None;

    public string savePath = "";

    public string loadPath
    {
        get{
            return (player == null) ? "":player.loadPath;
        }
    }

	private void _SetupPlayerComponent(UnityEngine.UI.Image destination)
	{
		player = destination.gameObject.GetComponent<ProGifPlayerImage>();
		if(player == null)
		{
			player = destination.gameObject.AddComponent<ProGifPlayerImage>();
		}
		player.displayType = ProGifPlayerComponent.DisplayType.Image;
	}

	private void _SetupPlayerComponent(Renderer destination)
	{
		player = destination.gameObject.GetComponent<ProGifPlayerRenderer>();
		if(player == null)
		{
			player = destination.gameObject.AddComponent<ProGifPlayerRenderer>();
		}
		player.displayType = ProGifPlayerComponent.DisplayType.Renderer;
	}

	private void _SetupPlayerComponent(GUITexture destination)
	{
		player = destination.gameObject.GetComponent<ProGifPlayerGuiTexture>();
		if(player == null)
		{
			player = destination.gameObject.AddComponent<ProGifPlayerGuiTexture>();
		}
		player.displayType = ProGifPlayerComponent.DisplayType.GuiTexture;
	}

    private void _SetupPlayerComponent(RawImage destination)
    {
        player = destination.gameObject.GetComponent<ProGifPlayerRawImage>();
        if (player == null)
        {
            player = destination.gameObject.AddComponent<ProGifPlayerRawImage>();
        }
        player.displayType = ProGifPlayerComponent.DisplayType.RawImage;
    }

    /// <summary>
    /// Play gif frames in the specified recorder, display with Image.
    /// </summary>
	public void Play(ProGifRecorder recorder, UnityEngine.UI.Image destination, bool optimizeMemoryUsage)
	{
		_SetupPlayerComponent(destination);
		_PlayRecorder(recorder, optimizeMemoryUsage);
	}

    /// <summary>
    /// Play gif frames in the specified recorder, display with Renderer.
    /// </summary>
	public void Play(ProGifRecorder recorder, Renderer destination, bool optimizeMemoryUsage)
	{
		_SetupPlayerComponent(destination);
		_PlayRecorder(recorder, optimizeMemoryUsage);
	}

	/// <summary>
	/// Play gif frames in the specified recorder, display with GUITexture.
	/// </summary>
	public void Play(ProGifRecorder recorder, GUITexture destination, bool optimizeMemoryUsage)
	{
		_SetupPlayerComponent(destination);
		_PlayRecorder(recorder, optimizeMemoryUsage);
    }

    /// <summary>
    /// Play gif frames in the specified recorder, display with RawImage.
    /// </summary>
	public void Play(ProGifRecorder recorder, RawImage destination, bool optimizeMemoryUsage)
    {
        _SetupPlayerComponent(destination);
		_PlayRecorder(recorder, optimizeMemoryUsage);
    }

	private void _PlayRecorder(ProGifRecorder recorder, bool optimizeMemoryUsage)
	{
		//player.Play(recorder.FPS, _Setup(recorder), optimizeMemoryUsage);
		this.optimizeMemoryUsage = optimizeMemoryUsage;
		this.rotation = recorder.Rotation;
		this.savePath = recorder.SavedFilePath;
		player.Play(recorder.Frames, recorder.FPS, recorder.IsCustomRatio, recorder.Width, recorder.Height, optimizeMemoryUsage);
	}

    /// <summary>
    /// Load & Decode to Play gif at the loadPath, display with Image.
    /// </summary>
	public void Play(string loadPath, UnityEngine.UI.Image destination, bool shouldSaveFromWeb)
    {
        rotation = ImageRotator.Rotation.None;
        _SetupPlayerComponent(destination);
        _SetDecodeSettings();
        player.Play(loadPath, shouldSaveFromWeb);
    }

    /// <summary>
    /// Load & Decode to Play gif at the loadPath, display with Renderer.
    /// </summary>
    public void Play(string loadPath, Renderer destination, bool shouldSaveFromWeb)
    {
        rotation = ImageRotator.Rotation.None;
        _SetupPlayerComponent(destination);
        _SetDecodeSettings();
        player.Play(loadPath, shouldSaveFromWeb);
    }

    /// <summary>
    /// Load & Decode to Play gif at the loadPath, display with GUITexture.
    /// </summary>
    public void Play(string loadPath, GUITexture destination, bool shouldSaveFromWeb)
    {
        rotation = ImageRotator.Rotation.None;
        _SetupPlayerComponent(destination);
        _SetDecodeSettings();
        player.Play(loadPath, shouldSaveFromWeb);
    }

    /// <summary>
    /// Load & Decode to Play gif at the loadPath, display with RawImage.
    /// </summary>
    public void Play(string loadPath, RawImage destination, bool shouldSaveFromWeb)
    {
        rotation = ImageRotator.Rotation.None;
        _SetupPlayerComponent(destination);
        _SetDecodeSettings();
        player.Play(loadPath, shouldSaveFromWeb);
    }

    /// <summary>
    /// Sets the player decode settings. 
    /// You can ignore the TargetDecodeFrameNum and FramePickingMethod parameters if DecodeMode is 'Normal';
    /// Else give the TargetDecodeFrameNum and FramePickingMethod parameters with the values you want(if DecodeMode is 'Advanced').
    /// </summary>
    public void SetDecodeSettings(ProGifPlayerComponent.Decoder inDecoder, ProGifPlayerComponent.DecodeMode inDecodeMode, 
		int inTargetDecodeFrameNum = -1, ProGifPlayerComponent.FramePickingMethod inFramePickingMethod = ProGifPlayerComponent.FramePickingMethod.ContinuousFromBeginning, bool inOptimizeMemoryUsage = true)
	{
		decoder = inDecoder;
		decodeMode = inDecodeMode;
		targetDecodeFrameNum = inTargetDecodeFrameNum;
		framePickingMethod = inFramePickingMethod;
		optimizeMemoryUsage = inOptimizeMemoryUsage;
		if(player != null) _SetDecodeSettings();
	}

	private void _SetDecodeSettings()
	{
		switch(decodeMode)
		{
		case ProGifPlayerComponent.DecodeMode.Advanced:
			player.SetAdvancedDecodeSettings(decoder, targetDecodeFrameNum, framePickingMethod, optimizeMemoryUsage);
			break;

		case ProGifPlayerComponent.DecodeMode.Normal:
			player.ResetDecodeSettings();
			player.optimizeMemoryUsage = optimizeMemoryUsage;
			break;
		}
	}

	public void Pause()
	{
		player.Pause();
	}

	public void Resume()
	{
		player.Resume();
	}

	public void Stop()
	{
		player.Stop();
	}

	/// <summary>
	/// Set the callback for checking the decode progress.
	/// If using a recorder source for playback, this becomes a loading-complete callback.
	/// </summary>
	/// <param name="onLoading">On loading callback, returns the decode progress(float).</param>
	public void SetLoadingCallback(Action<float> onLoading)
	{
		if(player != null)
		{
			player.SetLoadingCallback(onLoading);
		}
		else
		{
			Debug.LogWarning("Gif player not exist, please set callback after the player is set!");
		}
	}

	/// <summary>
	/// Set the callback to be fired when the first gif frame ready.
	/// If using a recorder source for playback, this becomes a loading-complete callback with the first GIF frame returned.
	/// </summary>
	/// <param name="onFirstFrame">On first frame callback, returns the first gifTexture and related data.</param>
	public void SetOnFirstFrameCallback(Action<ProGifPlayerComponent.FirstGifFrame> onFirstFrame)
	{
		if(player != null)
		{
			player.SetOnFirstFrameCallback(onFirstFrame);
		}
		else
		{
			Debug.LogWarning("Gif player not exist, please set callback after the player is set!");
		}
	}

	/// <summary>
	/// Set the callback to be fired on every frame during play gif.
	/// </summary>
	/// <param name="onPlaying">On playing callback, returns the gifTexture of the current playing frame.</param>
	public void SetOnPlayingCallback(Action<GifTexture> onPlaying)
	{
		if(player != null)
		{
			player.SetOnPlayingCallback(onPlaying);
		}
		else
		{
			Debug.LogWarning("Gif player not exist, please set callback after the player is set!");
		}
	}

	/// <summary>
	/// Set the callback to be fired when all frames decode complete (for decoder only).
	/// </summary>
	/// <param name="onDecodeComplete">On decode complete callback, returns the gifTextures list and related data.</param>
	public void SetOnDecodeCompleteCallback(Action<ProGifPlayerComponent.DecodedResult> onDecodeComplete)
	{
		if(player != null)
		{
			player.SetOnDecodeCompleteCallback(onDecodeComplete);
		}
		else
		{
			Debug.LogWarning("Gif player not exist, please set callback after the player is set!");
		}
	}

	/// <summary>
	/// Change the destination image for displaying gif.
	/// </summary>
	public void ChangeDestination(UnityEngine.UI.Image destination)
	{
		if(player.GetComponent<ProGifPlayerImage>() != null)
		{
			player.GetComponent<ProGifPlayerImage>().ChangeDestination(destination);
		}
		else Debug.LogWarning("The display destination type should be an Image as this gif player component originately start with Image.");
	}

	/// <summary>
	/// Change the destination renderer for displaying gif.
	/// </summary>
	public void ChangeDestination(Renderer destination)
	{
		if(player.GetComponent<ProGifPlayerRenderer>() != null)
		{
			player.GetComponent<ProGifPlayerRenderer>().ChangeDestination(destination);
		}
		else Debug.LogWarning("The display destination type should be a Renderer as this gif player component originately start with Renderer.");
	}

    /// <summary>
    /// Change the destination guiTexture for displaying gif.
    /// </summary>
    public void ChangeDestination(GUITexture destination)
    {
        if(player.GetComponent<ProGifPlayerGuiTexture>() != null)
        {
            player.GetComponent<ProGifPlayerGuiTexture>().ChangeDestination(destination);
        }
		else Debug.LogWarning("The display destination type should be a GUITexture as this gif player component originately start with GUITexture.");
    }

    /// <summary>
    /// Change the destination rawImage for displaying gif.
    /// </summary>
    public void ChangeDestination(RawImage destination)
    {
        if(player.GetComponent<ProGifPlayerRawImage>() != null)
        {
            player.GetComponent<ProGifPlayerRawImage>().ChangeDestination(destination);
        }
		else Debug.LogWarning("The display destination type should be a RawImage as this gif player component originately start with RawImage.");
    }

    /// <summary>
    /// Add an extra destination image for displaying gif.
    /// </summary>
	public void AddExtraDestination(UnityEngine.UI.Image destination)
	{
		if(player.GetComponent<ProGifPlayerImage>() != null)
		{
			player.GetComponent<ProGifPlayerImage>().AddExtraDestination(destination);
		}
		else Debug.LogWarning("In order to share the sprite among multiple Image targets, you should play Gif on an Image destination.");
	}

	/// <summary>
	/// Add an extra destination renderer for displaying gif.
	/// </summary>
	public void AddExtraDestination(Renderer destination)
	{
		if(player.GetComponent<ProGifPlayerRenderer>() != null)
		{
			player.GetComponent<ProGifPlayerRenderer>().AddExtraDestination(destination);
		}
		else Debug.LogWarning("In order to share the texture among multiple Renderer targets, you should play Gif on a Renderer destination.");
	}

	/// <summary>
	/// Add an extra destination guiTexture for displaying gif.
	/// </summary>
	public void AddExtraDestination(GUITexture destination)
	{
		if(player.GetComponent<ProGifPlayerGuiTexture>() != null)
		{
			player.GetComponent<ProGifPlayerGuiTexture>().AddExtraDestination(destination);
		}
		else Debug.LogWarning("In order to share the texture among multiple GUITexture targets, you should play Gif on a GUITexture destination.");
	}

    /// <summary>
    /// Add an extra destination rawImage for displaying gif.
    /// </summary>
    public void AddExtraDestination(RawImage destination)
    {
        if(player.GetComponent<ProGifPlayerRawImage>() != null)
        {
            player.GetComponent<ProGifPlayerRawImage>().AddExtraDestination(destination);
		}
		else Debug.LogWarning("In order to share the texture among multiple RawImage targets, you should play Gif on a RawImage destination.");
    }

    /// <summary>
    /// Remove a specific extra destination image from the extra display list.
    /// </summary>
    public void RemoveFromExtraDestination(Image destination)
	{
		if(player.GetComponent<ProGifPlayerImage>() != null)
		{
			player.GetComponent<ProGifPlayerImage>().RemoveFromExtraDestination(destination);
		}
	}

    /// <summary>
    /// Remove a specific extra destination renderer from the extra display list.
    /// </summary>
    public void RemoveFromExtraDestination(Renderer destination)
	{
		if(player.GetComponent<ProGifPlayerRenderer>() != null)
		{
			player.GetComponent<ProGifPlayerRenderer>().RemoveFromExtraDestination(destination);
		}
	}

    /// <summary>
    /// Remove a specific extra destination guiTexture from the extra display list.
    /// </summary>
    public void RemoveFromExtraDestination(GUITexture destination)
	{
		if(player.GetComponent<ProGifPlayerGuiTexture>() != null)
		{
			player.GetComponent<ProGifPlayerGuiTexture>().RemoveFromExtraDestination(destination);
		}
	}

    /// <summary>
    /// Remove a specific extra destination rawImage from the extra display list.
    /// </summary>
    public void RemoveFromExtraDestination(RawImage destination)
    {
        if(player.GetComponent<ProGifPlayerRawImage>() != null)
        {
            player.GetComponent<ProGifPlayerRawImage>().RemoveFromExtraDestination(destination);
        }
    }

    /// <summary>
    /// Clear this instance, clear all textures/sprites.
    /// </summary>
    public void Clear()
	{
		if(player != null)
		{
			player.Clear();
		}
	}
}
