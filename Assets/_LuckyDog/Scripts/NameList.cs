using App.UI.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LuckyDog
{
    public class NameListEvents
    {
        public static Action<NameList> OnEditNameList;
    }
    [Serializable]
    public class NameList
    {
        public string Name = "NewList",Description = "Description",ItemsTex = "";

        public List<string> Split
        {
            get
            {
                List<string> items = ItemsTex.Split('\n').ToList();

                if(items.Contains("")) 
                    items.RemoveAll(x => x == "");

                return items;
            }
        }

        public void SetNameList(string name,string desc,string items)
        {
            Name = name;
            Description = desc;
            ItemsTex = items;

            NameListEvents.OnEditNameList?.Invoke(this);
        }

        /// <summary>
        /// Get name by index, if index is out of range, return last name
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetName(int index)
        {
            if (index >= Split.Count)
                return Split[Split.Count - 1];

            return Split[index];
        }
        /// <summary>
        /// Get a random name from the list
        /// </summary>
        /// <returns></returns>
        public string GetRandomName()
        {
            int count = Split.Count;

            if (count <= 0) return "???";

            int index = UnityEngine.Random.Range(0, count);
            return Split[index];
        }
    }
}
