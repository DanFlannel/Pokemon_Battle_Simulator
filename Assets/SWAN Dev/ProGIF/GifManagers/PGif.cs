using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PGif : MonoBehaviour
{
	#region ----- GIF Recorder Settings -----
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

	public Dictionary<string, ProGifRecorder> m_GifRecorderDict = new Dictionary<string, ProGifRecorder>();
	public Dictionary<string, ProGifPlayer> m_GifPlayerDict = new Dictionary<string, ProGifPlayer>();

	private static PGif _instance = null;
	/// <summary>
	/// Gets the instance of PGif. Create new one if no existing instance.
	/// Use this instance to control gif Record, Playback and Settings.
	/// </summary>
	/// <value>The instance.</value>
	public static PGif Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new GameObject("[PGif]").AddComponent<PGif>();
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

	#region ----- Recorders -----
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

	/// <summary>
	/// Sets the GIF rotation.
	/// </summary>
	/// <param name="rotation">Rotation: None, -90, 90, 180</param>
	public void SetGifRotation(ImageRotator.Rotation rotation)
	{
		m_Rotation = rotation;
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
		m_TransparentColor = color32;
		m_AutoTransparent = false;
	}

	/// <summary>
	/// Auto detects the input image(s) pixels for enable/disable transparent feature. 
	/// *** Use case: for pre-made images that have transparent pixels manually set.
	/// (Also be reminded, the transparent feature takes more time to encoding the GIF)
	/// </summary>
	/// <param name="autoDetectTransparent">If set to <c>true</c> auto detect transparent.</param>
	public void SetTransparent(bool autoDetectTransparent)
	{
		m_AutoTransparent = autoDetectTransparent;
		m_TransparentColor = new Color32(0, 0, 0, 0);
	}

	/// <summary>
	/// Create/Start a new recorder to store frames with specific camera. 
	/// </summary>
	/// <param name="camera">The target Camera to attach the newly create gif recroder.</param>
	/// <param name="recorderName">Recorder Name for identifying recorders in the dictionary.</param>
	/// <param name="onRecordProgress">Update the record progress. Return values: record progress(float)</param>
	/// <param name="onRecordDurationMax">To be fired when target duration frames reached.</param>
	/// <param name="onPreProcessingDone">On pre processing done.</param>
	/// <param name="onFileSaveProgress">On file save progress. Retrun values: worker id(int), save progress(float).</param>
	/// <param name="onFileSaved">On file saved. Return values: id(int), saved path(string).</param>
	/// <param name="autoClear">If set to <c>true</c>, clear the recorder after gif saved.</param>
	public void StartRecord(Camera camera, string recorderName,
		Action<float> onRecordProgress = null, Action onRecordDurationMax = null, 
		Action onPreProcessingDone = null, Action<int, float> onFileSaveProgress = null, Action<int, string> onFileSaved = null, bool autoClear = true)
	{
		if(camera.GetComponent<ProGifRecorderComponent>() != null)
		{
			Debug.LogWarning("The target camera already has a recorder attached!");
			return;
		}

		ProGifRecorder newGifRecorder = new ProGifRecorder(camera);

		//Add the new recorder to dictionary
		if(m_GifRecorderDict.ContainsKey(recorderName))
		{
			m_GifRecorderDict[recorderName] = newGifRecorder;
		}
		else
		{
			m_GifRecorderDict.Add(recorderName, newGifRecorder);
		}

		if(m_AspectRatio.x > 0 && m_AspectRatio.y > 0)
		{
			newGifRecorder.Setup(
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
			newGifRecorder.Setup(
				m_AutoAspect, 	//autoAspect
				m_Width,  		//width
				m_Height,  		//height
				m_Fps,   		//fps
				m_Duration, 	//recorder time
				m_Loop,    		//repeat, -1: no repeat, 0: infinite, >0: repeat count
				m_Quality);  	//quality (1 - 100), 1: best, 100: faster
		}

		//Set the gif transparent color
		newGifRecorder.SetTransparent(m_TransparentColor);

		//Enable/Disable auto detect the image transparent setting for enabling transparent
		newGifRecorder.SetTransparent(m_AutoTransparent);

		//Set the gif rotation
		newGifRecorder.SetGifRotation(m_Rotation);

		//Start the recording with a callback that will be called when max. frames are stored in recorder
		newGifRecorder.Record(onRecordDurationMax);

		//Set the callback to update the record progress during recording.
		newGifRecorder.SetOnRecordAction(onRecordProgress);

		//Set the callback to be called when pre-processing complete
		newGifRecorder.OnPreProcessingDone += onPreProcessingDone;

		//Set the callback to update the gif save progress
		newGifRecorder.OnFileSaveProgress += onFileSaveProgress;

		//Set the callback to be called when gif file saved
		newGifRecorder.OnFileSaved += onFileSaved;

		//Set the callback to clear the recorder after gif saved
		if(autoClear)
		{
			Action<int, string> clearRecorder =(id, path)=>{
				if(newGifRecorder != null)
				{
					newGifRecorder.Clear();
					newGifRecorder = null;
				}
			};
			newGifRecorder.OnFileSaved += clearRecorder;
		}
	}

	public ProGifRecorder GetRecorder(string recorderName)
	{
		ProGifRecorder recorder = null;
		if(!m_GifRecorderDict.TryGetValue(recorderName, out recorder))
		{
			Debug.LogWarning("GetRecorder - Recorder not found: " + recorderName);
		}
		return recorder;
	}

	public void PauseRecord(string recorderName)
	{
		ProGifRecorder recorder = null;
		if(m_GifRecorderDict.TryGetValue(recorderName, out recorder))
		{
			recorder.Pause();
		}
		else
		{
			Debug.LogWarning("PauseRecord - Recorder not found: " + recorderName);
		}
	}

	public void ResumeRecord(string recorderName)
	{
		ProGifRecorder recorder = null;
		if(m_GifRecorderDict.TryGetValue(recorderName, out recorder))
		{
			recorder.Resume();
		}
		else
		{
			Debug.LogWarning("ResumeRecord - Recorder not found: " + recorderName);
		}
	}

	public void StopRecord(string recorderName)
	{
		ProGifRecorder recorder = null;
		if(m_GifRecorderDict.TryGetValue(recorderName, out recorder))
		{
			recorder.Stop();
		}
		else
		{
			Debug.LogWarning("StopRecord - Recorder not found: " + recorderName);
		}
	}

	public void SaveRecord(string recorderName, string fileNameWithoutExtension = "")
	{
		ProGifRecorder recorder = null;
		if(m_GifRecorderDict.TryGetValue(recorderName, out recorder))
		{
			recorder.Save(fileNameWithoutExtension);
		}
		else
		{
			Debug.LogWarning("SaveRecord - Recorder not found: " + recorderName);
		}
	}

	public void StopAndSaveRecord(string recorderName, string fileNameWithoutExtension = "")
	{
		ProGifRecorder recorder = null;
		if(m_GifRecorderDict.TryGetValue(recorderName, out recorder))
		{
			recorder.Stop();
			recorder.Save(fileNameWithoutExtension);
		}
		else
		{
			Debug.LogWarning("StopAndSaveRecord - Recorder not found: " + recorderName);
		}
	}

	public void ClearRecorder(string recorderName)
	{
		ProGifRecorder recorder = null;
		if(m_GifRecorderDict.TryGetValue(recorderName, out recorder))
		{
			recorder.Clear();
			recorder = null;
		}
		else
		{
			Debug.LogWarning("ClearRecorder - Recorder not found: " + recorderName);
		}
	}
	#endregion


	#region ----- Players -----

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

	private ProGifPlayer _SetupPlayer(GameObject targetPlayerObject, string playerName)
	{
		if(targetPlayerObject.GetComponent<ProGifPlayerComponent>() != null)
		{
			//If the target image already has a player attached, clear it before play
			targetPlayerObject.GetComponent<ProGifPlayerComponent>().Clear();
		}

		ProGifPlayer newGifPlayer = new ProGifPlayer();

		//Add the new player to dictionary
		if(m_GifPlayerDict.ContainsKey(playerName))
		{
			m_GifPlayerDict[playerName] = newGifPlayer;
		}
		else
		{
			m_GifPlayerDict.Add(playerName, newGifPlayer);
		}
		return newGifPlayer;
	}

	/// <summary>
	/// Play gif from Recorder, display with Image.
	/// </summary>
	/// <param name="recorderSource">The recorder which the gif frames are stored.</param>
	/// <param name="playerImage">Target image for displaying gif.</param>
	/// <param name="playerName">The Name for identifying players in the dictionary.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public void PlayGif(ProGifRecorder recorderSource, UnityEngine.UI.Image playerImage, string playerName, Action<float> onLoading = null)
	{
		if(recorderSource == null)
		{
			Debug.Log("GIF recorder not found!");
			return;
		}

		ProGifPlayer newGifPlayer = _SetupPlayer(playerImage.gameObject, playerName);
		newGifPlayer.Play(recorderSource, playerImage, m_OptimizeMemoryUsage);
		newGifPlayer.SetLoadingCallback((progress)=>{
			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

	/// <summary>
	/// Play gif from Recorder, display with Renderer.
	/// </summary>
	/// <param name="recorderSource">The recorder which the gif frames are stored.</param>
	/// <param name="playerRenderer">Target renderer for displaying gif.</param>
	/// <param name="playerName">The Name for identifying players in the dictionary.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public void PlayGif(ProGifRecorder recorderSource, Renderer playerRenderer, string playerName, Action<float> onLoading = null)
	{
		if(recorderSource == null)
		{
			Debug.Log("GIF recorder not found!");
			return;
		}

		ProGifPlayer newGifPlayer = _SetupPlayer(playerRenderer.gameObject, playerName);
		newGifPlayer.Play(recorderSource, playerRenderer, m_OptimizeMemoryUsage);
		newGifPlayer.SetLoadingCallback((progress)=>{
			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

	/// <summary>
	/// Play gif from Recorder, display with GUITexture.
	/// </summary>
	/// <param name="recorderSource">The recorder which the gif frames are stored.</param>
	/// <param name="playerGuiTexture">Target GUITexture for displaying gif.</param>
	/// <param name="playerName">The Name for identifying players in the dictionary.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public void PlayGif(ProGifRecorder recorderSource, GUITexture playerGuiTexture, string playerName, Action<float> onLoading = null)
	{
		if(recorderSource == null)
		{
			Debug.Log("GIF recorder not found!");
			return;
		}

		ProGifPlayer newGifPlayer = _SetupPlayer(playerGuiTexture.gameObject, playerName);
		newGifPlayer.Play(recorderSource, playerGuiTexture, m_OptimizeMemoryUsage);
		newGifPlayer.SetLoadingCallback((progress)=>{
			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

    /// <summary>
    /// Play gif from Recorder, display with RawImage.
    /// </summary>
    /// <param name="recorderSource">The recorder which the gif frames are stored.</param>
    /// <param name="playerRawImage">Target RawImage for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    public void PlayGif(ProGifRecorder recorderSource, RawImage playerRawImage, string playerName, Action<float> onLoading = null)
    {
        if(recorderSource == null)
        {
            Debug.Log("GIF recorder not found!");
            return;
        }

        ProGifPlayer newGifPlayer = _SetupPlayer(playerRawImage.gameObject, playerName);
		newGifPlayer.Play(recorderSource, playerRawImage, m_OptimizeMemoryUsage);
        newGifPlayer.SetLoadingCallback((progress) => {
            if(onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with Image.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerImage">Target image for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
	public void PlayGif(string gifPath, UnityEngine.UI.Image playerImage, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        if(string.IsNullOrEmpty(gifPath))
        {
            Debug.LogWarning("Gif path is null or empty!");
            return;
        }

        ProGifPlayer newGifPlayer = _SetupPlayer(playerImage.gameObject, playerName);
		newGifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
        newGifPlayer.Play(gifPath, playerImage, shouldSaveFromWeb);
        newGifPlayer.SetLoadingCallback((progress) => {
            //Check progress
            if(progress >= 1f)
            {
                newGifPlayer.SetLoadingCallback(null);
            }

            if(onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with Renderer.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerRenderer">Target renderer for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public void PlayGif(string gifPath, Renderer playerRenderer, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        if(string.IsNullOrEmpty(gifPath))
        {
            Debug.LogWarning("Gif path is null or empty!");
            return;
        }

        ProGifPlayer newGifPlayer = _SetupPlayer(playerRenderer.gameObject, playerName);
		newGifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
        newGifPlayer.Play(gifPath, playerRenderer, shouldSaveFromWeb);
        newGifPlayer.SetLoadingCallback((progress) => {
            //Check progress
            if(progress >= 1f)
            {
                newGifPlayer.SetLoadingCallback(null);
            }

            if(onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with GUITexture.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerGuiTexture">Target GUITexture for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public void PlayGif(string gifPath, GUITexture playerGuiTexture, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
	{
		if(string.IsNullOrEmpty(gifPath))
		{
			Debug.LogWarning("Gif path is null or empty!");
			return;
		}

		ProGifPlayer newGifPlayer = _SetupPlayer(playerGuiTexture.gameObject, playerName);
		newGifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
		newGifPlayer.Play(gifPath, playerGuiTexture, shouldSaveFromWeb);
		newGifPlayer.SetLoadingCallback((progress)=>{
			//Check progress
			if(progress >= 1f)
			{
				newGifPlayer.SetLoadingCallback(null);
			}

			if(onLoading != null)
			{
				onLoading(progress);
			}
		});
	}

    /// <summary>
    /// Load GIF from path/url for playback, display with RawImage.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerRawImage">Target RawImage for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public void PlayGif(string gifPath, RawImage playerRawImage, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        if(string.IsNullOrEmpty(gifPath))
        {
            Debug.LogWarning("Gif path is null or empty!");
            return;
        }

        ProGifPlayer newGifPlayer = _SetupPlayer(playerRawImage.gameObject, playerName);
		newGifPlayer.SetDecodeSettings(m_Decoder, m_DecodeMode, m_TargetDecodeFrameNum, m_FramePickingMethod, m_OptimizeMemoryUsage);
        newGifPlayer.Play(gifPath, playerRawImage, shouldSaveFromWeb);
        newGifPlayer.SetLoadingCallback((progress) => {
            //Check progress
            if(progress >= 1f)
            {
                newGifPlayer.SetLoadingCallback(null);
            }

            if(onLoading != null)
            {
                onLoading(progress);
            }
        });
    }

    /// <summary>
	/// Set the callback for checking the decode progress. 
	/// If using a recorder source for playback, this becomes a loading-complete callback.
    /// </summary>
    /// <param name="onLoading">On loading callback, returns the decode/loading progress(float).</param>
    public void SetPlayerOnLoading(string playerName, Action<float> onLoading)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.SetLoadingCallback(onLoading);
		}
		else
		{
			Debug.LogWarning("SetPlayerOnLoading - Player not found: " + playerName);
		}
	}

	/// <summary>
	/// Set the callback to be fired when the first gif frame ready.
	/// If using a recorder source for playback, this becomes a loading-complete callback with the first GIF frame returned.
	/// </summary>
	/// <param name="onFirstFrame">On first frame callback, returns the first gifTexture and related data.</param>
	public void SetPlayerOnFirstFrame(string playerName, Action<ProGifPlayerComponent.FirstGifFrame> onFirstFrame)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.SetOnFirstFrameCallback(onFirstFrame);
		}
		else
		{
			Debug.LogWarning("SetPlayerOnFirstFrame - Player not found: " + playerName);
		}
	}

	/// <summary>
	/// Set the callback to be fired on every frame when playing gif.
	/// </summary>
	/// <param name="onPlaying">On gif playing callback, returns the current gifTexture.</param>
	public void SetPlayerOnPlaying(string playerName, Action<GifTexture> onPlaying)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.SetOnPlayingCallback(onPlaying);
		}
		else
		{
			Debug.LogWarning("SetPlayerOnPlaying - Player not found: " + playerName);
		}
	}

	/// <summary>
	/// Set the callback to be fired when all frames decode complete (for decoder only).
	/// </summary>
	/// <param name="onDecodeComplete">On decode complete callback, returns the gifTextures list and related data.</param>
	public void SetPlayerOnDecodeComplete(string playerName, Action<ProGifPlayerComponent.DecodedResult> onDecodeComplete)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.SetOnDecodeCompleteCallback(onDecodeComplete);
		}
		else
		{
			Debug.LogWarning("SetPlayerOnDecodeComplete - Player not found: " + playerName);
		}
	}

	public ProGifPlayer GetPlayer(string playerName)
	{
		ProGifPlayer player = null;
		if(!m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			Debug.LogWarning("GetPlayer - Player not found: " + playerName);
		}
		return player;
	}

	public void PausePlayer(string playerName)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.Pause();
		}
		else
		{
			Debug.LogWarning("PausePlayer - Player not found: " + playerName);
		}
	}

	public void ResumePlayer(string playerName)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.Resume();
		}
		else
		{
			Debug.LogWarning("ResumePlayer - Player not found: " + playerName);
		}
	}

	public void StopPlayer(string playerName)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.Stop();
		}
		else
		{
			Debug.LogWarning("StopPlayer - Player not found: " + playerName);
		}
	}

	public void ClearPlayer(string playerName)
	{
		ProGifPlayer player = null;
		if(m_GifPlayerDict.TryGetValue(playerName, out player))
		{
			player.Clear();
			player = null;
		}
		else
		{
            Debug.LogWarning("ClearPlayer - Player not found: " + playerName);
        }
	}
	#endregion


	#region ----- Static methods -----

	//================= Recorder ===================
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
	public static void iSetRecordSettings(bool autoAspect, int width, int height, float duration, int fps, int loop, int quality)
	{
		Instance.SetRecordSettings(autoAspect, width, height, duration, fps, loop, quality);
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
	public static void iSetRecordSettings(Vector2 aspectRatio, int width, int height, float duration, int fps, int loop, int quality)
	{
		Instance.SetRecordSettings(aspectRatio, width, height, duration, fps, loop, quality);
	}

	/// <summary>
	/// Sets the GIF rotation.
	/// </summary>
	/// <param name="rotation">Rotation: None, -90, 90, 180</param>
	public static void iSetGifRotation(ImageRotator.Rotation rotation)
	{
		Instance.SetGifRotation(rotation);
	}

	/// <summary>
	/// Sets the transparent color, hide this color in the GIF. 
	/// The GIF specification allows setting a color to be transparent. 
	/// *** Use case: if you want to record gameObject, character or anything else with transparent background, 
	/// please make sure the background is of solid color(no gradient), and the target object do not contain this color.
	/// (Also be reminded, the transparent feature takes more time to encoding the GIF)
	/// </summary>
	/// <param name="color32">The Color to hide.</param>
	public static void iSetTransparent(Color32 color32)
	{
		Instance.SetTransparent(color32);
	}

	/// <summary>
	/// Auto detects the input image(s) pixels for enable/disable transparent feature. 
	/// *** Use case: for pre-made images that have transparent pixels manually set.
	/// (Also be reminded, the transparent feature takes more time to encoding the GIF)
	/// </summary>
	/// <param name="autoDetectTransparent">If set to <c>true</c> auto detect transparent.</param>
	public static void iSetTransparent(bool autoDetectTransparent)
	{
		Instance.SetTransparent(autoDetectTransparent);
	}

	/// <summary>
	/// Create/Start a new recorder to store frames with specific camera. 
	/// </summary>
	/// <param name="camera">The target Camera to attach the newly create gif recroder.</param>
	/// <param name="recorderName">Recorder Name for identifying recorders in the dictionary.</param>
	/// <param name="onRecordProgress">Update the record progress. Return values: record progress(float)</param>
	/// <param name="onRecordDurationMax">To be fired when target duration frames reached.</param>
	/// <param name="onPreProcessingDone">On pre processing done.</param>
	/// <param name="onFileSaveProgress">On file save progress. Retrun values: worker id(int), save progress(float).</param>
	/// <param name="onFileSaved">On file saved. Return values: id(int), saved path(string).</param>
	/// <param name="autoClear">If set to <c>true</c> Clear the recorder when gif saved?</param>
	public static void iStartRecord(Camera camera, string recorderName,
		Action<float> onRecordProgress = null, Action onRecordDurationMax = null, 
		Action onPreProcessingDone = null, Action<int, float> onFileSaveProgress = null, Action<int, string> onFileSaved = null, bool autoClear = true)
	{
		Instance.StartRecord(camera, recorderName, onRecordProgress, onRecordDurationMax, onPreProcessingDone, onFileSaveProgress, onFileSaved, autoClear);
	}

	public static ProGifRecorder iGetRecorder(string recorderName)
	{
		return Instance.GetRecorder(recorderName);
	}

	public static void iPauseRecord(string recorderName)
	{
		Instance.PauseRecord(recorderName);
	}

	public static void iResumeRecord(string recorderName)
	{
		Instance.ResumeRecord(recorderName);
	}

	public static void iStopRecord(string recorderName)
	{
		Instance.StopRecord(recorderName);
	}

	public static void iSaveRecord(string recorderName, string fileNameWithoutExtension = "")
	{
		Instance.SaveRecord(recorderName, fileNameWithoutExtension);
	}

	public static void iStopAndSaveRecord(string recorderName, string fileNameWithoutExtension = "")
	{
		Instance.StopAndSaveRecord(recorderName, fileNameWithoutExtension);
	}

	public static void iClearRecorder(string recorderName)
	{
		Instance.ClearRecorder(recorderName);
	}

	//================= Player ===================

	/// <summary> Resets the decode settings(Set the decodeMode as Normal, simply decodes the entire gif without applying advanced settings) </summary>
	public static void iResetPlayerDecodeSettings()
	{
		Instance.ResetPlayerDecodeSettings();
	}

	/// <summary> Sets the decodeMode as Advanced, apply the advanced settings(targetDecodeFrameNum, framePickingMethod..) </summary>
	public static void iSetAdvancedPlayerDecodeSettings(ProGifPlayerComponent.Decoder decoder, int targetDecodeFrameNum = -1, 
		ProGifPlayerComponent.FramePickingMethod framePickingMethod = ProGifPlayerComponent.FramePickingMethod.ContinuousFromBeginning, bool optimizeMemoryUsage = true)
	{
		Instance.SetAdvancedPlayerDecodeSettings(decoder, targetDecodeFrameNum, framePickingMethod, optimizeMemoryUsage);
	}

	/// <summary>
	/// Play gif from Recorder, display with Image.
	/// </summary>
	/// <param name="recorderSource">The recorder which the gif frames are stored.</param>
	/// <param name="playerImage">Target image for displaying gif.</param>
	/// <param name="playerName">The Name for identifying players in the dictionary.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public static void iPlayGif(ProGifRecorder recorderSource, UnityEngine.UI.Image playerImage, string playerName, Action<float> onLoading = null)
	{
		Instance.PlayGif(recorderSource, playerImage, playerName, onLoading);
	}

	/// <summary>
	/// Play gif from Recorder, display with Renderer.
	/// </summary>
	/// <param name="recorderSource">The recorder which the gif frames are stored.</param>
	/// <param name="playerRenderer">Target renderer for displaying gif.</param>
	/// <param name="playerName">The Name for identifying players in the dictionary.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public static void iPlayGif(ProGifRecorder recorderSource, Renderer playerRenderer, string playerName, Action<float> onLoading = null)
	{
		Instance.PlayGif(recorderSource, playerRenderer, playerName, onLoading);
	}

	/// <summary>
	/// Play gif from Recorder, display with GUITexture.
	/// </summary>
	/// <param name="recorderSource">The recorder which the gif frames are stored.</param>
	/// <param name="playerGuiTexture">Target GUITexture for displaying gif.</param>
	/// <param name="playerName">The Name for identifying players in the dictionary.</param>
	/// <param name="onLoading">On loading. Return value: loading progress(float)</param>
	public static void iPlayGif(ProGifRecorder recorderSource, GUITexture playerGuiTexture, string playerName, Action<float> onLoading = null)
	{
		Instance.PlayGif(recorderSource, playerGuiTexture, playerName, onLoading);
	}

    /// <summary>
    /// Play gif from Recorder, display with RawImage.
    /// </summary>
    /// <param name="recorderSource">The recorder which the gif frames are stored.</param>
    /// <param name="playerRawImage">Target RawImage for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    public static void iPlayGif(ProGifRecorder recorderSource, RawImage playerRawImage, string playerName, Action<float> onLoading = null)
    {
        Instance.PlayGif(recorderSource, playerRawImage, playerName, onLoading);
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with Image.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerImage">Target image for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
	public static void iPlayGif(string gifPath, UnityEngine.UI.Image playerImage, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        Instance.PlayGif(gifPath, playerImage, playerName, onLoading, shouldSaveFromWeb);
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with Renderer.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerRenderer">Target renderer for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public static void iPlayGif(string gifPath, Renderer playerRenderer, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        Instance.PlayGif(gifPath, playerRenderer, playerName, onLoading, shouldSaveFromWeb);
    }

    /// <summary>
    /// Load GIF from path/url for playback, display with GUITexture.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerGuiTexture">Target GUITexture for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public static void iPlayGif(string gifPath, GUITexture playerGuiTexture, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
	{
		Instance.PlayGif(gifPath, playerGuiTexture, playerName, onLoading, shouldSaveFromWeb);
	}

    /// <summary>
    /// Load GIF from path/url for playback, display with RawImage.
    /// The memory usage could be quite large for high-resolution gifs or large frame count gifs. 
    /// If you need to play multiple gifs at a time(e.g.from Giphy API), suggest use the preview(low-resolution) version of the gifs.
    /// </summary>
    /// <param name="gifPath">GIF path or url.</param>
    /// <param name="playerRawImage">Target RawImage for displaying gif.</param>
    /// <param name="playerName">The Name for identifying players in the dictionary.</param>
    /// <param name="onLoading">On loading. Return value: loading progress(float)</param>
    /// <param name="shouldSaveFromWeb">The flag indicating to save or not to save file that download from web.</param>
    public static void iPlayGif(string gifPath, RawImage playerRawImage, string playerName, Action<float> onLoading = null, bool shouldSaveFromWeb = false)
    {
        Instance.PlayGif(gifPath, playerRawImage, playerName, onLoading, shouldSaveFromWeb);
    }

    public static ProGifPlayer iGetPlayer(string playerName)
	{
		return Instance.GetPlayer(playerName);
	}

	public static void iPausePlayer(string playerName)
	{
		Instance.PausePlayer(playerName);
	}

	public static void iResumePlayer(string playerName)
	{
		Instance.ResumePlayer(playerName);
	}

	public static void iStopPlayer(string playerName)
	{
		Instance.StopPlayer(playerName);
	}

	public static void iClearPlayer(string playerName)
	{
		Instance.ClearPlayer(playerName);
	}

	//================= Others ===================
	/// <summary>
	/// Decode the first gif frame to get detailed info.(first frame texture, width, height, fps, total frame count, interval)
	/// </summary>
	/// <param name="gifPath">GIF path.</param>
	/// <param name="onComplete">On complete callback, return the first frame info</param>
	public static void iGetGifInfo(string gifPath, Action<ProGifPlayerComponent.FirstGifFrame> onComplete, ProGifPlayerComponent.Decoder decoder = ProGifPlayerComponent.Decoder.ProGif_QueuedThread)
	{
		ProGifInfo gifInfo = (new GameObject("[GifInfo]")).AddComponent<ProGifInfo>();
		gifInfo.GetInfo(gifPath, onComplete, decoder);
	}

	#endregion
}
