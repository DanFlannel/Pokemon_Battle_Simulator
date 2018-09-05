/// <summary>
/// Created by SWAN DEV
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SDemoControl
{
	public State m_State = State.Playing;

	public enum State
	{
		Kill = 0,		// The tweening will be destroyed in the next update.
		Playing,
		Paused,
	}

	public SelfAnimation.SelfAnimType m_SelfAnimType = SelfAnimation.SelfAnimType.None;

//	public Vector3 initValue;
//	public Vector3 fromValue;
//	public Vector3 toValue;
//	public float time = 0.5f;
//	public float delay = 0f;
//	public SDemoAnimation.LoopType loop = SDemoAnimation.LoopType.None;
//	public bool destroyOnComplete = false;
//	public bool executeAtStart = true;
//	public bool enableInitValue = false;
//
//	public UnityEvent onComplete;

}
