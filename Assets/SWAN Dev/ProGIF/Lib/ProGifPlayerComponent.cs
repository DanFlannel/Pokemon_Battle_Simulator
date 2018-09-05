using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using ThreadPriority = System.Threading.ThreadPriority;

[DisallowMultipleComponent]
public abstract class ProGifPlayerComponent : MonoBehaviour
{
	public string loadPath;

	[HideInInspector] public List<GifTexture> gifTextures = new List<GifTexture>();

	[HideInInspector] public int totalFrame = 0;

	[HideInInspector] public DisplayType displayType = DisplayType.None;	// Indicates the display target is an Image, Renderer, or GUITexture

	[HideInInspector] public float nextFrameTime = 0.0f;					// The game time to show next frame
	[HideInInspector] public int spriteIndex = 0;							// The current sprite index to be played

	/// Textures filter mode
	[SerializeField]
	private FilterMode m_filterMode = FilterMode.Point;
	/// Textures wrap mode
	[SerializeField]
	private TextureWrapMode m_wrapMode = TextureWrapMode.Clamp;

	/// Indicates the gif frames is loaded from recorder or decoder
	private bool isDecoderSource = false;

	/// <summary> Sets the worker threads priority. This will only affect newly created threads (on save). </summary>
	public ThreadPriority m_workerPriority = ThreadPriority.BelowNormal;

	/// <summary> Gets the progress when load Gif from path/url. </summary>
	public float LoadingProgress
	{
		get{
			return (float)gifTextures.Count/(float)totalFrame;
		}
	}

	/// <summary> This component state enum </summary>
	public enum PlayerState
	{
		None,
		Loading,
		Ready,
		Playing,
		Pause,
	}

	/// <summary> Current state </summary>
	public PlayerState State
	{
		get;
		private set;
	}
	public void SetState(PlayerState state)
	{
		State = state;
	}

	/// <summary> Animation loop count (0 is infinite) </summary>
	public int loopCount
	{
		get;
		private set;
	}

	/// <summary> Texture width (px) </summary>
	public int width
	{
		get;
		private set;
	}

	/// <summary> Texture height (px) </summary>
	public int height
	{
		get;
		private set;
	}

	private float _interval = 0.0f; 										// Waiting time among frames
	public float interval
	{
		get
		{
			// get the current frame waiting time
			return (gifTextures[spriteIndex].m_delaySec <= 0f)? _interval:gifTextures[spriteIndex].m_delaySec;
		}
		set
		{
			_interval = value;
		}
	}

	[HideInInspector] public bool shouldSaveFromWeb = false; 				// True: save file download from web

	public enum DisplayType
	{
		None = 0,
		Image,
		Renderer,
		GuiTexture,
        RawImage,
	}

	//Decode settings
	public enum DecodeMode
	{
		/// <summary> Decode all gif frames. </summary>
		Normal = 0,

		/// <summary> Decode gif by skipping some frames, targetDecodeFrameNum is the number of frames to decode. </summary>
		Advanced,
	}
	public enum FramePickingMethod
	{
		/// <summary> Decode gif frame by frame from first to the target number(targetDecodeFrameNum). </summary>
		ContinuousFromBeginning = 0,

		/// <summary> Decode a target amount(targetDecodeFrameNum) of gif frames(skip frames by an averaged interval). </summary>
		AverageInterval,

		/// <summary> Decode the first half of the gif frames(not more than targetDecodeFrameNum if provided targetDecodeFrameNum larger than 0). </summary>
		OneHalf,

		/// <summary> Decode the first one-third of the gif frames(not more than targetDecodeFrameNum if provided targetDecodeFrameNum larger than 0). </summary>
		OneThird,

		/// <summary> Decode the first one-fourth of the gif frames(not more than targetDecodeFrameNum if provided targetDecodeFrameNum larger than 0). </summary>
		OneFourth
	}
	public enum Decoder
	{
        ProGif_QueuedThread = 0,
        ProGif_Coroutines,
	}

	protected ProGifDecoder proGifDecoder;

