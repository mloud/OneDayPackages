using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using OneDay.Core;
using OneDay.Requests;

namespace OneDay.Story
{
    public class PrivateStorySaver<T> : ISaver<T> where T: IStoryModel
    {
        private string Key { get; }
        public PrivateStorySaver(string key) =>
            Key = key;
        public async UniTask<Result> Save(T storyModel)
        {
            var data = new Dictionary<string, object>
            {
                {"id", storyModel.Id},
                {"fairyTaleModel", JsonConvert.SerializeObject(storyModel)}
            };

            var result = await ObjectLocator.GetObject<IRequestManager>()
                .SendRequest<bool>(new(Key, data));
            
            return result.Data ? Result.WithOk() : Result.WithError("Error during saving fairy tale");
        }
    }
}