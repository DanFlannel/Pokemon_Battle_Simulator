/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// SWAN Demo Animation (Tweening).
/// </summary>
public class SDemoAnimation : MonoBehaviour
{
	private static SDemoAnimation _instance = null;
	public static SDemoAnimation Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new GameObject("[SDEMO Animation]").AddComponent<SDemoAnimation>();
			}
			return _instance;
		}
	}

	public enum LoopType
	{
		None = 0,
		Loop,
		PingPong,
	}

	public SDemoControl Move(GameObject targetGO, Vector3 fromPosition, Vector3 toPosition, float time, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Move(targetGO, fromPosition, toPosition, time, 0f, loop, onComplete, control));
		return control;
	}
	public SDemoControl Move(GameObject targetGO, Vector3 fromPosition, Vector3 toPosition, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Move(targetGO, fromPosition, toPosition, time, delay, loop, onComplete, control));
		return control;
	}
	private IEnumerator _Move(GameObject targetGO, Vector3 fromPosition, Vector3 toPosition, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		targetGO.transform.localPosition = fromPosition;
		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
				targetGO.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, (elapsedTime / time));
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		targetGO.transform.localPosition = toPosition;
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Move(targetGO, fromPosition, toPosition, time, delay, loop, onComplete, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_Move(targetGO, toPosition, fromPosition, time, delay, loop, onComplete, control));
	}

	public SDemoControl Scale(GameObject targetGO, Vector3 fromScale, Vector3 toScale, float time, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Scale(targetGO, fromScale, toScale, time, 0f, loop, onComplete, control));
		return control;
	}
	public SDemoControl Scale(GameObject targetGO, Vector3 fromScale, Vector3 toScale, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Scale(targetGO, fromScale, toScale, time, delay, loop, onComplete, control));
		return control;
	}
	private IEnumerator _Scale(GameObject targetGO, Vector3 fromScale, Vector3 toScale, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		targetGO.transform.localScale = fromScale;
		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
				targetGO.transform.localScale = Vector3.Lerp(fromScale, toScale, (elapsedTime / time));
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		targetGO.transform.localScale = toScale;
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Scale(targetGO, fromScale, toScale, time, delay, loop, onComplete, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_Scale(targetGO, toScale, fromScale, time, delay, loop, onComplete, control));
	}

	public SDemoControl Rotate(GameObject targetGO, Vector3 fromEulerAngle, Vector3 toEulerScale, float time, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Rotate(targetGO, fromEulerAngle, toEulerScale, time, 0f, loop, onComplete, control));
		return control;
	}
	public SDemoControl Rotate(GameObject targetGO, Vector3 fromEulerAngle, Vector3 toEulerScale, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Rotate(targetGO, fromEulerAngle, toEulerScale, time, delay, loop, onComplete, control));
		return control;
	}
	private IEnumerator _Rotate(GameObject targetGO, Vector3 fromEulerAngle, Vector3 toEulerAngle, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		targetGO.transform.localEulerAngles = fromEulerAngle;
		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
				targetGO.transform.localEulerAngles = Vector3.Lerp(fromEulerAngle, toEulerAngle, (elapsedTime / time));
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		targetGO.transform.localEulerAngles = toEulerAngle;
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Rotate(targetGO, fromEulerAngle, toEulerAngle, time, delay, loop, onComplete, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_Rotate(targetGO, toEulerAngle, fromEulerAngle, time, delay, loop, onComplete, control));
	}

	public SDemoControl FloatTo(float fromValue, float toValue, float time, Action<float> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_FloatTo(fromValue, toValue, time, 0f, onUpdate, loop, onComplete, control));
		return control;
	}
	public SDemoControl FloatTo(float fromValue, float toValue, float time, float delay, Action<float> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_FloatTo(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		return control;
	}
	private IEnumerator _FloatTo(float fromValue, float toValue, float time, float delay, Action<float> onUpdate, LoopType loop = LoopType.None, Action onComplete = null, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		float val = 0f;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
				val = Mathf.Lerp(fromValue, toValue, (elapsedTime / time));
				onUpdate(val);
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_FloatTo(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_FloatTo(toValue, fromValue, time, delay, onUpdate, loop, onComplete, control));
	}

	public SDemoControl Vector2To(Vector2 fromValue, Vector2 toValue, float time, Action<Vector2> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Vector2To(fromValue, toValue, time, 0f, onUpdate, loop, onComplete, control));
		return control;
	}
	public SDemoControl Vector2To(Vector2 fromValue, Vector2 toValue, float time, float delay, Action<Vector2> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Vector2To(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		return control;
	}
	private IEnumerator _Vector2To(Vector2 fromValue, Vector2 toValue, float time, float delay, Action<Vector2> onUpdate, LoopType loop = LoopType.None, Action onComplete = null, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		Vector2 val = Vector2.zero;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
				val = Vector2.Lerp(fromValue, toValue, (elapsedTime / time));
				onUpdate(val);
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Vector2To(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_Vector2To(toValue, fromValue, time, delay, onUpdate, loop, onComplete, control));
	}

	public SDemoControl Vector3To(Vector3 fromValue, Vector3 toValue, float time, Action<Vector3> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Vector3To(fromValue, toValue, time, 0f, onUpdate, loop, onComplete, control));
		return control;
	}
	public SDemoControl Vector3To(Vector3 fromValue, Vector3 toValue, float time, float delay, Action<Vector3> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Vector3To(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		return control;
	}
	private IEnumerator _Vector3To(Vector3 fromValue, Vector3 toValue, float time, float delay, Action<Vector3> onUpdate, LoopType loop = LoopType.None, Action onComplete = null, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		Vector3 val = Vector3.zero;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
				val = Vector3.Lerp(fromValue, toValue, (elapsedTime / time));
				onUpdate(val);
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Vector3To(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_Vector3To(toValue, fromValue, time, delay, onUpdate, loop, onComplete, control));
	}

	public SDemoControl Vector4To(Vector4 fromValue, Vector4 toValue, float time, Action<Vector4> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Vector4To(fromValue, toValue, time, 0f, onUpdate, loop, onComplete, control));
		return control;
	}
	public SDemoControl Vector4To(Vector4 fromValue, Vector4 toValue, float time, float delay, Action<Vector4> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Vector4To(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		return control;
	}
	private IEnumerator _Vector4To(Vector4 fromValue, Vector4 toValue, float time, float delay, Action<Vector4> onUpdate, LoopType loop = LoopType.None, Action onComplete = null, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		Vector4 val = Vector4.zero;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
				val = Vector4.Lerp(fromValue, toValue, (elapsedTime / time));
				onUpdate(val);
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Vector4To(fromValue, toValue, time, delay, onUpdate, loop, onComplete, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_Vector4To(toValue, fromValue, time, delay, onUpdate, loop, onComplete, control));
	}

	public SDemoControl Wait(float time, Action onComplete)
	{
		if(time < 0) return null;
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Wait(time, 0f, onComplete, LoopType.None, control));
		return control;
	}
	public SDemoControl Wait(float time, Action onComplete, LoopType loop = LoopType.None)
	{
		if(time < 0) return null;
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Wait(time, 0f, onComplete, loop, control));
		return control;
	}
	public SDemoControl Wait(float time, float delay, Action onComplete, LoopType loop = LoopType.None)
	{
		if(time < 0) return null;
		SDemoControl control = new SDemoControl();
		StartCoroutine(_Wait(time, delay, onComplete, loop, control));
		return control;
	}
	private IEnumerator _Wait(float time, float delay, Action onComplete, LoopType loop = LoopType.None, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				elapsedTime += Time.deltaTime;
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		if(onComplete != null) onComplete();

		if(loop == LoopType.Loop) StartCoroutine(_Wait(time, delay, onComplete, loop, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_Wait(time, delay, onComplete, loop, control));
	}

	public SDemoControl WaitFrames(int frameNum, Action onComplete)
	{
		if(frameNum < 0) return null;
		SDemoControl control = new SDemoControl();
		StartCoroutine(_WaitFrames(frameNum, 0f, onComplete, LoopType.None, control));
		return control;
	}
	public SDemoControl WaitFrames(int frameNum, Action onComplete, LoopType loop = LoopType.None)
	{
		if(frameNum < 0) return null;
		SDemoControl control = new SDemoControl();
		StartCoroutine(_WaitFrames(frameNum, 0f, onComplete, loop, control));
		return control;
	}
	public SDemoControl WaitFrames(int frameNum, float delay, Action onComplete, LoopType loop = LoopType.None)
	{
		if(frameNum < 0) return null;
		SDemoControl control = new SDemoControl();
		StartCoroutine(_WaitFrames(frameNum, delay, onComplete, loop, control));
		return control;
	}
	private IEnumerator _WaitFrames(int frameNum, float delay, Action onComplete, LoopType loop = LoopType.None, SDemoControl control = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		int currFrame = 0;
		while (currFrame < frameNum)
		{
			if(control.m_State == SDemoControl.State.Playing)
			{
				currFrame++;
			}
			else if(control.m_State == SDemoControl.State.Kill)
			{
				loop = LoopType.None;
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		if(control.m_State == SDemoControl.State.Kill) yield break;

		if(onComplete != null) onComplete();

		if(loop == LoopType.Loop) StartCoroutine(_WaitFrames(frameNum, delay, onComplete, loop, control));
		else if(loop == LoopType.PingPong) StartCoroutine(_WaitFrames(frameNum, delay, onComplete, loop, control));
	}

}