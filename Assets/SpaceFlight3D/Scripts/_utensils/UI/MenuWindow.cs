using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
{

    public void SetActive(bool onOff)
    {
        gameObject.SetActive(onOff);
        if (onOff)
        {
            SelectFirstSelectableElement();
        }
    }

    private void SelectFirstSelectableElement() //Depth First Search
    {
        Selectable selected = GetComponentInChildren<Selectable>(false);
        if (selected == null) return;
        selected.Select();
       // Debug.Log(selected + " selected from " + gameObject);
    }

    
}