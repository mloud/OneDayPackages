using Cysharp.Threading.Tasks;
using OneDay.Core;

namespace OneDay.Permissions
{
    public interface IPermissionManager : IManager
    {
        UniTask<PermissionRequestResult> RequestPermission(string permissionName);
        bool HasPermission(string permissionName);
    }
}