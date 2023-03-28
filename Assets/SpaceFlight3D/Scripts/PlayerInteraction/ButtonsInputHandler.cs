using SpaceFlight3D.UI;
using UnityEngine.UI;

internal class ButtonsInputHandler : InputHandler
{
    private bool buttonPressed;

    internal void Setup(ButtonHoldable buttonLeft, ButtonHoldable buttonRight, Slider handle)
    {
        SetThrust(handle.value);
        handle.onValueChanged.AddListener( SetThrust );
        buttonLeft.onDown.AddListener(SetLeft);
        buttonRight.onDown.AddListener(SetRight);

        buttonLeft.onUp.AddListener(ResetLeftRight);
        buttonRight.onUp.AddListener(ResetLeftRight);
    }

    protected override void Update()
    {

    }

    void ResetLeftRight()
    {
        leftRight = 0;
    }
    void SetThrust(float value)
    {
        upwards = value;
    }

    void SetLeft()
    {
        leftRight = -1;
    }

    void SetRight()
    {
        leftRight = 1;
    }
}