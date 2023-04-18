using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.Core
{
    [DefaultExecutionOrder(-100)]
    public abstract class AApp : MonoBehaviour
    {
        private async void Awake() => await Boot();
       
        private async UniTask Boot() =>
            await OnInitialize();
        
        protected virtual UniTask OnInitialize()
        {
            return UniTask.CompletedTask;
        }
    }
}