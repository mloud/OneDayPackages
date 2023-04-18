using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using OneDay.Core;

namespace OneDay.Data
{
    public class DataManager : ABaseManager, IDataManager
    {
        private DataManagerSettings Settings { get; }

        public DataManager(DataManagerSettings settings) =>
            Settings = settings;
    
        public async UniTask<DataResult<T>> GetData<T>(string storageType, string key)
        {
            if (!Settings.Storages.ContainsKey(storageType))
                return DataResult<T>.WithError($"Storage type {storageType} does not exist");
          
            var result = await Settings.Storages[storageType].Load(key);

            return DataResult<T>.WithData(result != null ? JsonConvert.DeserializeObject<T>(result) : default);
        }

        public async UniTask<Result> SaveData<T>(string storageType, string key, T data)
        {
            if (!Settings.Storages.ContainsKey(storageType))
                return Result.WithError($"Storage type {storageType} does not exist");
         
            var result = await Settings.Storages[storageType].Save(key, JsonConvert.SerializeObject(data));
            return result;
        }
        
        public async UniTask<Result> DeleteData<T>(string storageType, string key)
        {
            if (!Settings.Storages.ContainsKey(storageType))
                return Result.WithError($"Storage type {storageType} does not exist");
         
            var result = await (Settings.Storages[storageType]).Delete(key);
            return result;
        }
    }
}