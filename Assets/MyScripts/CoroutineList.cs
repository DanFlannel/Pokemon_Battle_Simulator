using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CoroutineQueueHelper
{
    public class CoroutineList : MonoBehaviour
    {
        private List<IEnumerator> CoroutineQueue = new List<IEnumerator>();
        private bool isRunning;
        private bool isCoroutineRunning;
        public int lengthOFQueue;

        // Use this for initialization
        public void CoroutineStart()
        {
            Init();
        }

        void Init()
        {
            isRunning = false;
        }

        public void StartQueue()
        {
            lengthOFQueue = CoroutineQueue.Count;
            StartCoroutine(masterEnumerator());
        }

        public void StopQueue()
        {
            StopCoroutine(masterEnumerator());   
        }

        public void AddCoroutineToQueue(IEnumerator addThis)
        {
            CoroutineQueue.Add(addThis);
        }

        public void ClearQueue()
        {
            CoroutineQueue.Clear();
        }

        public void RemoveAt(int index)
        {
            if (CoroutineQueue.Count <= index)
            {
                CoroutineQueue.RemoveAt(index);
            }
            else
            {
                Debug.LogWarning("Index not found");
            }
        }

        public bool isQueueRunning()
        {
            return isRunning;
        }

        IEnumerator masterEnumerator()
        {
            isRunning = true;
            int i = 0;
            while(i < CoroutineQueue.Count && isRunning)
            {
                Debug.Log("Doing IEnum index: " + i);
                IEnumerator temp = CoroutineQueue[i];
                while (temp.MoveNext())
                {
                    yield return null;
                }
                i++;
            }
            isRunning = false;
            ClearQueue();
        }
    }
}
