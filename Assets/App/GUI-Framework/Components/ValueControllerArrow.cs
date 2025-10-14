using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Components
{


    public class ValueControllerArrow : MonoBehaviour
    {
        public enum ArrowDirection
        {
            Decrease,
            Increase,
        }

        public ValueController controller;
        public ArrowDirection arrowDirection = ArrowDirection.Decrease;

        public Button button;

        public void OnEnable()
        {
            controller.OnFieldChange += SetArrowActivation;

            button.onClick.AddListener(SetValue);
        }
        public void OnDisable()
        {
            controller.OnFieldChange -= SetArrowActivation;

            button.onClick.RemoveListener(SetValue);
        }

        private void SetValue()
        {
            if(arrowDirection == ArrowDirection.Decrease)
            {
                controller.ValueDecrease();
            }
            else
            {
                controller.ValueIncrease();
            }
        }
        private void SetArrowActivation(ValueController.ValueField field)
        {
            if(field == ValueController.ValueField.maximum && arrowDirection == ArrowDirection.Increase)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
            if (field == ValueController.ValueField.minimum && arrowDirection == ArrowDirection.Decrease)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }
}
