using System.Collections.Generic;

namespace OneDay.Permissions
{
    public class PermissionManagerSettings
    { 
        public IReadOnlyList<IPermissionService> PermissionServices { get; }
        
        public PermissionManagerSettings(params IPermissionService[] permissionServices) =>
            PermissionServices = permissionServices;
    }
}