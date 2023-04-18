using Cysharp.Threading.Tasks;
using OneDay.CasualGame;
using OneDay.Core;
using OneDay.Samples.FallingBlocks.Screens;
using OneDay.StateMachine.Common;
using OneDay.Ui;
using UnityEngine.SceneManagement;

namespace OneDay.Samples.FallingBlocks.States
{
    public class MenuState : AppState
    {
        public MenuState(AppStateConfig appStateConfig) 
            : base(appStateConfig)
        { }

        protected override async UniTask OnEnter()
        {
            await SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            await ObjectLocator.GetObject<IUiManager>()
                .SwitchScreen<MenuScreenController, MenuScreenData>(
                    new MenuScreenData(ObjectLocator.GetObject<ILevelManager<LevelDefinition, LevelState>>()
                        .GetLevelStates()));
            
        }
    }
}