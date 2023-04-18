using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OneDay.Config;
using OneDay.Core;
using OneDay.Data;
using UnityEngine;

namespace OneDay.CasualGame
{
    public class LevelManager<TLevelDefinition, TLevelState> : 
        ABaseManager,
        ILevelManager<TLevelDefinition, TLevelState> where TLevelState : LevelState, new()
    {
        public IReadOnlyList<TLevelDefinition> GetLevelDefinitions() => LevelDefinitions;
        public IReadOnlyList<TLevelState> GetLevelStates() => LevelStates;
        private List<TLevelDefinition> LevelDefinitions { get; set; }
        private List<TLevelState> LevelStates { get; set; }
        private string DataStorageType { get; }
    
        public LevelManager(string dataStorageType) =>
            DataStorageType = dataStorageType;
        
        protected override async UniTask OnInitialize()
        {
            await FetchLevelDefinitions();
            await FetchLevelStates();
        }
        
        public IReadOnlyList<(TLevelDefinition levelDefinition, TLevelState levelState)>
            GetLevels()
        {
            Debug.Assert(LevelDefinitions != null);
            Debug.Assert(LevelStates != null);
            Debug.Assert(LevelStates.Count <= LevelDefinitions.Count);

            var levels = new List<(TLevelDefinition levelDefinition, TLevelState levelState)>();
            
            for (int i = 0; i < LevelDefinitions.Count; i++)
            {
                levels.Add((LevelDefinitions[i],  i < LevelStates.Count ? LevelStates[i] : null));
            }

            return levels;
        }

        private class LevelDefinitionsContainer
        {
            public List<TLevelDefinition> Levels;
        }
        
        private async UniTask<Result> FetchLevelDefinitions()
        {
            var levelDefinitionContainer = ObjectLocator.GetObject<IConfigManager>()
                .GetCustomConfig<LevelDefinitionsContainer>("level_definitions");

            LevelDefinitions = levelDefinitionContainer?.Levels;
            
            return levelDefinitionContainer == null
                ? Result.WithError("Could not fetch Level definition")
                : Result.WithOk();
        }

        public async UniTask<Result> SaveLevelStates() => 
            await ObjectLocator.GetObject<IDataManager>()
                .SaveData(DataStorageType, "level_progression", LevelStates);
        
        private async UniTask<Result> FetchLevelStates()
        {
            var levelStatesResult = await ObjectLocator.GetObject<IDataManager>()
                .GetData<List<TLevelState>>(DataStorageType, "level_progression");

            if (!levelStatesResult.HasError())
            {
                LevelStates = levelStatesResult.Data ?? new List<TLevelState>();
                return Result.WithOk();
            }
            else
            {
                Debug.LogError($"FetchLevelStates resulted in error {levelStatesResult.Error}");
                return Result.WithError("GetLevelStates resulted in error {levelStatesResult.Error}");
            }
        }

        public int GetCurrentLevel()
        {
            int lastIndex = LevelStates.FindLastIndex(x => x.Finished);
            return lastIndex == -1 ? 0 : lastIndex;
        }

        public void SetLevelFinished(int level, int score)
        {
                
            if (level < LevelStates.Count)
            {
                LevelStates[level].Finished = true;
                LevelStates[level].Score = score;
            }
            else
            {
                Debug.Assert(level == LevelStates.Count);
                LevelStates.Add(new TLevelState());
            }

            LevelStates[level].Finished = true;
            LevelStates[level].Score = score;
        }
    }
}