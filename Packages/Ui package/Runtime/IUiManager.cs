using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Ui
{
    public interface IUiManager : IManager
    {
        ILoading Loading { get; }
        UniTask SwitchScreen<TType, TData>(TData data) where TType : IScreen where TData : UiData;
        UniTask ShowPopup<TType, TData>(TData data, string specificName = null) where TType : IPopup where TData : PopupData;
        T GetPanel<T>() where T : IPanel;
    }
}