// Example for InternetReachabilityVerifier.
//
// Copyright 2014-2016 Jetro Lauha (Strobotnik Ltd)
// http://strobotnik.com
// http://jet.ro
//
// $Revision: 1309 $
//
// File version history:
// 2014-06-18, 1.0.0 - Initial version
// 2016-08-27, 1.1.0 - Minor changes.

using UnityEngine;
using System.Collections;

public class IRVExample : MonoBehaviour
{
    public InternetReachabilityVerifier internetReachabilityVerifier;

    string log = "";
    bool logChosenDefaultByPlatformMethodPending;
    string url = "https://www.google.com";
    WWW testWWW;

    void appendLog(string s)
    {
        log += s + "\n";
        Debug.Log(s, this);
    }

    void netStatusChanged(InternetReachabilityVerifier.Status newStatus)
    {
        appendLog("Net status changed: " + newStatus);
        if (newStatus == InternetReachabilityVerifier.Status.Error)
        {
            string error = internetReachabilityVerifier.lastError;
            appendLog("Error: " + error);
            if (error.Contains("no crossdomain.xml"))
            {
                appendLog("See http://docs.unity3d.com/462/Documentation/Manual/SecuritySandbox.html - You should also check WWW Security Emulation Host URL of Unity Editor in Edit->Project Settings->Editor");
            }
        }
    }

    void Start()
    {
        if (internetReachabilityVerifier == null)
        {
            internetReachabilityVerifier = (InternetReachabilityVerifier)GameObject.FindObjectOfType(typeof(InternetReachabilityVerifier));
            if (internetReachabilityVerifier == null)
            {
                Debug.LogError("No Internet Reachability Verifier set up for the IRVExample and none can be found in the scene!", this);
                return;
            }
        }

        internetReachabilityVerifier.statusChangedDelegate += netStatusChanged;

        appendLog("IRVExample log:\n");
        appendLog("(Initially selected method: " + internetReachabilityVerifier.captivePortalDetectionMethod + ")");

        if (internetReachabilityVerifier.captivePortalDetectionMethod ==
            InternetReachabilityVerifier.CaptivePortalDetectionMethod.DefaultByPlatform)
        {
            // InternetReachabilityVerifier Start() has not been run yet,
            // which will determine the actual method. We want to show that
            // in the log and OnGUI here, so this flag indicates it's pending.
            logChosenDefaultByPlatformMethodPending = true;
        }

        selectedMethod = (int)internetReachabilityVerifier.captivePortalDetectionMethod;
        int methodCount = 9;
        methodNames = new string[methodCount];
        for (int a = 0; a < methodCount; ++a)
            methodNames[a] = ((InternetReachabilityVerifier.CaptivePortalDetectionMethod)a).ToString();
    }


    string[] methodNames;
    int selectedMethod = 0;
    Vector2 scrollPos;

    void OnGUI()
    {
        if (logChosenDefaultByPlatformMethodPending &&
            internetReachabilityVerifier.captivePortalDetectionMethod != InternetReachabilityVerifier.CaptivePortalDetectionMethod.DefaultByPlatform)
        {
            appendLog("DefaultByPlatform selected, actual method: " + internetReachabilityVerifier.captivePortalDetectionMethod);
            logChosenDefaultByPlatformMethodPending = false;
        }

        GUI.color = new Color(0.9f, 0.95f, 1.0f);
        GUILayout.Label("Strobotnik InternetReachabilityVerifier for Unity");

        GUILayout.Label("Selected method: (changes to actual method as needed)");
        selectedMethod = (int)internetReachabilityVerifier.captivePortalDetectionMethod;
        int newSelectedMethod = GUILayout.SelectionGrid(selectedMethod, methodNames, 2);
        if (selectedMethod != newSelectedMethod)
        {
            selectedMethod = newSelectedMethod;
            internetReachabilityVerifier.captivePortalDetectionMethod = (InternetReachabilityVerifier.CaptivePortalDetectionMethod)selectedMethod;
            if (selectedMethod == (int)InternetReachabilityVerifier.CaptivePortalDetectionMethod.DefaultByPlatform)
                logChosenDefaultByPlatformMethodPending = true;
            else if (selectedMethod == (int)InternetReachabilityVerifier.CaptivePortalDetectionMethod.Custom)
            {
                appendLog("Using custom method " +
                    (internetReachabilityVerifier.customMethodWithCacheBuster ? "with cache buster, base url:\n" : "without cache buster, url:\n") +
                    internetReachabilityVerifier.customMethodURL);
            }
        }

        if (GUILayout.Button("Force reverification"))
        {
            // *****
            // This is how you force "pending verification" status
            // so that Internet access will be tested again.
            internetReachabilityVerifier.forceReverification();
            // (you should do this if you know some other way that
            //  network connectivity has been lost or something)
            // *****
        }

        GUILayout.BeginHorizontal();
        GUI.color = new Color(0.7f, 0.8f, 0.9f);
        GUILayout.Label("Status: ");
        GUI.color = Color.white;
        GUILayout.Label("" + internetReachabilityVerifier.status);
        GUILayout.EndHorizontal();
        GUI.color = new Color(0.7f, 0.8f, 0.9f);
        GUILayout.Label("Test WWW access:");

        // *****
        // This is how you check if internet access is verified:
        bool netVerified = (internetReachabilityVerifier.status == InternetReachabilityVerifier.Status.NetVerified);
        // *****

        GUILayout.BeginHorizontal();
        if (!netVerified || (testWWW != null && !testWWW.isDone))
            GUI.enabled = false;
        if (GUILayout.Button("Fetch"))
        {
            testWWW = new WWW(url);
        }
        if (testWWW != null && !testWWW.isDone)
            GUI.enabled = false;
        else
            GUI.enabled = true;
        url = GUILayout.TextField(url);
        GUI.enabled = true;
        GUILayout.EndHorizontal();

        string testStatus = "";
        if (testWWW != null)
        {
            if (testWWW.error != null && testWWW.error.Length > 0)
                testStatus = "error:" + testWWW.error;
            else if (testWWW.isDone)
                testStatus = "done";
            else
                testStatus = "progress:" + (int)(testWWW.progress * 100) + "%";
        }
        GUILayout.Label(testStatus);

        GUI.color = Color.white;
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.Label(log);
        GUILayout.EndScrollView();
    }
}
