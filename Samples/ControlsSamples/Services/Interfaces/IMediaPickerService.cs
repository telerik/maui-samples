using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace QSF.Services
{
    public interface IMediaPickerService
    {
        Task<FileResult> PickPhotoAsync();
    }
}