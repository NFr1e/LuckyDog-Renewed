using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    [Serializable]
    public class TipContent
    {
        public string title,subTitle;

        public TipContent(string title ="Title", string subTitle = "Subtitle")
        {
            this.title = title;
            this.subTitle = subTitle;
        }
    }
    public class TipBase : CanvasElementBase
    {
        [ReadOnly]public TipContent content;
        [SerializeField] private Text titleText,subTitleText;

        private float _displayedTime = 0f;

        protected override void OnInit()
        {
            base.OnInit();

            titleText.text = content.title;
            subTitleText.text = content.subTitle;

            if (content.subTitle == string.Empty)
            {
                subTitleText.gameObject.SetActive(false);
            }

            _displayedTime = 0;
        }

        protected override void OnExit()
        {
            base.OnExit();

            Destroy(gameObject);
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (m_IsActive)
            {
                _displayedTime += Time.deltaTime;

                if (_displayedTime >= 2f)
                {
                    TipsManager.Instance.Unload(this);
                }
            }
        }
    }
}
