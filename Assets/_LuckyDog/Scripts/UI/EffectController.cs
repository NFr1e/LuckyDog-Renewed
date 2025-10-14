using App.User;
using App.User.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuckyDog.Others
{
    public class EffectController : MonoBehaviour
    {
        public UserPrefsCollection userPrefs;
        public GameObject effect;

        private void OnEnable()
        {
            UserPrefsEvents.OnValueChanged += SetEffect;
        }

        private void OnDisable()
        {
            UserPrefsEvents.OnValueChanged -= SetEffect;
        }
        private void Start()
        {
            SetEffect();
        }
        
        private void SetEffect()
        {
            effect.SetActive(userPrefs.UseEffect);
        }
    }
}
