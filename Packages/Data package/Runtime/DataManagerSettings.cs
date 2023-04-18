using System.Collections.Generic;
using OneDay.Core;

namespace OneDay.Data
{
    public class DataManagerSettings
    {
        public Dictionary<string, IStorageService<string>> Storages { get; set; } 
    }
}