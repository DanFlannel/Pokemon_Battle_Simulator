using System.Threading;
using UnityEngine;

#if UNITY_ANDROID
public class MMPermissionReceiver_Android : AndroidJavaProxy
{
	public int Result { get; private set; }

	private object threadLockObject; 

	public MMPermissionReceiver_Android(object threadLockObject) : base("unity.swanob2.com.mobilemedia.MMPermissionReceiver")
	{
		Result = -1;
		this.threadLockObject = threadLockObject;
	}

	public void OnPermissionResult(int result)
	{
		Result = result;

		lock(threadLockObject)
		{
			Monitor.Pulse(threadLockObject);
		}
	}
}
#else
public class MMPermissionReceiver_Android{}
#endif