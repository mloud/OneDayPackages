using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace OneDay.OpenAi.Chat
{
    public class Result
    {
        public ChatGPTResponseModel Data { get; }
        public string Error { get; }

        public Result(ChatGPTResponseModel data, string error)
        {
            Data = data;
            Error = error;
        }
    }
    
    public class ChatGPT
    {
        private string Url { get; }
        private string ApiKey { get; }
        
        public ChatGPT(string url, string apiKey)
        {
            Url = url;
            ApiKey = apiKey;
        }

        public async UniTask<Result> Generate(string prompt, string model)
        {
            var requestModel = new ChatGPTRequestModel
            {
                model = model,
                messages = new List<ChatGPTRequestModel.Message> { new() { content = prompt, role = "user"}}
            };
            
            using var request = UnityWebRequest.Post(Url, "");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {ApiKey}");
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestModel)));
            request.downloadHandler = new DownloadHandlerBuffer();

            try
            {
                await request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    var responseModel =
                        JsonConvert.DeserializeObject<ChatGPTResponseModel>(request.downloadHandler.text);
                    return new Result(responseModel, null);
                    //return DataResult<string>.WithData(responseModel.choices[0].message.content);
                }

                return new Result(null, request.error);
            }
            catch (UnityWebRequestException ex)
            {
                Debug.LogError(ex.Message);
                return new Result(null, ex.Message);
            }
            catch (ArgumentException ex)
            {
                Debug.LogError(ex.Message);
                return new Result(null, ex.Message);

            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return new Result(null, ex.Message);
            }
        }
    }
}