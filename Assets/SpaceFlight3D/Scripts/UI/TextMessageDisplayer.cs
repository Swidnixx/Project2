using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMessageDisplayer : MonoBehaviour
{
    public Text targetText;
    public Color textMessageColor;

    public void DisplayMessage(string message)
    {
        targetText.color = textMessageColor;
        targetText.text = message;
        targetText.enabled = true;
    }

    public void HideMessage()
    {
        targetText.text = "";
        targetText.enabled = false;
    }
}
