using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.Ui
{
    public abstract class UiElement : MonoBehaviour
    {
        public virtual UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public void SetHidden()
        {
            gameObject.SetActive(false);
        }
    }
}