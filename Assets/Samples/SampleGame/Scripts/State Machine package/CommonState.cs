using Cysharp.Threading.Tasks;

namespace OneDay.StateMachine
{
    public class CommonState : AState
    {
        public delegate UniTask MethodDelegate();
        public delegate UniTask UpdateMethodDelegate(float dt);

        public MethodDelegate OnEnterAsync { get; set; }
        public MethodDelegate OnLeaveAsync { get; set; }
        public MethodDelegate OnSetupAsync { get; set; }
        public UpdateMethodDelegate OnUpdateAsync { get; set; }
        protected override async UniTask OnEnter()
        {
            if (OnEnterAsync != null)
            {
                await OnEnterAsync();
            }
        }

        protected override async UniTask OnLeave()
        {
            if (OnLeaveAsync != null)
            {
                await OnLeaveAsync();
            }
        }

        protected override async UniTask OnSetup()
        {
            if (OnSetupAsync != null)
            {
                await OnSetupAsync();
            }
        }
        
        protected override async UniTask OnUpdate(float dt)
        {
            if (OnUpdateAsync != null)
            {
                await OnUpdateAsync(dt);
            }
        }
    }
}