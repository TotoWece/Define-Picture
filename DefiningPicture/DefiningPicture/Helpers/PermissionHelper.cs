using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace DefiningPicture
{
    public static class PermissionHelper
    {
        static IPermissions PermissionService => CrossPermissions.Current;

        public static async Task<bool> Has(Permission permission)
        {
            var permissionStatus = await PermissionService.CheckPermissionStatusAsync(permission);
            return permissionStatus == PermissionStatus.Granted;
        }

        public static async Task Request(params Permission[] permission)
        {
            await PermissionService.RequestPermissionsAsync(permission);
        }
    }
}
