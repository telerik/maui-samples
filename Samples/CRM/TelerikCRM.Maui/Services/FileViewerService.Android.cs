#if ANDROID
using System.IO;
using System.Threading.Tasks;
using TelerikCRM.Maui.Helpers;

namespace TelerikCRM.Maui.Services;

public partial class FileViewerService
{
    public async Task<bool> View(Stream stream, string filename)
    {
        try
        {
            if ((int)Android.OS.Build.VERSION.SdkInt >= 23 && (int)Android.OS.Build.VERSION.SdkInt < 33)
            {
                bool result = await PermissionsHelper.RequestStorageRead();
                if (!result)
                {
                    return false;
                }
            }

            var context = Android.App.Application.Context;
            Java.IO.File externalFilesDir = context.GetExternalFilesDir(null);
            Java.IO.File myDir;

            if (externalFilesDir != null)
            {
                myDir = new Java.IO.File(externalFilesDir, "/Telerik");
            }
            else
            {
                myDir = new Java.IO.File(context.FilesDir, "/TelerikFiles");
            }

            if (!myDir.Exists() && myDir.Mkdir())
            {
                 return false;
            }

            Java.IO.File file = new Java.IO.File(myDir, filename);

            if (file.Exists())
            {
                file.Delete();
            }

            if (!file.CreateNewFile())
            {
                return false;
            }

            using (Java.IO.FileOutputStream outs = new Java.IO.FileOutputStream(file))
            {
                stream.Seek(0, SeekOrigin.Begin);
                byte[] buffer = new byte[stream.Length];
                await stream.ReadExactlyAsync(buffer, 0, buffer.Length);
                await outs.WriteAsync(buffer);
            }

            if (file.Exists())
            {
                Android.Net.Uri path = AndroidX.Core.Content.FileProvider.GetUriForFile(context, context.PackageName + ".fileprovider", file);
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file)?.ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton?.GetMimeTypeFromExtension(extension);
                Android.Content.Intent intent = new Android.Content.Intent(Android.Content.Intent.ActionView);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(Android.Content.ActivityFlags.GrantReadUriPermission);

                var choserActivity = Android.Content.Intent.CreateChooser(intent, "Choose App");

                if (choserActivity != null)
                {
                    choserActivity.SetFlags(Android.Content.ActivityFlags.NewTask);
                    choserActivity.AddFlags(Android.Content.ActivityFlags.GrantReadUriPermission);

                    context.StartActivity(choserActivity);
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
#endif
