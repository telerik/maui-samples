namespace TelerikCRM.Maui.Helpers;

internal static class PermissionsHelper
{
    internal static async Task<bool> RequestStorageRead()
    {
        var currentStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

        if (currentStatus == PermissionStatus.Granted)
        {
            return true;
        }

        var status = await Permissions.RequestAsync<Permissions.StorageRead>();
        return status == PermissionStatus.Granted;
    }
}