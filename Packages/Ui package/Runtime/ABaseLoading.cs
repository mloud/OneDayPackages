using Cysharp.Threading.Tasks;

namespace OneDay.Ui
{
    public abstract class ABaseLoading: UiElement, ILoading
    {
        public abstract UniTask Show(CancelSettings cancelSettings);
        public abstract UniTask Hide(bool showDone);
    }
}