using OneDay.Core;
using OneDay.Assets;
using OneDay.Localization.Models;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace OneDay.Providers
{
    public class LocalTextsProvider : IProvider<LocalizationDictionary>
    {
        public async UniTask<LocalizationDictionary> Provide()
        {
            var textAsset = await ObjectLocator.GetObject<IAssetManager>().Load<TextAsset>("localization");
            return JsonConvert.DeserializeObject<LocalizationDictionary>(textAsset.text);
        }
    }
}