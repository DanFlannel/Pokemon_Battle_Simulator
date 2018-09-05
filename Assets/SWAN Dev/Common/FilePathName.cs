/// <summary>
/// By SwanDEV 2017
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;

/// <summary> Files, Paths, Names & IO common methods. (Last update: 2018-06-13) </summary>
public class FilePathName
{
	private static string _lastGeneratedFileNameWithoutExt_fff = "";
	private static int _lastSameFileNameCounter_fff = 1;

	private static string _lastGeneratedFileNameWithoutExt = "";
	private static int _lastSameFileNameCounter = 1;

	public enum SaveFormat
	{
		NONE = -1,
		GIF = 0,
		JPG,
		PNG,
	}

	#region ----- Path & FileName -----
	public string GetSaveDirectory(bool isTemporaryPath = false)
	{
		if(isTemporaryPath)
		{
			return Application.temporaryCachePath;
		}
		else
		{
			//GIF store in Virtual Memory
			//Available path: Application.persistentDataPath, Application.temporaryCachePath, Application.dataPath
			//Do not allow sub-Folder under the path. If you need to view gif, you can filter the file names to include .gif only.
			#if UNITY_EDITOR
			return Application.dataPath; 
			//return Application.persistentDataPath;
			#else
			return Application.persistentDataPath;
			#endif
		}
	}

	public string GeFileNameWithoutExt(bool millisecond = false)
	{
		if(millisecond)
		{
			return _GetComparedFileName(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"),
				_lastGeneratedFileNameWithoutExt_fff, _lastSameFileNameCounter_fff,
				out _lastGeneratedFileNameWithoutExt_fff, out _lastSameFileNameCounter_fff);
		}
		return _GetComparedFileName(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"),
			_lastGeneratedFileNameWithoutExt, _lastSameFileNameCounter,
			out _lastGeneratedFileNameWithoutExt, out _lastSameFileNameCounter);
	}

	private string _GetComparedFileName(string newFileName, string lastGeneratedFileName, int sameFileNameCounter,
		out string outLastGeneratedFileName, out int outSameFileNameCounter)
	{
		if(lastGeneratedFileName == newFileName)
		{
			sameFileNameCounter++;
		}
		else
		{
			sameFileNameCounter = 1;
		}

		outLastGeneratedFileName = newFileName;
		outSameFileNameCounter = sameFileNameCounter;

		if(sameFileNameCounter > 1)
		{
			newFileName += " " + sameFileNameCounter;
		}

		return newFileName;
	}

	public string EnsureLocalPath(string path)
	{
		if(!path.ToLower().StartsWith("file:///"))
		{
			while(path.StartsWith("/"))
			{
				path = path.Remove(0, 1);
			}
			path = "file:///" + path;
		}
		return path;
	}

	public string GetGifFileName()
	{
		string timestamp = GeFileNameWithoutExt();
		return "GIF_" + timestamp;
	}
	public string GetGifFullPath()
	{
		return GetSaveDirectory() + "/" + GetGifFileName() + ".gif";
	}
	public string GetDownloadedGifSaveFullPath()
	{
		return GetSaveDirectory() + "/" + GetGifFileName() + ".gif";
	}

	public string GetJpgFileName()
	{
		string timestamp = GeFileNameWithoutExt(true);
		return "Photo_" + timestamp;
	}
	public string GetJpgFullPath()
	{
		return GetSaveDirectory() + "/" + GetJpgFileName() + ".jpg";
	}

	public string GetPngFileName()
	{
		string timestamp = GeFileNameWithoutExt(true);
		return "Photo_" + timestamp;
	}
	public string GetPngFullPath()
	{
		return GetSaveDirectory() + "/" + GetPngFileName() + ".png";
	}

	#endregion

	#region ----- IO -----
	public byte[] ReadFileToBytes(string fromFullPath)
	{
		return File.ReadAllBytes(fromFullPath);
	}

	public void WriteBytesToFile(string toFullpath, byte[] byteArray)
	{
		File.WriteAllBytes(toFullpath, byteArray);
	}

