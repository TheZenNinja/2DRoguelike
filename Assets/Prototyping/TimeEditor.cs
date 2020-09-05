using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
public class TimeEditor : EditorWindow
{
    private static string time = "1";
    [MenuItem("Window/Time Editor")]
    public static void ShowWindow()
    {
        GetWindow<TimeEditor>("Time Editor");
        time = Time.timeScale.ToString();
    }
    private void OnGUI()
    {
        GUILayout.Label("Time Manager", EditorStyles.boldLabel);

        float timeScale = Time.timeScale;
        GUILayout.Label("Current Timescale: " + timeScale);

        time = GUILayout.TextField(time);
        time = Regex.Replace(time, "[^0-9.]", "");

        if (GUILayout.Button("Change Time"))
        {
            Time.timeScale = float.Parse(time);
        }
            if (GUILayout.Button("Reset"))
                Time.timeScale = 1;
    }
}
