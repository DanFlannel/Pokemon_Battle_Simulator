using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WWWRequestHandler : MonoBehaviour
{
	public static WWWRequestHandler Create(string name = "")
	{
		return new GameObject("[ WWWRequestHandler " + name + " ]").AddComponent<WWWRequestHandler>();
	}

	public void Request(string apiUrl, Action<bool, string> onComplete)
	{
		StartCoroutine(_CallApi(apiUrl, onComplete));
	}

	IEnumerator _CallApi(string apiUrl, Action<bool, string> onComplete)
	{
		WWW www = new WWW(apiUrl);
		yield return www;

		if(www.error == null){
			onComplete(true, www.text);
		}else{
			onComplete(false, "");
			#if UNITY_EDITOR
			Debug.Log("Error during get sticker: " + apiUrl + ", Error: "+ www.error);
			#endif
		}

		www.Dispose();
		www = null;
		GameObject.Destroy(gameObject);
	}

}
