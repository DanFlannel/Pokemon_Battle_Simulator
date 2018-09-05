using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using ThreadPriority = System.Threading.ThreadPriority;

public class ProGifTexturesToGIF : MonoBehaviour
{
	public List<Texture2D> previewTextures = new List<Texture2D>();
	public ThreadPriority workerPriority = ThreadPriority.BelowNormal;

	private List<string> _fileExtensions = new List<string>{".jpg", ".png"};
	private ImageResizer _imageResizer = new ImageResizer();

	private float frameDelay_Override = 0f;

	/// <summary> The transparent color to hide in the GIF. </summary>
	[SerializeField] 
	Color32 m_TransparentColor = new Color32(0, 0, 0, 0);

	/// <summary> If 'TRUE', check if any pixels' alpha value equal zero and auto encode GIF with transpareny. </summary>
	[SerializeField] 
	bool m_AutoTransparent = false;

	public ResolutionHandle resolutionHandle = ResolutionHandle.ResizeKeepRatio;
	public enum ResolutionHandle
	{
		Resize = 0,
		ResizeKeepRatio,
	}
	
	public void SetFileName(string fileNameWithoutExtension)
	{
		_providedFileName = fileNameWithoutExtension;
	}

	private string _providedFileName = string.Empty;
	private string FileName
	{
		get{
			return (_providedFileName == string.Empty)? new FilePathName().GetGifFileName():_providedFileName;
		}
	}

	private string SaveFolder
	{
		get{
			return new FilePathName().GetSaveDirectory();
		}
	}

	private static ProGifTexturesToGIF _instance;
	public static ProGifTexturesToGIF Instance
	{
		get{
			if(_instance == null)
			{
				_instance = Create("[ProGifTexturesToGIF]");
			}
			return _instance;
		}
	}

