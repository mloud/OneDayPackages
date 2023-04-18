using System.Collections.Generic;

namespace OneDay.Story
{
    public class StoryLibraryModel<T> : IStoryLibraryModel<T> where T : IStoryModel
    {
        public string Id { get; set; }
        public List<T> Story { get; set; }
    }
}