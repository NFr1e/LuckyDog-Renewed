using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Utils
{
    /// <summary>
    /// �Զ��������ݳߴ粢ִ�в��䶯��
    /// </summary>
    public class AutoContentSizeFitTweener : ContentSizeFitterTweener
    {
        public float sizeChangeThreshold = 0.5f;

        private Vector2 m_lastRecordedSize,m_currentSize;

        protected override void Start()
        {
            base.Start();

            RebuildLayouts();

            m_lastRecordedSize = ContentSize(m_rectTrans);
            m_currentSize = ContentSize(m_rectTrans);

            targetSizeFitter.enabled = false;
        }

        private void LateUpdate()
        {
            m_currentSize = ContentSize(m_rectTrans);

            CheckSizeChange();
        }
        protected override void OnTweenComplete()
        {
            onFitted?.Invoke();
        }
        private void CheckSizeChange()
        {
            float sizeDiff = Vector2.Distance(m_lastRecordedSize,m_currentSize);

            if(sizeDiff > sizeChangeThreshold)
            {
                m_lastRecordedSize = m_currentSize;

                DoFit();
            }
        }
    }
}
