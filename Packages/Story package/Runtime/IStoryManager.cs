using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Story
{
    public interface IStoryManager<T> : IManager where T: IStoryModel
    {
        delegate void LibraryUpdateDelegate(string libraryId, IStoryLibraryModel<T> model);
        LibraryUpdateDelegate LibraryUpdated { get; set; }
     
        UniTask<IStoryLibraryModel<T>> GetLibrary(string libraryId);
        UniTask<Result> SaveToLibrary(string libraryId, T model);
        UniTask UpdateLibrary(string libraryId);
    }
}