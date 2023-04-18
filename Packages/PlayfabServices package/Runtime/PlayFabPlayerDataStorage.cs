using OneDay.Core;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace OneDay.PlayfabServices
{
    public class PlayFabUserStorage : IStorageService<string>
    {
        public static readonly int DataDoesNotExist = 0;
        public static readonly int DataKeyDoesNotExist = 1;
        public static readonly int DataProviderError = 2;

        private string TitleId { get; }

        public PlayFabUserStorage(string titleId) =>
            TitleId = titleId;
       
        public async UniTask<Dictionary<string, string>> Load(params string[] keys)
        {
            var result = await GetUserData(keys);
            return result.HasError() ? null : result.Data;
        }

        public async UniTask<bool> Has(string key)
        {
            Debug.LogWarning("This call is http request, don't use of not really necessary");
            return (await Load(key)) != null;
        }

        public async UniTask<string> Load(string id)
        {
            var result = await GetUserData(id);
            if (result.HasError())
                return null;
            
            return result.Data.TryGetValue(id, out var value) ? value : null;
        }

        public async UniTask<Result> Delete(string id) =>
            await DeleteUserData(id);
        
        public async UniTask<Result> Save(string id, string value) =>
            await SaveUserData(new Dictionary<string, string>{ {id, value}});
        
        private static async UniTask<Result> SaveUserData(Dictionary<string, string> data)
        {
            bool isDone = false;
            string requestError = null;
            int errorCode = -1;

            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    Data = data
                },
                result =>
                {
                    Debug.Log($"PlayFabPlayerDataReadonlyStorage SaveUserData {string.Join(",", data.Keys)} saved");
                    isDone = true;
                },
                error =>
                {
                    requestError = error.ErrorMessage;
                    errorCode = DataProviderError;
                });
            await UniTask.WaitUntil(() => requestError != null || isDone);

            return errorCode == -1
                ? Result.WithOk()
                : Result.WithError(errorCode, requestError);
        }
        
        private static async UniTask<Result> DeleteUserData(params string[] keys)
        {
            bool isDone = false;
            string requestError = null;
            int errorCode = -1;

            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    KeysToRemove = keys.ToList()
                },
                result =>
                {
                    Debug.Log($"PlayFabPlayerDataReadonlyStorage DeleteUserData {string.Join(",", keys)} deleted");
                    isDone = true;
                },
                error =>
                {
                    requestError = error.ErrorMessage;
                    errorCode = DataProviderError;
                });
            await UniTask.WaitUntil(() => requestError != null || isDone);

            return errorCode == -1
                ? Result.WithOk()
                : Result.WithError(errorCode, requestError);
        }
    
        private static async UniTask<DataResult<Dictionary<string, string>>> GetUserData(params string[] keys)
        {
            string requestError = null;
            int errorCode = -1;
            Dictionary<string, string> requestResult = null;

            PlayFabClientAPI.GetUserData(new GetUserDataRequest
                {
                    Keys = keys.Length == 0 ? null : keys.ToList(),
                },
                result =>
                {
                    if (result.Data == null)
                    {
                        requestError = "Data does not exist";
                        errorCode = DataDoesNotExist;
                    }
                    else
                    {
                        requestResult =
                            result.Data.ToDictionary(x => x.Key, y => y.Value.Value);
                    }
                },
                error =>
                {
                    requestError = error.ErrorMessage;
                    errorCode = DataProviderError;
                }
            );
            
            await UniTask.WaitUntil(() => requestError != null || requestResult != null);

            return errorCode == -1
                ? DataResult<Dictionary<string, string>>.WithData(requestResult)
                : DataResult<Dictionary<string, string>>.WithError(errorCode, requestError);
        }
    }
}