	public void CopyFile(string fromFullPath, string toFullPath, bool overwrite = false)
	{
		File.Copy(fromFullPath, toFullPath, overwrite);
	}

	public void MoveFile(string fromFullPath, string toFullPath)
	{
		File.Move(fromFullPath, toFullPath);
	}

	public void DeleteFile(string fullPath)
	{
		File.Delete(fullPath);
	}

	public bool FileStreamTo(string fullpath, byte[] byteArray)
	{
		try
		{
			using(FileStream fs = new FileStream(fullpath, FileMode.Create, FileAccess.Write))
			{
				fs.Write(byteArray, 0, byteArray.Length);
				return true; // success
			}
		}
		catch(Exception e)
		{
			Console.WriteLine("Exception caught in process: {0}", e);
			return false;	// fail
		}
	}

	public void WriteBytesToText(byte[] bytes, string toFullPath, string separator = "", bool toChar = true)
	{
//		int bkCount = 0;

		StringBuilder sb = new StringBuilder();
		if(string.IsNullOrEmpty(separator))
		{
			if(toChar)
			{
				for (int i = 0; i < bytes.Length; i++)
				{
					sb.Append((char)bytes[i]);
				}
			}
			else
			{
				for (int i = 0; i < bytes.Length; i++)
				{
					sb.Append(bytes[i]);

					//Test 
//					bkCount++;
//					if(bkCount == 3) 
//					{
//						bkCount = 0;
//						sb.Append(" (" + (i+1)/3 + ")\n");
//					}
				}
			}
		}
		else
		{
			if(toChar)
			{
				for (int i = 0; i < bytes.Length; i++)
				{
					sb.Append((char)bytes[i]);
					sb.Append(separator);
				}
			}
			else
			{
				for (int i = 0; i < bytes.Length; i++)
				{
					sb.Append(bytes[i]);
					sb.Append(separator);

					//Test 
//					bkCount++;
//					if(bkCount == 3) 
//					{
//						bkCount = 0;
//						sb.Append(" (" + ((i+1)/3-1) + ")\n");
//					}
				}
			}
		}
		File.WriteAllText(toFullPath, sb.ToString());
	}

	public string SaveTextureAs(Texture2D texture2D, SaveFormat format = SaveFormat.JPG)
	{
		string savePath = string.Empty;
		switch(format)
		{
		case SaveFormat.JPG:
			savePath = GetJpgFullPath();
			WriteBytesToFile(savePath, texture2D.EncodeToJPG(90));
			break;
		case SaveFormat.PNG:
			savePath = GetPngFullPath();
			WriteBytesToFile(savePath, texture2D.EncodeToPNG());
			break;
		case SaveFormat.GIF:
			savePath = ProGifTexturesToGIF.Instance.Save(new List<Texture2D>{texture2D}, texture2D.width, texture2D.height, 1, -1, 10);
			break;
		}
		return savePath;
	}

	public string SaveTexturesAsGIF(List<Texture2D> textureList, int width, int height, int fps, int loop, int quality,
		Action<int, string> onFileSaved = null, Action<int, float> onFileSaveProgress = null, 
		ProGifTexturesToGIF.ResolutionHandle resolutionHandle = ProGifTexturesToGIF.ResolutionHandle.ResizeKeepRatio)
	{
		return ProGifTexturesToGIF.Instance.Save(textureList, width, height, fps, loop, quality, onFileSaved, onFileSaveProgress, resolutionHandle);
	}

	public Texture2D LoadImage(string fullFilePath)
	{
		if(!File.Exists(fullFilePath))
		{
			#if UNITY_EDITOR
			Debug.LogWarning("File not exist! " + fullFilePath);
			#endif
			return null;
		}
		else
		{
			Texture2D tex2D = new Texture2D(1, 1); //, TextureFormat.ARGB32, false);
			tex2D.LoadImage(ReadFileToBytes(fullFilePath));
			return tex2D;
		}
	}

