using App.DataSO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace App.UI
{
    public class PagesManager : Singleton<PagesManager>
    {
        [SerializeField] private CanvasElementData canvasElementData;
        [SerializeField] private Transform pageLayer;

        private Dictionary<string,PageBase> _pageCache = new();
        private List<PageBase> _pageTree = new();

        private PageBase _currentPage
        {
            get => _pageTree.Count >= 1 ? _pageTree[_pageTree.Count - 1] : null;
        }

        public void Load(string id)
        {
            if (_pageCache.ContainsKey(id))
            {
                StartCoroutine(_pageCache[id].Resume());
                return;
            }

            if (_currentPage != null)
            {
                StartCoroutine(_currentPage.Pause());
            }

            var prefab = Instantiate(canvasElementData.Page(id).gameObject,
                pageLayer != null ? pageLayer : new GameObject("PageLayer").transform);

            PageBase page = prefab.GetComponent<PageBase>();

            if (page == null) return;

            _pageCache.Add(id, page);
            _pageTree.Add(page);

            StartCoroutine(page.Load());
        }

        public void Unload(string id)
        {
            if (!_pageCache.ContainsKey(id)) return;

            PageBase page = _pageCache[id];

            StartCoroutine(page.Unload());
            _pageCache.Remove(id);
            _pageTree.Remove(page);

            if(_currentPage != null)
                StartCoroutine(_currentPage.Resume());
        }
        public void Unload(PageBase page)
        {
            if (page == null) return;

            foreach(var item in _pageCache)
            {
                if(item.Value == page)
                {
                    StartCoroutine(page.Unload());
                    _pageCache.Remove(item.Key);
                    _pageTree.Remove(page);
                    break;
                }
            }

            if (_currentPage != null) 
            { 
                StartCoroutine(_currentPage?.Resume()); 
            }
        }
    }
}
