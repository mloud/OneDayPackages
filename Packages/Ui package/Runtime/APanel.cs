using Cysharp.Threading.Tasks;

namespace OneDay.Ui
{
    public interface IPanel
    {
        UniTask Setup(IUiManager uiManager);
        UniTask Show();
        UniTask Hide();
        bool IsVisible();
    }
    
    public abstract class APanel : UiElement, IPanel
    {
        protected IUiManager UiManager { get; private set; }
        public async UniTask Setup(IUiManager uiManager)
        {
            UiManager = uiManager;
            await OnSetup();
        }

        public override async UniTask Show()
        {
            await base.Show();
            await OnShow();
        }

        public override async UniTask Hide()
        {
            await OnHide();
            await base.Hide();
        }

        public bool IsVisible() => gameObject.activeSelf;
        
        protected virtual UniTask OnShow() => UniTask.CompletedTask;
        protected virtual UniTask OnHide() => UniTask.CompletedTask;
        protected virtual UniTask OnSetup() => UniTask.CompletedTask;
    }
}