    //Advanced settings ------------------
	/// If 'True', use the settings on the prefab, this will ignore changes from PGif/ProGifManager.
    [Header("[ Advanced Decode Settings ]")]
    [Tooltip("If 'True', use the settings on the prefab, this will ignore changes from PGif/ProGifManager.")]
	public bool UsePresetSettings = false;

	public Decoder decoder = Decoder.ProGif_QueuedThread;

	public DecodeMode decodeMode = DecodeMode.Normal;

	public FramePickingMethod framePickingMethod = FramePickingMethod.ContinuousFromBeginning;

	public int targetDecodeFrameNum = -1;	//if targetDecodeFrameNum <= 0: decode & play all frames (+/- 1 frame)

	/// Set to 'true' to take advantage of the highly optimized ProGif playback solution for significantly save the memory usage.
	public bool optimizeMemoryUsage = true;
	//Advanced settings ------------------

	/// <summary> Resets the decode settings(Set the decodeMode as Normal, simply decodes the entire gif without applying advanced settings) </summary>
	public void ResetDecodeSettings()
	{
		if(UsePresetSettings)
		{
			#if UNITY_EDITOR
			Debug.Log("UsePresetSettings is selected, the decoder will use the settings on the prefab and ignore changes from PGif/ProGifManager.");
			#endif
			return;
		}
		decoder = Decoder.ProGif_QueuedThread;
		decodeMode = ProGifPlayerComponent.DecodeMode.Normal;
		framePickingMethod = ProGifPlayerComponent.FramePickingMethod.ContinuousFromBeginning;
		targetDecodeFrameNum = -1;
		optimizeMemoryUsage = true;
	}

	/// <summary> Sets the decodeMode as Advanced, apply the advanced settings(targetDecodeFrameNum, framePickingMethod..) </summary>
	public void SetAdvancedDecodeSettings(Decoder decoder, int targetDecodeFrameNum = -1, FramePickingMethod framePickingMethod = FramePickingMethod.ContinuousFromBeginning, bool optimizeMemoryUsage = true)
	{
		if(UsePresetSettings)
		{
			#if UNITY_EDITOR
			Debug.Log("UsePresetSettings is selected, the decoder will use the settings on the prefab and ignore changes from PGif/ProGifManager.");
			#endif
			return;
		}
		this.decoder = decoder;
		this.decodeMode = DecodeMode.Advanced;
		this.framePickingMethod = framePickingMethod;
		this.targetDecodeFrameNum = targetDecodeFrameNum;
		this.optimizeMemoryUsage = optimizeMemoryUsage;
	}

	void OnEnable()
	{
		if(string.IsNullOrEmpty(this.loadPath) == false)
		{
			Play(this.loadPath, false);
		}
	}

	public void Play(string loadPath, bool shouldSaveFromWeb)
	{
		this.shouldSaveFromWeb = shouldSaveFromWeb;
		Clear();
		gifTextures = new List<GifTexture>();
		LoadGifFromUrl(loadPath);
		this.loadPath = loadPath;
	}

	/// Setup to play the stored textures from gif recorder.
	public virtual void Play(RenderTexture[] gifFrames, int fps, bool isCustomRatio, int customWidth, int customHeight, bool optimizeMemoryUsage)
	{
		//RenderTexture[] gifFrames = recorder.Frames;
		gifTextures = new List<GifTexture>();

		//int fps = recorder.FPS;
		this.optimizeMemoryUsage = optimizeMemoryUsage;
		isDecoderSource = false;

		interval = 1.0f / fps;

		Clear();

		if(isCustomRatio)
		{
			width = customWidth;
			height = customHeight;

			for(int i = 0; i < gifFrames.Length; i++)
			{
				Texture2D tex = new Texture2D(width, height);
				RenderTexture.active = gifFrames[i];
				tex.ReadPixels(new Rect((gifFrames[i].width - tex.width) / 2, (gifFrames[i].height - tex.height) / 2, tex.width, tex.height), 0, 0);
                tex.Apply();
				gifTextures.Add(new GifTexture(tex, _interval, optimizeMemoryUsage));
			}
		}
		else
		{
			width = gifFrames[0].width;
			height = gifFrames[0].height;

			for(int i = 0; i < gifFrames.Length; i++)
			{
				Texture2D tex = new Texture2D(gifFrames[i].width, gifFrames[i].height);
				RenderTexture.active = gifFrames[i];
				tex.ReadPixels(new Rect(0.0f, 0.0f, gifFrames[i].width, gifFrames[i].height), 0, 0);
                tex.Apply();
				gifTextures.Add(new GifTexture(tex, _interval, optimizeMemoryUsage));
			}
		}

		totalFrame = gifTextures.Count;

		//Ensure the sprite is updated, call onLoading at next frame
		StartCoroutine(_DelayCallOnloading());

		State = PlayerState.Playing;
	}

