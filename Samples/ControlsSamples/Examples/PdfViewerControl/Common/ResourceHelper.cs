using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace QSF.Examples.PdfViewerControl.Common;

public static class ResourceHelper
{
    internal static Func<CancellationToken, Task<Stream>> GetStreamFromEmbeddedResource(string fileName)
    {
        return ct => Task.Run(() =>
        {
            Assembly assembly = typeof(ResourceHelper).Assembly;
            string file = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(fileName));
            Stream stream = assembly.GetManifestResourceStream(file);
            return stream;
        });
    }
}