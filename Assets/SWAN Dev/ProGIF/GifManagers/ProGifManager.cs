/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ProGifManager : MonoBehaviour
{
	public static string PP_LastGifPathKey = "ProGIF_LastGifPathKey";

	//----- Apply your gif channel & api keys here: https://developers.giphy.com/ -----//
	public string m_GiphyUserName = "";		//Your user name on Giphy (Required only if you are using a production api key, else you must leave it empty if you are using a rate-limited key)
	public string m_GiphyApiKey = "";		//Your API key associated with your app on Giphy (for NormalGifApi & StickerApi & Rate-limited upload)
	public string m_GiphyUploadApiKey = ""; //Your upload API key associated with your app on Giphy

	[HideInInspector]
	public ProGifRecorder m_GifRecorder = null;
	[HideInInspector]
	public ProGifPlayer m_GifPlayer = null;
	[HideInInspector]
	public  string m_CurrentGifPath = "";

	#region ----- GIF Recorder Settings -----
	private int m_MaxFps = 60;

	public Vector2 m_AspectRatio = new Vector2(0, 0);
	public bool m_AutoAspect = true;
	public int m_Width = 360;
	public int m_Height = 360;
	public float m_Duration = 3f;
	[Range(1, 60)] public int m_Fps = 15;
	public int m_Loop = 0;								//-1: no repeat, 0: infinite, >0: repeat count
	[Range(1, 100)] public int m_Quality = 20;			//(1 - 100), 1: best(larger storage size), 100: faster(smaller storage size)

	public ImageRotator.Rotation m_Rotation = ImageRotator.Rotation.None;
	public Color32 m_TransparentColor = new Color32(0, 0, 0, 0);
	public bool m_AutoTransparent = false;

	public bool LoadFile{
		get{
			return (m_AspectRatio.x > 0 && m_AspectRatio.y > 0)? true:false;
		}
	}
	#endregion

	#region ----- GIF Player Settings -----
	[Space()]
	[Header("[ GIF Player Decode Settings ]")]
	public ProGifPlayerComponent.Decoder m_Decoder = ProGifPlayerComponent.Decoder.ProGif_QueuedThread;
	public ProGifPlayerComponent.DecodeMode m_DecodeMode = ProGifPlayerComponent.DecodeMode.Normal;
	public ProGifPlayerComponent.FramePickingMethod m_FramePickingMethod = ProGifPlayerComponent.FramePickingMethod.ContinuousFromBeginning;
	[Range(-1, 9999)] public int m_TargetDecodeFrameNum = -1;		//if targetDecodeFrameNum <= 0: decode & play all frames
	public bool m_OptimizeMemoryUsage = true;
	#endregion

	private Action<int, string> _OnFileSavedAction = null;
	private Action<int, float> _OnFileSaveProgressAction = null;
	private Action _OnRecorderPreProcessingDoneAction = null;
	private Action<float> _OnRecordProgressAction = null;
	private Action _OnRecordDurationMaxAction = null;

	private static ProGifManager _instance = null;
	/// <summary>
	/// Gets the instance of ProGifManager. Create new one if no existing instance.
	/// Use this instance to control gif Record, Playback and Settings.
	/// </summary>
	/// <value>The instance.</value>
	public static ProGifManager Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new GameObject("[ProGifManager]").AddComponent<ProGifManager>();
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
		}
	}

	/// <summary>
	/// (Settings-1) Sets the recording settings before StartRecord
	/// </summary>
	/// <param name="autoAspect">If set to true, auto aspect. Else force scale gif size to width*height.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height. If autoAspect, height will be recalculated.</param>
	/// <param name="duration">Total time to record.</param>
	/// <param name="fps">Frames per second.</param>
	/// <param name="loop">Loop. -1: no repeat, 0: infinite, >0: repeat count</param>
	/// <param name="quality">Quality. (1 - 100), 1: best, 100: faster</param>
	public void SetRecordSettings(bool autoAspect, int width, int height, float duration, int fps, int loop, int quality)
	{
		m_AutoAspect = autoAspect;
		m_Width = width;
		m_Height = height;
		m_Fps = fps;
		m_Duration = duration;
		m_Loop = loop;
		m_Quality = quality;

		m_AspectRatio = new Vector2(0, 0); //Use auto aspect
	}

	/// <summary>
	/// (Settings-2) Sets the recording settings before StartRecord
	/// </summary>
	/// <param name="aspectRatio">A Specify aspect ratio for cropping gif. Set (0,0) if dont use, or use Settings-1 instead.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height. If autoAspect, height will be recalculated.</param>
	/// <param name="duration">Total time to record.</param>
	/// <param name="fps">Frames per second.</param>
	/// <param name="loop">Loop. -1: no repeat, 0: infinite, >0: repeat count</param>
	/// <param name="quality">Quality. (1 - 100), 1: best, 100: faster</param>
	public void SetRecordSettings(Vector2 aspectRatio, int width, int height, float duration, int fps, int loop, int quality)
	{
		m_AspectRatio = aspectRatio;
		m_Width = width;
		m_Height = height;
		m_Fps = fps;
		m_Duration = duration;
		m_Loop = loop;
		m_Quality = quality;
	}

	private void _InitRecorder(Camera camera, bool autoClear)
	{
		Clear();

		if(m_Fps > m_MaxFps) m_Fps = m_MaxFps;

		m_GifRecorder = new ProGifRecorder(camera);
		if(m_AspectRatio.x > 0 && m_AspectRatio.y > 0)
		{
			m_GifRecorder.Setup(
				m_AspectRatio, 	//a specify aspect ratio for cropping gif
				m_Width,  		//width
				m_Height,  		//height
				m_Fps,   		//fps
				m_Duration, 	//recorder time
				m_Loop,    		//repeat, -1: no repeat, 0: infinite, >0: repeat count
				m_Quality);  	//quality (1 - 100), 1: best, 100: faster
		}
		else
		{
			m_GifRecorder.Setup(
				m_AutoAspect, 	//autoAspect
				m_Width,  		//width
				m_Height,  		//height
				m_Fps,   		//fps
				m_Duration, 	//recorder time
				m_Loop,    		//repeat, -1: no repeat, 0: infinite, >0: repeat count
				m_Quality);  	//quality (1 - 100), 1: best, 100: faster
		}

		//Set the gif transparent color
		m_GifRecorder.SetTransparent(m_TransparentColor);

		//Enable/Disable auto detect the image transparent setting for enabling transparent
		m_GifRecorder.SetTransparent(m_AutoTransparent);

		//Set the gif rotation
		m_GifRecorder.SetGifRotation(m_Rotation);

        //Set the callback to be called when pre-processing complete
        m_GifRecorder.OnPreProcessingDone += _OnRecorderPreProcessingDone;

        //Set the callback to update the gif save progress
        m_GifRecorder.OnFileSaveProgress += _OnRecorderFileSaveProgress;

        //Set the callback to be called when gif file saved
        m_GifRecorder.OnFileSaved += _OnRecorderFileSaved;

        //Set the callback to clear the recorder after gif saved
        if(autoClear)
        {
            Action<int, string> clearRecorder = (id, path) => {
				ClearRecorder();
            };
            m_GifRecorder.OnFileSaved += clearRecorder;
        }
    }

	/// <summary>
	/// To be called during save frames to gif file in worker thread
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="progress">Progress.</param>
	private void _OnRecorderFileSaveProgress(int id, float progress)
	{
		if(_OnFileSaveProgressAction != null) _OnFileSaveProgressAction(id, progress);
	}

	/// <summary>
	/// To be called when PreProcessing complete
	/// (PreProcessing: The process to extract, crop frame data and send everything to a separate worker thread)
	/// </summary>
	private void _OnRecorderPreProcessingDone()
	{
        if(_OnRecorderPreProcessingDoneAction != null) _OnRecorderPreProcessingDoneAction();
	}

    /// <summary>
    /// Starts the recorder to store frames with Camera.main.
    /// (For changing recorder settings, you can use the SetRecordSettings method before calling StartRecord)
    /// </summary>
    /// <param name="onRecordProgress">Update the record progress. Return values: record progress(float)</param>
    /// <param name="onRecordDurationMax">To be fired when target duration frames reached.</param>
    /// <param name="autoClear">If set to <c>true</c>, clear the recorder after gif saved.</param>
    public void StartRecord(Action<float> onRecordProgress = null, Action onRecordDurationMax = null, bool autoClear = true)
	{
		StartRecord(Camera.main, onRecordProgress, onRecordDurationMax, autoClear);
	}

    /// <summary>
    /// Starts the recorder to store frames with specific camera. 
    /// (For changing recorder settings, you can use the SetRecordSettings method before calling StartRecord)
    /// </summary>
    /// <param name="camera">Target Camera for recording gif</param>
    /// <param name="onRecordProgress">Update the record progress. Return values: record progress(float)</param>
    /// <param name="onRecordDurationMax">To be fired when target duration frames reached.</param>
    /// <param name="autoClear">If set to <c>true</c>, clear the recorder after gif saved.</param>
    public void StartRecord(Camera camera, Action<float> onRecordProgress = null, Action onRecordDurationMax = null, bool autoClear = true)
	{
		_InitRecorder(camera, autoClear);

		//Start the recording with a callback that will be called when max. frames are stored in recorder
		m_GifRecorder.Record(onRecordDurationMax);

		//Set the callback to update the record progress during recording.
		m_GifRecorder.SetOnRecordAction(onRecordProgress);
	}

	/// <summary>
	/// Sets the GIF rotation.
	/// </summary>
	/// <param name="rotation">Rotation: None, -90, 90, 180</param>
	public void SetGifRotation(ImageRotator.Rotation rotation)
	{
		if(m_GifRecorder == null)
		{
			m_Rotation = rotation;
			return;
		}
		m_GifRecorder.SetGifRotation(rotation);
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
		if(m_GifRecorder == null)
		{
			m_TransparentColor = color32;
			m_AutoTransparent = false;
			return;
		}
		m_GifRecorder.SetTransparent(color32);
	}

	/// <summary>
	/// Auto detects the input image(s) pixels for enable/disable transparent feature. 
	/// *** Use case: for pre-made images that have transparent pixels manually set.
	/// (Also be reminded, the transparent feature takes more time to encoding the GIF)
	/// </summary>
	/// <param name="autoDetectTransparent">If set to <c>true</c> auto detect transparent.</param>
	public void SetTransparent(bool autoDetectTransparent)
	{
		if(m_GifRecorder == null)
		{
			m_AutoTransparent = autoDetectTransparent;
			m_TransparentColor = new Color32(0, 0, 0, 0);
			return;
		}
		m_GifRecorder.SetTransparent(autoDetectTransparent);
	}

	/// <summary>
	/// Updates the record progress during recording.
	/// </summary>
	/// <param name="progress">Progress.</param>
	private void _UpdateRecordProgress(float progress)
	{
		#if UNITY_EDITOR
		Debug.Log("Progress: " + (int)(100 * progress) + " %");
		#endif
		if(_OnRecordProgressAction != null) _OnRecordProgressAction(progress);
	}

	/// <summary>
	/// To be called when the number of frames in recorder reached maximum(duration x fps).
	/// </summary>
	private void _OnRecordDurationMax()
	{
		//You can stop the recording here, or let the recording continue until user press the stop button, 
		//if continue recording, the recorder will discard oldest frames and store new frames for keeping target duration GIF frames
		#if UNITY_EDITOR
		Debug.Log("Target duration: " + m_Duration + "s\nPress save or set your timing to generate GIF");
		#endif

		if(_OnRecordDurationMaxAction != null) _OnRecordDurationMaxAction();
	}

	/// <summary>
	/// Pauses the recorder.
	/// </summary>
	public void PauseRecord()
	{
		//Pause the recording process. The recording may be resumed
		m_GifRecorder.Pause();
	}

	/// <summary>
	/// Resume the recorder.
	/// </summary>
	public void ResumeRecord()
	{
		//Resume the recording process. Continue to save frames
		m_GifRecorder.Resume();
	}

	/// <summary>
	/// Stops the recorder.
	/// </summary>
	public void StopRecord()
	{
		//Stop the recording. The recording can NOT be resumed after it has been stopped
		m_GifRecorder.Stop();
	}

	/// <summary>
	/// Saves the stored frames to a gif file.
	/// </summary>
	/// <param name="onRecorderPreProcessingDone">On recorder pre processing done.</param>
	/// <param name="onFileSaveProgress">On file save progress. Retrun values: worker id(int), save progress(float).</param>
	/// <param name="onFileSaved">On file saved. Return values: id(int), saved path(string).</param>
	public void SaveRecord(Action onRecorderPreProcessingDone = null, Action<int, float> onFileSaveProgress = null, Action<int, string> onFileSaved = null, string fileNameWithoutExtension = "")
	{
		//Set callbacks
		_OnRecorderPreProcessingDoneAction = onRecorderPreProcessingDone;
		_OnFileSaveProgressAction = onFileSaveProgress;
		_OnFileSavedAction = onFileSaved;

		//Saves the stored frames to a gif file. 
		m_GifRecorder.Save(fileNameWithoutExtension);
	}

	/// <summary>
	/// Stops the recorder and saves the stored frames to a gif file.
	/// </summary>
	/// <param name="onRecorderPreProcessingDone">On recorder pre processing done.</param>
	/// <param name="onFileSaveProgress">On file save progress. Retrun values: worker id(int), save progress(float).</param>
	/// <param name="onFileSaved">On file saved. Return values: id(int), saved path(string).</param>
	public void StopAndSaveRecord(Action onRecorderPreProcessingDone = null, Action<int, float> onFileSaveProgress = null, Action<int, string> onFileSaved = null, string fileNameWithoutExtension = "")
	{
		//Stops the recording. The recording can NOT be resumed after it has been stopped
		m_GifRecorder.Stop();

		//Sets callbacks and saves the stored frames to a gif file. You can call this method in any place in your code. 
		SaveRecord(onRecorderPreProcessingDone, onFileSaveProgress, onFileSaved, fileNameWithoutExtension);
	}

	//Unsubscribe and memory flushing in OnFileSaved callback
	private void _OnRecorderFileSaved(int id, string path)
	{
		#if UNITY_EDITOR
		Debug.Log("Current saved gif path: " + path);
		#endif
		m_CurrentGifPath = path;
		PlayerPrefs.SetString(PP_LastGifPathKey, path);

		if(_OnFileSavedAction != null) _OnFileSavedAction(id, path);
	}


	#region ----- Player -----

	/// <summary> Resets the decode settings(Set the decodeMode as Normal, simply decodes the entire gif without applying advanced settings) </summary>
	public void ResetPlayerDecodeSettings()
	{
		m_Decoder = ProGifPlayerComponent.Decoder.ProGif_QueuedThread;
		m_DecodeMode = ProGifPlayerComponent.DecodeMode.Normal;
		m_FramePickingMethod = ProGifPlayerComponent.FramePickingMethod.ContinuousFromBeginning;
		m_TargetDecodeFrameNum = -1;
		m_OptimizeMemoryUsage = true;
	}
    
    /// <summary> Sets the decodeMode as Advanced, apply the advanced settings(decoder, targetDecodeFrameNum, framePickingMethod..) </summary>
    /// <param name="decoder">Decoder option: select to run the decode process in Coroutine, or in the Thread.</param>
    /// <param name="targetDecodeFrameNum">Limit the number of frames to decode.</param>
    /// <param name="framePickingMethod">The method for picking a limited number of frames to decode.</param>
    public void SetAdvancedPlayerDecodeSettings(ProGifPlayerComponent.Decoder decoder, int targetDecodeFrameNum = -1, 
		ProGifPlayerComponent.FramePickingMethod framePickingMethod = ProGifPlayerComponent.FramePickingMethod.ContinuousFromBeginning, bool optimizeMemoryUsage = true)
	{
		m_Decoder = decoder;
		m_DecodeMode = ProGifPlayerComponent.DecodeMode.Advanced;
		m_TargetDecodeFrameNum = targetDecodeFrameNum;
		m_FramePickingMethod = framePickingMethod;
		m_OptimizeMemoryUsage = optimizeMemoryUsage;
	}

	/// <summary>
	/// Play gif from Recorder, display with Image.
	/// </summary>
	/// <param name="playerImage">Target image for displaying gif.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public void PlayGif(Image playerImage, Action<float> onLoading = null)
	{
		if(m_GifRecorder == null)
		{
			Debug.LogWarning("GIF recorder not found!");
			return;
		}

		m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.Play(m_GifRecorder, playerImage, m_OptimizeMemoryUsage);
		m_GifPlayer.SetLoadingCallback((progress)=>{
			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

	/// <summary>
	/// Play gif from Recorder, display with Renderer.
	/// </summary>
	/// <param name="playerRenderer">Target renderer for displaying gif.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public void PlayGif(Renderer playerRenderer, Action<float> onLoading = null)
	{
		if(m_GifRecorder == null)
		{
			Debug.LogWarning("GIF recorder not found!");
			return;
		}

		m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.Play(m_GifRecorder, playerRenderer, m_OptimizeMemoryUsage);
		m_GifPlayer.SetLoadingCallback((progress)=>{
			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

	/// <summary>
	/// Play gif from Recorder, display with GUITexture.
	/// </summary>
	/// <param name="playerGuiTexture">Target GUITexture for displaying gif.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public void PlayGif(GUITexture playerGuiTexture, Action<float> onLoading = null)
	{
		if(m_GifRecorder == null)
		{
			Debug.LogWarning("GIF recorder not found!");
			return;
		}

		m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.Play(m_GifRecorder, playerGuiTexture, m_OptimizeMemoryUsage);
		m_GifPlayer.SetLoadingCallback((progress)=>{
			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

    /// <summary>
    /// Play gif from Recorder, display with RawImage.
    /// </summary>
    /// <param name="playerRawImage">Target RawImage for displaying gif.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    public void PlayGif(RawImage playerRawImage, Action<float> onLoading = null)
    {
        if (m_GifRecorder == null)
        {
            Debug.LogWarning("GIF recorder not found!");
            return;
        }

        m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.Play(m_GifRecorder, playerRawImage, m_OptimizeMemoryUsage);
        m_GifPlayer.SetLoadingCallback((progress) => {
            if (onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with Image.
    /// </summary>
    /// <param name="gifPath">GIF path.</param>
    /// <param name="playerImage">Target image for displaying gif.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public void PlayGif(string gifPath, Image playerImage, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        Clear();
        m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
        m_GifPlayer.Play(gifPath, playerImage, shouldSaveFromWeb);
        m_GifPlayer.SetLoadingCallback((progress) => {
            //Check progress
            if (progress >= 1f)
            {
                m_GifPlayer.SetLoadingCallback(null);
            }

            if (onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with Renderer.
    /// </summary>
    /// <param name="gifPath">GIF path.</param>
    /// <param name="playerRenderer">Target renderer for displaying gif.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public void PlayGif(string gifPath, Renderer playerRenderer, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        Clear();
        m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
        m_GifPlayer.Play(gifPath, playerRenderer, shouldSaveFromWeb);
        m_GifPlayer.SetLoadingCallback((progress) => {
            //Check progress
            if (progress >= 1f)
            {
                m_GifPlayer.SetLoadingCallback(null);
            }

            if (onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with GUITexture.
    /// </summary>
    /// <param name="gifPath">GIF path.</param>
    /// <param name="playerGuiTexture">Target GUITexture for displaying gif.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public void PlayGif(string gifPath, GUITexture playerGuiTexture, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
	{
		Clear();
		m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
		m_GifPlayer.Play(gifPath, playerGuiTexture, shouldSaveFromWeb);
		m_GifPlayer.SetLoadingCallback((progress)=>{
			//Check progress
			if(progress >= 1f)
			{
				m_GifPlayer.SetLoadingCallback(null);
			}

			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

    /// <summary>
    /// Load GIF from path/url for playback, display with RawImage.
    /// </summary>
    /// <param name="gifPath">GIF path.</param>
    /// <param name="playerRawImage">Target RawImage for displaying gif.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public void PlayGif(string gifPath, RawImage playerRawImage, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        Clear();
        m_GifPlayer = new ProGifPlayer();
		m_GifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
        m_GifPlayer.Play(gifPath, playerRawImage, shouldSaveFromWeb);
        m_GifPlayer.SetLoadingCallback((progress) => {
            //Check progress
            if (progress >= 1f)
            {
                m_GifPlayer.SetLoadingCallback(null);
            }

            if (onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
	/// Set the callback for checking the decode progress.
	/// If using a recorder source for playback, this becomes a loading-complete callback.
    /// </summary>
    /// <param name="onLoading">On loading callback, returns the decode progress(float).</param>
    public void SetPlayerOnLoading(Action<float> onLoading)
	{
		if(m_GifPlayer == null)
		{
			Debug.LogWarning("Player not found!");
			return;
		}
		m_GifPlayer.SetLoadingCallback(onLoading);
	}

	/// <summary>
	/// Set the callback to be fired when the first gif frame ready.
	/// If using a recorder source for playback, this becomes a loading-complete callback with the first GIF frame returned.
	/// </summary>
	/// <param name="onFirstFrame">On first frame callback, returns the first gifTexture and related data.</param>
	public void SetPlayerOnFirstFrame(Action<ProGifPlayerComponent.FirstGifFrame> onFirstFrame)
	{
		if(m_GifPlayer == null)
		{
			Debug.LogWarning("Player not found!");
			return;
		}
		m_GifPlayer.SetOnFirstFrameCallback(onFirstFrame);
	}

	/// <summary>
	/// Set the callback to be fired on every frame when playing gif.
	/// </summary>
	/// <param name="onPlaying">On gif playing callback, returns the current gifTexture.</param>
	public void SetPlayerOnPlaying(Action<GifTexture> onPlaying)
	{
		if(m_GifPlayer == null)
		{
			Debug.LogWarning("Player not found!");
			return;
		}
		m_GifPlayer.SetOnPlayingCallback(onPlaying);
	}

	/// <summary>
	/// Set the callback to be fired when all frames decode complete (for decoder only).
	/// </summary>
	/// <param name="onDecodeComplete">On decode complete callback, returns the gifTextures list and related data.</param>
	public void SetPlayerOnDecodeComplete(Action<ProGifPlayerComponent.DecodedResult> onDecodeComplete)
	{
		if(m_GifPlayer == null)
		{
			Debug.LogWarning("Player not found!");
			return;
		}
		m_GifPlayer.SetOnDecodeCompleteCallback(onDecodeComplete);
	}

	public void PausePlayer()
	{
		if(m_GifPlayer == null)
		{
			Debug.LogWarning("Player not found!");
			return;
		}
		m_GifPlayer.Pause();
	}

	public void ResumePlayer()
	{
		if(m_GifPlayer == null)
		{
			Debug.LogWarning("Player not found!");
			return;
		}
		m_GifPlayer.Resume();
	}

	public void StopPlayer()
	{
		if(m_GifPlayer == null)
		{
			Debug.LogWarning("Player not found!");
			return;
		}
		m_GifPlayer.Stop();
	}
	#endregion

	/// <summary>
	/// Twitter share example.
	/// </summary>
	/// <param name="filePath">File path.</param>
	public void ShareTwitter(string filePath)
	{
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			ShareTwitter_Mobile(filePath);
		}
		else
		{
			if(string.IsNullOrEmpty(filePath)) filePath = m_CurrentGifPath;

			#if UNITY_EDITOR
			Debug.Log("ShareTwitter: " + filePath);
			#endif

			//Your social share code here: upload to Giphy and get the gif id in the response, 
			GiphyManager giphyMgr = GiphyManager.Instance;
			giphyMgr.SetChannelAuthentication(userName: m_GiphyUserName, apiKey: m_GiphyApiKey, uploadApiKey: m_GiphyUploadApiKey);

			giphyMgr.Upload(filePath, new List<string>{"TestTweet", "SwanDEV", "GameGIF"},
				(uploadResponse)=>{

					#if UNITY_EDITOR
					Debug.Log("Upload Response - Giphy GIF Id: " + uploadResponse.data.id);
					#endif

					//Social share code: share on Twitter(desktop) require GIF Id only, so we dont need to call the GetById api for gif url
					GifSocialShare gifShare = new GifSocialShare();
					gifShare.ShareTo(GifSocialShare.Social.Twitter, "", "", uploadResponse.data.id, "");

				}, 
				(uploadProgress)=>{
					//This is upload progress only, Giphy server need to take more time for setting up the uploaded GIF, 
					//the server will return the GIF data in the upload response json when complete.
				}
			);
		}
	}

	/// <summary>
	/// Twitter mobile share example.
	/// </summary>
	/// <param name="filePath">File path.</param>
	public void ShareTwitter_Mobile(string filePath)
	{
		if(string.IsNullOrEmpty(filePath)) filePath = m_CurrentGifPath;

		#if UNITY_EDITOR
		Debug.Log("ShareTwitter: " + filePath);
		#endif

		//Your social share code here: upload to Giphy and get the gif id in the response,
		GiphyManager giphyMgr = GiphyManager.Instance;
		giphyMgr.SetChannelAuthentication(userName: m_GiphyUserName, apiKey: m_GiphyApiKey, uploadApiKey: m_GiphyUploadApiKey);

		giphyMgr.Upload(filePath, new List<string>{"TestTweet", "SwanDEV", "GameGIF"},
			(uploadResponse)=>{

				#if UNITY_EDITOR
				Debug.Log("Upload Response - Giphy GIF Id: " + uploadResponse.data.id);
				#endif

				//Get the uploaded gif data, url for sharing on social platforms
				giphyMgr.GetById(uploadResponse.data.id, 
					(byIdResponse)=>{

						#if UNITY_EDITOR
						Debug.Log("GetById Response - Giphy GIF url: " + byIdResponse.data.images.original.url);
						#endif

						//Social share code:
						GifSocialShare gifShare = new GifSocialShare();
						gifShare.ShareTo(GifSocialShare.Social.Twitter_Mobile, "GIFTest", 
							"This GIF is created with ProGIF/GameGIF. Get the plugins on the Asset Store now: http://u3d.as/QkW", 
							"GameGIF", byIdResponse.data.bitly_gif_url);
					}
				);

			}, 
			(uploadProgress)=>{
				//This is upload progress only, Giphy server need to take more time for setting up the uploaded GIF, 
				//the server will return the GIF data in the upload response json when complete.
			}
		);
	}

	/// <summary>
	/// Facebook share example.
	/// </summary>
	/// <param name="filePath">File path.</param>
	public void ShareFacebook(string filePath = "")
	{
		if(string.IsNullOrEmpty(filePath)) filePath = m_CurrentGifPath;

		#if UNITY_EDITOR
		Debug.Log("ShareFacebook: " + filePath);
		#endif

		//Your social share code here: upload to Giphy and get the gif id in the response, 
		GiphyManager giphyMgr = GiphyManager.Instance;
		giphyMgr.SetChannelAuthentication(userName: m_GiphyUserName, apiKey: m_GiphyApiKey, uploadApiKey: m_GiphyUploadApiKey);

		giphyMgr.Upload(filePath, new List<string>{"TestFB", "SwanDEV", "GameGIF"},
			(uploadResponse)=>{

				#if UNITY_EDITOR
				Debug.Log("Upload Response - Giphy GIF Id: " + uploadResponse.data.id);
				#endif

				//Get the uploaded gif data, url for sharing on social platforms
				giphyMgr.GetById(uploadResponse.data.id, 
					(byIdResponse)=>{

						#if UNITY_EDITOR
						Debug.Log("GetById Response - Giphy GIF url: " + byIdResponse.data.images.original.url);
						#endif

						//Social share code:
						GifSocialShare gifShare = new GifSocialShare();
						gifShare.ShareTo(GifSocialShare.Social.Facebook, "", "", "", byIdResponse.data.images.original.url);

					}
				);

			}, 
			(uploadProgress)=>{
				//This is upload progress only, Giphy server need to take more time for setting up the uploaded GIF, 
				//the server will return the GIF data in the upload response json when complete.
			}
		);
	}

	public void Clear()
	{
		ClearRecorder();
		ClearPlayer();
	}

	public void ClearRecorder()
	{
		if(m_GifRecorder != null)
		{
			m_GifRecorder.OnPreProcessingDone -= _OnRecorderPreProcessingDone;
			m_GifRecorder.OnFileSaveProgress -= _OnRecorderFileSaveProgress;
			m_GifRecorder.OnFileSaved -= _OnRecorderFileSaved;

			m_GifRecorder.Clear();
			m_GifRecorder = null;
		}
	}

	public void ClearPlayer()
	{
		if(m_GifPlayer != null)
		{
			m_GifPlayer.Clear();
			m_GifPlayer = null;
		}
	}

	#region ---- Util ----
	public static T InstantiatePrefab<T>(GameObject prefab) where T: MonoBehaviour
	{
		if(prefab != null)
		{
			GameObject go = GameObject.Instantiate(prefab) as GameObject;
			if(go != null)
			{
				go.name = "[Prefab]" + prefab.name;
				go.transform.localScale = Vector3.one;
				return go.GetComponent<T>();
			}
			else
			{	
				Debug.Log("prefab is null!") ;
				return null ;
			}
		}
		else
			return null ;
	}

	public static Color GetColor(CommonColorEnum colorEnum)
	{
		Color c = Color.white;;
		switch(colorEnum)
		{
		case CommonColorEnum.Black:
			c = Color.black; //black
			break;
		case CommonColorEnum.Blue:
			c = new Color(0f, 0.5f, 1f, 1f); //blue
			break;
		case CommonColorEnum.Green:
			c = new Color(0.5f, 1f, 0.5f, 1f); //green
			break;
		case CommonColorEnum.Red:
			c = new Color(1f, 0.5f, 0.5f, 1f); //red
			break;
		case CommonColorEnum.LightYellow:
			c = new Color(1f, 220f/255f, 110f/255f, 1f);; //light yellow
			break;
		}

		return c;
	}
	public enum CommonColorEnum{
		White = 0,
		Black,
		Blue,
		Green,
		Red,
		LightYellow,
	}

	#endregion
}