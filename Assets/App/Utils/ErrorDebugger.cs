using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.UI;

namespace App
{
    public class ErrorDebugger : Singleton<ErrorDebugger>
    {
        private string lastLog;
        [SerializeField] private string errorModalId = "modals.error",alertModalId = "modals.alert";
        private void Start()
        {
            Application.logMessageReceived += HandleLog;

            lastLog = string.Empty;
        }
        void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }
        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (type != LogType.Log)
            {
                lastLog = logString;

                switch(type)
                {
                    case LogType.Warning:
                        ModalManager.Instance.Load(errorModalId, $"{type}", $"{logString} \n{stackTrace}");
                        break;
                    case LogType.Error:
                        ModalManager.Instance.Load(errorModalId, $"{type}", $"{logString} \n{stackTrace}");
                        break;
                }
            }
        }
    }
}
