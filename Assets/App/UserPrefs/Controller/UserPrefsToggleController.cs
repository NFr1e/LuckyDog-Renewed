using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.User.Controller
{
    [RequireComponent(typeof(Button))]
    public class UserPrefsToggleController : MonoBehaviour
    {
        public UserPrefsCollection UserPrefs;
        public string TargetField = "Filed";

        public Toggle Toggle;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();

            InitToggle();
            RegisterListener();
        }
        private void OnDestroy()
        {
            UnregisterListener();
        }
        private void InitToggle()
        {
            Toggle.isOn = (bool)typeof(UserPrefsCollection).GetField(TargetField).GetValue(UserPrefs);
        }

        private void RegisterListener()
        {
            _button.onClick.AddListener(SetValue);
        }
        private void UnregisterListener()
        {
            _button.onClick.RemoveAllListeners();
        }
        private void SetValue()
        {
            typeof(UserPrefsCollection).GetField(TargetField).SetValue(UserPrefs, Toggle.isOn);
            Debug.Log($"{typeof(UserPrefsCollection).Name}.{typeof(UserPrefsCollection).GetField(TargetField)} …Ë÷√Œ™{Toggle.isOn}");

            UserPrefsEvents.ChangeUserPrefsValue();
        }
    }
}
