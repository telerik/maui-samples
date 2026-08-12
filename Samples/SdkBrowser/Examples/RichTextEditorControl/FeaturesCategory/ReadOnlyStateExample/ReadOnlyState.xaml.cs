using Microsoft.Maui.Controls;
using SDKBrowserMaui.Behaviors;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.FeaturesCategory.ReadOnlyStateExample;

public partial class ReadOnlyState : ContentView
{
    public ReadOnlyState()
    {
        InitializeComponent();

        // >> richtexteditor-readonly-state-code-behind
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(ReadOnlyState).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("PickYourHoliday.html"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.richTextEditor.Source = RichTextSource.FromStream(streamFunc);
        // << richtexteditor-readonly-state-code-behind

        this.richTextEditor.Behaviors.Add(new PickImageBehavior());
        this.grid.Behaviors.Add(new AndroidKeyboardPaddingBehavior());
    }
}