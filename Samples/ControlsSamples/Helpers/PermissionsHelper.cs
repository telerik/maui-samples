using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;
namespace QSF.Helpers
{
    internal static class PermissionsHelper
    {
        internal static async Task<bool> RequestStorageRead()
        {
            var currentStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (currentStatus != PermissionStatus.Granted)
            {
                var status = await Permissions.RequestAsync<Permissions.StorageRead>();
                return status == PermissionStatus.Granted;
            }
            else
            {
                return true;
            }
        }

        internal static async Task<bool> RequestStorageWrite()
        {
            var currentStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (currentStatus != PermissionStatus.Granted)
            {
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                return status == PermissionStatus.Granted;
            }
            else
            {
                return true;
            }
        }

        internal static async Task<bool> RequestCameraAccess()
        {
            var currentStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (currentStatus != PermissionStatus.Granted)
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                return status == PermissionStatus.Granted;
            }
            else
            {
                return true;
            }
        }

        internal static async Task<bool> RequestPhotosAccess()
        {
            var currentStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();
            if (currentStatus != PermissionStatus.Granted)
            {
                var status = await Permissions.RequestAsync<Permissions.Photos>();
                return status == PermissionStatus.Granted;
            }
            else
            {
                return true;
            }
        }
    }
}
