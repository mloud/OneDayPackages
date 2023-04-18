using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OneDay.Core
{
    public interface IStorageWithCache
    {
        public UniTask Fetch(params string[] keys);
        public UniTask Update();
    }
    
    public class StorageWithCache<T> : IStorageService<T>, IStorageWithCache
    {
        private IStorageService<T> Storage { get; }
        private ICache<T> Cache { get; }

        public StorageWithCache(IStorageService<T> storage)
        {
            Storage = storage;
            Cache = new DictionaryCache<T>();
        }

        public async UniTask Fetch(params string[] keys)
        {
            if (Cache.HasChangedKeys())
            {
                Debug.LogWarning("Storage witch cache has some changed keys, updating first..");
                await Update();
            }

            var storageResult = await Storage.Load(keys);
            foreach (var pair in storageResult)
            {
                Cache.Add(pair.Key, pair.Value, true);
            }
            Cache.MarkCurrentCacheState();
        }

        public async UniTask Update()
        {
            var changedObjects = Cache.GetChangedKeys(
                ModificationType.Added | ModificationType.Modified);

            foreach (var changedPair in changedObjects)
            {
                await Storage.Save(changedPair.Key, changedPair.Value);
            }
            changedObjects = Cache.GetChangedKeys(
                ModificationType.Removed);
      
            foreach (var changedPair in changedObjects)
            {
                await Storage.Delete(changedPair.Key);
            }
        }
        
        public async UniTask<T> Load(string id)
        {
            if (Cache.Has(id))
            {
                return Cache.Get(id);
            }

            bool has = await Storage.Has(id);
            if (has)
            {
                var result = await Storage.Load(id);
                Cache.Add(id, result, true);
                return result;
            }
            return default;
        }

        public async UniTask<Dictionary<string, T>> Load(params string[] keys)
        {
            var result = new Dictionary<string, T>();
            foreach (var key in keys)
            {
                result.Add(key, await Load(key));
            }

            return result;
        }

        public UniTask<bool> Has(string key) => 
            new(Cache.Has(key));
       
        public UniTask<Dictionary<string, T>> GetAll() => 
            new(new Dictionary<string, T>(Cache.GetAll()));

        public UniTask<string[]> GetAllIds() => 
            new(Cache.GetKeys().ToArray());
        
        public UniTask<Result> Save(string key, T value)
        {
            Cache.Add(key, value, true);
            return new UniTask<Result>(Result.WithOk());
        }

        public UniTask<Result> Delete(string key)
        {
            Cache.Remove(key);
            return new UniTask<Result>(Result.WithOk());
        }
    }
}