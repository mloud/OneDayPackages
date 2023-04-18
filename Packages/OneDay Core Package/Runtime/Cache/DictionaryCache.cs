using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OneDay.Core;

namespace OneDay.Core
{
    public class DictionaryCache<T> : ICache<T>
    {
        private Dictionary<string, T> CachedObjects { get; }
        private HashSet<string> InitialKeys { get; set; }
        private HashSet<string> ModifiedKeys { get; set; }

        public DictionaryCache()
        {
            CachedObjects = new Dictionary<string, T>();
            InitialKeys = new HashSet<string>();
            ModifiedKeys = new HashSet<string>();
            MarkCurrentCacheState();
        }

        public DictionaryCache(Dictionary<string, T> cachedObjects)
        {
            CachedObjects = cachedObjects;
            InitialKeys = new HashSet<string>();
            ModifiedKeys = new HashSet<string>();
            MarkCurrentCacheState();
        }

        public bool Has(string key) => CachedObjects.ContainsKey(key);
        public void Add(string key, T obj, bool overwrite)
        {
            if (CachedObjects.ContainsKey(key))
            {
                if (overwrite)
                {
                    CachedObjects[key] = obj;
                    ModifiedKeys.Add(key);
                }
                else
                {
                    throw new ArgumentException($"Object with key is already present in cache {key}");
                }
            }
            else
            {
                CachedObjects.Add(key, obj);
            }
        }
        
        
        public T Get(string key)
        {
            if (CachedObjects.TryGetValue(key, out var value))
                return (T) value;
            
            return default;
        }

        public IReadOnlyDictionary<string, T> Get(params string[] keys)
        {
            var result = new Dictionary<string, T>();
            foreach (var key in keys)
            {
                result.Add(key, Get(key));
            }

            return result;
        }

        public void MarkCurrentCacheState()
        {
            InitialKeys = CachedObjects.Keys.ToHashSet();
            ModifiedKeys.Clear();
        }
        
        public IReadOnlyList<string> GetKeys() =>
            CachedObjects.Keys.ToArray();

        public bool HasChangedKeys() =>
            ModifiedKeys.Count > 0 || 
            InitialKeys.Except(CachedObjects.Keys).Any() ||
            CachedObjects.Keys.Except(InitialKeys).Any();
        
        public IReadOnlyDictionary<string, T> GetChangedKeys(ModificationType modificationFlag)
        {
            var changedObjects = new Dictionary<string,T>();

            // removed
            if ((modificationFlag & ModificationType.Removed) != 0)
            {
                var removedKeys = InitialKeys.Except(CachedObjects.Keys);
                foreach (var removedKey in removedKeys)
                {
                    changedObjects.Add(removedKey, Get(removedKey));
                }
            }
            
            // added
            if ((modificationFlag & ModificationType.Added) != 0)
            {
                var addedKeys = CachedObjects.Keys.Except(InitialKeys);
                foreach (var addedKey in addedKeys)
                {
                    changedObjects.Add(addedKey, Get(addedKey));
                }
            }
            
            // modified
            if ((modificationFlag & ModificationType.Modified) != 0)
            {
                foreach (var modifiedKey in ModifiedKeys)
                {
                    if (!changedObjects.ContainsKey(modifiedKey))
                    {
                        changedObjects.Add(modifiedKey, Get(modifiedKey));
                    }
                }
            }

            return changedObjects;
        }

        public IReadOnlyDictionary<string, T> GetAll() => 
            new ReadOnlyDictionary<string, T>(CachedObjects);

        public bool Remove(string key) =>
            CachedObjects.Remove(key);
      
        public void RemoveAll() =>
            CachedObjects.Clear();
    }
}