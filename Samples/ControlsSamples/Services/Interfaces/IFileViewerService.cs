using System.IO;
using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IFileViewerService
    {
        Task<bool> View(Stream stream, string filename);
    }
}
