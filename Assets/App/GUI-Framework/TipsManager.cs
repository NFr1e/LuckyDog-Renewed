using App.DataSO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class TipsManager : Singleton<TipsManager>
    {
        [SerializeField] private CanvasElementData canvasElementData;
        [SerializeField] private VerticalLayoutGroup tipLayer;
        [SerializeField] private Transform tipExitLayer;

        private List<TipBase> _currentTips = new();

        public void Load(string typeId = "tips.none",string title = "Title",string subtitle = "Subtitle")
        {
            var prefab = Instantiate(canvasElementData.Tip(typeId).gameObject,tipLayer != null ? tipLayer.transform : new GameObject("TipLayer").transform);
            TipBase tip = prefab.GetComponent<TipBase>();

            tip.content = new(title, subtitle);

            if(_currentTips.Count > 5)
            {
                Unload(_currentTips[0]);
            }

            _currentTips.Add(tip);

            StartCoroutine(tip.Load());
        }
        public void Unload(TipBase tip)
        {
            if(_currentTips.Contains(tip))
            {
                tip.transform.SetParent(tipExitLayer);
                StartCoroutine(tip.Unload());

                _currentTips.Remove(tip);
            }
        }
    }
}