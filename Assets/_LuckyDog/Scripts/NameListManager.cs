using App.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuckyDog
{
    public class NameListManager : Singleton<NameListManager>
    {
        public string editPageKey = "pages.edit";

        public List<NameList> NameLists
        {
            get
            {
                return nameLists;
            }
            set
            {
                nameLists = value;
                OnListChange?.Invoke();
            }
        }
        private List<NameList> nameLists = new List<NameList>();

        public static Action OnListChange;

        public NameList CurNameList
        {
            get
            {
                return curNameList;
            }
            set
            {
                if (curNameList == value) return;

                curNameList = value;

                OnCurNameListChange?.Invoke(curNameList);
            }
        }
        private NameList curNameList;

        public static Action<NameList> OnCurNameListChange;

        public void SetCurNameList(NameList namelist)
        {
            CurNameList = namelist;
        }
        public void ApplyNameList(NameList list)
        {
            if (!NameLists.Contains(list))
            {
                NameLists.Add(list);
            }
            else
            {
                NameLists[NameLists.IndexOf(list)] = list;
            }

            OnListChange?.Invoke();
            SaveLists();
        }

        public void CreateNew()
        {
            NameList nameList = new();

            PagesManager.Instance.Load(editPageKey);
            ListEditor editor = FindObjectOfType<ListEditor>();

            editor.applyMode = ListEditor.ApplyMode.Create;

            editor.targetNameList = nameList;
            editor.Init();
        }
        public void Edit(NameList list)
        {
            PagesManager.Instance.Load(editPageKey);
            ListEditor editor = FindObjectOfType<ListEditor>();

            editor.applyMode = ListEditor.ApplyMode.Edit;

            editor.targetNameList = list;
            editor.Init();
        }
        public void Delete(NameList list)
        {
            if (NameLists.Contains(list))
            {
                NameLists.Remove(list);

                if (CurNameList == list)
                {
                    CurNameList = NameLists.Count > 0 ? NameLists[0] : null;
                }

                OnListChange?.Invoke();
                SaveLists();
            }
        }
        private void OnEnable()
        {
            ListEditor.OnApply += ApplyNameList;
        }
        private void OnDisable()
        {
            ListEditor.OnApply -= ApplyNameList;
        }

        private void Start()
        {
            LoadLists();
        }

        #region Save & Load
        private int saveCount = 0;
        private void SaveLists()
        {
            saveCount = nameLists.Count;
            PlayerPrefs.SetInt("namelist_count", saveCount);

            if (CurNameList != null)
            {
                int index = nameLists.IndexOf(CurNameList);
                PlayerPrefs.SetInt("namelist_last_index", index >= 0 ? index : -1);
            }
            else
            {
                PlayerPrefs.SetInt("namelist_last_index", -1);
            }

            for (int i = 0; i < saveCount; i++)
            {
                PlayerPrefs.SetString("namelist_" + i, JsonUtility.ToJson(nameLists[i]));
            }

            PlayerPrefs.Save();
        }

        private void LoadLists()
        {
            nameLists.Clear();

            saveCount = PlayerPrefs.GetInt("namelist_count", 0);
            int lastIndex = PlayerPrefs.GetInt("namelist_last_index", -1);

            for (int i = 0; i < saveCount; i++)
            {
                string json = PlayerPrefs.GetString("namelist_" + i, "");
                if (!string.IsNullOrEmpty(json))
                {
                    NameList list = JsonUtility.FromJson<NameList>(json);
                    nameLists.Add(list);
                }
            }

            if (lastIndex >= 0 && lastIndex < nameLists.Count)
            {
                CurNameList = nameLists[lastIndex];
            }
            else
            {
                CurNameList = nameLists.Count > 0 ? nameLists[0] : null;
            }

            OnListChange?.Invoke();
        }
        #endregion
    }
}
