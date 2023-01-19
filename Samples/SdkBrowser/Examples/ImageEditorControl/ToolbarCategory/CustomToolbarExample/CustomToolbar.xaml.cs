using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ImageEditorControl.ToolbarCategory.CustomToolbarExample;

public partial class CustomToolbar : ContentView
{
	public CustomToolbar()
	{
		InitializeComponent();

        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(CustomToolbar).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("imageavatar.png"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.imageEditor.Source = ImageSource.FromStream(streamFunc);
    }
}