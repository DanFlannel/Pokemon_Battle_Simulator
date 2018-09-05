using UnityEngine;
using System;
using System.Collections.Generic;

public class ProGifRecorder
{
	public enum RecorderState
	{
		Paused,
		PreProcessing,
		Recording,
		Stopped,
	}

	/// <summary>
	/// Called when the pre-processing step has finished.
	/// </summary>
	public event Action OnPreProcessingDone = delegate{};

	/// <summary>
	/// Called by each worker thread every time a frame is processed during the save process.
	/// The first parameter holds the worker ID and the second one a value in range [0;1] for
	/// the actual progress. This callback is probably not thread-safe, use at your own risks.
	/// </summary>
	public event Action<int, float> OnFileSaveProgress = delegate{};

	/// <summary>
	/// Called once a gif file has been saved. The first parameter will hold the worker ID and
	/// the second one the absolute file path.
	/// </summary>
	public event Action<int, string> OnFileSaved = delegate{};

	public ProGifRecorderComponent recorderCom = null;
	private string _SavedFilePath = string.Empty;

	public ProGifRecorder(Camera camera = null) 
	{
		if (camera == null) 
		{
			camera = Camera.main;

			if (camera == null) 
			{
				Debug.LogWarning ("You are trying to create recorder with NO Camera!");
				return;
			}
		}

		recorderCom = camera.gameObject.GetComponent<ProGifRecorderComponent> ();
		if (recorderCom == null) 
		{
			recorderCom = camera.gameObject.AddComponent<ProGifRecorderComponent> ();
		}

		recorderCom.OnPreProcessingDone += Recorder_OnPreProcessingDone;
		recorderCom.OnFileSaveProgress += Recorder_OnFileSaveProgress;
		recorderCom.OnFileSaved += Recorder_OnFileSaved;
	}

	/// <summary>
	/// Initializes the component. Use this if you need to change the recorder settings in a script.
	/// This will flush the previously saved frames as settings can't be changed while recording.
	/// </summary>
	/// <param name="autoAspect">Automatically compute height from the current aspect ratio if True, 
	/// force scale the gif size to width*height if False.
	/// </param>
	/// <param name="width">Width in pixels</param>
	/// <param name="height">Height in pixels</param>
	/// <param name="fps">Recording FPS</param>
	/// <param name="recorderTime">Maximum amount of seconds to record to memory</param>
	/// <param name="repeat">-1: no repeat, 0: infinite, >0: repeat count</param>
	/// <param name="quality">Quality of color quantization (conversion of images to the maximum
	/// 256 colors allowed by the GIF specification). Lower values (minimum = 1) produce better
	/// colors, but slow processing significantly. Higher values will speed up the quantization
	/// pass at the cost of lower image quality (maximum = 100).</param>
	public void Setup(bool autoAspect, int width, int height, int fps, float recorderTime, int repeat, int quality) 
	{
		if (recorderCom != null)
			recorderCom.Setup (autoAspect, width, height, fps, recorderTime, repeat, quality);
	}

	/// <summary>
	/// Initializes the component. Use this if you need to change the recorder settings in a script.
	/// This will flush the previously saved frames as settings can't be changed while recording.
	/// (Use this Setup if you need to crop the image to a specify aspect ratio. 
	/// The pixels out of the provided aspect ratio will be cut.)
	/// </summary>
	/// <param name="gifAspectRatio">Image ratio, 1:1, 16:9, 4:3, 3:2, etc. Use autoAspect if x or y of gifAspectRatio not greater than 0.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	/// <param name="fps">Frames per second</param>
	/// <param name="recorderTime">Maximum amount of seconds to record to memory</param>
	/// <param name="repeat">-1: no repeat, 0: infinite, >0: repeat count</param>
	/// <param name="quality">Quality of color quantization (conversion of images to the maximum
	/// 256 colors allowed by the GIF specification). Lower values (minimum = 1) produce better
	/// colors, but slow processing significantly. Higher values will speed up the quantization
	/// pass at the cost of lower image quality (maximum = 100).</param>
	public void Setup(Vector2 gifAspectRatio, int width, int height, int fps, float recorderTime, int repeat, int quality) 
	{
		if (recorderCom != null)
			recorderCom.Setup (gifAspectRatio, width, height, fps, recorderTime, repeat, quality);
	}

