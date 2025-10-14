using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.ScreenManagement
{
    public class ResolutionSelector : MonoBehaviour
    {
        [SerializeField]private Resolution[] resolutions;

        [SerializeField] private GameObject selectorInstance;
        [SerializeField] private Transform container;

        private ResolutionSelectorInstance[] _elements;

        private void Start()
        {
            resolutions = Screen.resolutions;
            int count = resolutions.Length; 

            _elements = new ResolutionSelectorInstance[count];

            for (int i = 0; i < count; i++)
            {
                Resolution resolution = resolutions[i];

                if (container == null) return;

                _elements[i] = Instantiate(selectorInstance.gameObject, container).GetComponent<ResolutionSelectorInstance>();
                _elements[i].targetResolution = resolution;
                
            }
        }
    }
}
