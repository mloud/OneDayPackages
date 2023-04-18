using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using OneDay.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace OneDay.Samples.FallingBlocks
{
    public class FallingBlockConfigStorage : MonoBehaviour, IReadOnlyKeyStorage<string>
    {
        [SerializeField] private LevelsDefinition levelsDefinition;

        public static FallingBlockConfigStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    var go = Resources.Load("FallingBlockConfigStorage");
                    Instantiate(go);
                    instance = go.GetComponent<FallingBlockConfigStorage>();
                }

                return instance;
            }
        }

        private static FallingBlockConfigStorage instance;
        
        public UniTask<string> Load(string id)
        {
            if (id == "level_definitions")
            {
                return new UniTask<string>(JsonConvert.SerializeObject(levelsDefinition));
            }

            return new UniTask<string>(null);
        }

        public async UniTask<Dictionary<string, string>> Load(params string[] keys)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in keys)
            {
                var value = await Load(key);
                result.Add(key, value);
            }

            return result;
        }

        public UniTask<bool> Has(string key) => 
            new(key == "level_definitions");
    }
}