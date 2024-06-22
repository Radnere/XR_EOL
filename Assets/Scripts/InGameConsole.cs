using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InGameConsole : MonoBehaviour
{
    public Text consoleText; // Assign a UI Text element in the Inspector
    private List<string> logMessages = new List<string>();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logMessages.Add(logString);
        if (logMessages.Count > 20)
        {
            logMessages.RemoveAt(0);
        }

        consoleText.text = string.Join("\n", logMessages.ToArray());
    }
}