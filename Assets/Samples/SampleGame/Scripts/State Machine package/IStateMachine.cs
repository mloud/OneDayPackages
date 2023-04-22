using Cysharp.Threading.Tasks;

namespace OneDay.StateMachine
{
    public interface IStateMachine
    {
        public void RegisterState(IState state, string name);
        public void RegisterTransition(string triggerName, string sourceState, string destinationState);

        UniTask Run();
        UniTask<bool> MakeTransition(string triggerName);
        UniTask Update(float dt);
    }
}