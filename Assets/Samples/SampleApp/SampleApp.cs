using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OneDay.Assets;
using OneDay.Core;
using OneDay.Data;
using OneDay.Data.Storages;
using OneDay.PlayfabServices;
using UnityEngine;

namespace OneDay.Sample
{
    public class SampleApp : AApp
    {
        protected override async UniTask OnInitialize()
        {
            Debug.Log("SampleApp");

            ObjectLocator.RegisterObject<IAssetManager>(new AssetManager());
            ObjectLocator.RegisterObject<IDataManager>(new DataManager(GetDataManagerSettings()));
            foreach (var managers in ObjectLocator.GetObjects<IManager>())
            {
                await managers.Initialize();
            }

            await ObjectLocator.GetObject<IDataManager>().SaveData("local", "test", 1);
            var result = await ObjectLocator.GetObject<IDataManager>().GetData<int>("local", "test");
            Debug.Assert(result.Data == 1);
            await ObjectLocator.GetObject<IDataManager>().DeleteData<int>("local", "test");
        }

        private static DataManagerSettings GetDataManagerSettings()
        {
            return new DataManagerSettings
            {
                Storages = new Dictionary<string, IStorageService<string>>()
                {
                    {"local",  new PlayerPrefsStorage() },
                    {"cloud",  new PlayFabUserStorage("113B9")},
                }
            };
        }
    }
}
