using Cysharp.Threading.Tasks;
using OneDay.CasualGame;
using OneDay.Samples.FallingBlocks.States;
using OneDay.StateMachine;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class FallingBlocksGame : AGameLoop<LevelDefinition>
    {
        [SerializeField] private LevelBuilder levelBuilder;
        [SerializeField] private int level;
      
        private IStateMachine StateMachine { get; set; }
        
        protected override UniTask OnLoad(LevelDefinition levelDefinition, int level)
        {
            // create environment
            levelBuilder.BuildLevel(levelDefinition);
          
            // prepare state machine for game
            var context = new FallingBlockStateContext
            {
                BlockSpawner = GetComponentInChildren<BlockSpawner>(),
                MovingBox = GetComponentInChildren<MovingBox>(),
                LostAction = TriggerLevelFailed,
                WinAction = ()=> TriggerLevelFinished(0),
                PlaceholdersCount = GetComponentsInChildren<FallingBlock>().Length
            };
            
            StateMachine = new StateMachine.StateMachine(StateNames.PlayState);
            StateMachine.RegisterState(PlayStateFactory.CreateState(context), StateNames.PlayState);
            StateMachine.RegisterState(LostStateFactory.CreateState(context), StateNames.LostState);
            StateMachine.RegisterState(LostStateFactory.CreateState(context), StateNames.WinState);

            StateMachine.RegisterTransition(TransitionsNames.ToLostState, StateNames.PlayState, StateNames.LostState);
            StateMachine.RegisterTransition(TransitionsNames.ToWinState, StateNames.PlayState, StateNames.WinState);

            return UniTask.CompletedTask;
        }

        protected override void OnPlay(LevelDefinition levelDefinition, int level)
        {
            // start state machine
            StateMachine.Run();
        }

        protected override void OnPaused(bool paused)
        { }

        private void Update() =>
            StateMachine?.Update(Time.deltaTime);
    }
}