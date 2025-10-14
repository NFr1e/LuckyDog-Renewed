using App;
using App.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyDog
{
    public class NameListElement : MonoBehaviour
    {
        public NameList nameList;
        public Text nameDisplayer,descDisplayer;

        public Button editButton,selectButton;
        public string editPageKey = "pages.edit";

        public DOTweenSequence selectTransition, unselectTransition;

        public void Start()
        {
            ResponseEdit(nameList);

            DoSelectAnim(NameListManager.Instance.CurNameList);
        }

        private void OnEnable()
        {
            NameListEvents.OnEditNameList += ResponseEdit;
            NameListManager.OnCurNameListChange += DoSelectAnim;

            editButton.onClick.AddListener(Edit);
            selectButton.onClick.AddListener(Select);
        }
        private void OnDisable()
        {
            NameListManager.OnCurNameListChange -= DoSelectAnim;
            NameListEvents.OnEditNameList -= ResponseEdit;
        }
        public void Select()
        {
            NameListManager.Instance.CurNameList = nameList;
        }
        public void DoSelectAnim(NameList list)
        {
            bool isThis = list == nameList;

            if (isThis)
            {
                unselectTransition?.DOKill();
                selectTransition?.Play();
            }
            else
            {
                selectTransition?.DOKill();
                unselectTransition?.Play();
            }
        }
        public void Edit()
        {
            if (nameList != null)
                NameListManager.Instance.Edit(nameList);
        }

        private void ResponseEdit(NameList list)
        {
            if (list == nameList)
            {
                nameDisplayer.text = nameList != null ? nameList.Name : "ERROR";
                descDisplayer.text = nameList != null ? nameList.Description : "ERROR";
            }
        }
    }
}
