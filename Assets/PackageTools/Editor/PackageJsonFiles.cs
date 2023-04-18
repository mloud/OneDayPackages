using System.Collections.Generic;
using UnityEngine;

namespace OneDay.PackageTools
{
    [CreateAssetMenu(fileName = "PackageRegistry", menuName = "PackageTools/Create/PackageRegistry", order = 1)]
    public class PackageJsonFiles : ScriptableObject
    {
        public List<TextAsset> packageFiles;
    }
}