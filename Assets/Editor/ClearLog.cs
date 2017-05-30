using UnityEditor;
using System.Reflection;
using System;

public static class ClearLog {


    public static void ClearLogConsole()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type logEntries = assembly.GetType("UnityEditorInternal.LogEntries");
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
    }
}

