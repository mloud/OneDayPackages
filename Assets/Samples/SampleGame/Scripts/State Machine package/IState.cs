using Cysharp.Threading.Tasks;

namespace OneDay.StateMachine
{
    public interface IState
    {
        IStateMachine StateMachine { get; set; }
        UniTask Setup();
        UniTask Enter();
        UniTask Leave();
    }
}