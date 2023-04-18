using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.Core
{
    public abstract class ABaseMonoManager : MonoBehaviour, IManager
    {
        [SerializeField] private bool autoRegister;
   
        public async UniTask Initialize()
        {
            await OnInitialize();
        }

        public async UniTask Release()
        {
            await OnRelease();
        }

        protected virtual UniTask OnInitialize() => UniTask.CompletedTask;

        protected virtual UniTask OnRelease()  => UniTask.CompletedTask;
    }
}