using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI.Events {
    public class ModalUnloader : MonoBehaviour
    {
        public void Unload()
        {
            ModalManager.Instance.UnloadCurrent();
        }
    }
}
