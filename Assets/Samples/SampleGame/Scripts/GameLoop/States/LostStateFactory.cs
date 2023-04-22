using Cysharp.Threading.Tasks;
using OneDay.StateMachine;

namespace OneDay.Samples.FallingBlocks.States
{
    public static class LostStateFactory
    {
        public static IState CreateState(FallingBlockStateContext context)
        {
            var state = new CommonState();

            state.OnEnterAsync = () =>
            {
                context.LostAction();
                return UniTask.CompletedTask;
            };
            return state;
        }
    }
}