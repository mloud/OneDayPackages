using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using OneDay.Localization;
using UnityEngine;

namespace OneDay.Ui
{
    public class UiManager: ABaseMonoManager, IUiManager
    {
        public ILoading Loading => loading;
        public ILocalizationManager LocalizationManager { get; private set; }
        [SerializeField] private Transform screenContainer;
        [SerializeField] private Transform dialogContainer;
        [SerializeField] private Transform panelContainer;
        [SerializeField] private ABaseLoading loading;
        private List<IScreen> Screens { get; set; }
        private List<IPopup> Popups { get; set; }
        private List<IPanel> Panels { get; set; }
        protected override UniTask OnInitialize()
        {
            Screens = screenContainer.GetComponentsInChildren<IScreen>(true).ToList();
            Popups =  dialogContainer.GetComponentsInChildren<IPopup>(true).ToList();
            Panels = panelContainer.GetComponentsInChildren<IPanel>(true).ToList();
            LocalizationManager = ObjectLocator.GetObject<ILocalizationManager>();
            Screens.ForEach(x=>x.Setup(this));
            Screens.ForEach(x=>x.Hide());

            Popups.ForEach(x=>x.SetHidden());
            Panels.ForEach(x=>x.Setup(this));
            loading.SetHidden();
            return UniTask.CompletedTask;
        }
        
        public async UniTask SwitchScreen<TType, TData>(TData data) where TType : IScreen where TData : UiData
        {
            var screenInternalData = new ScreenInternalData();
            
            var screenToOpen = Screens.Find(x => x.GetType() == typeof(TType));
            if (screenToOpen == null)
            {
                Debug.LogError($"No such screen exists {typeof(TType)}");
                return;
            }

            if (screenToOpen.IsVisible())
            {
                Debug.Log($"Screen {typeof(TType)} is already visible - skipping");
                return;
            }

            // close all visible screens
            for (int i = Screens.Count - 1; i >= 0; i--)
            {
                if (Screens[i].IsVisible())
                {
                    await Screens[i].Hide();
                    screenInternalData.PreviousScreen = Screens[i].ScreenId;
                }
            }
            
            await screenToOpen.Show(data, screenInternalData);
        }

        public async UniTask HideScreen<T>() where T : IScreen
        {
            var screen = Screens.Find(x => x.GetType() == typeof(T));
            if (screen == null)
            {
                Debug.LogError($"No such screen exists {typeof(T)}");
                return;
            }

            await screen.Hide();
        }

        public async UniTask ShowPopup<TType, TData>(TData data, string specificName = null) where TType : IPopup where TData: UiData
        {
            var popup = specificName == null 
                ? Popups.Find(x => x.GetType() == typeof(TType)) 
                : Popups.Find(x => x.Name == specificName);

            if (popup == null)
            {
                Debug.LogError($"No such popup exists {typeof(TType)}");
                return;
            }

            await popup.Show(data); 
        }

        public T GetPanel<T>() where T : IPanel => 
            (T)Panels.Find(x => x.GetType() == typeof(T));
    }
    
    public class ScreenInternalData : IScreenInternalData
    {
        public string PreviousScreen { get; set; }
    }
}