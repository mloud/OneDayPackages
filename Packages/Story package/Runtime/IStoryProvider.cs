using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Story
{
    public interface IStoryProvider<T> : IProvider<IStoryLibraryModel<T>> where T: IStoryModel
    {
        UniTask<StoryLibraryModel<T>> Provide();
    }
}