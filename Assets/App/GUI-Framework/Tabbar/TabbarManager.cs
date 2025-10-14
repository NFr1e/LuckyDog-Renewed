using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using App.UI;

namespace App.UI.Components
{
    public class TabbarManager : MonoBehaviour
    {
        [System.Serializable]
        public class TabItem
        {
            public string TabName = "Default";
            public string PageKey;
            public GameObject TabButton;

            public UnityEvent TabEvent;
        }

        [SerializeField]
        private List<TabItem> tabs = new List<TabItem>();

        private string currentPanelPath;

        void Start()
        {
            foreach (var tab in tabs)
            {
                var button = tab.TabButton.GetComponent<Button>();
                button.onClick.AddListener(() => SwitchTab(tab.PageKey));
            }

            if (tabs.Count > 0) SwitchTab(tabs[0].PageKey);
        }

        public void SwitchTab(string targetPanelPath)
        {
            if (currentPanelPath == targetPanelPath) return;

            if (!string.IsNullOrEmpty(currentPanelPath))
            {
                PagesManager.Instance.Unload(currentPanelPath);
            }

            PagesManager.Instance.Load(targetPanelPath);
            currentPanelPath = targetPanelPath;

            UpdateTabButtons(targetPanelPath);
        }

        private void UpdateTabButtons(string activePanelPath)
        {
            foreach (var tab in tabs)
            {
                bool isActive = tab.PageKey == activePanelPath;

                Transform indicatorTrnasform = tab.TabButton.transform.Find("IsSelected");
                Image indicator = indicatorTrnasform.gameObject.GetComponent<Image>();

                indicatorTrnasform.gameObject.SetActive(isActive);
                indicator
                    .DOFade(isActive ? 1 : 0, 0.5f)
                    .From(0)
                    .SetEase(Ease.OutExpo);
            }
        }
    }
}
