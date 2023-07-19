using Microsoft.Maui.Controls;
using SDKBrowserMaui.Behaviors;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.ToolbarCategory.ToolbarExample;

public partial class Toolbar : ContentView
{
	public Toolbar()
	{
		InitializeComponent();

        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(Toolbar).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("PickYourHoliday.html"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.richTextEditor.Source = RichTextSource.FromStream(streamFunc);
        this.richTextEditor.Behaviors.Add(new PickImageBehavior());
    }
}