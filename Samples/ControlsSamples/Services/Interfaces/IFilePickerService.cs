using System.Collections.Generic;
using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IFilePickerService
    {
        Task<string> PickFileAsync(string pickerTitle, params string[] fileTypes);
    }
}
