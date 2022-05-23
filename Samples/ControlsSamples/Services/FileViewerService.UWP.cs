#if WINDOWS
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Windows.Storage;

namespace QSF.Services
{
    public partial class FileViewerService
    {
        public async Task<bool> View(Stream stream, string filename)
        {
            try
            {

                var temporatyFolder = ApplicationData.Current.TemporaryFolder;

                var file = await temporatyFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                stream.Seek(0, SeekOrigin.Begin);

                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (var managedFileStream = fileStream.AsStreamForWrite())
                    {
                        stream.CopyTo(managedFileStream);
                    }
                }

                // Set the option to show the picker
                var options = new Windows.System.LauncherOptions();
                options.DisplayApplicationPicker = true;

                // Launch the retrieved file
                return await Windows.System.Launcher.LaunchFileAsync(file, options);
            }
            catch
            {
                return false;
            }
        }
    }
}
#endif