	public static ProGifTexturesToGIF Create(string objectName)
	{
		return new GameObject(objectName).AddComponent<ProGifTexturesToGIF>();
	}

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
		}
	}

	int id = 0;
	float progress = 0.0f;
	string filePath = string.Empty;
	bool invokeFileProgress = false;
	bool invokeFileSaved = false;

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

	private void Update()
	{
		if(invokeFileProgress)
		{
			invokeFileProgress = false;
			OnFileSaveProgress(id, progress);
		}

		if(invokeFileSaved)
		{
			invokeFileSaved = false;
			OnFileSaved(id, filePath);
		}
	}

	private void FileSaved(int id, string path)
	{
		this.id = id;
		this.filePath = path;
		this.invokeFileSaved = true;
	}

	private void FileSaveProgress(int id, float progress)
	{
		this.id = id;
		this.progress = progress;
		this.invokeFileProgress = true;
	}

	/// <summary>
	/// Convert and save a List of Texture2D to GIF.
	/// </summary>
	/// <param name="textureList">Texture list.</param>
	/// <param name="fps">Frame count per second.</param>
	/// <param name="loop">Repeat time, -1: no repeat, 0: infinite, >0: repeat count.</param>
	/// <param name="quality">Quality, (1 - 100) 1: best(larger storage size), 100: faster(smaller storage size)</param>
	/// <param name="workerPriority">Worker priority.</param>
	/// <param name="onFileSaved">On file saved callback.</param>
	/// <param name="onFileSaveProgress">On file save progress callback.</param>
	/// <param name="autoClear">If set to <c>true</c> Clear when gif saved?</param>
	public string Save(List<Texture2D> textureList, int width, int height, int fps, int loop, int quality,
		Action<int, string> onFileSaved = null, Action<int, float> onFileSaveProgress = null, 
		ResolutionHandle resolutionHandle = ResolutionHandle.ResizeKeepRatio, bool autoClear = true)
	{
		this.resolutionHandle = resolutionHandle;
		return _Save(textureList, width, height, fps, loop, quality, onFileSaved, onFileSaveProgress, autoClear);
	}
		
	public string Save(List<Texture2D> textureList, int width, int height, float frameDelay, int loop, int quality,
		Action<int, string> onFileSaved = null, Action<int, float> onFileSaveProgress = null, 
		ResolutionHandle resolutionHandle = ResolutionHandle.ResizeKeepRatio, bool autoClear = true)
	{
		frameDelay_Override = frameDelay;
		this.resolutionHandle = resolutionHandle;
		return _Save(textureList, width, height, 0, loop, quality, onFileSaved, onFileSaveProgress, autoClear);
	}

	private string _Save(List<Texture2D> textureList, int width, int height, int fps, int loop, int quality,
		Action<int, string> onFileSaved = null, Action<int, float> onFileSaveProgress = null, bool autoClear = true)
	{
		this.OnFileSaveProgress = (onFileSaveProgress != null)? onFileSaveProgress:(id, progress)=>{};
		this.OnFileSaved = (onFileSaved != null)? onFileSaved:(id, path)=>{};

		if(autoClear)
		{
			Action<int, string> clearCallback =(id, path)=>{
				Clear();
			};
			this.OnFileSaved += clearCallback;
		}

		string filepath = SaveFolder + "/" + FileName + ".gif";
		List<Frame> frames = Texture2DsToFrames(textureList, width, height);

		ProGifEncoder encoder = new ProGifEncoder(loop, quality);

		if(m_AutoTransparent)
		{
			encoder.m_AutoTransparent = m_AutoTransparent;
		}
		else if(m_TransparentColor.a != 0)
		{
			encoder.SetTransparencyColor(m_TransparentColor);
		}

		if(frameDelay_Override > 0f)
		{
			encoder.SetDelay(Mathf.RoundToInt(frameDelay_Override * 1000f));
		}
		else
		{
			float timePerFrame = 1f / fps;
			encoder.SetDelay(Mathf.RoundToInt(timePerFrame * 1000f));
		}

		ProGifWorker worker = new ProGifWorker(workerPriority)
		{
			m_Encoder = encoder,
			m_Frames = frames,
			m_FilePath = filepath,
			m_OnFileSaved = FileSaved,
			m_OnFileSaveProgress = FileSaveProgress
		};
		worker.Start();

		return filepath;
	}

	private List<Frame> Texture2DsToFrames(List<Texture2D> textureList, int width, int height)
	{
		previewTextures = new List<Texture2D>();
		List<Frame> frames = new List<Frame>();
		for(int i=0; i<textureList.Count; i++)
		{
			frames.Add(Texture2DToFrame(textureList[i], width, height));
		}
		return frames;
	}

	private Frame Texture2DToFrame(Texture2D texture2d, int width, int height)
	{
		if(texture2d.width != width || texture2d.height != height)
		{
			switch(resolutionHandle)
			{
			case ResolutionHandle.Resize:
				texture2d = _imageResizer.ResizeTexture32(texture2d, width, height);
				break;
			case ResolutionHandle.ResizeKeepRatio:
				texture2d = _imageResizer.ResizeTexture32_KeepRatio(texture2d, width, height);
				break;
			}
		}
		previewTextures.Add(texture2d);

		//-- Rotate image -----
		//Using looping through width & height (Only support rotate with interval of 90 degrees)
		//Color32[] colors = ImageRotator.RotateImageToColor32(texture2d, m_Rotation);
		//==== Rotate Image ===========
		int newWidth = width;
		int newHeight = height;

		//Rotate image?
		if(m_Rotation == ImageRotator.Rotation.None)
		{
			return new Frame(){Width = newWidth, Height = newHeight, Data = texture2d.GetPixels32()};
		}
		else
		{
			switch(m_Rotation)
			{
			case ImageRotator.Rotation.Right: //90
				newWidth = height;
				newHeight = width;
				break;
			case ImageRotator.Rotation.HalfCircle: //180
				break;
			case ImageRotator.Rotation.Left: //-90
				newWidth = height;
				newHeight = width;
				break;
			}

			//Using looping through width & height (Only support rotate with interval of 90 degrees)
			Color32[] colors = ImageRotator.RotateImageToColor32(texture2d, m_Rotation);
			return new Frame(){Width = newWidth, Height = newHeight, Data = colors};
		}
		//=============================
		//-- Rotate image -----

		//return new Frame(){Width = width, Height = height, Data = texture2d.GetPixels32()};
	}

	private ImageRotator.Rotation m_Rotation = ImageRotator.Rotation.None;
	/// <summary>
	/// Set the GIF rotation (Support rotate 0, -90, 90, 180 degrees).
	/// * Change during/after PreProcessing state will not be applied.
	/// </summary>
	/// <param name="rotation">Rotation. 0, -90, 90, 180</param>
	public void SetGifRotation(ImageRotator.Rotation rotation)
	{
		m_Rotation = rotation;
	}
	public ImageRotator.Rotation GetGifRotation()
	{
		return m_Rotation;
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
	}

	public string Save(List<RenderTexture> textureList, int width, int height, int fps, int loop, int quality,
		Action<int, string> onFileSaved = null, Action<int, float> onFileSaveProgress = null,
		ResolutionHandle resolutionHandle = ResolutionHandle.ResizeKeepRatio, bool autoClear = true)
	{
		return _Save(textureList, width, height, fps, loop, quality, onFileSaved, onFileSaveProgress, autoClear);
		//StartCoroutine(_Save(textureList, width, height, fps, loop, quality, onFileSaved, onFileSaveProgress, autoClear));
	}

	public string Save(List<RenderTexture> textureList, int width, int height, float frameDelay, int loop, int quality,
		Action<int, string> onFileSaved = null, Action<int, float> onFileSaveProgress = null,
		ResolutionHandle resolutionHandle = ResolutionHandle.ResizeKeepRatio, bool autoClear = true)
	{
		frameDelay_Override = frameDelay;
		this.resolutionHandle = resolutionHandle;
		return _Save(textureList, width, height, 0, loop, quality, onFileSaved, onFileSaveProgress, autoClear);
		//StartCoroutine(_Save(textureList, width, height, 0, loop, quality, onFileSaved, onFileSaveProgress, autoClear));
	}

	private string _Save(List<RenderTexture> textureList, int width, int height, int fps, int loop, int quality,
		Action<int, string> onFileSaved = null, Action<int, float> onFileSaveProgress = null, bool autoClear = true)
	{
		this.OnFileSaveProgress = (onFileSaveProgress != null)? onFileSaveProgress:(id, progress)=>{};
		this.OnFileSaved = (onFileSaved != null)? onFileSaved:(id, path)=>{};

		if(autoClear)
		{
			Action<int, string> clearCallback =(id, path)=>{
				Clear();
			};
			this.OnFileSaved += clearCallback;
		}
		string filepath = SaveFolder + "/" + FileName + ".gif";
		//List<Frame> frames = new List<Frame>(textureList.Count);

		//Resize render textures
		for(int i=0; i<textureList.Count; i++)
		{
			float texWidth = textureList[i].width;
			float texHeight = textureList[i].height;
			float texRatio = texHeight / texWidth;
			int newHeight = Mathf.RoundToInt(width * texRatio);

			RenderTexture newTex = new RenderTexture(width, newHeight, 24);
			Graphics.Blit(textureList[i], newTex);
			//Flush(textureList[i]);
			textureList[i] = newTex;
		}

		// Get a temporary texture to read RenderTexture data
		Texture2D temp = new Texture2D(width, height, TextureFormat.RGB24, false);
		temp.hideFlags = HideFlags.HideAndDontSave;
		temp.wrapMode = TextureWrapMode.Clamp;
		temp.filterMode = FilterMode.Bilinear;
		temp.anisoLevel = 0;

		List<Frame> frames = RenderTexturesToFrames(textureList, temp);

		// Setup a worker thread and let it do its magic
		ProGifEncoder encoder = new ProGifEncoder(loop, quality);

		if(m_AutoTransparent)
		{
			encoder.m_AutoTransparent = m_AutoTransparent;
		}
		else if(m_TransparentColor.a != 0)
		{
			encoder.SetTransparencyColor(m_TransparentColor);
		}

		if(frameDelay_Override > 0f)
		{
			encoder.SetDelay(Mathf.RoundToInt(frameDelay_Override * 1000f));
		}
		else
		{
			float timePerFrame = 1f / fps;
			encoder.SetDelay(Mathf.RoundToInt(timePerFrame * 1000f));
		}

		ProGifWorker worker = new ProGifWorker(workerPriority)
		{
			m_Encoder = encoder,
			m_Frames = frames,
			m_FilePath = filepath,
			m_OnFileSaved = FileSaved,
			m_OnFileSaveProgress = FileSaveProgress
		};
		worker.Start();

		return filepath;
		//yield return 0;
	}

	private List<Frame> RenderTexturesToFrames(List<RenderTexture> rTextureList, Texture2D temp)
	{
		previewTextures = new List<Texture2D>();
		List<Frame> frames = new List<Frame>();
		foreach(RenderTexture rt in rTextureList)
		{
			frames.Add(RenderTextureToFrame(rt, temp));
		}
		return frames;
	}

	// Converts a RenderTexture to a GifFrame
	// Should be fast enough for low-res textures but will tank the framerate at higher res
	private Frame RenderTextureToFrame(RenderTexture source, Texture2D target)
	{
		RenderTexture.active = source;

		target.ReadPixels(new Rect((source.width - target.width)/2, (source.height - target.height)/2, target.width, target.height), 0, 0);
		target.Apply();
		RenderTexture.active = null;

		previewTextures.Add(target);

		//==== Rotate Image ===========
		int newWidth = target.width;
		int newHeight = target.height;

		//Rotate image?
		if(m_Rotation == ImageRotator.Rotation.None)
		{
			return new Frame(){Width = newWidth, Height = newHeight, Data = target.GetPixels32()};
		}
		else
		{
			switch(m_Rotation)
			{
			case ImageRotator.Rotation.Right: //90
				newWidth = target.height;
				newHeight = target.width;
				break;
			case ImageRotator.Rotation.HalfCircle: //180
				break;
			case ImageRotator.Rotation.Left: //-90
				newWidth = target.height;
				newHeight = target.width;
				break;
			}

			//Using looping through width & height (Only support rotate with interval of 90 degrees)
			Color32[] colors = ImageRotator.RotateImageToColor32(target, m_Rotation);
			return new Frame(){Width = newWidth, Height = newHeight, Data = colors};
		}
		//=============================

	}

	/// <summary>
	/// Set the file extensions filter for LoadImages.
	/// </summary>
	/// <param name="fileExtensions">File extension names in lower case</param>
	public void SetFileExtension(List<string> fileExtensions)
	{
		_fileExtensions = fileExtensions;
	}

	/// <summary>
	/// Load images from the target directory, to a texture2D list.
	/// </summary>
	/// <returns>The images.</returns>
	/// <param name="directory">Directory.</param>
	public List<Texture2D> LoadImages(string directory)
	{
		List<Texture2D> textureList = new List<Texture2D>();

		string[] allFiles_src = Directory.GetFiles(directory);
		foreach(string f in allFiles_src)
		{
			if(_fileExtensions.Contains(Path.GetExtension(f).ToLower()))
			{
				byte[] bytes = File.ReadAllBytes(f);

				Texture2D tex2D = new Texture2D(4, 4);
				tex2D.LoadImage(bytes);

				textureList.Add(tex2D);
			}
		}
		return textureList;
	}

	/// <summary>
	/// Load images from a resources floder, to a texture2D list. (eg.: Resources/Photo).
	/// </summary>
	/// <returns>The images from the resources floder.</returns>
	/// <param name="resourcesFolderPath">Resources folder path.</param>
	public List<Texture2D> LoadImagesFromResourcesFolder(string resourcesFolderPath = "Photo/")
	{
		//Load image as texture 2D from resources folder, do not support File Extension
		List<Texture2D> tex2DList = new List<Texture2D>();
		Texture2D[] tex2Ds = Resources.LoadAll<Texture2D>(resourcesFolderPath);
		if(tex2Ds != null && tex2Ds.Length > 0)
		{
			for(int i=0; i<tex2Ds.Length; i++)
			{
				tex2DList.Add(tex2Ds[i]);
			}
		}
		return tex2DList;
	}

	public Sprite GetSprite(int index)
	{
		index = Mathf.Clamp(index, 0, previewTextures.Count - 1);
		return ToSprite(previewTextures[index]);
	}

	public Sprite ToSprite(Texture2D texture)
	{
		Vector2 pivot = new Vector2(0.5f, 0.5f);
		float pixelPerUnit = 100;
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, pixelPerUnit);
	}

	public void TakeScreenshot(Action<Texture2D> onComplete)
	{
		StartCoroutine(_TakeScreenshot(onComplete));
	}

	private IEnumerator _TakeScreenshot(Action<Texture2D> onComplete)
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D readTex = new Texture2D(width, height, TextureFormat.RGB24, false);
		Rect rect = new Rect(0, 0, width, height);
		readTex.ReadPixels(rect, 0, 0);
		readTex.Apply();
		onComplete(readTex);
	}

	// Flushe a single Texture object
	private void Flush(Texture texture)
	{
		if(RenderTexture.active == texture) return;

		#if UNITY_EDITOR
		Texture2D.DestroyImmediate(texture);
		#else
		Texture2D.Destroy(texture);
		#endif
	}

	/// <summary>
	/// It is important to Clear textures every time (prevent memory leak)
	/// </summary>
	public void Clear()
	{
		_providedFileName = string.Empty;

		//Clear texture
		if(previewTextures != null)
		{
			foreach(Texture2D tex in previewTextures)
			{
				if(tex != null)
				{
					Texture2D.Destroy(tex);
				}
			}
			previewTextures = null;
		}

		_instance = null;

		if(Application.isPlaying)
		{
			GameObject.Destroy(gameObject);
		}
		else
		{
			GameObject.DestroyImmediate(gameObject);
		}
	}
}
