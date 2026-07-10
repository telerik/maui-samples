#if WINDOWS
using Windows.Storage;

namespace TelerikCRM.Maui.Services;

public partial class FileViewerService
{
    public async Task<bool> View(Stream stream, string filename)
    {
        try
        {
            var temporaryFolder = ApplicationData.Current.TemporaryFolder;
            var file = await temporaryFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            stream.Seek(0, SeekOrigin.Begin);

            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await using (var managedFileStream = fileStream.AsStreamForWrite())
                {
                    await stream.CopyToAsync(managedFileStream);
                }
            }

            // Set the option to show the picker
            var options = new Windows.System.LauncherOptions
            {
                DisplayApplicationPicker = true
            };

            // Launch the retrieved file
            return await Windows.System.Launcher.LaunchFileAsync(file, options);
        }
        catch
        {
            return false;
        }
    }
}
#endif
