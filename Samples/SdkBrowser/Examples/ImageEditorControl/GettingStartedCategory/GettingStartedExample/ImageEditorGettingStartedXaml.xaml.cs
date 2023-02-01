using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ImageEditorControl.GettingStartedCategory.GettingStartedExample;

public partial class ImageEditorGettingStartedXaml : ContentView
{
	public ImageEditorGettingStartedXaml()
	{
		InitializeComponent();

        // >> load-image-from-stream
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(ImageEditorGettingStartedXaml).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("imageavatar.png"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.imageEditor.Source = ImageSource.FromStream(streamFunc);
        // << load-image-from-stream
    }
}