    IEnumerator _DelayCallOnloading()
	{
		yield return new WaitForEndOfFrame();
		if(gifTextures != null && gifTextures.Count > 0) _OnFirstFrameReady(gifTextures[0]);
		if(_OnLoading != null) _OnLoading(LoadingProgress);
	}

	public void Pause()
	{
		State = PlayerState.Pause;
	}

	public void Resume()
	{
		State = PlayerState.Playing;
	}

	public void Stop()
	{
		State = PlayerState.Pause;
		spriteIndex = 0;
	}

	/// <summary>
	/// Set GIF texture from url
	/// </summary>
	/// <param name="url">GIF image url (Web link or local path)</param>
	public void LoadGifFromUrl(string url)
	{
		StartCoroutine(_LoadGifFromUrl(url));
	}

    //private static ProGifDeWorker _proGifDeWorker = null;

    /// <summary>
    /// Set GIF texture from url
    /// </summary>
    /// <param name="url">GIF image url (Web link or local path)</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator _LoadGifFromUrl(string url)
	{
        if(string.IsNullOrEmpty(url))
		{
			Debug.LogError("URL is nothing.");
			yield break;
		}

		if(State == PlayerState.Loading)
		{
			Debug.LogWarning("Already loading.");
			yield break;
		}
		State = PlayerState.Loading;

		FilePathName filePathName = new FilePathName();

		bool isFromWeb = false;
		string path = url;
		if(path.StartsWith("http"))
		{
			// from WEB
			isFromWeb = true;
		}
		else
		{
			// from Local
			path = filePathName.EnsureLocalPath(path);

			#if UNITY_EDITOR
			Debug.Log("Local file path: " + path);
			#endif
		}

		// Load file
		using(WWW www = new WWW(path))
		{
			yield return www;

			if(string.IsNullOrEmpty(www.error) == false)
			{
				Debug.LogError("File load error.\n" + www.error);
				State = PlayerState.None;
				yield break;
			}

			State = PlayerState.Loading;

			//Save bytes to gif file if it is downloaded from web
			if(isFromWeb && shouldSaveFromWeb)
			{
				filePathName.FileStreamTo(filePathName.GetDownloadedGifSaveFullPath(), www.bytes);
			}

			isFirstFrame = true;

            #if UNITY_EDITOR
			Debug.Log(((decoder == Decoder.ProGif_QueuedThread)? "Decode process run in Threads: ":"Decode process run in Coroutines: ") + this.gameObject.name);
			startDecodeTime = Time.time;
			#endif

			isDecoderSource = true;

			if(decoder == Decoder.ProGif_QueuedThread)
			{
				currentDecodeIndex = 0;
				decodeCompletedFlag = false;

                if(proGifDecoder != null)
                {
                    ProGifDeWorker.GetInstance().DeQueueDecoder(proGifDecoder);
                }

				proGifDecoder = new ProGifDecoder(www.bytes,
					(gifTexList, loopCount, width, height) => {
						if(gifTexList != null)
						{
							this.loopCount = loopCount;
							this.width = width;
							this.height = height;
							decodeCompletedFlag = true;
						}
						else
						{
							State = PlayerState.None;
						}
					}, 
					m_filterMode, m_wrapMode, false, 
					(gTex) => {
						_AddGifTexture(gTex);
					}, 
					(frameCount)=>{
						totalFrame = frameCount;
					}
				);

                if (decodeMode == DecodeMode.Normal) proGifDecoder.ResetDecodeSettings();
                else proGifDecoder.SetAdvancedDecodeSettings(targetDecodeFrameNum, framePickingMethod);

                ProGifDeWorker.GetInstance(m_workerPriority, isBackgroundThread:true).QueueDecoder(proGifDecoder);
				ProGifDeWorker.GetInstance().Start();
			}
			else
			{
				proGifDecoder = new ProGifDecoder(www.bytes,
					(gifTexList, loopCount, width, height) => {
						if(gifTexList != null)
						{
							#if UNITY_EDITOR
							Debug.Log(gameObject.name + " - Total Decode Time: " + (Time.time - startDecodeTime));
							#endif
							this.loopCount = loopCount;
							this.width = width;
							this.height = height;

							//clear un-use gifTextures
							//_ClearGifTexture2Ds(gifTexList);

							_OnComplete();
						}
						else
						{
							Debug.LogError("Gif texture get error.");
							State = PlayerState.None;
						}
					},
					m_filterMode, m_wrapMode, false,
					(gTex) => {
						_AddGifTexture(gTex);
						if(isFirstFrame) _OnFirstFrameReady(gTex);
						_OnFrameReady(gTex, isFirstFrame);
						if(_OnLoading != null) _OnLoading(LoadingProgress);

						isFirstFrame = false;
					},
					(frameCount)=>{
						totalFrame = frameCount;
					}
				);
                
                if (decodeMode == DecodeMode.Normal) proGifDecoder.ResetDecodeSettings();
                else proGifDecoder.SetAdvancedDecodeSettings(targetDecodeFrameNum, framePickingMethod);

                yield return StartCoroutine(proGifDecoder.GetTextureListCoroutine());
			}
		}
	}

