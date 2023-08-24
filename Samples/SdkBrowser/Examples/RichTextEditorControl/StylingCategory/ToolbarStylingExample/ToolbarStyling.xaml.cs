using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls.RichTextEditor;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.StylingCategory.ToolbarStylingExample;

public partial class ToolbarStyling : ContentView
{
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

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
	}

    protected override void OnParentSet()
    {
        base.OnParentSet();
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            if (this.Parent != null)
            {
                if (this.lastInputMode == AndroidSpecific.WindowSoftInputModeAdjust.Unspecified)
                {
                    this.lastInputMode = GetSoftInputMode();
                }

                SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust.Resize);
            }
            else
            {
                SetSoftInputMode(this.lastInputMode);
            }
        }
    }

    private static AndroidSpecific.WindowSoftInputModeAdjust GetSoftInputMode()
    {
        return AndroidSpecific.Application.GetWindowSoftInputModeAdjust(Application.Current);
    }

    private static void SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust inputMode)
    {
        AndroidSpecific.Application.SetWindowSoftInputModeAdjust(Application.Current, inputMode);
    }
}