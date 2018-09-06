using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace DFC
{
    public class EasyThread : MonoBehaviour
    {

        private static EasyThread instance;
        public static EasyThread Instance { get { return CheckInstance(); } }

        public int activeThreadCount;
        public static List<string> ThreadNames = new List<string>();

        private static List<Thread> concurrentThreads = new List<Thread>();
        private static List<RulyCanceler> cancelationTokens = new List<RulyCanceler>();

        //.. Instancing

        private static EasyThread CheckInstance()
        {
            if (instance == null)
            {

                GameObject go = new GameObject("EasyThread");
                instance = go.AddComponent<EasyThread>();
                if (!DFC.MainThreadInvoke.instanceExists)
                {
                    go.AddComponent<DFC.MainThreadInvoke>();
                }
            }
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(instance);
                    instance = this;
                }
            }

            if (!DFC.MainThreadInvoke.instanceExists)
            {
                this.gameObject.AddComponent<DFC.MainThreadInvoke>();
            }

        }

        //.. Unity Methods

        private void Start()
        {

        }

        private void Update()
        {
            for (int i = concurrentThreads.Count - 1; i > 0; i--)
            {
                if (!concurrentThreads[i].IsAlive || concurrentThreads[i].ThreadState == ThreadState.Stopped)
                {
                    ThreadNames.Remove(concurrentThreads[i].Name);
                    concurrentThreads.RemoveAt(i);
                }
            }
            activeThreadCount = concurrentThreads.Count;
        }

        //.. Public Threading Calls

        public static Thread CreateContinuousThread(Action action, string name, int sleep = 100, object locker = null)
        {
            CheckInstance();

            RulyCanceler canceler = new RulyCanceler();
            cancelationTokens.Add(canceler);

            Thread thread = new Thread(() =>
            {
                if (locker == null)
                {
                    InternalThreadContinuous(action, sleep, canceler);
                }
                else
                {
                    InternalThreadContinuous(action, sleep, locker, canceler);
                }
            });

            thread.Name = string.Format("{0} Thread", name);
            thread.IsBackground = true;
            concurrentThreads.Add(thread);
            ThreadNames.Add(thread.Name);
            thread.Start();

            return thread;
        }

        public static Thread CreateLoopingThread(Action action, string name, int times, int sleep = 100, object locker = null)
        {
            CheckInstance();

            RulyCanceler canceler = new RulyCanceler();
            cancelationTokens.Add(canceler);

            Thread thread = new Thread(() =>
            {
                if (locker == null)
                {
                    InternalThreadLoop(action, sleep, times, canceler);
                }
                else
                {
                    InternalThreadLoop(action, sleep, times, locker, canceler);
                }
            });

            thread.Name = string.Format("{0} Thread", name);
            thread.IsBackground = true;
            concurrentThreads.Add(thread);
            thread.Start();

            return thread;
        }

        public static Thread CreateSingleThread(Action action, string name, object lockObj = null)
        {
            CheckInstance();

            RulyCanceler canceler = new RulyCanceler();
            cancelationTokens.Add(canceler);

            Thread thread = new Thread(() =>
            {
                if (lockObj == null)
                {
                    InternalThreadMethodOnce(action, canceler);
                }
                else
                {
                    InternalThreadMethodOnce(action, lockObj, canceler);
                }
            });

            thread.Name = string.Format("{0} Thread", name);
            thread.IsBackground = true;
            concurrentThreads.Add(thread);
            ThreadNames.Add(thread.Name);
            thread.Start();

            return thread;
        }

        //.. Callbacks

        public static void ExecuteOnMainThread(Action action)
        {
            MainThreadInvoke.InvokeAsync(() =>
            {
                action();
            });
        }

        public static void ExecuteOnMainThread(object action)
        {
            MainThreadInvoke.InvokeAsync(() =>
            {
                Action a = (Action)action;
                a();
            });
        }

        //.. Action Overloads

        public static void ExecuteOnMainThread<T>(Action<T> action, T parameter)
        {
            MainThreadInvoke.InvokeAsync(() =>
            {
                action(parameter);
            });
        }

        public static void ExecuteOnMainThread<T1,T2>(Action<T1,T2> action, T1 p1, T2 p2)
        {
            MainThreadInvoke.InvokeAsync(() =>
            {
                action(p1,p2);
            });
        }

        public static void ExecuteOnMainThread<T1, T2, T3>(Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3)
        {
            MainThreadInvoke.InvokeAsync(() =>
            {
                action(p1, p2, p3);
            });
        }

        public static void ExecuteOnMainThread<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            MainThreadInvoke.InvokeAsync(() =>
            {
                action(p1, p2, p3, p4);
            });
        }

        //.. Continuous Threading

        private static void InternalThreadContinuous(Action action, int sleep, RulyCanceler canceler)
        {
            try
            {
                canceler.ThrowIfCancellationRequested();
                while (true)
                {
                    try
                    {
                        action();
                        canceler.ThrowIfCancellationRequested();
                        Thread.Sleep(sleep);
                    }
                    catch (OperationCanceledException) { /*this is expected if we cancel*/ }
                    catch (Exception ex)
                    {
                        Debug.Log(ex.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Operation Cancelled");
            }
        }

        private static void InternalThreadContinuous(Action action, int sleep, object lockObj, RulyCanceler canceler)
        {
            try
            {
                while (true)
                {
                    canceler.ThrowIfCancellationRequested();
                    if (Monitor.TryEnter(lockObj, TimeSpan.FromSeconds(2)))
                    {
                        try
                        {
                            action();
                            canceler.ThrowIfCancellationRequested();
                            Thread.Sleep(sleep);
                        }
                        catch (OperationCanceledException) { /*this is expected if we cancel*/ }
                        catch (Exception ex)
                        {
                            Debug.Log(ex.Message);
                        }
                        finally
                        {
                            Monitor.Exit(lockObj);
                        }
                    }
                    else
                    {
                        Thread.Sleep(sleep);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Operation Cancelled");
            }
        }
        
        //.. Looping Threading

        private static void InternalThreadLoop(Action action, int sleep, int times, RulyCanceler canceler)
        {
            try
            {
                canceler.ThrowIfCancellationRequested();
                while (times > 0)
                {
                    try
                    {
                        action();
                        canceler.ThrowIfCancellationRequested();
                        times--;
                        Thread.Sleep(sleep);
                    }
                    catch (OperationCanceledException) { /*this is expected if we cancel*/ }
                    catch (Exception ex)
                    {
                        Debug.Log(ex.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Operation Cancelled");
            }
        }

        private static void InternalThreadLoop(Action action, int sleep, int times, object lockObj, RulyCanceler canceler)
        {
            try
            {
                canceler.ThrowIfCancellationRequested();
                while (times > 0)
                {
                    if (Monitor.TryEnter(lockObj, TimeSpan.FromSeconds(2)))
                    {
                        try
                        {
                            action();
                            canceler.ThrowIfCancellationRequested();
                            times--;
                            Thread.Sleep(sleep);
                        }
                        catch (OperationCanceledException) { /*this is expected if we cancel*/ }
                        catch (Exception ex)
                        {
                            Debug.Log(ex.Message);
                        }
                        finally
                        {
                            Monitor.Exit(lockObj);
                        }
                    }
                    else
                    {
                        Thread.Sleep(sleep);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Operation Cancelled");
            }
        }

        //.. Single Threading

        private static void InternalThreadMethodOnce(Action action, RulyCanceler canceler)
        {
            if (canceler.IsCancellationRequested) { return; }
            action();
        }

        private static void InternalThreadMethodOnce(Action action, object lockObj, RulyCanceler canceler)
        {
            if (canceler.IsCancellationRequested) { return; }

            if (Monitor.TryEnter(lockObj, TimeSpan.FromSeconds(2)))
            {
                try
                {
                    action();
                }
                catch (OperationCanceledException) { /*this is expected if we cancel*/ }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
                finally
                {
                    Monitor.Exit(lockObj);
                }
            }
        }

        //.. Exit Saftey

        public static void StopAllThreads()
        {
            for (int i = 0; i < cancelationTokens.Count; i++)
            {
                cancelationTokens[i].Cancel();
            }
        }

        private void OnApplicationQuit()
        {
            StopAllThreads();
        }
    }
}
