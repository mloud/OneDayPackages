using Cysharp.Threading.Tasks;

namespace OneDay.Core
{
    public abstract class ABaseManager : IManager
    {
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