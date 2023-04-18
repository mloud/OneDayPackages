using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.Ui
{

    public interface IScreenInternalData
    {
        string PreviousScreen { get; }
    }
    
    public interface IScreen
    {
        string ScreenId { get; }
        UniTask Setup(IUiManager uiManager);
        UniTask Show(object customData, IScreenInternalData internalData);
        UniTask Hide();
        bool IsVisible();
    }

    public abstract class ANoDataScreenController<TView> : AScreenController<TView, EmptyData>
        where TView : AScreenView
    { }

    public abstract class AScreenController<TView, TData> : MonoBehaviour, IScreen where TView: AScreenView where TData: UiData
    {
        public string ScreenId => gameObject.name;
        protected TView View { get; private set; }
        protected IUiManager UiManager  { get; private set; }

        public async UniTask Setup(IUiManager uiManager)
        {
            UiManager = uiManager;
            View = GetComponent<TView>();
            Debug.Assert(View != null, $"No view of type {typeof(TView)} found for ");
            await OnSetup();
        }

        public async UniTask Show(object data, IScreenInternalData internalData)
        {
            await Show((TData) data, internalData);
        }
        private async UniTask Show(TData data, IScreenInternalData internalData)
        {
            SetInputActive(false);
            await View.Show();
            await OnShow(data, internalData);
            SetInputActive(true);
        }

        public async UniTask Hide()
        {
            SetInputActive(false);
            await OnHide();
            await View.Hide();
            SetInputActive(true);
        }

        public bool IsVisible() => gameObject.activeSelf;
        
        protected async UniTask SwitchScreen<TType, TData>(TData data) where TType : IScreen where TData: UiData
        {
            await UiManager.SwitchScreen<TType, TData>(data);
        }
        protected async UniTask ShowPopup<TType, TPopupData>(TPopupData data) where TType : IPopup where TPopupData : PopupData
        {
            await UiManager.ShowPopup<TType, TPopupData>(data);
        }
        
        protected TType GetPanel<TType>() where TType : IPanel
        {
            return UiManager.GetPanel<TType>();
        }
        
        protected virtual UniTask OnShow(TData data, IScreenInternalData internalData) => UniTask.CompletedTask;
        protected virtual UniTask OnHide() => UniTask.CompletedTask;
        protected virtual UniTask OnSetup() => UniTask.CompletedTask;

        private void SetInputActive(bool active)
        {
            var cg = GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.interactable = active;
            }
        }
    }
}