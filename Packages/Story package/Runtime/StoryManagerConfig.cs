using System.Collections.Generic;
using OneDay.Core;

namespace OneDay.Story
{
    public class StoryManagerConfig<T> where T: IStoryModel
    {
        public Dictionary<string, IProvider<IStoryLibraryModel<T>>> LibraryProviders { get; set; }
        public Dictionary<string, ISaver<T>> LibrarySavers { get; set; }
    }
}