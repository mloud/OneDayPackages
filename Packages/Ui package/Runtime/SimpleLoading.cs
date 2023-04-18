using Cysharp.Threading.Tasks;

namespace OneDay.Ui
{
    public class SimpleLoading : ABaseLoading
    {
        public override UniTask Show(CancelSettings cancelSettings)
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide(bool showDone)
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}