using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace App.UI.Events
{
    public class ModalLoader : MonoBehaviour
    {
        [SerializeField] private string id = "modals.",title = "Title";
        [SerializeField][TextArea] private string content = "Content";

        [SerializeField] private UnityEvent OnConfirm,OnCancel;

        public void Load()
        {
            ModalManager.Instance.Load(id, title, content, () => OnConfirm?.Invoke(), () => OnCancel?.Invoke());
        }
    }
}
