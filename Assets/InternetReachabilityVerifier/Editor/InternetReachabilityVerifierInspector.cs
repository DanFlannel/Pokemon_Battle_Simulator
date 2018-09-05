// Copyright 2017 Jetro Lauha (Strobotnik Ltd)
//
// $Revision: 1054 $
//
// File version history:
// 2017-04-06, 1.1.1 - First version of the custom inspector.

#if !UNITY_3_5

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InternetReachabilityVerifier))]
public class InternetReachabilityVerifierInspector : Editor
{
    public override void OnInspectorGUI()
    {
        InternetReachabilityVerifier irv = (InternetReachabilityVerifier)target;
        UnityEditor.MessageType mt = MessageType.Warning;

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("captivePortalDetectionMethod"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("customMethodURL"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("customMethodExpectedData"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("customMethodWithCacheBuster"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dontDestroyOnLoad"), true);

        mt = MessageType.None;
        if (irv.defaultCheckPeriod < 1 / 5.0f) mt = MessageType.Error;
        else if (irv.defaultCheckPeriod < 1) mt = MessageType.Warning;
        if (mt != MessageType.None)
            EditorGUILayout.HelpBox("Suggested minimum for Default Check Period is 1 second.\nUsing too small value can degrade performance.", mt);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultCheckPeriod"), true);

        mt = MessageType.None;
        if (irv.errorRetryDelay < 1 / 2.0f) mt = MessageType.Error;
        else if (irv.errorRetryDelay < 3) mt = MessageType.Warning;
        if (mt != MessageType.None)
            EditorGUILayout.HelpBox("Suggested minimum for Error Retry Delay is 3 seconds.\nUsing too small value can degrade performance.", mt);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("errorRetryDelay"), true);

        mt = MessageType.None;
        if (irv.mismatchRetryDelay < 1 / 3.0f) mt = MessageType.Error;
        else if (irv.mismatchRetryDelay < 2) mt = MessageType.Warning;
        if (mt != MessageType.None)
            EditorGUILayout.HelpBox("Suggested minimum for Mismatch Retry Delay is 2 seconds.\nUsing too small value can degrade performance.", mt);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mismatchRetryDelay"), true);

        serializedObject.ApplyModifiedProperties();
    }
}

#endif // !UNITY_3_5
