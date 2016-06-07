using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class was pulled from the Unity Answer on: 
/// http://answers.unity3d.com/questions/1040319/whats-the-proper-way-to-queue-and-space-function-c.html
/// The author of this class is username Bunny83 on the Unity Forums
/// </summary>
public class CoroutineQueue : MonoBehaviour {

    MonoBehaviour m_Owner = null;
    Coroutine m_InternalCoroutine = null;
    Queue<IEnumerator> actions = new Queue<IEnumerator>();
    public CoroutineQueue(MonoBehaviour aCoroutineOwner)
    {
        m_Owner = aCoroutineOwner;
    }
    public void StartLoop()
    {
        m_InternalCoroutine = m_Owner.StartCoroutine(Process());
    }
    public void StopLoop()
    {
        m_Owner.StopCoroutine(m_InternalCoroutine);
        m_InternalCoroutine = null;
    }
    public void EnqueueAction(IEnumerator aAction)
    {
        actions.Enqueue(aAction);
    }

    private IEnumerator Process()
    {
        while (true)
        {
            if (actions.Count > 0)
                yield return m_Owner.StartCoroutine(actions.Dequeue());
            else
                yield return null;
        }
    }
}