	/// <summary>
	/// Gets a stored texture by index in the texture list (You can use it as the preview for displaying on a RawImage/Renderer).
	/// </summary>
	/// <returns>RenderTexture.</returns>
	/// <param name="index">RenderTexture index in the list. 0: the first RenderTexture, -1: the last RenderTexture, other value: will clamped between 0 and count-1. </param>
	public RenderTexture GetTexture(int index = -1)
	{
		if(recorderCom != null)
		{
			if(recorderCom.Frames.Count <= 0) return null;
			if(index != 0) index = (index == -1)? recorderCom.Frames.Count-1:Mathf.Clamp(index, 0, recorderCom.Frames.Count-1);
			return recorderCom.Frames.ToArray()[index];
		}
		return null;
	}

	/// <summary>
	/// Set the GIF rotation (Support rotate 0, -90, 90, 180 degrees).
	/// * Change during/after PreProcessing state will not be applied.
	/// </summary>
	/// <param name="rotation">Rotation: 0, -90, 90, 180 degrees</param>
	public void SetGifRotation(ImageRotator.Rotation rotation)
	{
		if (recorderCom != null)
			recorderCom.SetGifRotation(rotation);
	}

	/// <summary>
	/// Set the gif aspect ratio.
	/// * Change during/after PreProcessing state will not be applied.
	/// </summary>
	/// <param name="gifAspectRatio">Aspect ratio for gif.</param>
	public void SetGifAspectRatio(Vector2 gifAspectRatio)
	{
		if (recorderCom != null)
			recorderCom.SetGifAspectRatio(gifAspectRatio);
	}

	/// <summary>
	/// Set the new frame delay to override the FPS(m_FramePerSecond).
	/// * Change during/after PreProcessing state will not be applied.
	/// </summary>
	/// <param name="frameDelayInSeconds">Frame delay in seconds.</param>
	public void SetOverrideFrameDelay(float frameDelayInSeconds)
	{
		if (recorderCom != null)
			recorderCom.SetOverrideFrameDelay(frameDelayInSeconds);
	}

	/// <summary>
	/// Sets the transparent color, hide this color in the GIF. 
	/// The GIF specification allows setting a color to be transparent. 
	/// *** Use case: if you want to record gameObject, character or anything else with transparent background, 
	/// please make sure the background is of solid color(no gradient), and the target object do not contain this color.
	/// (Also be reminded, the transparent feature takes more time to encoding the GIF)
	/// </summary>
	/// <param name="color32">The Color to hide.</param>
	public void SetTransparent(Color32 color32)
	{
		if (recorderCom != null)
			recorderCom.SetTransparent(color32);
	}

	/// <summary>
	/// Auto detects the input image(s) pixels for enable/disable transparent feature. 
	/// *** Use case: for pre-made images that have transparent pixels manually set.
	/// (Also be reminded, the transparent feature takes more time to encoding the GIF)
	/// </summary>
	/// <param name="autoDetectTransparent">If set to <c>true</c> auto detect transparent.</param>
	public void SetTransparent(bool autoDetectTransparent)
	{
		if (recorderCom != null)
			recorderCom.SetTransparent(autoDetectTransparent);
	}

	/// <summary>
	/// Pauses recording.
	/// </summary>
	public void Pause() 
	{
		if (recorderCom != null)
			recorderCom.Pause();
	}

	/// <summary>
	/// Resume recording.
	/// </summary>
	public void Resume() 
	{
		if (recorderCom != null)
			recorderCom.Resume();
	}

	/// <summary>
	/// Starts or resumes recording. You can't resume while it's pre-processing data to be saved.
	/// </summary>
	public void Record(Action onDurationEnd) 
	{
		if (recorderCom != null)
			recorderCom.Record(onDurationEnd);
	}

