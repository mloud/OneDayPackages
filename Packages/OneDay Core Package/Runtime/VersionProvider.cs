using UnityEngine;

namespace OneDay.Core
{
    public static class VersionProvider
    {
        public static string GetVersionString() => Application.version;

        public static int GetCode()
        {
            var textAsset = Resources.Load<TextAsset>("BundleCode");
            
            return int.Parse(textAsset.text);
        }
    }
}