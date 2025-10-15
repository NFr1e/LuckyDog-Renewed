using App.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyDog
{
    public class ListEditor : MonoBehaviour
    {
        public NameList targetNameList { get; set; }
        public InputField nameInput,descInput,itemsInput;
        public enum ApplyMode { Create,Edit}
        public ApplyMode applyMode = ApplyMode.Create;

        public Button deleteButton;

        public static Action<NameList> OnApply;

        public string editPageKey = "pages.edit";

        public void Start()
        {
            Init();
        }
        public void Init()
        {
            if (targetNameList != null && applyMode == ApplyMode.Edit)
            {
                nameInput.text = targetNameList.Name;
                descInput.text = targetNameList.Description;
                itemsInput.text = string.Join("\n", targetNameList.Split);

                deleteButton.gameObject.SetActive(true);

                deleteButton.onClick.AddListener(() =>
                {
                    PagesManager.Instance.Unload(editPageKey);
                    NameListManager.Instance.Delete(targetNameList);
                });
            }
            else 
            { 
                deleteButton.gameObject.SetActive(false); 
            }
        }
        
        public void Apply()
        {
            targetNameList
                .SetNameList(nameInput.text != string.Empty ? nameInput.text : "БъЬт", 
                            descInput.text != string.Empty ? descInput.text : $"{DateTime.Now}", 
                            itemsInput.text != string.Empty ? itemsInput.text : $"A\nB");
            NameListManager.Instance.CurNameList = targetNameList;

            OnApply?.Invoke(targetNameList);
        }
    }
}
