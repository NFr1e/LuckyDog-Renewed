using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyDog
{
    public class PlayButton : MonoBehaviour
    {
        public LuckDogPlayer player;
        public Text listNameDisplayer;

        public Button playButton,addButton;

        public string editPageKey = "pages.edit";

        private void Start()
        {
            if (NameListManager.Instance.CurNameList == null)
            {
                listNameDisplayer.gameObject.SetActive(false);
            }
            else
            {
                listNameDisplayer.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            NameListManager.OnCurNameListChange += ChangeText;
            NameListEvents.OnEditNameList += ChangeText;
        }
        private void OnDisable()
        {
            NameListManager.OnCurNameListChange -= ChangeText;
            NameListEvents.OnEditNameList -= ChangeText;
        }

        public void ClickAddButton()
        {
            NameListManager.Instance.CreateNew();
        }
        public void ClickPlayButton()
        {
            if (player)
            {
                player.Play();
            }
        }
        void ChangeText(NameList list)
        {
            if (list == null)
            {
                listNameDisplayer.gameObject.SetActive(false);
                playButton.gameObject.SetActive(false);
                addButton.gameObject.SetActive(true);


                return;
            }
            else
            {
                if (listNameDisplayer)
                {
                    listNameDisplayer.gameObject.SetActive(true);
                    playButton.gameObject.SetActive(true);
                    addButton.gameObject.SetActive(false);

                    listNameDisplayer.text = list.Name;
                }
            }
        }
    }
}
