using OneDay.Core;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace OneDay.Statistic
{
    public class SimpleStatisticManagerSettings
    {
        public string[] StatisticNames { get; }
        public SimpleStatisticManagerSettings(params string[] statisticNames)
        {
            StatisticNames = statisticNames;
        }
    }
    public class SimpleStatisticManager : ABaseManager, ISimpleStatisticManager
    {
        public bool IsInitialized { get; private set; }
        private SimpleStatisticManagerSettings Settings { get; }
        private StorageWithCache<int>  StorageService { get; }
        
        public SimpleStatisticManager(StorageWithCache<int> storageService, SimpleStatisticManagerSettings settings)
        {
            Settings = settings;
            StorageService = storageService;
        }

        protected override async UniTask OnInitialize()
        {
            await FetchAllStatistic();
            #if DEBUG
            var all = await StorageService.GetAll();
            Debug.Log($"StatisticManager: {JsonConvert.SerializeObject(all)}");
           #endif
            IsInitialized = true;
        }

        public async UniTask FetchAllStatistic() => 
            await StorageService.Fetch(Settings.StatisticNames);

        public async UniTask SaveAllStatistic()
        {
            if (!CheckIfInitialized())
                return;
            
            await StorageService.Update();
        }

        public async UniTask ClearAllStatistic()
        {
            foreach (var statistic in Settings.StatisticNames)
            {
                await StorageService.Delete(statistic);
            }
        }

        public async UniTask<int> GetStatistic(string key)
        {
            if (!CheckIfInitialized())
                return default;
                
            return await StorageService.Load(key);
        }

        public async UniTask SetStatistic(string key, int value)
        {
            if (!CheckIfInitialized())
                return;
            
            await StorageService.Save(key, value);
        }

        public async UniTask IncrementStatistic(string key, int byValue)
        {
            if (!CheckIfInitialized())
                return;
                
            bool hasKey = await StorageService.Has(key);
            int value = hasKey ? await StorageService.Load(key) : 0;
            await StorageService.Save(key, value + byValue);
        }

        private bool CheckIfInitialized()
        {
            if (!IsInitialized)
            {
                Debug.LogError("Statistic manager is not initialized");
                return false;
            }

            return true;
        }
    }
}