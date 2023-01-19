using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace QSF.Services
{
    public class FilePickerService : IFilePickerService
    {
        public async Task<string> PickFileAsync(string pickerTitle, params string[] fileTypes)
        {
            var pickOptions = new PickOptions
            {
                PickerTitle = pickerTitle
            };

            if (fileTypes.Length > 0)
            {
                var currentPlatform = DeviceInfo.Current.Platform;
                var platformFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { currentPlatform, fileTypes }
                };
                var pickFileTypes = new FilePickerFileType(platformFileTypes);

                pickOptions.FileTypes = pickFileTypes;
            }

            var fileResult = await FilePicker.PickAsync(pickOptions);

            return fileResult?.FullPath;
        }
    }
}
