using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.DataSO
{
    [CreateAssetMenu(menuName = "App/DataSO/HotkeyData")]
    public class HotkeyData : ScriptableObject
    {
        [System.Serializable]
        public class HotkeyEntry
        {
            public string identifier = "";
            public KeyCode keyCode;
        }

        [SerializeField]
        private List<HotkeyEntry> hotkeys = new List<HotkeyEntry>();

        private Dictionary<string, KeyCode> hotkeyDic;

        private void EnsureDictionaryInitialized()
        {
            if (hotkeyDic == null)
            {
                hotkeyDic = new Dictionary<string, KeyCode>();
                RebuildDictionary();
            }
        }
        public void RebuildDictionary()
        {
            hotkeyDic.Clear();
            foreach (var entry in hotkeys)
            {
                if (string.IsNullOrEmpty(entry.identifier))
                {
                    Debug.LogWarning("Hotkey entry has empty identifier");
                    continue;
                }
                if (hotkeyDic.ContainsKey(entry.identifier))
                {
                    Debug.LogWarning($"Duplicate hotkey identifier: {entry.identifier}");
                    hotkeyDic[entry.identifier] = entry.keyCode;
                }
                else
                {
                    hotkeyDic.Add(entry.identifier, entry.keyCode);
                }
            }
        }

        private void OnEnable()
        {
            EnsureDictionaryInitialized();
        }

        public KeyCode Key(string id)
        {
            EnsureDictionaryInitialized();
            if (hotkeyDic.TryGetValue(id, out var keyCode))
            {
                return keyCode;
            }
            Debug.LogWarning($"{GetType().Name} has no key: {id}");
            return KeyCode.None;
        }
    }
}