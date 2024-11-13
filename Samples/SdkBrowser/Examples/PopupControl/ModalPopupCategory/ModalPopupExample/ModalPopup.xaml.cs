using Microsoft.Maui.Controls;
using System;

namespace SDKBrowserMaui.Examples.PopupControl.ModalPopupCategory.ModalPopupExample;

public partial class ModalPopup : ContentView
{
    public ModalPopup()
    {
        InitializeComponent(); 
    }

    // >> popup-features-modal-events
    private void ClosePopup(object sender, EventArgs e) => popup.IsOpen = false;

    private void ShowPopup(object sender, EventArgs e) => popup.IsOpen = true;
    // << popup-features-modal-events
}