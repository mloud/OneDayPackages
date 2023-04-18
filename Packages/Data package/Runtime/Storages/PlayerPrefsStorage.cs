using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Data.Storages
{
    public class PlayerPrefsStorage : IStorageService<string>
    {
        public UniTask<string> Load(string id) => 
            new(PlayerPrefs.GetString(id, null));

        public UniTask<Dictionary<string, string>> Load(params string[] keys)
        {
            var result = new Dictionary<string, string>();
            Array.ForEach(keys, key => result.Add(key, PlayerPrefs.GetString(key,null)));
            return new(result);
        }

        public UniTask<bool> Has(string key) => 
            new(PlayerPrefs.HasKey(key));

        public UniTask<Result> Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
            return new UniTask<Result>(Result.WithOk());
        }
 
        public UniTask<Result> Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
            return new UniTask<Result>(Result.WithOk());
        }
    }
}