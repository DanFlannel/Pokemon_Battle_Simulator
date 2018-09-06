using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class CSVSaver : MonoBehaviour
{
    public static CSVSaver Instance { get { return instance; } }
    private static CSVSaver instance;

    [SerializeField]
    private bool showDebugs = false;

    public float cvsTimer = 0;
    public float csvPostInterval = 60;

    public List<string> sessionCalls = new List<string>();
    public static string savedCSVPath;

    private Thread saveThread;
    private Mutex mutex = new Mutex();

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
        savedCSVPath = Application.persistentDataPath + "CSV";
    }

    private void Start()
    {
        saveThread = new Thread(SaveToCsv);
        cvsTimer = csvPostInterval;
    }

    private void Update()
    {
        if (cvsTimer != 0)
        {
            cvsTimer -= Time.deltaTime;
            if (cvsTimer <= 0)
            {
                if (!saveThread.IsAlive)
                {
                    cvsTimer = csvPostInterval;
                    saveThread = new Thread(SaveToCsv);
                    saveThread.Start();
                }
            }
        }
    }

    public static void postLocally(string call)
    {
        if(instance == null)
        {
            GameObject go = new GameObject("CSVSaver");
            instance = go.AddComponent<CSVSaver>();
            savedCSVPath = Application.persistentDataPath + "CSV";
        }
        string timeStamp = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
        call += "," + timeStamp;
        CSVSaver.instance.sessionCalls.Add(call);
    }

    public static void postLocally(List<string> calls, bool onOneLine = false)
    {
        string timeStamp = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
        for (int i = 0; i < calls.Count; i++)
        {
            calls[i] += "," + timeStamp;
        }
        if (!onOneLine)
        {
            foreach (var call in calls)
            {
                CSVSaver.instance.sessionCalls.Add(call);
            }
        }
        else
        {
            StringBuilder textOutput = new StringBuilder("");
            foreach (var call in calls)
            {
                textOutput.Append(call + ",");
            }

            CSVSaver.instance.sessionCalls.Add(textOutput.ToString());
        }
    }

    public static void postLocally(string[] calls, bool onOneLine = false)
    {
        List<string> list = calls.ToList();
        postLocally(list, onOneLine);
    }

    private void SaveToCsv()
    {
        if (sessionCalls.Count > 0)
        {
            mutex.WaitOne();
            List<string> calls = new List<string>();
            calls = sessionCalls;
            sessionCalls = new List<string>();

            StringBuilder textOutput = new StringBuilder("");
            foreach (var call in calls)
            {
                textOutput.AppendLine(call);
            }
            string currentDate = DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Year.ToString();
            if (!Directory.Exists(savedCSVPath))
            {
                Directory.CreateDirectory(savedCSVPath);
            }
            if (File.Exists(savedCSVPath + "CSVData_" + currentDate + ".csv"))
            {
                File.AppendAllText(savedCSVPath + "CSVData_" + currentDate + ".csv", textOutput.ToString());
            }
            else
            {
                File.WriteAllText(savedCSVPath + "CSVData_" + currentDate + ".csv", textOutput.ToString());
            }
            mutex.ReleaseMutex();
            if (showDebugs)
            {
                Debug.Log("Posted to cvs file at " + DateTime.Now + " at " + savedCSVPath);
            }
        }
    }

    private void OnApplicationQuit()
    {
        cvsTimer = csvPostInterval;
        if (saveThread != null && !saveThread.IsAlive)
        {
            saveThread = new Thread(SaveToCsv);
            saveThread.Start();
        }
    }
}