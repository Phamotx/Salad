using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtils : MonoBehaviour{

	public static void Log(string value)
    {
        Debug.Log(value);
    }

    public static void LogError(string value)
    {
        Debug.LogError(value);
    }

    public static void LogWarning(string value)
    {
        Debug.LogWarning(value);
    }
}
