using System.Collections.Generic;

namespace OneDay.Story
{
    public interface IStoryLibraryModel<T> where T: IStoryModel
    {
        string Id { get; set; }
        List<T> Story { get; set; }
    }
}