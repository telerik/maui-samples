using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using System;
using Telerik.Maui.Controls;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace QSF.Examples.RichTextEditorControl.CustomToolbarExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomToolbarView : RadContentView
{
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

    public CustomToolbarView()
    {
        this.InitializeComponent();

#if !WINDOWS
         this.colorPicker.SelectionChanged += this.ColorPickerSelectionChanged;
#endif
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

#if !WINDOWS
    private void ColorPickerSelectionChanged(object sender, EventArgs e)
    {
        this.richTextEditor.BackgroundColor = (Microsoft.Maui.Graphics.Color)this.colorPicker.SelectedItem;
    }
#endif
}