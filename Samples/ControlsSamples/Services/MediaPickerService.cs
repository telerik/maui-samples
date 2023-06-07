using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Media;

namespace QSF.Services;

public class MediaPickerService : IMediaPickerService
{
    public async Task<string> PickPhotoAsync()
    {
        var fileResult = await MediaPicker.Default.PickPhotoAsync();

        return fileResult?.FullPath;
    }
}
