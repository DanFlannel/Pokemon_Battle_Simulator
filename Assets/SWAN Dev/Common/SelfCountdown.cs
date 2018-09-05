/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SelfCountdown : MonoBehaviour
{
	public SDemoControl m_Control = null;

	public float time = 0.5f;
	public SDemoAnimation.LoopType loop = SDemoAnimation.LoopType.Loop;
	public bool destroyOnComplete = false;
	public bool executeAtStart = true;

	public UnityEvent onComplete;

	void Start()
	{
		if(!executeAtStart) return;
		StartAnimation();
	}

	void OnComplete()
	{
		if(onComplete != null) onComplete.Invoke();
		if(destroyOnComplete) GameObject.Destroy(gameObject);
	}

	void OnEnable()
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Playing;
	}

	void OnDisable()
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Paused;
	}

	void OnDestroy()
	{
		if(m_Control != null) m_Control.m_State = SDemoControl.State.Kill;
	}

	public void StartAnimation()
	{
		m_Control = SDemoAnimation.Instance.Wait(time, OnComplete, loop);
	}
}
