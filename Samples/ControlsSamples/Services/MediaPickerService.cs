using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace QSF.Services;

public class MediaPickerService : IMediaPickerService
{
    public async Task<FileResult> PickPhotoAsync()
    {
        var pickOptions = new MediaPickerOptions();
        var pickedPhotos = await MediaPicker.Default.PickPhotosAsync(pickOptions);
        return pickedPhotos?.FirstOrDefault();
    }
}
