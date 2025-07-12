using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectLogger : MonoBehaviour
{
    public Text logText;  // Assign EffectsLog (UI Text) here

    public void LogEffect(string message)
    {
        if (logText != null)
            logText.text = message;
    }
}
