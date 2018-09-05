using System.Threading;
using UnityEngine;

#if UNITY_ANDROID
public class MMPickerReceiver_Android : AndroidJavaProxy
{
	public string Path { get; private set; }
	public string[] Paths { get; private set; }

	private object threadLockObject;

	public MMPickerReceiver_Android(object threadLockObject) : base("unity.swanob2.com.mobilemedia.MMMediaReceiver")
	{
		Path = string.Empty;
		this.threadLockObject = threadLockObject;
	}

	public void OnMediaReceived(string path)
	{
		Path = path;

		lock(threadLockObject)
		{
			Monitor.Pulse(threadLockObject);
		}
	}
}
#else
public class MMPickerReceiver_Android{}
#endif