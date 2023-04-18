using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Story
{
    public abstract class StoryManager<T> : ABaseManager, IStoryManager<T> where T: IStoryModel
    {
        public IStoryManager<T>.LibraryUpdateDelegate LibraryUpdated { get; set; }
        private StoryManagerConfig<T> Config { get; }
        
        // libraries
        private Dictionary<string, IStoryLibraryModel<T>> Libraries { get; set; }

        public StoryManager(StoryManagerConfig<T> config) => 
            Config = config;
        
        protected override async UniTask OnInitialize()
        {
            Libraries = new Dictionary<string, IStoryLibraryModel<T>>();
      
            var provideResult =
                await UniTask.WhenAll(Config.LibraryProviders.Select(x => x.Value.Provide()));
        
            foreach (var result in provideResult)
            {
                Libraries.Add(result.Id, result);
            }
        }

        public UniTask<IStoryLibraryModel<T>> GetLibrary(string libraryId) =>
            new(Libraries[libraryId]);
        
        public async UniTask<Result> SaveToLibrary(string libraryId, T storyModel)
        {
            if (Config.LibrarySavers.TryGetValue(libraryId, out var saver))
            {
                var result = await saver.Save(storyModel);
                if (!result.HasError())
                {
                    UpdateLibrary(libraryId);
                }
                return result;
            }
            return Result.WithError($"No saver for library: {libraryId} found");
        }
    
        public async UniTask UpdateLibrary(string libraryId)
        {
            var library = await Config.LibraryProviders[libraryId].Provide();
            if (library != null)
            {
                if (Libraries.ContainsKey(libraryId))
                {
                    Libraries[libraryId] = library;
                }
                else
                {
                    Libraries.Add(libraryId, library);
                }

                LibraryUpdated?.Invoke(libraryId, Libraries[libraryId]);
            }
            else
            {
                Debug.LogError($"Could not UpdateFairyTalesLibrary {libraryId}");
            }
        }
    }
}