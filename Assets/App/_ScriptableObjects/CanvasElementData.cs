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
        /// ��Pages�м���ҳ�����
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
            Debug.LogError($"PageKey '{key}' ������");
            return null;
        }
        /// <summary>
        /// ����Tip����
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
            Debug.LogError($"TipKey '{key}' ������");
            return null;
        }
        /// <summary>
        /// ����Modal����
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
            Debug.LogError($"ModalKey '{key}' ������");
            return null;
        }
    }
}
