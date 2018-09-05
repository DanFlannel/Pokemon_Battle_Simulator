/// <summary>
/// Created by SwanOB2
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;

using Newtonsoft.Json;

public class GiphyManager : MonoBehaviour
{
	public enum Rating
	{
		None = 0,
		Y,
		G,
		PG,
		PG_13,
		R
	}

	//lang - specify default country for regional content; format is 2-letter ISO 639-1 language code
	public enum Language
	{
		None = 0,
		en, //English
		es, //Spanish
		pt, //Portuguese
		id, //Indonesian
		fr, //French
		ar, //Arabic
		tr, //Turkish
		th, //Thai
		vi, //Vietnamese
		de, //German
		it, //Italian
		ja, //Japanese
		ru, //Russian
		ko, //Korean
		pl, //Polish
		nl, //Dutch
		ro, //Romanian
		hu, //Hungarian
		sv, //Swedish
		cs, //Czech
		hi, //Hindi
		bn, //Bengali
		da, //Danish
		fa, //Farsi
		tl, //Filipino
		fi, //Finnish
		iw, //Hebrew
		ms, //Malay
		no, //Norwegian
		uk, //Ukrainian
		CN, //Chinese Simplified
		TW, //Chinese Traditional
	}

	//-------------------------------------------------------------------------------

	[Header("[ Giphy Channel ]")]
	//----- Apply your gif channel & api keys here: https://developers.giphy.com/ -----//
	public string m_GiphyUserName = "";		//Your user name on Giphy
	public string m_GiphyApiKey = "";		//Your API key associated with your app on Giphy (for NormalGifApi & StickerApi)
	public string m_GiphyUploadApiKey = ""; //Your upload API key associated with your app on Giphy (for UploadApi, need Request a production key to get this key)

	[Header("[ Giphy API URL ]")]
	public string m_NormalGifApi = "http://api.giphy.com/v1/gifs";
	public string m_StickerApi = "http://api.giphy.com/v1/stickers";
	public string m_UploadApi = "http://upload.giphy.com/v1/gifs";

	[Header("[ Optional-Settings ]")]
	public int m_ResultLimit = 10;				//Number of results to return, maximum 100. Default 25
	public int m_ResultOffset = 0;				//Results offset, defaults to 0
	public Rating m_Rating = Rating.None;		//Limit results to those rated GIFs (y,g, pg, pg-13 or r)
	public Language m_Language = Language.None; //Language use with Sticker API call, specify default country for regional content

	[Header("[ Optional-Promotion ]")]
	public string m_Source_Post_Url = "";		//An url attach to the GIF Info>SOURCE field, for the uploaded GIF (e.g. Your website/The page on which this GIF was found)

	//-------------------------------------------------------------------------------

	private string _FullJsonResponseText = "Full Json Response Text"; 
	public string FullJsonResponseText
	{
		get{
			return _FullJsonResponseText;
		}
		set{
			string json = value.Replace("\\", "");
			Debug.Log(json);

			_FullJsonResponseText = value;
		}
	}

