using UnityEngine;
using System;

#if UNITY_IOS
public class MMPickerReceiver_iOS : MonoBehaviour
{
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern int iMediaPickerBusy();

	private static MMPickerReceiver_iOS _instance;
	private static MMPickerReceiver_iOS Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new GameObject("MMPickerReceiver_iOS").AddComponent<MMPickerReceiver_iOS>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	private Action<string> onMediaReceived;

	private float nextCheckTime; 

	public static bool IsBusy { get; private set; }

	public static void Initialize(Action<string> onMediaReceived)
	{
		if(IsBusy) return;
		Instance.onMediaReceived = onMediaReceived;
		Instance.nextCheckTime = Time.realtimeSinceStartup + 1f;
		IsBusy = true;
	}
	
	private void Update()
	{
		if(IsBusy)
		{
			if(Time.realtimeSinceStartup >= nextCheckTime)
			{
				nextCheckTime = Time.realtimeSinceStartup + 1f;

				if(iMediaPickerBusy() == 0)
				{
					if(onMediaReceived != null)
					{
						onMediaReceived(null);
						onMediaReceived = null;
					}
					IsBusy = false;
				}
			}
		}
	}

	public void OnMediaReceived(string path)
	{
		if(string.IsNullOrEmpty(path))
		{
			path = null;
		}

		if(onMediaReceived != null)
		{
			onMediaReceived(path);
			onMediaReceived = null;
		}
		IsBusy = false;
	}
}
#else
public class MMPickerReceiver_iOS{}
#endif