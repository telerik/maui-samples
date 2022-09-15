using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IFilePickerService
    {
        Task<string> PickFileAsync(string pickerTitle, params string[] fileExtensions);
    }
}
