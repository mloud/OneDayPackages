using Cysharp.Threading.Tasks;
using OneDay.Core;
using OneDay.GameUi;
using OneDay.Samples.FallingBlocks.States;
using OneDay.StateMachine.Common;
using OneDay.Ui;

namespace OneDay.CasualGame
{
    public class GameManager<TLevelDefinition, TLevelState> :
        ABaseManager, IGameManager<TLevelDefinition, TLevelState>
        where TLevelState: LevelState, new() 
        where TLevelDefinition: class
    {
        public LevelStarted LevelStarted { get; set; }
        public LevelFinished LevelFinished { get; set; }
        public GamePaused GamePaused { get; set; }

        private IGameLoop<TLevelDefinition> GameLoop { get; } 
   
        private ILevelManager<TLevelDefinition, TLevelState> LevelManager { get; set; }
        private class GameLoopInfo
        {
            public int Level;
            public bool IsRunning;
        }
        private GameLoopInfo CurrentGameLoop { get; set; }

        public GameManager(
            IGameLoop<TLevelDefinition> gameLoop)
        {
            GameLoop = gameLoop;
            CurrentGameLoop = new();
            GameLoop.LevelFinished += OnLevelFinished;
            GameLoop.LevelFailed += OnLevelFailed;
        }

        protected override async UniTask OnInitialize()
        {
            LevelManager = ObjectLocator.GetObject<ILevelManager<TLevelDefinition, TLevelState>>();
        }
        
        
        public bool IsGameRunning() => CurrentGameLoop.IsRunning;

        public void Play() =>
            InternalPlayAsync(LevelManager.GetCurrentLevel());

        public void Play(int level) => 
            InternalPlayAsync(level);
    
        private async UniTask InternalPlayAsync(int level)
        {
            CurrentGameLoop.Level = level;
            CurrentGameLoop.IsRunning = true;
            await GameLoop.Load(LevelManager.GetLevelDefinitions()[level], level);
            GameLoop.Play(LevelManager.GetLevelDefinitions()[level], level);
            LevelStarted?.Invoke(level);
        }
        
        public void Pause()
        {
            GameLoop.SetPaused(true);      
        }

        private void OnLevelFinished(int score)
        {
            CurrentGameLoop.IsRunning = false;
            LevelManager.SetLevelFinished(CurrentGameLoop.Level, score);
            LevelFinished?.Invoke(new LevelResult
            {
                Finished = true,
                Score = score
            });

            // TODO
            int reward = 99;
            int stars = 3;
            
            ObjectLocator.GetObject<IUiManager>().ShowPopup<LevelWinPopup, LevelWinPopupData>(new LevelWinPopupData
            {
                Stars = stars,
                Reward = reward,
                ClaimButtonAction = () =>
                {
                    // goto menu
                    ObjectLocator.GetObject<IAppStateManager>().MakeTransition(TransitionsConst.ToMenuState);
                },
                DoubleRewardAction = () =>
                {
                    // goto menu
                    ObjectLocator.GetObject<IAppStateManager>().MakeTransition(TransitionsConst.ToMenuState);
                }
            });
        }

        private void OnLevelFailed()
        {
            CurrentGameLoop.IsRunning = false;
            LevelFinished?.Invoke(new LevelResult
            {
                Finished = false,
                Score = 0
            });
            
            ObjectLocator.GetObject<IUiManager>().ShowPopup<LevelLoosePopup, LevelLoosePopupData>(new LevelLoosePopupData()
            {
            
                RetryButtonAction = () =>
                {
                    // goto menu
                    ObjectLocator.GetObject<IAppStateManager>().MakeTransition(TransitionsConst.ToMenuState);
                },
                ExitButtonAction = () =>
                {
                    ObjectLocator.GetObject<IAppStateManager>().MakeTransition(TransitionsConst.ToMenuState);
                }
            });
        }
    }
}