using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.FeaturesCategory.CustomImagePickerExample;

public partial class CustomImagePicker : ContentView
{
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

    public CustomImagePicker()
	{
		InitializeComponent();
        this.BindingContext = new ViewModel();
    }

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
}