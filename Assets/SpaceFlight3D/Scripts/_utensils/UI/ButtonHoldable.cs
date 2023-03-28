using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpaceFlight3D.UI
{
    public class ButtonHoldable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent onDown;
        public UnityEvent onUp;

        public void OnPointerDown(PointerEventData data)
        {
            onDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData data)
        {
            onUp?.Invoke();
        }

    }
}
