using Microsoft.Maui.Controls;
using System;

namespace SDKBrowserMaui.Examples.PopupControl.StylingCategory.StylingExample;

public partial class Styling : ContentView
{
    public Styling()
    {
        InitializeComponent();
    }

    // >> popup-styling-events
    private void ClosePopup(object sender, EventArgs e) => popup.IsOpen = false;

    private void ShowPopup(object sender, EventArgs e) => popup.IsOpen = true;
    // << popup-styling-events
}