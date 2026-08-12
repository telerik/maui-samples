using Microsoft.Maui.Controls;
using SDKBrowserMaui.Behaviors;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.StylingCategory.ToolbarStylingExample;

public partial class ToolbarStyling : ContentView
{
    public ToolbarStyling()
    {
        InitializeComponent();

        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(ToolbarStyling).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("richtexteditor-htmlsource.html"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.richTextEditor.Source = RichTextSource.FromStream(streamFunc);
        this.grid.Behaviors.Add(new AndroidKeyboardPaddingBehavior());
    }
}