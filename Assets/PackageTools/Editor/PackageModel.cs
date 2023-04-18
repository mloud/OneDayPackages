using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace OneDay.PackageTools.Editor
{
    public class PackageModel
    {
        public string name;
        public string displayName;
        public string version;
        public string description;
        public Dictionary<string, string> dependencies;
       
        
        [JsonIgnore]
        public TextAsset source;

        public static PackageModel Load(TextAsset sourceAsset)
        {
            var packageModel = JsonConvert.DeserializeObject<PackageModel>(sourceAsset.text);
            packageModel.source = sourceAsset;
            return packageModel;
        }

        public void Save()
        {
            Debug.Assert(source != null);
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(AssetDatabase.GetAssetPath(source), json);
            EditorUtility.SetDirty(source);
        }
    }

    public class PackageModels
    {
        public List<PackageModel> Packages = new();
    }
}