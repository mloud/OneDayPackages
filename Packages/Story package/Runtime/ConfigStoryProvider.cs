using Cysharp.Threading.Tasks;
using OneDay.Config;
using OneDay.Core;

namespace OneDay.Story
{
    public class ConfigStoryProvider<T> : IProvider<IStoryLibraryModel<T>> where T: IStoryModel
    {
        private string Key { get; }

        public ConfigStoryProvider(string key) =>
            Key = key;
            
        public UniTask<IStoryLibraryModel<T>> Provide()
        {
            return new UniTask<IStoryLibraryModel<T>>(
                ObjectLocator.GetObject<IConfigManager>().GetCustomConfig<StoryLibraryModel<T>>(Key));
        }
    }
}