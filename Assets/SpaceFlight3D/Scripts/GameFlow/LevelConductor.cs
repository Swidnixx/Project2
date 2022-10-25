using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConductor : MonoBehaviour
{
    // These are quoted on each stage of a level
    public string[] quotes;

    // This will probably be refactored
    public Text displayText;

    int current = 0;

    private void Start()
    {
        
    }

    public void TriggerNext()
    {
        current++;
        if (current < quotes.Length)
        {
            displayText.text = quotes[current]; 
        }
        else
        {
            Finish();
        }
    }

    private void Finish()
    {
       // Destroy(gameObject);
    }
}
