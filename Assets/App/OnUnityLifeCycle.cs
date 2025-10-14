using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace App
{
    public class OnUnityLifeCycle : MonoBehaviour
    {
        [SerializeField] private UnityEvent onEnable,onDisable,onAwake,onStart,onUpdate,onFixedUpdate,onLateUpdate;

        private void Awake()
        {
            onAwake.Invoke();
        }
        private void OnEnable()
        {
            onEnable.Invoke();
        }
        private void Start()
        {
            onStart.Invoke();
        }
        private void Update()
        {
            onUpdate.Invoke();
        }
        private void FixedUpdate()
        {
            onFixedUpdate.Invoke();
        }
        private void LateUpdate()
        {
            onLateUpdate.Invoke();
        }
        private void OnDisable()
        {
            onDisable.Invoke();
        }
    }
}
