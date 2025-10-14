using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI.Events
{
    public class TipLoader : MonoBehaviour
    {
        [SerializeField] private string typeId = "tips.none", title = "Title", subtitle = "Subtitle";

        public void Load()
        {
            TipsManager.Instance.Load(typeId, title, subtitle);
        }
    }
}
