using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.PopupControl.ContentTemplateCategory.ContentTemplateExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ContentTemplate : RadContentView
{
    public ContentTemplate()
    {
        InitializeComponent();

        this.popup.PropertyChanged += PopupPropertyChanged;
    }

    private void PopupPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsOpen" && (sender as RadPopup).IsOpen == false)
        {
            this.checkbox.IsChecked = false;
        }
    }

    // >> popup-features-contenttemplate-events
    private void CheckBoxIsCheckedChanged(object sender, IsCheckedChangedEventArgs e)
    {
        if (e.NewValue == true)
        {
            this.popup.IsOpen = true;
        }
    }
    // << popup-features-contenttemplate-events
}