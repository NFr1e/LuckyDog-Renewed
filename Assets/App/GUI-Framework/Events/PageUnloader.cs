using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI.Events
{
    public class PageUnloader : MonoBehaviour
    {
        [SerializeField] private string pageKey = "pages.";

        public void Unload()
        {
            PagesManager.Instance.Unload(pageKey);
        }
    }
}
