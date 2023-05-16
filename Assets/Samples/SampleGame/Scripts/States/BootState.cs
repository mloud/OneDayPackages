using Cysharp.Threading.Tasks;
using OneDay.Assets;
using OneDay.CasualGame;
using OneDay.Config;
using OneDay.Core;
using OneDay.Data;
using OneDay.Data.Storages;
using OneDay.Localization;
using OneDay.Providers;
using OneDay.StateMachine.Common;
using OneDay.Ui;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks.States
{
    public class BootState : AppState
    {
        public BootState(AppStateConfig appStateConfig)
            : base(appStateConfig)
        { }
   
        protected override async UniTask OnEnter()
        {
            ObjectLocator.RegisterObject<IConfigManager>(
                new ConfigManager(FallingBlockConfigStorage.Instance, new ConfigManagerSettings("level_definitions")));
            ObjectLocator.RegisterObject<IDataManager>(
                new DataManager(GetDataManagerSettings()));
            ObjectLocator.RegisterObject<IAssetManager>(
                new AssetManager());
            ObjectLocator.RegisterObject<ILevelManager<LevelDefinition, LevelState>>(
                new LevelManager<LevelDefinition, LevelState>("local"));
            ObjectLocator.RegisterObject<ILocalizationManager>(new LocalizationManager(new LocalTextsProvider(), null));
            ObjectLocator.RegisterObject<IUiManager>(GameObject.FindObjectOfType<UiManager>());

            foreach (var managers in ObjectLocator.GetObjects<IManager>())
            {
                await managers.Initialize();
            }

            await MakeTransition(TransitionsConst.ToMenuState);
        }
        
        private DataManagerSettings GetDataManagerSettings() =>
            new()
            {
                Storages = new()
                {
                    {"local", new PlayerPrefsStorage()}
                }
            };
    }
}