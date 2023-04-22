﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.CasualGame
{
    public abstract class AGameLoop<TLevelDefinition> : MonoBehaviour, IGameLoop<TLevelDefinition>
    {
        public IGameLoop<TLevelDefinition>.LevelFinishedDelegate LevelFinished { get; set; }
        public IGameLoop<TLevelDefinition>.LevelFailedDelegate LevelFailed { get; set; }

        public async UniTask Load(TLevelDefinition levelDefinition, int level) => 
            await OnLoad(levelDefinition, level);
        
        public void Play(TLevelDefinition levelDefinition, int level) =>
            OnPlay(levelDefinition, level);
      
        public void SetPaused(bool paused) =>
            OnPaused(paused);
        
        protected abstract UniTask OnLoad(TLevelDefinition levelDefinition, int level);
        protected abstract void OnPlay(TLevelDefinition levelDefinition, int level);
        protected abstract void OnPaused(bool paused);

        protected void TriggerLevelFailed()
        {
            LevelFailed();
            Debug.Log("AGameLoop - TriggerLevelFailed");
        }

        protected void TriggerLevelFinished(int score)
        {
            Debug.Log("AGameLoop - TriggerLevelFinished");
            LevelFinished(score);
        }
    }
}