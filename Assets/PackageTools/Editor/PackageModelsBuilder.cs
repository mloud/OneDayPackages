using OneDay.PackageTools;
using OneDay.PackageTools.Editor;

namespace PackageTools.Editor
{
    public static class PackageModelsBuilder
    {
        public static PackageModels Create(PackageJsonFiles packageJsonFiles)
        {
            var packageModels = new PackageModels();
            packageJsonFiles.packageFiles.ForEach(
                sourceAsset => packageModels.Packages.Add(PackageModel.Load(sourceAsset)));
      
            return packageModels;
        }
    }
}