	float startDecodeTime = 0f;
	int currentDecodeIndex = 0;
	bool isFirstFrame = true;
	/// Update for decoder using thread.
	protected void ThreadsUpdate()
	{
		if(!isDecoderSource || decoder != Decoder.ProGif_QueuedThread) return;

		if(isFirstFrame && gifTextures.Count > 0) 
		{
			isFirstFrame = false;
			_OnFirstFrameReady(gifTextures[0]);
		}

		if(currentDecodeIndex < gifTextures.Count)
		{
			currentDecodeIndex++;
			_OnFrameReady(gifTextures[currentDecodeIndex-1], (currentDecodeIndex-1 == 0));
		}

		if(_OnLoading != null && gifTextures.Count > 0) _OnLoading(LoadingProgress);

		if(decodeCompletedFlag)
		{
            #if UNITY_EDITOR
            Debug.Log(gameObject.name + " - Total Decode Time: " + (Time.time - startDecodeTime));
			#endif
			decodeCompletedFlag = false;
			//_ClearGifTexture2Ds(gifTextures);
			_OnComplete();
		}
	}
		
	// Optional to override
	protected virtual void _AddGifTexture(GifTexture gTex)
	{
		gifTextures.Add(gTex);
	}

	/// <summary>
	/// This is called on every gif frame decode finish
	/// </summary>
	/// <param name="gTex">GifTexture.</param>
	protected abstract void _OnFrameReady(GifTexture gTex, bool isFirstFrame);

	public void _OnFirstFrameReady(GifTexture gifTex)
	{
		interval = gifTex.m_delaySec;
		width = gifTex.m_Width;
		height = gifTex.m_Height;
		if(_OnFirstFrame != null)
		{
			_OnFirstFrame(new FirstGifFrame(){
				gifTexture = gifTex,
				width = this.width,
				height = this.height,
				interval = this.interval,
				totalFrame = this.totalFrame,
			});
		}
		State = PlayerState.Playing;
	}

	private void _OnComplete()
	{
		if(_OnDecodeComplete != null)
		{
			_OnDecodeComplete(new DecodedResult(){
				gifTextures = this.gifTextures,
				loopCount = this.loopCount,
				width = this.width,
				height = this.height,
				interval = this.interval,
				totalFrame = this.totalFrame,
			});
		}
	}

	private Action<FirstGifFrame> _OnFirstFrame = null;
	public void SetOnFirstFrameCallback(Action<FirstGifFrame> onFirstFrame)
	{
		_OnFirstFrame = onFirstFrame;
	}