	private static GiphyManager _instance = null;
	public static GiphyManager Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new GameObject("[GiphyManager]").AddComponent<GiphyManager>();
			}
			return _instance;
		}
	}

	public void SetChannelAuthentication(string userName, string apiKey = "", string uploadApiKey = "")
	{
		m_GiphyUserName = userName;
		m_GiphyApiKey = apiKey;
		m_GiphyUploadApiKey = uploadApiKey;
	}

	private bool HasUserName
	{
		get{
			bool hasUserName = !string.IsNullOrEmpty(m_GiphyUserName);
			if(!hasUserName) 
			{
				#if UNITY_EDITOR
				Debug.LogWarning("Giphy User Name is required if you use a production api key.");
				#endif
			}
			return true;
		}
	}

	private bool HasApiKey
	{
		get{
			bool hasApiKey = !string.IsNullOrEmpty(m_GiphyApiKey);
			if(!hasApiKey) Debug.LogWarning("Giphy API Key is required!");
			return hasApiKey;
		}
	}

	private bool HasUploadApiKey
	{
		get{
			bool hasApiKey = !string.IsNullOrEmpty(m_GiphyUploadApiKey);
			if(!hasApiKey) Debug.LogWarning("Giphy Upload API Key is required!");
			return hasApiKey;
		}
	}

	void Start()
	{
		if(_instance == null)
		{
			_instance = this;
		}
	}
		
	void Update()
	{
		if(wwwUpload != null) 
		{
			if(_onUploadProgress != null)
			{
				_onUploadProgress(wwwUpload.uploadProgress);
			}
		}
	}

	#region ------- Upload GIF API -------
	WWW wwwUpload;
	Action<float> _onUploadProgress = null;

	/// <summary>
	/// Upload a specified GIF with its filePath to Giphy.
	/// </summary>
	/// <param name="filePath">File path.</param>
	/// <param name="onComplete">On complete.</param>
	/// <param name="onProgress">On progress.</param>
	public void Upload(string filePath, Action<GiphyUpload.Response> onComplete, Action<float> onProgress = null, Action onFail = null)
	{
		if(!HasUploadApiKey || !HasUserName) return;
		StartCoroutine(_Upload(filePath, null, onComplete, onProgress, onFail));
	}

	/// <summary>
	/// Upload a specified GIF with its filePath to Giphy. 
	/// And add tag(s) to the GIF that allows it to be searched by browsing/searching.
	/// </summary>
	/// <param name="filePath">File path.</param>
	/// <param name="tags">Tags.</param>
	/// <param name="onComplete">On complete.</param>
	/// <param name="onProgress">On progress.</param>
	public void Upload(string filePath, List<string> tags, Action<GiphyUpload.Response> onComplete, Action<float> onProgress = null, Action onFail = null)
	{
		if(!HasUploadApiKey || !HasUserName) return;
		StartCoroutine(_Upload(filePath, tags, onComplete, onProgress, onFail));
	}

	IEnumerator _Upload(string filePath, List<string> tags, Action<GiphyUpload.Response> onComplete, Action<float> onProgress = null, Action onFail = null)
	{
		//string url = m_UploadApi;
		string url = m_UploadApi + "?api_key=" + m_GiphyUploadApiKey;// + (string.IsNullOrEmpty(m_GiphyUserName)? "":"&username=" + m_GiphyUserName);

		_onUploadProgress = onProgress;

		string tagsStr = "";
		if(tags != null && tags.Count > 0)
		{
			foreach(string tag in tags)
			{
				if(!string.IsNullOrEmpty(tag)) tagsStr += tag + ",";
			}
			if(!string.IsNullOrEmpty(tagsStr)) tagsStr = tagsStr.Substring(0, tagsStr.Length-1);
		}

		byte[] bytes = File.ReadAllBytes(filePath);

		WWWForm postForm = new WWWForm();
		postForm.AddBinaryData("file", bytes);
		postForm.AddField("api_key", m_GiphyUploadApiKey);
		postForm.AddField("username", m_GiphyUserName);
		if(!string.IsNullOrEmpty(tagsStr)) postForm.AddField("tags", tagsStr);
		if(!string.IsNullOrEmpty(m_Source_Post_Url)) postForm.AddField("source_post_url", m_Source_Post_Url);

		// Upload (WWW)
		wwwUpload = new WWW(url, postForm);
		wwwUpload.threadPriority = ThreadPriority.High;

		#if UNITY_EDITOR
		Debug.Log("UserName: " + m_GiphyUserName + " | Upload Api Key: " + m_GiphyUploadApiKey + 
			" | Api Key: " + m_GiphyApiKey + " | tags: " + tagsStr + " | m_Source_Post_Url: " + m_Source_Post_Url);
		
		Debug.Log("url: " + wwwUpload.url + " | postForm: " + postForm.ToString());
		#endif
        
        yield return wwwUpload;
		if(!string.IsNullOrEmpty(wwwUpload.error))
		{
			if(onFail != null) onFail();
			Debug.Log("Error during upload: " + wwwUpload.error + "\n" + wwwUpload.text);
		}
		else
		{
            FullJsonResponseText = wwwUpload.text;
        
			GiphyUpload.Response uploadResponse = JsonConvert.DeserializeObject<GiphyUpload.Response>(wwwUpload.text);
			onComplete(uploadResponse);
		}

		if(wwwUpload != null)
		{
			wwwUpload.Dispose();
			wwwUpload = null;
		}


//		// Upload (UnityWebRequest)
//		using(var w = UnityWebRequest.Post(url, postForm))
//		{
//			yield return w.Send();
//			if(w.isError) 
//			{
//				if(onFail != null) onFail();
//				Debug.LogError("Error during upload: " + w.error + "\nResult: " + w.downloadHandler.text);
//			}
//			else if(w.isDone)
//			{
//				FullJsonResponseText = w.downloadHandler.text;
//	
//				GiphyUpload.Response uploadResponse = JsonConvert.DeserializeObject<GiphyUpload.Response>(w.downloadHandler.text);
//				onComplete(uploadResponse);
//				Debug.Log("Finished Uploading GIF. Response: " + w.responseCode + " Result: " + w.downloadHandler.text);
//			}
//			else
//			{
//				if(onFail != null) onFail();
//				Debug.LogWarning("Fail to upload!");
//			}
//		}

	}
	#endregion


	#region ------- Normal GIF API --------
	/// <summary>
	/// Returns a GIF given that GIF's unique ID
	/// </summary>
	/// <param name="giphyGifId">Giphy GIF identifier.</param>
	/// <param name="onComplete">On complete.</param>
	public void GetById(string giphyGifId, Action<GiphyGetById.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_GetById(giphyGifId, onComplete, onFail));
	}

	IEnumerator _GetById(string giphyGifId, Action<GiphyGetById.Response> onComplete, Action onFail)
	{
		if(!string.IsNullOrEmpty(giphyGifId))
		{
			string url = m_NormalGifApi +"/" + giphyGifId + "?api_key=" + m_GiphyApiKey;

			WWW www = new WWW(url);
			yield return www;

			if(www.error==null){
				FullJsonResponseText = www.text;

				GiphyGetById.Response response = JsonConvert.DeserializeObject<GiphyGetById.Response>(www.text);
				if(onComplete != null) onComplete(response);
			}else{
				if(onFail != null) onFail();
				Debug.Log("Error during get by id: " + giphyGifId + ", Error: "+ www.error);
			}

			www.Dispose();
			www = null;
		}
		else
		{
			Debug.LogWarning("GIF id is empty!");
		}
	}

	/// <summary>
	/// Returns an array of GIFs given that GIFs' unique IDs
	/// </summary>
	/// <param name="giphyGifIds">Giphy GIF identifiers.</param>
	/// <param name="onComplete">On complete.</param>
	public void GetByIds(List<string> giphyGifIds, Action<GiphyGetByIds.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_GetByIds(giphyGifIds, onComplete, onFail));
	}

	IEnumerator _GetByIds(List<string> giphyGifIds, Action<GiphyGetByIds.Response> onComplete, Action onFail = null)
	{
		string giphyGifIdsStr = "";
		foreach(string id in giphyGifIds)
		{
			if(!string.IsNullOrEmpty(id)) giphyGifIdsStr += id + ",";
		}

		if(!string.IsNullOrEmpty(giphyGifIdsStr))
		{
			giphyGifIdsStr = giphyGifIdsStr.Substring(0, giphyGifIdsStr.Length-1);
		
			string url = m_NormalGifApi + "?ids=" + giphyGifIdsStr + "&api_key=" + m_GiphyApiKey;

			WWW www = new WWW(url);
			yield return www;

			if(www.error==null){
				FullJsonResponseText = www.text;

				GiphyGetByIds.Response response = JsonConvert.DeserializeObject<GiphyGetByIds.Response>(www.text);
				if(onComplete != null) onComplete(response);
			}else{
				if(onFail != null) onFail();
				Debug.Log("Error during get by ids: " + giphyGifIdsStr + ", Error: "+ www.error);
			}

			www.Dispose();
			www = null;
		}
		else
		{
			Debug.LogWarning("GIF ids is empty!");
		}
	}

	/// <summary>
	/// Search all GIPHY GIFs for a word or phrase. Punctuation will be stripped and ignored.
	/// </summary>
	/// <param name="keyWords">Key words.</param>
	/// <param name="onComplete">On complete.</param>
	public void Search(List<string> keyWords, Action<GiphySearch.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Search(keyWords, onComplete, onFail));
	}

	IEnumerator _Search(List<string> keyWords, Action<GiphySearch.Response> onComplete, Action onFail = null)
	{
		string keyWordsStr = "";
		foreach(string k in keyWords)
		{
			keyWordsStr += k + "+";
		}
		keyWordsStr = keyWordsStr.Substring(0, keyWordsStr.Length-1);

		string url = m_NormalGifApi + "/search?q=" + keyWordsStr + "&api_key=" + m_GiphyApiKey;
		if(m_ResultLimit > 0) url += "&limit=" + m_ResultLimit;
		if(m_ResultOffset > 0) url += "&offset=" + m_ResultOffset;
		if(m_Rating != Rating.None) url += "&rating=" + m_Rating.ToString().ToUpper();

		WWW www = new WWW(url);
		yield return www;

		if(www.error==null){
			FullJsonResponseText = www.text;
			GiphySearch.Response response = JsonConvert.DeserializeObject<GiphySearch.Response>(www.text);
			if(onComplete != null) onComplete(response);
		}else{
			if(onFail != null) onFail();
			Debug.Log("Error during search: " + www.error);
		}

		www.Dispose();
		www = null;
	}

	/// <summary>
	/// Get a random GIF from Giphy
	/// </summary>
	/// <param name="onComplete">On complete.</param>
	public void Random(Action<GiphyRandom.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Random(null, onComplete, onFail));
	}

	/// <summary>
	/// Get a random GIF, limited by tag.
	/// </summary>
	/// <param name="tag">Tag: the GIF tag to limit randomness by</param>
	/// <param name="onComplete">On complete.</param>
	public void Random(string tag, Action<GiphyRandom.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Random(tag, onComplete, onFail));
	}

	IEnumerator _Random(string tag, Action<GiphyRandom.Response> onComplete, Action onFail = null)
	{
		string url = m_NormalGifApi + "/random?api_key=" + m_GiphyApiKey;
		if(!string.IsNullOrEmpty(tag)) url += "&tag=" + tag;
		if(m_Rating != Rating.None) url += "&rating=" + m_Rating.ToString().ToUpper();

		WWW www = new WWW(url);
		yield return www;

		if(www.error==null){
			FullJsonResponseText = www.text;
			GiphyRandom.Response response = JsonConvert.DeserializeObject<GiphyRandom.Response>(www.text);
			if(onComplete != null) onComplete(response);
		}else{
			if(onFail != null) onFail();
			Debug.Log("Error during Random: " + www.error);
		}

		www.Dispose();
		www = null;
	}

	/// <summary>
	/// The translate API draws on search, but uses the GIPHY special sauce to handle translating from one vocabulary to another. 
	/// In this case, words and phrases to GIFs. The result is Random even for the same term.
	/// </summary>
	/// <param name="term">term.</param>
	/// <param name="onComplete">On complete.</param>
	public void Translate(string term, Action<GiphyTranslate.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Translate(term, onComplete, onFail));
	}

	IEnumerator _Translate(string term, Action<GiphyTranslate.Response> onComplete, Action onFail = null)
	{
		if(!string.IsNullOrEmpty(term))
		{
			string url = m_NormalGifApi + "/translate?api_key=" + m_GiphyApiKey + "&s=" + term;
			if(m_Rating != Rating.None) url += "&rating=" + m_Rating.ToString().ToUpper();

			WWW www = new WWW(url);
			yield return www;

			if(www.error==null){
				FullJsonResponseText = www.text;
				GiphyTranslate.Response response = JsonConvert.DeserializeObject<GiphyTranslate.Response>(www.text);
				if(onComplete != null) onComplete(response);
			}else{
				if(onFail != null) onFail();
				Debug.Log("Error during Translate: " + www.error);
			}

			www.Dispose();
			www = null;
		}
		else
		{
			Debug.LogWarning("Search term is empty!");
		}
	}

	/// <summary>
	/// Fetch GIFs currently trending online. Hand curated by the GIPHY editorial team. 
	/// The data returned mirrors the GIFs showcased on the GIPHY homepage. 
	/// Returns 25 results by default.
	/// </summary>
	/// <param name="onComplete">On complete.</param>
	public void Trending(Action<GiphyTrending.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Trending(onComplete, onFail));
	}

	IEnumerator _Trending(Action<GiphyTrending.Response> onComplete, Action onFail = null)
	{
		string url = m_NormalGifApi + "/trending?api_key=" + m_GiphyApiKey;
		if(m_ResultLimit > 0) url += "&limit=" + m_ResultLimit;
		if(m_ResultOffset > 0) url += "&offset=" + m_ResultOffset;
		if(m_Rating != Rating.None) url += "&rating=" + m_Rating.ToString().ToUpper();

		WWW www = new WWW(url);
		yield return www;

		if(www.error==null){
			FullJsonResponseText = www.text;
			GiphyTrending.Response response = JsonConvert.DeserializeObject<GiphyTrending.Response>(www.text);
			if(onComplete != null) onComplete(response);
		}else{
			if(onFail != null) onFail();
			Debug.Log("Error during Trending: " + www.error);
		}

		www.Dispose();
		www = null;
	}
	#endregion


	#region -------- Sticker GIF API --------
	private string _GetLanguageString(Language lang)
	{
		string langStr = "";
		switch(lang)
		{
		case Language.None:
			//Do nothing
			break;

		case Language.CN:
			langStr = "zh-CN";
			break;

		case Language.TW:
			langStr = "zh-TW";
			break;

		default:
			langStr = lang.ToString().ToLower();
			break;
		}
		return langStr;
	}


	public void Search_Sticker(List<string> keyWords, Action<GiphyStickerSearch.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Search_Sticker(keyWords, onComplete, onFail));
	}

	IEnumerator _Search_Sticker(List<string> keyWords, Action<GiphyStickerSearch.Response> onComplete, Action onFail = null)
	{
		string keyWordsStr = "";
		foreach(string k in keyWords)
		{
			keyWordsStr += k + "+";
		}
		keyWordsStr = keyWordsStr.Substring(0, keyWordsStr.Length-1);

		string url = m_StickerApi + "/search?q=" + keyWordsStr + "&api_key=" + m_GiphyApiKey;
		if(m_ResultLimit > 0) url += "&limit=" + m_ResultLimit;
		if(m_ResultOffset > 0) url += "&offset=" + m_ResultOffset;
		if(m_Rating != Rating.None) url += "&rating=" + m_Rating.ToString().ToUpper();
		if(m_Language != Language.None) url += "&lang=" + _GetLanguageString(m_Language);

		WWW www = new WWW(url);
		yield return www;

		if(www.error==null){
			FullJsonResponseText = www.text;
			GiphyStickerSearch.Response searchResponse = JsonConvert.DeserializeObject<GiphyStickerSearch.Response>(www.text);
			if(onComplete != null) onComplete(searchResponse);
		}else{
			if(onFail != null) onFail();
			Debug.Log("Error during Search_Sticker: " + www.error);
		}

		www.Dispose();
		www = null;
	}

	public void Random_Sticker(Action<GiphyStickerRandom.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Random_Sticker(null, onComplete, onFail));
	}

	public void Random_Sticker(string tag, Action<GiphyStickerRandom.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Random_Sticker(tag, onComplete, onFail));
	}

	IEnumerator _Random_Sticker(string tag, Action<GiphyStickerRandom.Response> onComplete, Action onFail = null)
	{
		string url = m_StickerApi + "/random?api_key=" + m_GiphyApiKey;
		if(!string.IsNullOrEmpty(tag)) url += "&tag=" + tag;
		if(m_Rating != Rating.None) url += "&rating=" + m_Rating.ToString().ToUpper();

		WWW www = new WWW(url);
		yield return www;

		if(www.error==null){
			FullJsonResponseText = www.text;
			GiphyStickerRandom.Response searchResponse = JsonConvert.DeserializeObject<GiphyStickerRandom.Response>(www.text);
			if(onComplete != null) onComplete(searchResponse);
		}else{
			if(onFail != null) onFail();
			Debug.Log("Error during Random_Sticker: " + www.error);
		}

		www.Dispose();
		www = null;
	}


	public void Translate_Sticker(string term, Action<GiphyStickerTranslate.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Translate_Sticker(term, onComplete, onFail));
	}

	IEnumerator _Translate_Sticker(string term, Action<GiphyStickerTranslate.Response> onComplete, Action onFail = null)
	{
		if(!string.IsNullOrEmpty(term))
		{
			string url = m_StickerApi + "/translate?api_key=" + m_GiphyApiKey + "&s=" + term;

			WWW www = new WWW(url);
			yield return www;

			if(www.error==null){
				FullJsonResponseText = www.text;
				GiphyStickerTranslate.Response searchResponse = JsonConvert.DeserializeObject<GiphyStickerTranslate.Response>(www.text);
				if(onComplete != null) onComplete(searchResponse);
			}else{
				if(onFail != null) onFail();
				Debug.Log("Error during Translate_Sticker: " + www.error);
			}

			www.Dispose();
			www = null;
		}
		else
		{
			Debug.LogWarning("Search term is empty!");
		}
	}

	public void Trending_Sticker(Action<GiphyStickerTrending.Response> onComplete, Action onFail = null)
	{
		if(!HasApiKey || !HasUserName) return;
		StartCoroutine(_Trending_Sticker(onComplete, onFail));
	}

	IEnumerator _Trending_Sticker(Action<GiphyStickerTrending.Response> onComplete, Action onFail = null)
	{
		string url = m_StickerApi + "/trending?api_key=" + m_GiphyApiKey;
		if(m_ResultLimit > 0) url += "&limit=" + m_ResultLimit;
		if(m_Rating != Rating.None) url += "&rating=" + m_Rating.ToString().ToUpper();

		WWW www = new WWW(url);
		yield return www;

		if(www.error==null){
			FullJsonResponseText = www.text;
			GiphyStickerTrending.Response searchResponse = JsonConvert.DeserializeObject<GiphyStickerTrending.Response>(www.text);
			if(onComplete != null) onComplete(searchResponse);
		}else{
			if(onFail != null) onFail();
			Debug.Log("Error during Trending_Sticker: " + www.error);
		}

		www.Dispose();
		www = null;
	}
	#endregion
}
