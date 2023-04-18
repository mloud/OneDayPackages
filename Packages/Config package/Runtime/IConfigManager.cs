using Newtonsoft.Json.Linq;
using OneDay.Core;

namespace OneDay.Config
{
    public interface IConfigManager: IManager
    {
        T GetCustomConfig<T>(string key = null);
        JObject GetCustomConfig(string key);
    }
}