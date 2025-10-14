using App.DataSO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace App.UI
{
    public class PageBase : CanvasElementBase
    {
        public Canvas canvas;
        [SerializeField] private HotkeyData hotkeyData;
        [SerializeField][ReadOnly] private string unloadKey = "return";
        [SerializeField] private bool enableHotkey = true;

        protected override void OnExit()
        {
            base.OnExit();

            Destroy(gameObject);
        }
        protected override void OnUpdate()
        {
            if (!enableHotkey) return;
            if (hotkeyData == null) return;

            if(m_IsActive)
            {
                if(Input.GetKeyUp(hotkeyData.Key(unloadKey)))
                {
                    PagesManager.Instance.Unload(this);
                }
            }
        }
    }
}
