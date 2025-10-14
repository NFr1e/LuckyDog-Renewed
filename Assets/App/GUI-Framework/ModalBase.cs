using App.DataSO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;

namespace App.UI
{
    [Serializable]
    public class ModalContent
    {
        public string title, content;
        public Action Confirm, Cancel;
        public ModalContent(string title = "Title", string content = "Subtitle",Action onConfirm = null,Action onCancel = null)
        {
            this.title = title;
            this.content = content;
            Confirm = onConfirm;
            Cancel = onCancel;
        }
    }

    public class ModalBase : CanvasElementBase
    {
        [SerializeField] private HotkeyData hotkeyData;
        [SerializeField][ReadOnly] private string unloadKey = "return";

        [SerializeField] private Text titleText, contentText;
        [SerializeField] private Button confirmButton, cancelButton;

        [SerializeField][ReadOnly]public ModalContent content;

        protected override void OnInit()
        {
            base.OnInit();

            titleText.text = content.title;
            contentText.text = content.content;

            confirmButton?.onClick.AddListener(() => content.Confirm?.Invoke());
            cancelButton?.onClick.AddListener(() => content.Cancel?.Invoke());
        }
        protected override void OnExit()
        {
            base.OnExit();

            Destroy(gameObject);
        }
        protected override void OnUpdate()
        {
            if (hotkeyData == null) return;

            if (m_IsActive)
            {
                if (Input.GetKeyUp(hotkeyData.Key(unloadKey)))
                {
                    ModalManager.Instance.UnloadCurrent();
                }
            }
        }
    }
}
