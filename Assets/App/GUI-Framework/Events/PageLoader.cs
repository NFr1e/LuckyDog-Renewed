using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI.Events
{
    public class PageLoader : MonoBehaviour
    {
        [SerializeField] private string pageKey = "pages.";

        public void Load()
        {
            PagesManager.Instance.Load(pageKey);
        }
    }
}
