using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
// >> android-keyboard-namespace
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
// << android-keyboard-namespace
using Telerik.Maui;
using Telerik.Maui.Controls.RichTextEditor;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SDKBrowserMaui.Behaviors;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.FeaturesCategory.ReadOnlyStateExample;

public partial class ReadOnlyState : ContentView
{
    // >> android-keyborad
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;
    // << android-keyborad
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
    }

    // >> android-specific-keyborad
    protected override void OnParentSet()
    {
        base.OnParentSet();
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
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
    // << android-specific-keyborad
}