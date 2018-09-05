using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableTrigger : MonoBehaviour
{
	/// The condition/permission for triggering events in this script.
	public EventTriggerPermission m_EventTriggerPermission = EventTriggerPermission.NotReady;

	/// Do not trigger any event(in this script) earlier than this game time, game time is the time since the begin of the app started.
	/// (Why? Sometimes you need to wait for other scripts to completely initiated before calling their methods.)
	public float m_TiggerNotEarlyThanGameTime = 1f;

	public enum EventTriggerPermission
	{
		/// Do not trigger any event(in this script)
		NotReady = 0,

		/// Wait after the provided game time(m_TiggerNotEarlyThanGameTime)
		AfterGameTime, 

		/// Allow trigger events(in this script)
		Ready,
	}

	public UnityEvent m_OnEnableEvent;
	public UnityEvent m_OnDisableEvent;


	void OnEnable()
	{
		if(m_EventTriggerPermission == EventTriggerPermission.Ready || 
			(m_EventTriggerPermission == EventTriggerPermission.AfterGameTime && Time.time > m_TiggerNotEarlyThanGameTime))
		{
			m_OnEnableEvent.Invoke();
		}
	}
	

	void OnDisable()
	{
		if(m_EventTriggerPermission == EventTriggerPermission.Ready || 
			(m_EventTriggerPermission == EventTriggerPermission.AfterGameTime && Time.time > m_TiggerNotEarlyThanGameTime))
		{
			m_OnDisableEvent.Invoke();
		}
	}
}
