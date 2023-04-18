using System;
using System.Collections.Generic;

namespace OneDay.Core
{
    [Flags]
    public enum ModificationType
    {
        Added = 1,
        Modified = 2,
        Removed = 4
    }
    public interface ICache<T>
    {
        #region Write operations
        void Add(string key, T obj, bool overwrite);
        bool Remove(string key);
        void RemoveAll();
        void MarkCurrentCacheState();
        bool HasChangedKeys();
        #endregion

        #region Read operations
        bool Has(string key);
        T Get(string key);
        IReadOnlyDictionary<string, T> Get(params string[] keys);
        IReadOnlyDictionary<string, T> GetAll();
        IReadOnlyList<string> GetKeys();
        IReadOnlyDictionary<string, T> GetChangedKeys(ModificationType modificationFlag);
        #endregion
    }
}