using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.FeaturesCategory.CustomImagePickerExample
{
    // >> richtexteditor-customimage-viewmodel
    public class ViewModel
    {
        public ViewModel()
        {
            this.RTSource = RichTextSource.FromStream(() =>
            {
                Assembly assembly = typeof(ViewModel).GetTypeInfo().Assembly;
                string fileName = assembly.GetManifestResourceNames()
                    .FirstOrDefault(n => n.Contains("pick-image-demo.html"));

                return assembly.GetManifestResourceStream(fileName);
            });
            this.InitializeImages();
        }
        public List<RichTextImageSource> EmojiSource { get; private set; }

        public RichTextSource RTSource {get; private set; }

        private void InitializeImages()
        {
            var currentAssembly = typeof(ViewModel).GetTypeInfo().Assembly;
            var resourceNames = currentAssembly.GetManifestResourceNames();
            var imageSources = new List<RichTextImageSource>();

            foreach (var resourceName in resourceNames)
            {
                if (resourceName.Contains("emoji"))
                {
                    var imageSource = RichTextImageSource.FromStream(() =>
                        currentAssembly.GetManifestResourceStream(resourceName), RichTextImageType.Png);

                    imageSources.Add(imageSource);
                }
            }

            this.EmojiSource = imageSources;
        }
    }
    // << richtexteditor-customimage-viewmodel
}
