using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

namespace LuckyDog
{
    public class ListsPage : MonoBehaviour
    {
        public Transform listContainer;
        public NameListElement instance;

        public InputField searcher;
        public Button createButton;
        public Text stateDisplayer;

        private void Start()
        {
            createButton.onClick.AddListener(() =>
            {
                NameListManager.Instance.CreateNew();
            });
            searcher.onValueChanged.AddListener((string s) => Search());

            RefreshList(NameListManager.Instance.NameLists);
        }

        void OnEnable()
        {
            NameListManager.OnListChange += RefreshList;
        }
        private void OnDisable()
        {
            NameListManager.OnListChange -= RefreshList;
        }
        private void RefreshList()
        {
            ClearList();

            List<NameList> list = NameListManager.Instance.NameLists;

            if (list.Count > 0)
            {
                foreach (var a in list)
                {
                    GameObject go = Instantiate(instance.gameObject, listContainer);
                    NameListElement element = go.GetComponent<NameListElement>();
                    element.nameList = a;
                    element.Start();
                }
            }

            DisplayState(list);
        }
        private void RefreshList(List<NameList> list)
        {
            ClearList();

            if (list == null) list = NameListManager.Instance.NameLists;

            if (list.Count > 0)
            {
                foreach (var a in list)
                {
                    GameObject go = Instantiate(instance.gameObject, listContainer);
                    NameListElement element = go.GetComponent<NameListElement>();
                    element.nameList = a;
                    element.Start();
                }
            }
            DisplayState(list);
        }
        private void ClearList()
        {
            for (int i = listContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(listContainer.GetChild(i).gameObject);
            }
        }
        private void DisplayState(List<NameList> list)
        {
            if (!stateDisplayer) return;

            if (list.Count <= 0) stateDisplayer.gameObject.SetActive(true);
            else stateDisplayer.gameObject.SetActive(false);
        }
        private void Search()
        {
            List<NameList> results = new();

            if (searcher.text != string.Empty)
            {
                results = FliteredLists(searcher.text);

                if (results.Count >= 1)
                {
                    RefreshList(results);
                }
                else
                {
                    DisplayState(results);
                    ClearList();
                }
            }
            else
            {
                RefreshList();
            }
        }
        public List<NameList> FliteredLists(string keyword)
        {
            string trimmedKeyword = keyword.Trim();
            if (string.IsNullOrEmpty(trimmedKeyword))
                return new List<NameList>();

            return NameListManager.Instance.NameLists.Where(list =>
                    list.Name.IndexOf(trimmedKeyword, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
        }
    }
}
