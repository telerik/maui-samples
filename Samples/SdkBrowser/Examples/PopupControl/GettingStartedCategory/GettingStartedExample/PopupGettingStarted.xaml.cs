using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.PopupControl.GettingStartedCategory.GettingStartedExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PopupGettingStarted : RadContentView
{
    public PopupGettingStarted()
    {
        InitializeComponent(); 

        this.closeButton.AutomationId = "closeButton";
    }

    // >> popup-gettingstarted-events
    private void ShowPopup(object sender, EventArgs e) => popup.IsOpen = true;

    private void ClosePopup(object sender, TappedEventArgs e) => popup.IsOpen = false;
    // << popup-gettingstarted-events
}