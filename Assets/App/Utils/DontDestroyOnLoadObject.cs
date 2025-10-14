using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
