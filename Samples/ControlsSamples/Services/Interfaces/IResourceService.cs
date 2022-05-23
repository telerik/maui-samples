using System.Collections.Generic;
using System.IO;

namespace QSF.Services
{
    public interface IResourceService
    {
        Stream GetResourceStream(string name);

        long GetResourceSize(string resourceName);

        IEnumerable<string> GetResourceNamesFromFolder(string v);
    }
}
