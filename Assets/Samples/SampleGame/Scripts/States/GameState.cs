using Cysharp.Threading.Tasks;
using OneDay.CasualGame;
using OneDay.Core;
using OneDay.Samples.FallingBlocks.Screens;
using OneDay.StateMachine.Common;
using OneDay.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDay.Samples.FallingBlocks.States
{
    public class GameState : AppState
    {
        public GameState(AppStateConfig appStateConfig) 
            : base(appStateConfig)
        { }

        protected override async UniTask OnEnter()
        {
            await SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);

            await ObjectLocator.GetObject<IUiManager>()
                .SwitchScreen<GameScreenController, GameScreenData>(
                    new GameScreenData());
            
            ObjectLocator.RegisterObject<IGameManager<LevelDefinition, LevelState>>(
                new GameManager<LevelDefinition, LevelState>(Object.FindObjectOfType<FallingBlocksGame>()));
            await ObjectLocator.GetObject<IGameManager<LevelDefinition, LevelState>>().Initialize();
            
            ObjectLocator.GetObject<IGameManager<LevelDefinition, LevelState>>().Play();
        }

        protected override async UniTask OnLeave()
        {
            ObjectLocator.UnregisterObject<IGameManager<LevelDefinition, LevelState>>();
        }
    }
}