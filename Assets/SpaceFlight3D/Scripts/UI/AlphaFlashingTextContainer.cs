using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceFlight
{
    public class AlphaFlashingTextContainer : MonoBehaviour
    {
        public float Timer { get { return timer; } set { timer = value; } }
        [SerializeField]float timer = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Header("Settings")]
        public List<TextAlpha> textList = new List<TextAlpha>();

        Color[] currentColor;
        Text[] thisText;
        float[] chAlpha;
        int textCount;


        void Awake()
        {
            //---------------------------------

            textCount = textList.Count;

            //---------------------------------

            currentColor = new Color[textCount];
            thisText = new Text[textCount];
            chAlpha = new float[textCount];

            //---------------------------------

            for (int num = 0; num < textCount; num++)
            {
                thisText[num] = textList[num].text.GetComponent<Text>();
                currentColor[num] = thisText[num].color;
            }

            //---------------------------------
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        void LateUpdate()
        {
            //---------------------------------

            for (int num = 0; num < textCount; num++)
            {
                chAlpha[num] = textList[num].minAlpha + Mathf.PingPong(Time.time / Timer, textList[num].maxAlpha - textList[num].minAlpha);
                thisText[num].color = new Color(textList[num].text.color.r, textList[num].text.color.g, textList[num].text.color.b, Mathf.Clamp(chAlpha[num], 0.0f, 1.0f));
            }

            //---------------------------------
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    [System.Serializable]
    public class TextAlpha
    {
        public Text text;
        public float minAlpha = 0.1f;
        public float maxAlpha = 0.9f;
    }
}