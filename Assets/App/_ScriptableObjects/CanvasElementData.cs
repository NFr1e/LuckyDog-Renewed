using App.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.DataSO
{
    [CreateAssetMenu(menuName ="App/DataSO/CanvasElementData")]
    public class CanvasElementData : ScriptableObject
    {
        [Serializable]
        public class PageEntry
        {
            public PageBase PageObject;
            public string PageKey = "pages.";
        }
        [Serializable]
        public class TipEntry
        {
            public TipBase TipObject;
            public string TipKey = "tips.";
        }
        [Serializable]
        public class ModalEntry
        {
            public ModalBase ModalObject;
            public string ModalKey = "modals.";
        }

        public List<PageEntry> Pages;
        public List<TipEntry> Tips;
        public List<ModalEntry> Modals;

        /// <summary>
        /// 从Pages中检索页面对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PageBase Page(string key)
        {
            foreach (var page in Pages)
            {
                if (page.PageKey == key)
                    return page.PageObject;
            }
            Debug.LogError($"PageKey '{key}' 不存在");
            return null;
        }
        /// <summary>
        /// 检索Tip对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TipBase Tip(string key)
        {
            foreach (var tip in Tips)
            {
                if (tip.TipKey == key)
                    return tip.TipObject;
            }
            Debug.LogError($"TipKey '{key}' 不存在");
            return null;
        }
        /// <summary>
        /// 检索Modal对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ModalBase Modal(string key)
        {
            foreach (var modal in Modals)
            {
                if (modal.ModalKey == key)
                    return modal.ModalObject;
            }
            Debug.LogError($"ModalKey '{key}' 不存在");
            return null;
        }
    }
}
