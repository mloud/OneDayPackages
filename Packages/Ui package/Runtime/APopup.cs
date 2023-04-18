using Cysharp.Threading.Tasks;

namespace OneDay.Ui
{
    public interface IPopup
    {
        string Name { get; }
        UniTask Show(object data);
        void SetHidden();
    }

    public abstract class APopup<TData> : UiElement, IPopup where TData : PopupData
    {
        public string Name => gameObject.name;
        
        public async UniTask Show(TData data)
        {
            await OnShow(data);
            await base.Show();
        }

        protected abstract UniTask OnShow(TData data);

        public async UniTask Show(object data)
        {
            await Show((TData) data);
        }
    }
}