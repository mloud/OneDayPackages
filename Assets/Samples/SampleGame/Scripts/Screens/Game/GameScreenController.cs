using Cysharp.Threading.Tasks;
using OneDay.Core;
using OneDay.Samples.FallingBlocks.States;
using OneDay.StateMachine.Common;
using OneDay.Ui;

namespace OneDay.Samples.FallingBlocks.Screens
{
    
    
    public class GameScreenController : AScreenController<GameScreenView, GameScreenData>
    {
        protected override UniTask OnSetup()
        {
            return UniTask.CompletedTask;
        }

        protected override async UniTask OnShow(GameScreenData data, IScreenInternalData internalData)
        {
            
        }
    }
}