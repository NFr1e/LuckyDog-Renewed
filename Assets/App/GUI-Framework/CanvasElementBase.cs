using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace App.UI
{
    public class CanvasElementBase : MonoBehaviour
    {
        protected IEnumerator m_LoadCoroutine, m_UnloadCoroutine,m_PauseCoroutine,m_ResumeCoroutine;

        protected bool m_IsActive = false;

        [FoldoutGroup("Transitions")]
        [SerializeField]
        private DOTweenSequence animEnter,animExit,animPause,animResume;

        [FoldoutGroup("Events")]
        [SerializeField]
        private UnityEvent OnLoad,OnLoaded,OnUnload,OnUnloaded,OnPause,OnPausedEvent,OnResume,OnResumedEvent,OnUpdateEvent;

        public virtual IEnumerator Load()
        {
            Debug.Log($"{gameObject.name} is Load");

            OnInit();

            LoadCoroutine();
            yield return m_LoadCoroutine;

            OnEnter();
        }
        public virtual IEnumerator Pause()
        {
            Debug.Log($"{gameObject.name} is Pause");

            m_IsActive = false;
            
            OnPause.Invoke();

            LoadCoroutine();
            yield return m_PauseCoroutine;

            OnPaused();
        }
        public virtual IEnumerator Resume()
        {

            Debug.Log($"{gameObject.name} is Resume");

            m_IsActive = true;

            OnResume.Invoke();

            LoadCoroutine();
            yield return m_ResumeCoroutine;

            OnResumed();
        }
        public virtual IEnumerator Unload()
        {
            Debug.Log($"{gameObject.name} is Unload");

            m_IsActive = false;

            OnUnload.Invoke();

            LoadCoroutine();
            yield return m_UnloadCoroutine;

            OnExit();
        }

        protected virtual void LoadCoroutine()
        {
            if (animEnter != null) m_LoadCoroutine = animEnter.DoCoroutine();
            if (animExit != null) m_UnloadCoroutine = animExit.DoCoroutine();
            if (animPause != null) m_PauseCoroutine = animPause.DoCoroutine();
            if (animResume != null) m_ResumeCoroutine = animResume.DoCoroutine();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void OnInit()
        {
            m_IsActive = true;
            OnLoad.Invoke();

            LoadCoroutine();
        }
        /// <summary>
        /// 进入后调用
        /// </summary>
        protected virtual void OnEnter()
        {
            OnLoaded.Invoke();
        }
        /// <summary>
        /// 聚焦时每帧调用
        /// </summary>
        protected virtual void OnUpdate()
        {
            OnUpdateEvent.Invoke();
        }
        /// <summary>
        /// post失焦调用
        /// </summary>
        protected virtual void OnPaused()
        {
            OnPausedEvent.Invoke();
        }
        /// <summary>
        /// post聚焦调用
        /// </summary>
        protected virtual void OnResumed()
        {
            OnResumedEvent.Invoke();
        }
        /// <summary>
        /// 离开时调用
        /// </summary>
        protected virtual void OnExit()
        {
            OnUnloaded.Invoke();
        }

        private void Update()
        {
            if (m_IsActive) OnUpdate();
        }
    }
}
