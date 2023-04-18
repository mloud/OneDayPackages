using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace OneDay.PlayfabServices
{
    public class PlayFabTitleDataReadonlyStorage : IReadOnlyKeyStorage<string>
    {
        public static readonly int DataDoesNotExist = 0;
        public static readonly int DataKeyDoesNotExist = 1;
        public static readonly int DataProviderError = 2;
    
        private string TitleId { get; }

        public PlayFabTitleDataReadonlyStorage(string titleId) =>
            TitleId = titleId;
        
        public async UniTask<Dictionary<string, string>> GetAll() =>
            (await GetAllData()).Data;
        
        public async UniTask<Dictionary<string, string>> Load(params string[] keys)
        {
            var result = await GetKeyValue(keys);
            return result.HasError() ? null : result.Data;
        }

        public async UniTask<bool> Has(string key)
        {
            Debug.LogWarning("This call is http request, don't use of not really necessary");    
            return (await Load(key)) != null;
        }

        public async UniTask<string> Load(string id)
        {
            var result = await GetKeyValue(id);

            if (result.HasError())
                return null;
            
            return result.Data.ContainsKey(id) 
                ? result.Data[id] 
                : null;
        }
        private async UniTask<DataResult<Dictionary<string, string>>> GetAllData()
        {
            string requestError = null;
            int errorCode = -1;
            Dictionary<string, string> requestResult = null;

            PlayFabSettings.TitleId = TitleId;
            
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
                result =>
                {
                    if (result.Data == null)
                    {
                        requestError = "Data does not exist";
                        errorCode = DataDoesNotExist;
                    }
                    else
                    {
                        requestResult = result.Data;
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
        
        private async UniTask<DataResult<Dictionary<string, string>>> GetKeyValue(params string[] keys)
        {
            string requestError = null;
            int errorCode = -1;
            Dictionary<string, string> output = null;
            
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest
                {
                    Keys = keys.ToList()
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
                        output = result.Data;
                    }
                },
                error =>
                {
                    requestError = error.ErrorMessage;
                    errorCode = DataProviderError;
                }
            );

            await UniTask.WaitUntil(() => requestError != null || output != null);

            return errorCode == -1
                ? DataResult<Dictionary<string, string>>.WithData(output)
                : DataResult<Dictionary<string, string>>.WithError(errorCode, requestError);
        }
    }
}