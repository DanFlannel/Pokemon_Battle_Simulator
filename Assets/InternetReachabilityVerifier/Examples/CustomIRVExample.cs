// Custom method example for InternetReachabilityVerifier.
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

[RequireComponent(typeof(InternetReachabilityVerifier))]
public class CustomIRVExample : MonoBehaviour
{
    InternetReachabilityVerifier irv;
    string log = "";

    void appendLog(string s, bool error = false)
    {
        log += s + "\n";
        if (error)
            Debug.LogError(s, this);
        else
            Debug.Log(s, this);
    }

    bool verifyNetCheckData(WWW www, string customMethodExpectedData)
    {
        // Example validation - require that given custom string is not empty
        // and that it appears at some place in the returned document.
        if (customMethodExpectedData == null ||
            customMethodExpectedData.Length == 0)
        {
            appendLog("Custom verifyNetCheckData - Null or empty customMethodExpectedData!");
            return false;
        }
        bool result = www.text.Contains(customMethodExpectedData);
        appendLog("Custom verifyNetCheckData - result:" + result + ", customMethodExpectedData:" + customMethodExpectedData + ", www.text:" + www.text);
        return result;
    }

    void netStatusChanged(InternetReachabilityVerifier.Status newStatus)
    {
        appendLog("Net status changed: " + newStatus);
        if (newStatus == InternetReachabilityVerifier.Status.Error)
        {
            string error = irv.lastError;
            appendLog("Error: " + error);
            if (error.Contains("no crossdomain.xml"))
            {
                appendLog("See http://docs.unity3d.com/462/Documentation/Manual/SecuritySandbox.html - You should also check WWW Security Emulation Host URL of Unity Editor in Edit->Project Settings->Editor");
            }
        }
    }

    void Start()
    {
        irv = GetComponent<InternetReachabilityVerifier>();
        irv.customMethodVerifierDelegate = verifyNetCheckData;
        irv.statusChangedDelegate += netStatusChanged;

        appendLog("CustomIRVExample log:\n");
        appendLog("Selected method: " + irv.captivePortalDetectionMethod);
        appendLog("Custom Method URL: " + irv.customMethodURL);
        appendLog("Custom Method Expected Data: " + irv.customMethodExpectedData);
        if (irv.customMethodVerifierDelegate != null)
            appendLog("Using custom method verifier delegate.");
        if (irv.customMethodURL.Contains("strobotnik.com"))
            appendLog("*** IMPORTANT WARNING: ***\nYou're using the default TEST value for Custom Method URL specified in example scene. THAT SERVER HAS NO GUARANTEE OF BEING UP AND RUNNING. Please use your own custom server and URL!\n*****\n", true);
    }

    Vector2 scrollPos;

    void OnGUI()
    {
        GUI.color = new Color(0.9f, 0.95f, 1.0f);
        GUILayout.Label("Strobotnik InternetReachabilityVerifier for Unity");
        GUILayout.BeginHorizontal();
        GUI.color = new Color(0.7f, 0.8f, 0.9f);
        GUILayout.Label("Status: ");
        GUI.color = Color.white;
        GUILayout.Label("" + irv.status);
        GUILayout.EndHorizontal();

        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.Label(log);
        GUILayout.EndScrollView();
    }
}
