using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ImageEditorControl.GettingStartedCategory.GettingStartedExample;

public class ImageEditorGettingStartedCSharp : ContentView
{
	public ImageEditorGettingStartedCSharp()
	{
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(ImageEditorGettingStartedXaml).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("imageavatar.png"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        // >> imageeditor-getting-started-csharp
        var imageEditor = new RadImageEditor();
        imageEditor.Source = ImageSource.FromStream(streamFunc);

        var toolbar = new RadImageEditorToolbar();
        toolbar.ImageEditor = imageEditor;

        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
        grid.Add(imageEditor, 0, 0);
        grid.Add(toolbar, 0, 1);
        // << imageeditor-getting-started-csharp
        this.Content = grid;
    }
}