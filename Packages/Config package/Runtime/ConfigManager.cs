using OneDay.Core;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace OneDay.Config
{
    public class ConfigManager : ABaseManager, IConfigManager
    {
        private IReadOnlyKeyStorage<string> Storage { get; }

        private JObject JObjectConfig { get; }
        private ICache<object> Cache { get; }
        
        private bool IsInitialized { get; set; }
        
        private ConfigManagerSettings Settings { get; }
        public ConfigManager(IReadOnlyKeyStorage<string> storage, ConfigManagerSettings settings)
        {
            Cache = new DictionaryCache<object>();
            Storage = storage;
            JObjectConfig = new JObject();
            Settings = settings;
        }
        
        protected override async UniTask OnInitialize()
        {
            Cache.RemoveAll();
            var result = await Storage.Load(Settings.ConfigKeys);
            
            foreach (var pair in result)
            {
                if (pair.Value != null)
                {
                    JObjectConfig.Add(pair.Key, JObject.Parse(pair.Value));
                    Debug.Log($"AppConfig loaded config with key:{pair.Key} and value:{pair.Value}");
                }
                else
                {
                    Debug.LogError($"Could not load config with key {pair.Key}");
                }
            }
            IsInitialized = true;
        }

        public T GetCustomConfig<T>(string key = null)
        {
            Debug.Assert(IsInitialized, "AppConfig is not initialized");    
                
            key ??= typeof(T).Name;
            var cachedItem = Cache.Get(key);
            if (cachedItem != null)
            {
                return (T)cachedItem;
            }

            
            if (!JObjectConfig.ContainsKey(key))
            {
                Debug.LogError($"AppConfig no such key {key} exists");
                return default;
            }

            var deserializedItem = JObjectConfig[key]!.ToObject<T>();
        
            if (deserializedItem != null)
            {
                Cache.Add(key, deserializedItem, false);
            }

            return deserializedItem;
        }

        public JObject GetCustomConfig(string key)
        {
            Debug.Assert(IsInitialized, "AppConfig is not initialized");
            return JObjectConfig.ContainsKey(key)
                ? JObjectConfig[key] as JObject
                : default;
        }
    }
}