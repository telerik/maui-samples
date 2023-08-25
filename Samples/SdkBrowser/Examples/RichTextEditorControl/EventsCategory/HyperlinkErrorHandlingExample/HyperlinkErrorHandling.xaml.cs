using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SDKBrowserMaui.Behaviors;
using Telerik.Maui.Controls.RichTextEditor;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.EventsCategory.HyperlinkErrorHandlingExample;

public partial class HyperlinkErrorHandling : ContentView
{
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

    public HyperlinkErrorHandling()
	{
		InitializeComponent();

        var htmlSource = @"<h5>Check the following links:</h5>
                        <p><a href='https://www.telerik.com/maui-ui' target='_blank'>Telerik.com - valid Url</a></p>
                        <p><a href='www.google.com'>Google - not absolute Url</a></p>
                        <p><a href='http:/www.wvtesting.com'>Wrong format of Url</a></p>";
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

    // >> richtexteditor-hyperlinkerrorhandling-code
    private void RichTextEditor_OpenHyperlinkError(object sender, OpenHyperlinkErrorEventArgs e)
    {
        e.Handled = true;
        Application.Current.MainPage.DisplayAlert(string.Format("Error opening {0}", e.Url), e.Error.Message, "Ok");
    }
    // << richtexteditor-hyperlinkerrorhandling-code
}