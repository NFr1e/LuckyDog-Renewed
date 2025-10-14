using App.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.ScreenManagement
{
    public class ApplySavedScreen : MonoBehaviour
    {
        public UserPrefsCollection userPrefs;

        private void Start()
        {
            Resolution[] resolutions = Screen.resolutions;
            Resolution fitResolution = resolutions[resolutions.Length - 1];

            
            if (userPrefs.HasSaved)
            {
                Screen.SetResolution(userPrefs.LastScreenWidth, userPrefs.LastScreenHeight, Screen.fullScreen);
            }
            else
            {
                if (Screen.currentResolution.width != fitResolution.width || Screen.currentResolution.height != fitResolution.height)
                    Screen.SetResolution(fitResolution.width, fitResolution.height, Screen.fullScreen);

            }
        }
    }
}
