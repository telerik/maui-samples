using Microsoft.Maui.Controls;
using QSF.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QSF.Examples.ImageEditorControl
{
    internal static class StorageHelper
    {
        public static Task<IEnumerable<string>> EnumerateFilesAsync(params string[] filePrefixes)
        {
            return Task.Run(() => EnumerateFiles(filePrefixes));
        }

        public static Task<IEnumerable<string>> ExtractResourcesAsync(params string[] filePrefixes)
        {
            return Task.Run(() => ExtractResources(filePrefixes));
        }

        private static IEnumerable<string> EnumerateFiles(params string[] filePrefixes)
        {
            var localPath = GetLocalFolder(filePrefixes);

            if (Directory.Exists(localPath))
            {
                return Directory.EnumerateFiles(localPath);
            }

            return Enumerable.Empty<string>();
        }

        private static IEnumerable<string> ExtractResources(params string[] filePrefixes)
        {
            var localPath = GetLocalFolder(filePrefixes);

            if (Directory.Exists(localPath))
            {
                return Directory.EnumerateFiles(localPath);
            }

            Directory.CreateDirectory(localPath);

            var resourceService = DependencyService.Get<IResourceService>();
            var resourceNames = GetResourceNames(filePrefixes);

            foreach (var resourceName in resourceNames)
            {
                var imagePath = Path.Combine(localPath, resourceName);

                using (var sourceStream = resourceService.GetResourceStream(resourceName))
                using (var targetStream = File.Create(imagePath))
                {
                    sourceStream.CopyTo(targetStream);
                }
            }

            return Directory.EnumerateFiles(localPath);
        }

        private static string GetLocalFolder(params string[] filePrefixes)
        {
            //var fileSystemService = DependencyService.Get<IFileSystemService>();
            //var localPath = fileSystemService.GetLocalFolder();

#if WINDOWS
            var localPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#else
            var localPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
#endif
            foreach (var filePrefix in filePrefixes)
            {
                localPath = Path.Combine(localPath, filePrefix);
            }

            return localPath;
        }

        private static IEnumerable<string> GetResourceNames(params string[] filePrefixes)
        {
            var resourceService = DependencyService.Get<IResourceService>();
            var resourcePath = string.Join(".", filePrefixes);

            return resourceService.GetResourceNamesFromFolder(resourcePath);
        }
    }
}
