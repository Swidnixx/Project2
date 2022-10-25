using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelDisplay : TextMessageDisplayer
{
    public FuelTank tank;

    public Color fullColor;
    public Color emptyColor;
    public Image backgroundImage;

    Slider displayerSlider;

    private void Start()
    {
        displayerSlider = GetComponent<Slider>();
    }

    void Update()
    {
        displayerSlider.value = tank.Status;
        backgroundImage.color = Color.Lerp(emptyColor, fullColor, displayerSlider.value);

        if(tank.Status == 0)
        {
            DisplayMessage("OUT\n\nOF\n\nFUEL");
        }
        else
        {
            HideMessage();
        }
    }
}
