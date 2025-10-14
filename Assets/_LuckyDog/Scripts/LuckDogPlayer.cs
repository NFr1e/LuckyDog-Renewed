using App.User;
using App.User.Controller;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyDog
{
    public class LuckDogPlayer : MonoBehaviour
    {
        public UserPrefsCollection userPrefs;

        public DOTweenSequence playAnim;

        public Text displayer,curListNameText;

        private int rollTimes = 30;
        private float interval = 0.02f;

        private bool played = false;

        private string lastName = "";
        private int rolledTimes = 0;
        private void Start()
        {
            if (NameListManager.Instance.CurNameList == null)
            {
                curListNameText.gameObject.SetActive(false);
                displayer.text = "Empty Lists";
            }
            else
            {
                curListNameText.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            UserPrefsEvents.OnValueChanged += ChangeRollingArgs;
            NameListManager.OnCurNameListChange += ChangeText;
        }
        private void OnDisable()
        {
            UserPrefsEvents.OnValueChanged -= ChangeRollingArgs;
            NameListManager.OnCurNameListChange -= ChangeText;
        }

        void ChangeText(NameList list)
        {
            if (list == null)
            {
                curListNameText.gameObject.SetActive(false);
                displayer.text = "Empty Lists";

                return;
            }
            else
            {
                if (curListNameText)
                {
                    curListNameText.gameObject.SetActive(true);
                    curListNameText.text = list.Name;
                }
            }
        }
        public void ChangeRollingArgs()
        {
            if (!userPrefs) return;

            rollTimes = (int)userPrefs.LuckyDogRollTimes;
            interval = (float)userPrefs.LuckyDogRollInterval;
        }
        public void Play()
        {
            if (!played) playAnim.Play();

            if (NameListManager.Instance.CurNameList != null)
                StartCoroutine(NameRolling());

            if (played)
            {
                displayer.transform
                    .DOScale(Vector3.one, rollTimes * interval + 0.5f)
                    .From(Vector3.one * 0.2f)
                    .SetEase(Ease.OutExpo);
                displayer
                    .DOFade(1, rollTimes * interval / 2)
                    .From(0)
                    .SetEase(Ease.InFlash);
            }

            played = true;
        }
        public IEnumerator NameRolling()
        {
            rolledTimes = 0;

            while (rolledTimes < rollTimes)
            {
                string name = NameListManager.Instance.CurNameList.GetRandomName();

                while (name == lastName)
                {
                    name = NameListManager.Instance.CurNameList.GetRandomName();
                    yield return null;
                }
                lastName = name;

                if (displayer) displayer.text = name;

                yield return new WaitForSeconds(interval);
                rolledTimes++;
            }

            string finalName = NameListManager.Instance.CurNameList.GetRandomName();
            if (displayer) displayer.text = finalName;
            lastName = finalName;
        }
    }
}
