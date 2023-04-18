using Cysharp.Threading.Tasks;

namespace OneDay.StateMachine.Common
{
    public abstract class AppState : AState
    {
        private AppStateConfig Config { get; }
        protected AppState(AppStateConfig appStateConfig) 
        {
            Config = appStateConfig;
        }
     
        protected async UniTask MakeTransition(string name) =>
            await StateMachine.MakeTransition(name);
    }
}