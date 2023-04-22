using Cysharp.Threading.Tasks;
using OneDay.StateMachine;

namespace OneDay.Samples.FallingBlocks.States
{
    public static class WinStateFactory
    {
        public static IState CreateState(FallingBlockStateContext context)
        {
            var state = new CommonState();

            state.OnEnterAsync = () =>
            {
                context.WinAction();
                return UniTask.CompletedTask;
            };
            return state;
        }
    }
}