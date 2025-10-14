using App.User.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.ScreenManagement
{
    [RequireComponent(typeof(Button))]
    public class ResolutionSelectorInstance : MonoBehaviour
    {
        public Resolution targetResolution = new();
        public Text resolutionDisplayer;

        public UserPrefsIntController widthRecorder,heightRecorder;

        void Start()
        {
            if (!resolutionDisplayer)
            {
                Debug.LogError("ResolutionDisplayer is not assigned in " + gameObject.name);
                return;
            }

            widthRecorder.TargetField = "LastScreenWidth";
            widthRecorder.TargetValue = targetResolution.width;
            heightRecorder.TargetField = "LastScreenHeight";
            heightRecorder.TargetValue = targetResolution.height;

            resolutionDisplayer.text = $"{targetResolution.width} x {targetResolution.height} {targetResolution.refreshRate}Hz";

            Button button = GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(SetResolution);

                widthRecorder.SetValue();
                heightRecorder.SetValue();
            }
        }
        private void SetResolution()
        {
            Screen.SetResolution(targetResolution.width, targetResolution.height, Screen.fullScreen, targetResolution.refreshRate);
            Application.targetFrameRate = targetResolution.refreshRate;
        }
    }
}
