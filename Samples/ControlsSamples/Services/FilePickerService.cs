using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace QSF.Services
{
    public class FilePickerService : IFilePickerService
    {
        public async Task<string> PickFileAsync(string pickerTitle, params string[] fileExtensions)
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, fileExtensions },
                    { DevicePlatform.Android, fileExtensions },
                    { DevicePlatform.WinUI, fileExtensions },
                    { DevicePlatform.Tizen, fileExtensions },
                    { DevicePlatform.macOS, fileExtensions },
                });

            PickOptions options = new()
            {
                PickerTitle = pickerTitle,
                FileTypes = customFileType,
            };

            FileResult result = await FilePicker.PickAsync(options);

            return result?.FullPath;
        }
    }
}
