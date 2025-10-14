using App.User;
using App.User.Controller;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class SafeAraeOffset : MonoBehaviour
    {
        [SerializeField] private LayoutElement safeAreaRect;

        [SerializeField] private UserPrefsCollection userPrefs;

        private void OnEnable()
        {
            UserPrefsEvents.OnValueChanged += Refresh;
        }

        private void OnDisable()
        {
            UserPrefsEvents.OnValueChanged -= Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (!userPrefs.HasSaved) return;

            safeAreaRect.gameObject.SetActive(userPrefs.UseSafeArea);
            safeAreaRect.preferredHeight = (float)userPrefs.SafeAreaSize;

            DOTween.To(() => safeAreaRect.preferredHeight, 
                x => safeAreaRect.preferredHeight = x, (float)userPrefs.SafeAreaSize, 
                0.5f).SetEase(Ease.OutExpo);
        }
    }
}
