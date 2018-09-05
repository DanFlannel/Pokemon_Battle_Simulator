using System;
using System.Collections.Generic;
//using System.Linq;
using System.Net;
using System.IO;

public class RequestHandler
{
	public RequestHandler()
	{
	}

	public static void Process(string url, Action<bool, string> onComplete)
	{
		#if UNITY_EDITOR
		UnityEngine.Debug.Log("WWW Request");
		#endif

		WWWRequestHandler.Create().Request(url, 
			(success, result)=>{
				onComplete(success, result);
			}
		);
	}

    public static string Process(string url)
    {
		#if UNITY_EDITOR
		UnityEngine.Debug.Log("Web Request");
		#endif

		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse HttpWResp = (HttpWebResponse)request.GetResponse();
        Stream streamResponse = HttpWResp.GetResponseStream();

        // And read it out
        StreamReader reader = new StreamReader(streamResponse);
        string response = reader.ReadToEnd();

        reader.Close();
        reader.Dispose();

		#if UNITY_EDITOR
		UnityEngine.Debug.Log("response: \n" + response);
		#endif
        return response;
    }
}