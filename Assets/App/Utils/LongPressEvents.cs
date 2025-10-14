using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace App
{
    public class LongPressEvents : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public float holdTime = 1.0f;
    
        public UnityEvent onLongPress = new UnityEvent();
    
        public UnityEvent onClick = new UnityEvent();
    
        private float pressTime = 0f;
        private bool isPressing = false;
        private bool longPressTriggered = false;
        private bool isClick = false;

        void Update()
        {
            if (isPressing && !longPressTriggered)
            {
                pressTime += Time.deltaTime;
                if (pressTime >= holdTime)
                {
                    longPressTriggered = true;
                    onLongPress?.Invoke();
                    isClick = false;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressing = true;
            pressTime = 0f;
            longPressTriggered = false;
            isClick = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressing = false;
        
            if (isClick && !longPressTriggered)
            {
                onClick?.Invoke();
            }
        
            isClick = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPressing = false;
            isClick = false;
        }
    }
}
