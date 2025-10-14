using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace App.User
{
    [CreateAssetMenu(menuName = "App/Users/UserPrefs")]
    public class UserPrefsCollection : ScriptableObject
    {
        [BoxGroup("General")]
        public bool HasSaved = false;
        [BoxGroup("Toggles")]
        public bool
            UseFullScreen = true,
            UseSplashScreen = false,
            UseVsync = false,
            UseSafeArea = false,
            UseEffect = true;
        [BoxGroup("Integers")]
        public int
            TargetFrameRate = 60,
            TargetRefreshRate = 60,
            PreferredStartPageIndex = 0,
            LastScreenWidth = 1920,
            LastScreenHeight = 1080;
        [BoxGroup("Doubles")]
        public double
            SafeAreaSize = 100,
            LuckyDogRollTimes = 30,
            LuckyDogRollInterval = 0.02;

        /// <summary>
        /// ��Scriptable�е�ֵ���浽PlayerPrefs��
        /// </summary>
        public void SavePrefs()
        {
            PlayerPrefs.SetInt("userprefs.toggles.usesplashscreen", UseSplashScreen ? 1 : 0);
            PlayerPrefs.SetInt("userprefs.toggles.usefullscreen", UseFullScreen ? 1 : 0);
            PlayerPrefs.SetInt("userprefs.toggles.usevsync", UseVsync ? 1 : 0);
            PlayerPrefs.SetInt("userprefs.toggles.usesafearea", UseSafeArea ? 1 : 0);
            PlayerPrefs.SetInt("userprefs.toggles.useeffect", UseEffect ? 1 : 0);
            PlayerPrefs.SetInt("userprefs.integers.targetframerate", TargetFrameRate);
            PlayerPrefs.SetInt("userprefs.integers.preferredstartpageindex", PreferredStartPageIndex);
            PlayerPrefs.SetInt("userprefs.integers.lastscreenwidth", LastScreenWidth);
            PlayerPrefs.SetInt("userprefs.integers.lastscreenheight", LastScreenHeight);
            PlayerPrefs.SetFloat("userprefs.integers.luckydogrolltimes", (float)LuckyDogRollTimes);
            PlayerPrefs.SetFloat("userprefs.doubles.safeareasize", (float)SafeAreaSize);
            PlayerPrefs.SetFloat("userprefs.doubles.luckydogrollinterval", (float)LuckyDogRollInterval);

            PlayerPrefs.SetInt("userprefs.hassaved", 1);

            PlayerPrefs.Save();
        }
        /// <summary>
        /// ����PlayerPrefs�洢��ֵ��ScriptableObject��
        /// </summary>
        public void LoadPrefs()
        {
            GetValue("userprefs.hassaved", ref HasSaved);
            GetValue("userprefs.toggles.usesplashscreen", ref UseSplashScreen);
            GetValue("userprefs.toggles.usefullscreen", ref UseFullScreen);
            GetValue("userprefs.toggles.usevsync", ref UseVsync);
            GetValue("userprefs.toggles.usesafearea", ref UseSafeArea);
            GetValue("userprefs.toggles.useeffect", ref UseEffect);
            GetValue("userprefs.integers.targetframerate", ref TargetFrameRate);
            GetValue("userprefs.integers.preferredstartpageindex", ref PreferredStartPageIndex);
            GetValue("userprefs.integers.lastscreenwidth", ref LastScreenWidth);
            GetValue("userprefs.integers.lastscreenheight", ref LastScreenHeight);
            GetValue("userprefs.integers.luckydogrolltimes", ref LuckyDogRollTimes);
            GetValue("userprefs.doubles.safeareasize", ref SafeAreaSize);
            GetValue("userprefs.doubles.luckydogrollinterval", ref LuckyDogRollInterval);
        }
        #region GetValue Overloads
        /// <summary>
        /// ��PlayerPrefs�л�ö�Ӧboolֵ��keyֵ��д��target��
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        private void GetValue(string key, ref bool target)
        {
            if (PlayerPrefs.HasKey(key))
            {
                int result = PlayerPrefs.GetInt(key);
                target = result == 1 ? true : false;
            }
            else Debug.Log("û����Ӧ��Keyֵ");
        }
        /// <summary>
        /// ��PlayerPrefs�л�ö�Ӧintֵ��keyֵ��д��target��
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        private void GetValue(string key, ref int target)
        {
            if (PlayerPrefs.HasKey(key))
            {
                target = PlayerPrefs.GetInt(key);
            }
            else Debug.Log("û����Ӧ��Keyֵ");
        }
        /// <summary>
        /// ��PlayerPrefs�л�ö�Ӧfloatֵ��keyֵ��д��target��
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        private void GetValue(string key,ref float target)
        {
            if (PlayerPrefs.HasKey(key))
            {
                target = PlayerPrefs.GetFloat(key);
            }
            else Debug.Log("û����Ӧ��Keyֵ");
        }
        /// <summary>
        /// ��PlayerPrefs�л�ö�Ӧdoubleֵ��keyֵ��д��target��
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        private void GetValue(string key, ref double target)
        {
            if (PlayerPrefs.HasKey(key))
            {
                target = PlayerPrefs.GetFloat(key);
            }
            else Debug.Log("û����Ӧ��Keyֵ");
        }
        /// <summary>
        /// ��PlayerPrefs�л�ö�Ӧstringֵ��keyֵ��д��target��
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        private void GetValue(string key, ref string target)
        {
            if (PlayerPrefs.HasKey(key))
            {
                target = PlayerPrefs.GetString(key);
            }
            else Debug.Log("û����Ӧ��Keyֵ");
        }
        #endregion
        public void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
