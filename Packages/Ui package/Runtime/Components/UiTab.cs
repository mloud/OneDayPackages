using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneDay.Ui.Components
{
    public class UiTab : MonoBehaviour
    {
        public Action<string> TabSelected;
        [Serializable]
        public class TabButton
        {
            public string Name;
            public bool IsDefault;
            public UiToggle Toggle;
            public Transform LinkedTransform;
        }
        
        [SerializeField] private List<TabButton> tabs;
 
        public TabButton CurrentTab { get; private set; }

        private bool IsInitialized { get; set; }
        private void Start() => TryToInitialize();

        public bool SelectTab(string tabName)
        {
            TryToInitialize();
            return SelectTab(tabs.First(x => x.Name == tabName));
        }

        private bool SelectTab(TabButton newTab)
        {
            if (newTab == CurrentTab)
                return false;
            
            if (CurrentTab != null)
            {
                CurrentTab.Toggle.isOn = false;
                CurrentTab.Toggle.interactable = true;
                if (CurrentTab.LinkedTransform != null)
                {
                    CurrentTab.LinkedTransform.gameObject.SetActive(false);
                }
            }
          
            newTab.Toggle.isOn = true;
            newTab.Toggle.interactable = false;
            if (newTab.LinkedTransform != null)
            {
                newTab.LinkedTransform.gameObject.SetActive(true);
            }
            CurrentTab = newTab;
            TabSelected?.Invoke(newTab.Name);
            return true;
        }

        private void TryToInitialize()
        {
            if (IsInitialized)
                return;
            
            foreach (var tab in tabs)
            { 
                tab.Toggle.onValueChanged.AddListener((on)=>
                {
                    if (on)
                    {
                        SelectTab(tab);
                    }
                });
                if (tab.LinkedTransform != null)
                {
                    tab.LinkedTransform.gameObject.SetActive(false);
                }
                tab.Toggle.isOn = false;
            }

            var selectedTab = tabs.FirstOrDefault(x => x.IsDefault);
            if (selectedTab != null)
            {
                SelectTab(selectedTab);
            }

            IsInitialized = true;
        }
    }
}