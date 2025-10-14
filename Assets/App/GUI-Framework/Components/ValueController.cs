using App.User;
using App.User.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace App.UI.Components
{
    public class ValueController : MonoBehaviour
    {

        public enum ValueField
        {
            maximum,minimum
        }

        public UserPrefsCollection UserPrefs;
        [ShowIf("UserPrefs")]public string targetField = "Filed";

        [SerializeField] private double minValue = 0,maxValue = 100;
        [SerializeField] private double interval = 10;
        [SerializeField] private string format = "F0";

        public Text displayer;

        public Action<double> OnValueChange;
        public Action<ValueField> OnFieldChange;

        private double _value;
        public double Value 
        { 
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                if (value > maxValue)
                {
                    _value = maxValue;
                    OnFieldChange?.Invoke(ValueField.maximum);
                }
                if (value < minValue)
                {
                    _value = minValue;
                    OnFieldChange?.Invoke(ValueField.minimum);
                }

                OnValueChange?.Invoke(_value);
                ChangeDisplayText();

                if (UserPrefs)
                {
                    typeof(UserPrefsCollection).GetField(targetField).SetValue(UserPrefs, _value);
                    UserPrefsEvents.ChangeUserPrefsValue();
                }
            }
        }
        public string ValueString
        {
            get
            {
                return _value.ToString(format);
            }
        }

        private void Start()
        {
            Value = (double)typeof(UserPrefsCollection).GetField(targetField).GetValue(UserPrefs);

            ChangeDisplayText();
        }

        public void ValueIncrease()
        {
            Value += interval;
        }
        public void ValueDecrease()
        {
            Value -= interval;
        }

        private void ChangeDisplayText()
        {
            if (displayer != null)
            {
                displayer.text = ValueString;
            }
        }
    }
}
