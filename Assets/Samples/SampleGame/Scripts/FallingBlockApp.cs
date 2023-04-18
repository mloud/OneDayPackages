using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class FallingBlockApp : AApp
    {
        [SerializeField] FallingBlocksGame gameLoop;
        [SerializeField] FallingBlockConfigStorage fallingBlockConfigStorage;
        protected override async UniTask OnInitialize()
        {
            //ObjectLocator.RegisterObject<IConfigManager>(
            //    new ConfigManager(fallingBlockConfigStorage, new ConfigManagerSettings("level_definitions")));
            //ObjectLocator.RegisterObject<IDataManager>(
            //    new DataManager(GetDataManagerSettings()));
            //ObjectLocator.RegisterObject<ILevelManager<LevelDefinition, LevelState>>(
            //    new LevelManager<LevelDefinition, LevelState>("local"));
            //ObjectLocator.RegisterObject<IGameManager<LevelDefinition, LevelState>>(
            //    new GameManager<LevelDefinition, LevelState>(gameLoop));

            //await ObjectLocator.GetObject<ILevelManager<LevelDefinition, LevelState>>().Initialize();
            
            //ObjectLocator.GetObject<IGameManager<LevelDefinition, LevelState>>().Play();
        }
    }
}