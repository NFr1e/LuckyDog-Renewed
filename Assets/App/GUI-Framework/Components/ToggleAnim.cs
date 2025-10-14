using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Components
{
    public class ToggleAnim : MonoBehaviour
    {
        public DOTweenSequence onTransition, offTransition;

        public Toggle toggle;

        public void Start()
        {
            if (!toggle) return;

            DoAnim(toggle.isOn);

            toggle.onValueChanged.AddListener((isOn) =>
            {
                DoAnim(isOn);
            });
        }
        public void DoAnim(bool isOn)
        {
            if (isOn)
            {
                offTransition.DOKill();
                onTransition.Play();
            }
            else
            {
                onTransition.DOKill();
                offTransition.Play();
            }
        }
        public void Toggle()
        {
            toggle.isOn = !toggle.isOn;
        }
    }
}
