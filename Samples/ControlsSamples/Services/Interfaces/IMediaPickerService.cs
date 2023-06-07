using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IMediaPickerService
    {
        Task<string> PickPhotoAsync();
    }
}