using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QSF.Services
{
    public class AssemblyResourceService : IResourceService
    {
        public IEnumerable<string> GetResourceNamesFromFolder(string folderName)
        {
            var assembly = GetCurrentAssembly();
            var resourceNames = assembly.GetManifestResourceNames().Where(p => p.Contains(folderName)).Select(p => GetFileName(folderName, p));

            return resourceNames;
        }

        public long GetResourceSize(string resourceName)
        {
            using (Stream stream = this.GetResourceStream(resourceName))
            {
                return stream.Length;
            }
        }

        public Stream GetResourceStream(string name)
        {
            var assembly = GetCurrentAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var resourceName = resourceNames.Where(p => p.Contains(name)).FirstOrDefault();

            if (string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentException($"Missing resource with given name: {name}", nameof(name));
            }

            return assembly.GetManifestResourceStream(resourceName);
        }

        private static Assembly GetCurrentAssembly()
        {
            return typeof(AssemblyResourceService).GetTypeInfo().Assembly;
        }

        private static string GetFileName(string folderName, string resourceName)
        {
            int index = resourceName.IndexOf(folderName);
            return resourceName.Substring(index + folderName.Length + 1);
        }
    }
}
