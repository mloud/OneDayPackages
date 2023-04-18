using OneDay.CasualGame;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class FallingBlocksGame : AGameLoop<LevelDefinition>
    {
        [SerializeField] private LevelBuilder levelBuilder;
        [SerializeField] private int level;
        private MovingBox MovingBox { get; set; }
        private BlockSpawner BlockSpawner { get; set; }

        private enum State
        {
            Targeting, 
            Falling,
            Failed
        }

        private State CurrentState { get; set; }
        private FallingBlock FallingBlock { get; set; }
        protected override void OnPlay(LevelDefinition levelDefinition, int level)
        {
            CurrentState = State.Targeting;
            levelBuilder.BuildLevel(levelDefinition);
            MovingBox = GetComponentInChildren<MovingBox>();
            BlockSpawner =  GetComponentInChildren<BlockSpawner>();
            MovingBox.StartMoving();
        }

        protected override void OnPaused(bool paused)
        {
            
        }

        private void OnStoppedFalling(GameObject placeholder)
        {
            if (placeholder != null)
            {
                Destroy(placeholder);
                CurrentState = State.Targeting;
            }
            else
            {
                Debug.LogError("LOOSE");      
                TriggerLevelFailed();
                CurrentState = State.Failed;
                MovingBox.StopMoving();
            }
            FallingBlock.StoppedFalling = null;
            FallingBlock = null;
        }
        
        private void Update()
        {
            switch (CurrentState)
            {
                case  State.Targeting:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        CurrentState = State.Falling;
                        FallingBlock = BlockSpawner.Spawn(MovingBox.Direction);
                        FallingBlock.StoppedFalling += OnStoppedFalling;
                    }
                    break;
                
                case  State.Falling:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        FallingBlock.TryStopOnPlaceholder(); 
                    }
                    break;
            }
        }
    }
}