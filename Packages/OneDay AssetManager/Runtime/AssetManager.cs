using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace OneDay.Assets
{
    public class AssetManager : ABaseManager, IAssetManager
    {
        private Dictionary<string, AsyncOperationHandle> Handles { get; }

        public AssetManager() =>
            Handles = new Dictionary<string, AsyncOperationHandle>();
       
        public async UniTask<T> Load<T>(AssetReference assetReference)
        {
            var handle = assetReference.LoadAssetAsync<T>();
            await handle.Task;
            return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : default;
        }
        
        public async UniTask<T> Load<T>(string assetName)
        {
            if (Handles.ContainsKey(assetName))
                return (T)Handles[assetName].Result;
            
            var loadHandle = Addressables.LoadAssetAsync<T>(assetName);
            await loadHandle.Task;
            
            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                if (!Handles.ContainsKey(assetName))
                {
                    Handles.Add(assetName, loadHandle);
                }
                else
                {
                    Addressables.Release(loadHandle);
                    return (T)Handles[assetName].Result;
                }

                return loadHandle.Result;
            }
            Addressables.Release(loadHandle);
        
            return default;
        }

        public void Release(string assetName)
        {
            if (Handles.ContainsKey(assetName))
            {
                Addressables.Release(Handles[assetName]);
                Handles.Remove(assetName);
            }
            else
            {
                ///Debug.LogError($"No such asset {assetName} exists among handles");
            }
        }

        public void ReleaseAll()
        {
            foreach (var handle in Handles)
            {
                Addressables.Release(handle);
            }
            Handles.Clear();
        }
    }
}