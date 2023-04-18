using Cysharp.Threading.Tasks;
using OneDay.Core;
using OneDay.Samples.FallingBlocks.States;
using OneDay.StateMachine.Common;
using OneDay.Ui;

namespace OneDay.Samples.FallingBlocks.Screens
{
    public class MenuScreenController : AScreenController<MenuScreenView, MenuScreenData>
    {
        protected override UniTask OnSetup()
        {
            View.PlayButton.RegisterListenerWithClear(OnPlay);
            return UniTask.CompletedTask;
        }

        protected override async UniTask OnShow(MenuScreenData data, IScreenInternalData internalData)
        {
    
        }

        private void OnPlay() =>
            ObjectLocator.GetObject<IAppStateManager>().MakeTransition(TransitionsConst.ToGameState);
    }
}