	/// <summary>
	/// Stops the recording. You can't resume the record after it has been stopped. You can save gif or start a new recording.
	/// </summary>
	public void Stop() 
	{
		if (recorderCom != null)
			recorderCom.Stop();
	}

	/// <summary>
	/// Clears all saved frames from memory and starts fresh.
	/// </summary>
	public void FlushMemory() 
	{
		if (recorderCom != null)
			recorderCom.FlushMemory();
	}

	/// <summary>
	/// Saves the stored frames to a gif file. The filename will automatically be generated.
	/// Recording will be paused and won't resume automatically. You can use the 
	/// <code>OnPreProcessingDone</code> callback to be notified when the pre-processing
	/// step has finished.
	/// </summary>
	public void Save() 
	{
		if (recorderCom != null)
			recorderCom.Save();
	}

	/// <summary>
	/// Saves the stored frames to a gif file. If the filename is null or empty, an unique one
	/// will be generated. You don't need to add the .gif extension to the name. Recording will
	/// be paused and won't resume automatically. You can use the <code>OnPreProcessingDone</code>
	/// callback to be notified when the pre-processing step has finished.
	/// </summary>
	/// <param name="filename">File name without extension</param>
	public void Save(string filename) 
	{
		if (recorderCom != null)
			recorderCom.Save(filename);
	}

	/// <summary>
	/// Current state of the recorder.
	/// </summary>
	public RecorderState State 
	{
		get {
			if (recorderCom != null)
				return recorderCom.State;
			else
				return RecorderState.Paused;
		}
	}

	public RenderTexture[] Frames 
	{
		get { 
			if (recorderCom == null) return null;
			return recorderCom.Frames.ToArray();
		}
	}

	/// <summary>
	/// Force assign frames(RenderTexture) to this recorder. This will clear all previous stored frames first.
	/// </summary>
	/// <param name="renderTextures">RenderTexture list(FIFO).</param>
	public void ForceSetFrames(Queue<RenderTexture> renderTextures)
	{
		if (recorderCom != null)
			recorderCom.ForceSetFrames(renderTextures);
	}

	public int Width 
	{
		get {
			if (recorderCom == null) return 0;
			return recorderCom.Width;
		}
	}

	public int Height 
	{
		get {
			if (recorderCom == null) return 0;
			return recorderCom.Height;
		}
	}

	public bool IsCustomRatio 
	{
		get {
			if (recorderCom == null) return false;
			return recorderCom.IsCustomRatio;
		}
	}

	public int FPS 
	{
		get {
			if (recorderCom == null) return 0;
			return recorderCom.FPS;
		}
	}

	public ImageRotator.Rotation Rotation
	{
		get {
			if (recorderCom == null) return ImageRotator.Rotation.None;
			return recorderCom.m_Rotation;
		}
	}

	/// <summary>
	/// Saved File Path. Empty if recorded gif was not yet saved
	/// </summary>
	public string SavedFilePath 
	{
		get {
			return _SavedFilePath;
		}
	}

	private void Recorder_OnFileSaved(int id, string path) 
	{
		_SavedFilePath = path;
		OnFileSaved (id, path);
	}

	private void Recorder_OnFileSaveProgress(int id, float progress) 
	{
		OnFileSaveProgress(id, progress);
	}

	private void Recorder_OnPreProcessingDone() 
	{
		OnPreProcessingDone();
	}


	public void SetOnRecordAction(Action<float> onRecordAction)
	{
		if(recorderCom != null) recorderCom.SetOnRecordAction(onRecordAction);
	}

	public float RecordProgress
	{
		get{
			if(recorderCom != null) return recorderCom.RecordProgress;
			return 0f;
		}
	}

	public float EstimatedMemoryUse
	{
		get{
			if(recorderCom != null) return recorderCom.EstimatedMemoryUse;
			return 0f;
		}
	}

	/// <summary>
	/// Clear & Remove the ProGifRecorderComponent from camera
	/// </summary>
	public void Clear()
	{
		if(recorderCom != null) recorderCom.ClearAndRemoveScript();
	}
}
	