	/// <summary>
	/// Load images in the target directory, to a texture2D list.
	/// </summary>
	/// <returns>The images.</returns>
	/// <param name="directory">Directory.</param>
	/// <param name="fileExtensions">A list of file extension names, indicating the type of files to be loaded. Load jpg, png and gif if Null or Empty.</param>
	public List<Texture2D> LoadImages(string directory, List<string> fileExtensions = null)
	{
		if(fileExtensions != null && fileExtensions.Count > 0) {} else
		{
			fileExtensions = new List<string>{".jpg", ".png", ".gif"};
		}

		List<Texture2D> textureList = new List<Texture2D>();

		foreach(string f in GetFilePaths(directory, fileExtensions))
		{
			if(fileExtensions.Contains(Path.GetExtension(f).ToLower()))
			{
				textureList.Add(LoadImage(f));
			}
		}
		return textureList;
	}

	/// <summary>
	/// Load files in the target directory, to a byte[] list.
	/// </summary>
	/// <returns>Files in byte[].</returns>
	/// <param name="directory">Directory.</param>
	/// <param name="fileExtensions">A list of file extension names, indicating the type of files to be loaded. Load all files if Null or Empty.</param>
	public List<byte[]> LoadFiles(string directory, List<string> fileExtensions = null)
	{
		List<byte[]> fileByteList = new List<byte[]>();

		foreach(string f in GetFilePaths(directory, fileExtensions))
		{
			fileByteList.Add(ReadFileToBytes(f));
		}
		return fileByteList;
	}

	/// <summary>
	/// Get file paths in the target directory.
	/// </summary>
	/// <returns>File paths list.</returns>
	/// <param name="directory">Directory.</param>
	/// <param name="fileExtensions">A list of file extension names, indicating the type of file paths to get. Get all file paths if Null or Empty.</param>
	public List<string> GetFilePaths(string directory, List<string> fileExtensions = null)
	{
		if(!Directory.Exists(directory))
		{
			throw new DirectoryNotFoundException("Directory not found at " + directory);
		}

		string[] allFiles_src = Directory.GetFiles(directory);

		bool loadAllFile = (fileExtensions == null)? true:((fileExtensions.Count <= 0)? true:false);
		if(loadAllFile)
		{
			#if UNITY_EDITOR
			Debug.Log("Load ALL");
			#endif
			return allFiles_src.ToList();
		}

		#if UNITY_EDITOR
		Debug.Log("Load Filtered");
		#endif

		if(fileExtensions == null)
		{
			fileExtensions = new List<string>();
		}
		else
		{
			for(int i=0; i<fileExtensions.Count; i++)
			{
				fileExtensions[i] = fileExtensions[i].ToLower();
			}
		}

		List<string> filteredFilePathList = new List<string>();
		foreach(string f in allFiles_src)
		{
			if(fileExtensions.Contains(Path.GetExtension(f).ToLower()))
			{
				filteredFilePathList.Add(f);
			}
		}
		return filteredFilePathList;
	}

	/// <summary>
	/// Loads file using WWW. Return the byte array of the file in onLoadCompleted callback.
	/// ( IEnumerator: Remember to call this method in StartCoroutine )
	/// </summary>
	/// <returns>The file byte array.</returns>
	/// <param name="url">Local file path or http/https link.</param>
	/// <param name="onLoadCompleted">On load completed callback.</param>
	public IEnumerator LoadFileWWW(string url, Action<byte[]> onLoadCompleted)
	{
		string path = url;
		if(path.StartsWith("http"))
		{
			// from WEB
		}
		else
		{
			// from Local
			path = EnsureLocalPath(path);

			#if UNITY_EDITOR
			Debug.Log("Local file path: " + path);
			#endif
		}

		using(WWW www = new WWW(path))
		{
			yield return www;

			if(string.IsNullOrEmpty(www.error) == false)
			{
				Debug.LogError("File load error.\n" + www.error);
				onLoadCompleted(null);
				yield break;
			}

			onLoadCompleted(www.bytes);
		}
	}

	#endregion

	public Sprite Texture2DToSprite(Texture2D texture2D)
	{
		if(texture2D == null) return null;

		Vector2 pivot = new Vector2(0.5f, 0.5f);
		float pixelPerUnit = 100;
		return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), pivot, pixelPerUnit);
	}

}
