using System;
using UnityEngine;

namespace SpaceFlight3D.UI
{
    public class Carousel : MonoBehaviour
    {
        public CarouselItem[] allItems;
        int current;
        bool transitionPending;

        private void Start()
        {
            Array.ForEach<CarouselItem>(allItems, item => item.TurnGameObjectOff());

            allItems[current].TurnGameObjectOn();
        }
        void OnEnable()
        {
            //if(MenuUI.Instance)
            //    MenuUI.Instance.playerInput.actions["UI/Navigate"].performed += ArrowsNavigation;
        }

        private void OnDisable()
        {
            //if (MenuUI.Instance.playerInput)
            //{
            //    MenuUI.Instance.playerInput.actions["UI/Navigate"].performed -= ArrowsNavigation; 
            //}
        }

        void ArrowsNavigation(Vector2 input/*InputAction.CallbackContext ctx*/)
        {
            //Vector2 input = ctx.ReadValue<Vector2>();
            //Debug.Log("Navigate action performed: " + input);
            float x = input.x;
            if (x < 0)
            {
                GoLeft();
            }
            else if (x > 0)
            {
                GoRight();
            }
        }


        public void GoRight()
        {
            if (transitionPending) return;
            transitionPending = true;

            allItems[current].GoLeft();
            if (allItems.Length > current + 1)
            {
                current++;
            }
            else
            {
                current = 0;
            }
            allItems[current].TurnGameObjectOn();
            allItems[current].FromRight(Reset);
        }

        public void GoLeft()
        {
            if (transitionPending) return;
            transitionPending = true;

            allItems[current].GoRight();
            if (current > 0)
            {
                current--;
            }
            else
            {
                current = allItems.Length - 1;
            }
            allItems[current].gameObject.SetActive(true);
            allItems[current].FromLeft(Reset);
        }

        private void Reset()
        {
            transitionPending = false;
            // When transition is done, select first active selectable item
            MenuUI.Instance.RefreshSelectables();
        }
    } 
}
