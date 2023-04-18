using OneDay.Core;
using Cysharp.Threading.Tasks;
using OneDay.Config;
using OneDay.Localization.Models;


namespace OneDay.Localization.Providers
{
    public class ConfigTextsProvider : IProvider<LocalizationDictionary>
    {
        public UniTask<LocalizationDictionary> Provide()
        {
            return new UniTask<LocalizationDictionary>(
                ObjectLocator.GetObject<IConfigManager>().GetCustomConfig<LocalizationDictionary>("localization"));
        }
    }
}