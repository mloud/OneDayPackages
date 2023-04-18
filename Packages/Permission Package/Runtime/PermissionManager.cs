using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Permissions
{
    public class PermissionManager : ABaseManager, IPermissionManager
    {
        private PermissionManagerSettings Settings { get; }
        public PermissionManager(PermissionManagerSettings settings) =>
            Settings = settings;

        public async UniTask<PermissionRequestResult> RequestPermission(string permissionName)
        {
            var servicesWithPermission = FindServicesForPermission(permissionName);
            PermissionRequestResult? permissionRequestResult = null;
            bool existsAnyService = false;
            foreach (var permissionService in servicesWithPermission)
            {
                permissionRequestResult = await permissionService.RequestPermission(permissionName);
                existsAnyService = true;
            }

            if (!existsAnyService)
            {
                Debug.LogError($"No such service for permission {permissionName} exists");
            }
            return permissionRequestResult ?? PermissionRequestResult.Granted;
        }
        
        public bool HasPermission(string permissionName)
        {
            var servicesWithPermission = FindServicesForPermission(permissionName);

            foreach (var permissionService in servicesWithPermission)
            {
                if (permissionService.HasPermission(permissionName))
                    return true;
            }

            return false;
        }

        IEnumerable<IPermissionService> FindServicesForPermission(string permissionName)
        {
            var servicesWithPermission 
                = Settings.PermissionServices.Where(service => service.Permissions.Contains(permissionName));

            return servicesWithPermission;
        }
    }
}