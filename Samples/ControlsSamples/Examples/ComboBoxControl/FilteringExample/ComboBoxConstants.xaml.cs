using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using System.ComponentModel;

namespace QSF.Examples.ComboBoxControl.FilteringExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ComboBoxConstants : ResourceDictionary
{
    public ComboBoxConstants()
    {
        this.InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            this["HighlightTextColor"] = Color.FromArgb("2B0B98");
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            this["HighlightTextColor"] = Color.FromArgb("005FB8");
        }
        else
        {
            this["HighlightTextColor"] = Color.FromArgb("007AFF");
        }
    }
}