	public class FirstGifFrame
	{
		public GifTexture gifTexture;
		public int width;
		public int height;
		public float interval;
		public int totalFrame;

		public int fps
		{
			get{
				return (int)(1f/interval);
			}
		}
	}

	protected Action<float> _OnLoading = null;
	public void SetLoadingCallback(Action<float> onLoading)
	{
		_OnLoading = onLoading;
	}
		
	private Action<DecodedResult> _OnDecodeComplete = null;
	public void SetOnDecodeCompleteCallback(Action<DecodedResult> onDecodeComplete)
	{
		_OnDecodeComplete = onDecodeComplete;
	}

	public class DecodedResult
	{
		public List<GifTexture> gifTextures;
		public int width;
		public int height;
		public float interval;
		public int loopCount;
		public int totalFrame;

		public int fps
		{
			get{
				return (int)(1f/interval);
			}
		}

	}

	protected Action<GifTexture> OnPlayingCallback = null;
	public void SetOnPlayingCallback(Action<GifTexture> onPlayingCallback)
	{
		OnPlayingCallback = onPlayingCallback;
	}

	private bool decodeCompletedFlag = false;
	/// <summary>
	/// Clear the texture2D in the list of GifTexture
	/// </summary>
//	private void _ClearGifTexture2Ds(List<GifTexture> gifTexList)
//	{
//		if (gifTexList != null)
//		{
//			for (int i = 0; i < gifTexList.Count; i++)
//			{
//				if (gifTexList[i] != null && gifTexList[i].m_texture2d != null)
//				{
//					gifTexList[i].GetSprite();
//
//					Destroy(gifTexList[i].m_texture2d);
//					gifTexList[i].m_texture2d = null;
//				}
//			}
//		}
//	}

	/// <summary>
	/// Clear the sprite & texture2D in the list of GifTexture
	/// </summary>
	protected void _ClearGifTextures(List<GifTexture> gifTexList)
	{
		if(gifTexList != null)
		{
			for(int i=0; i<gifTexList.Count; i++)
			{
				if(gifTexList[i] != null)
				{
					gifTexList[i].m_Colors = null;

					if(gifTexList[i].m_texture2d != null)
					{
						Texture2D.Destroy(gifTexList[i].m_texture2d);
						gifTexList[i].m_texture2d = null;
					}

					if(gifTexList[i].m_Sprite != null && gifTexList[i].m_Sprite.texture != null)
					{
						Texture2D.Destroy(gifTexList[i].m_Sprite.texture);
						gifTexList[i].m_Sprite = null;
					}
				}
			}
		}
	}

	public virtual void Clear()
	{
		State = PlayerState.None;
		spriteIndex = 0;

		StopAllCoroutines();

		//Clear gifTextures in loading coroutines/threads
		if(proGifDecoder != null)
		{
            ProGifDeWorker.GetInstance().DeQueueDecoder(proGifDecoder);
		}

		//Clear gifTextures of the PlayerComponent
		_ClearGifTextures(gifTextures);

		//Clear un-referenced textures
		Resources.UnloadUnusedAssets();
	}


	//-- Resize --------
	//	private int newFps = -1;
	//	private Vector2 newSize = Vector2.zero;
	//	private bool keepRatioForNewSize = true;
	//	public void Resize_AdvancedMode(GifTexture gTex)
	//	{
	//		ImageResizer imageResizer = null;
	//		bool reSize = false;
	//		if(newSize.x > 0 && newSize.y > 0 && decodeMode == ProGifPlayerComponent.DecodeMode.Advanced) 
	//		{
	//			imageResizer = new ImageResizer();
	//			reSize = true;
	//		}
	//
	//		if(reSize) gTex.m_texture2d = (keepRatioForNewSize)?
	//				imageResizer.ResizeTexture32_KeepRatio(gTex.m_texture2d, (int)newSize.x, (int)newSize.y):
	//				imageResizer.ResizeTexture32(gTex.m_texture2d, (int)newSize.x, (int)newSize.y);
	//	}
	//-- Resize ----------------

}
