using Cysharp.Threading.Tasks;

namespace OneDay.StateMachine
{
    public abstract class AState : IState
    {
        public IStateMachine StateMachine { get; set; }
        
        public async UniTask Setup()
        {
            await OnSetup(); 
        }

        public async UniTask Enter()
        {
            await OnEnter();
        }

        public async UniTask Leave()
        {
            await OnLeave();
        }
        
        public async UniTask Update(float dt)
        {
            await OnUpdate(dt);
        }

        protected virtual UniTask OnSetup() => UniTask.CompletedTask;
        protected virtual UniTask OnEnter() => UniTask.CompletedTask;
        protected virtual UniTask OnLeave() => UniTask.CompletedTask;
        protected virtual UniTask OnUpdate(float dt) => UniTask.CompletedTask;
    }
}