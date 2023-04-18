using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using OneDay.Core;
using OneDay.Requests;
using UnityEngine;

namespace OneDay.Story
{
    public class PrivateStoryProvider<T> : IProvider<IStoryLibraryModel<T>> where T:IStoryModel
    {
        public async UniTask<IStoryLibraryModel<T>> Provide()
        {
            var response =
                await ObjectLocator.GetObject<IRequestManager>()
                    .SendRequest<Dictionary<string, string>>(new("getPrivateFairyTales", null));

            if (response.HasError())
            {
                Debug.LogError(response.Error);
                return null;
            }

            
            var model = new StoryLibraryModel<T>
            {
                // TODO
                Id =  "private",
                Story = new List<T>()
            };
            
            foreach (var fairyTalePair in response.Data)
            {
                model.Story.Add(JsonConvert.DeserializeObject<T>(fairyTalePair.Value));
            }
            return model;
        }
    }
}