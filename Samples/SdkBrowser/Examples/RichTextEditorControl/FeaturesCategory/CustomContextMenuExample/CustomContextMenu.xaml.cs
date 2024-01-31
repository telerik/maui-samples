using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SDKBrowserMaui.Behaviors;
using Telerik.Maui.Controls.RichTextEditor;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.FeaturesCategory.CustomContextMenuExample;

public partial class CustomContextMenu : ContentView
{
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

    public CustomContextMenu()
    {
        InitializeComponent();

        // >> richtexteditor-contextmenu-setvm
        this.BindingContext = new ViewModel();
        // << richtexteditor-contextmenu-setvm

        var htmlSource = @"<h4>One of the Most Beautiful Islands on Earth - Tenerife</h4>
                        <p><strong>Tenerife</strong> is the largest and most populated island of the eight <a href='https://en.wikipedia.org/wiki/Canary_Islands' target='_blank'>Canary Islands</a>.</p>
                        <p style='color:#808080'>It is also the most populated island of <strong>Spain</strong>, with a land area of <i>2,034.38 square kilometers</i> and <i>904,713</i> inhabitants, 43% of the total population of the <strong>Canary Islands</strong>.</p>";
        this.richTextEditor.Source = RichTextSource.FromString(htmlSource);
        this.richTextEditor.Behaviors.Add(new PickImageBehavior());
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