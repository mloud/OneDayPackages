using Cysharp.Threading.Tasks;
using OneDay.StateMachine;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks.States
{
    public static class PlayStateFactory
    {
        public static IState CreateState(FallingBlockStateContext context)
        {
            var state = new CommonState();

            state.OnEnterAsync = () =>
            {
                context.MovingBox.StartMoving();
                return UniTask.CompletedTask;
            };

            state.OnLeaveAsync = () =>
            {
                context.MovingBox.StopMoving();
                return UniTask.CompletedTask;
            };

            state.OnUpdateAsync = (dt) =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // no block is falling
                    if (context.FallingBlock == null)
                    {
                        context.FallingBlock = context.BlockSpawner.Spawn(context.MovingBox.Direction);
                        context.FallingBlock.SetStoppedCallback(StoppedFalling);
                    }
                    // block is falling - try to stop him
                    else
                    {
                        context.FallingBlock.TryStopOnPlaceholder();
                    }
                }

                return UniTask.CompletedTask;
            };


            void StoppedFalling(GameObject placeholder)
            {
                if (placeholder != null)
                {
                    Object.Destroy(placeholder);
                    context.HitsCount++;
                    if (context.HitsCount == context.PlaceholdersCount)
                    {
                        state.StateMachine.MakeTransition(TransitionsNames.ToLostState);
                    }
                }
                else
                {
                    state.StateMachine.MakeTransition(TransitionsNames.ToLostState);
                }

                context.FallingBlock.SetStoppedCallback(null);
                context.FallingBlock = null;
            }

            return state;
        }
    }
}