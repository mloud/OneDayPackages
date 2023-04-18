using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine.AddressableAssets;

namespace OneDay.Assets
{
    public interface IAssetManager : IManager
    {
        UniTask<T> Load<T>(string name);
        UniTask<T> Load<T>(AssetReference assetReference);
        void Release(string name);
        void ReleaseAll();
    }
}