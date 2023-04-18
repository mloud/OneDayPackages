using System;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using OneDay.Requests;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine;

namespace OneDay.PlayfabServices
{
    public class PlayFabServerRequestService : IRequestService
    {
        public async UniTask<DataResult<T>> AcceptRequest<T>(Request request)
        {
            DataResult<T> requestResponse = null;
            try
            {
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                    {
                        FunctionName = request.Name,
                        FunctionParameter = request.Parameters,
                        GeneratePlayStreamEvent = true,
                    },
                    (result) =>
                    {
                        if (result.Error != null)
                        {
                            requestResponse = DataResult<T>.WithError(result.Error.Error + " " + result.Error.Message);
                            return;
                        }
                        if (result.FunctionResult == null)
                        {
                            requestResponse = DataResult<T>.WithError("No response from server");
                            return;
                        }

                        var type = result.FunctionResult.GetType();
                        
                        var jObject = (JsonObject) result.FunctionResult;

                        string error = jObject["Error"] != null ? jObject["Error"].ToString() : null;
                        var jData = jObject["Data"];

                        T data = default;
                        
                        if (jData != null && jData is not JsonObject)
                        {
                            data = (T) jData;
                        }
                        else
                        {
                            data = PlayFabSimpleJson.DeserializeObject<T>(jData.ToString());
                        }
                        Debug.Log($"Request {request.Name} finished with result: {jObject}");
                        requestResponse = error != null
                            ? DataResult<T>.WithError(error)
                            : DataResult<T>.WithData(data);
                    },

                    (error) => { requestResponse = DataResult<T>.WithError(error.ErrorMessage); }
                );
            }
            catch (Exception ex)
            {
                requestResponse = DataResult<T>.WithError(ex.Message);
            }

            await UniTask.WaitUntil(() => requestResponse != null);
            return requestResponse;
        }
    }
}