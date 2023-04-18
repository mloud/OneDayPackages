using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace OneDay.Permissions
{
    public interface IPermissionService
    {
        IEnumerable<string> Permissions { get; }
        UniTask<PermissionRequestResult> RequestPermission(string permissionName);
        bool HasPermission(string permissionName);
    }
}