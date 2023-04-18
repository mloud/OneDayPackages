using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Data
{
    public interface IDataManager : IManager
    {
        UniTask<DataResult<T>> GetData<T>(string storageType, string key);
        UniTask<Result> SaveData<T>(string storageType, string key, T data);
        UniTask<Result> DeleteData<T>(string storageType, string key);
    }
}