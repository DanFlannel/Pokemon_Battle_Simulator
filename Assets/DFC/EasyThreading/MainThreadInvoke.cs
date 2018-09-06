using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DFC
{
    public class MainThreadInvoke : MonoBehaviour
    {
        private static MainThreadInvoke instance;

        public static bool instanceExists;

        private static Thread main;
        private static object locker = new object();
        private static readonly Queue<Action> actions = new Queue<Action>();

        public static bool isMainThread
        {
            get
            {
                return Thread.CurrentThread == main;
            }
        }

        private void Awake()
        {
            if (instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                instance = this;
                instanceExists = true;
                main = Thread.CurrentThread;
            }
        }

        private void Update()
        {
            lock (locker)
            {
                while (actions.Count > 0)
                {
                    actions.Dequeue()();
                }
            }
        }

        public static void InvokeAsync(Action action)
        {
            if (!instanceExists)
            {
                Debug.LogError("No Dispatcher exists in the scene. Actions will not be invoked!");
                return;
            }

            if (isMainThread)
            {
                action();
            }
            else
            {
                lock (locker)
                {
                    actions.Enqueue(action);
                }
            }
        }

        public static void Invoke(Action action)
        {
            if (!instanceExists)
            {
                Debug.LogError("No Dispatcher exists in the scene. Actions will not be invoked!");
                return;
            }

            bool hasRun = false;

            InvokeAsync(() =>
            {
                action();
                hasRun = true;
            });

            while (!hasRun)
            {
                Thread.Sleep(5);
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
                instanceExists = false;
            }
        }
    }
}