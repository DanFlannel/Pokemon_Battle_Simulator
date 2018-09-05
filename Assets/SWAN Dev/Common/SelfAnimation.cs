/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SelfAnimation : MonoBehaviour
{
	public SDemoControl m_Control = null;

	public enum SelfAnimType{
		None = 0,
		Move,
		Rotate,
		Scale,
	}

	public SelfAnimType m_SelfAnimType = SelfAnimType.None;
	public SDemoAnimation.LoopType loop = SDemoAnimation.LoopType.None;

	//public Vector3 initValue;
	public Vector3 fromValue;
	public Vector3 toValue;

	public float time = 0.5f;
	public float delay = 0f;
	public float delay_Revert = 0f;

	public bool executeAtStart = true;
	public bool enableInitValue = false;

	public bool destroyOnComplete = false;

	public UnityEvent onComplete;

	void Awake()
	{
		if(executeAtStart) StartAnimation();
	}

	void OnEnable()
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Playing;
	}

	void OnDisable()
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Paused;
	}

	void OnComplete()
	{
		if(onComplete != null) onComplete.Invoke();
		if(destroyOnComplete) GameObject.Destroy(gameObject);
	}

	void OnDestroy()
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Kill;
	}

	private bool _isOdd = false;
	public void SwitchAnimation()
	{
		_isOdd = !_isOdd;
		if(_isOdd)
		{
			StartAnimation(delay);
		}
		else
		{
			StartAnimationRevert(delay_Revert);
		}
	}
	public void SwitchAnimationRevert()
	{
		_isOdd = !_isOdd;
		if(!_isOdd)
		{
			StartAnimation(delay);
		}
		else
		{
			StartAnimationRevert(delay_Revert);
		}
	}

	public void StartAnimation(float inDelay = 0f)
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Kill; // Kill the current tweening if existed

		switch(m_SelfAnimType)
		{
		case SelfAnimType.Move:
			if(enableInitValue) gameObject.transform.localPosition = fromValue;
			m_Control = SDemoAnimation.Instance.Move(gameObject, fromValue, toValue, time, delay, loop, OnComplete);
			break;

		case SelfAnimType.Rotate:
			if(enableInitValue) gameObject.transform.localEulerAngles = fromValue;
			m_Control = SDemoAnimation.Instance.Rotate(gameObject, fromValue, toValue, time, delay, loop, OnComplete);
			break;

		case SelfAnimType.Scale:
			if(enableInitValue) gameObject.transform.localScale = fromValue;
			m_Control = SDemoAnimation.Instance.Scale(gameObject, fromValue, toValue, time, delay, loop, OnComplete);
			break;
		}
	}

	public void StartAnimationRevert(float inDelay = 0f)
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Kill; // Kill the current tweening if existed

        switch (m_SelfAnimType)
		{
		case SelfAnimType.Move:
			if(enableInitValue) gameObject.transform.localPosition = toValue;
			m_Control = SDemoAnimation.Instance.Move(gameObject, toValue, fromValue, time, inDelay, loop, OnComplete);
			break;

		case SelfAnimType.Rotate:
			if(enableInitValue) gameObject.transform.localEulerAngles = toValue;
			m_Control = SDemoAnimation.Instance.Rotate(gameObject, toValue, fromValue, time, inDelay, loop, OnComplete);
			break;

		case SelfAnimType.Scale:
			if(enableInitValue) gameObject.transform.localScale = toValue;
			m_Control = SDemoAnimation.Instance.Scale(gameObject, toValue, fromValue, time, inDelay, loop, OnComplete);
			break;
		}
	}
}