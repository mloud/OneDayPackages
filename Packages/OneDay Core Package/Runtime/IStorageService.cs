using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace OneDay.Core
{
    public interface IReadOnlyKeyStorage<TData>
    {
        UniTask<TData> Load(string id);
        UniTask<Dictionary<string, TData>> Load(params string[] keys);
        UniTask<bool> Has(string key);
    }
    
    public interface IStorageService<TData> : IReadOnlyKeyStorage<TData>
    {
        UniTask<Result> Save(string key, TData value);
        UniTask<Result> Delete(string key);
    }
}