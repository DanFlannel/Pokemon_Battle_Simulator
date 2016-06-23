﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CoroutineQueueHelper
{
    /// <summary>
    /// This class was made in order to be able to run a set of Coroutines in a FIFO
    /// manner. This class needs to be expanded to add states of the masterIEnumerator.
    /// </summary>
    public class CoroutineList : MonoBehaviour
    {
        private List<IEnumerator> CoroutineQueue = new List<IEnumerator>();
        private bool isRunning = false;
        public int lengthOfQueue;

        public void StartQueue()
        {
            lengthOfQueue = CoroutineQueue.Count;
            StartCoroutine(masterIEnumerator());
        }

        public void StopQueue()
        {
            StopCoroutine(masterIEnumerator());   
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

        IEnumerator masterIEnumerator()
        {
            isRunning = true;
            int i = 0;
            while(i < CoroutineQueue.Count && isRunning)
            {
                //Debug.Log("Doing IEnum index: " + i);
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