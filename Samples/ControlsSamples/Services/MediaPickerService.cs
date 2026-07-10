using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace QSF.Services;

public class MediaPickerService : IMediaPickerService
{
    public async Task<FileResult> PickPhotoAsync()
    {
#if NET10_0_OR_GREATER
        var pickOptions = new MediaPickerOptions();
        var pickedPhotos = await MediaPicker.Default.PickPhotosAsync(pickOptions);
        return pickedPhotos?.FirstOrDefault();
#else
        return await MediaPicker.Default.PickPhotoAsync();
#endif
    }
}
