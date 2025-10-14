using App.DataSO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI
{
    public class ModalManager : Singleton<ModalManager>
    {
        [SerializeField] private CanvasElementData canvasElementData;
        [SerializeField] private Canvas modalLayer;

        private ModalBase _currentModal = null;

        private void Start()
        {
            if(modalLayer == null)
            {
                Debug.LogError("Modal Layer is not set. Please assign a Canvas for modal display.");
            }
        }
        public void Load(string id,string title,string content,Action onConfirm = null,Action onCancel = null)
        {
            var prefab = Instantiate(canvasElementData.Modal(id).gameObject, modalLayer.transform);
            ModalBase modal = prefab.GetComponent<ModalBase>();

            modal.content = new(title, content, onConfirm, onCancel);

            if (_currentModal != null)
            {
                UnloadCurrent();
            }

            StartCoroutine(modal.Load());
            _currentModal = modal;
        }
        public void UnloadCurrent()
        {
            if (_currentModal != null)
            {
                StartCoroutine(_currentModal.Unload());
                _currentModal = null;
            }
        }
    }
}
