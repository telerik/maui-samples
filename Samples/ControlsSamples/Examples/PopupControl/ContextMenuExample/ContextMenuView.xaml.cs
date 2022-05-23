using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace QSF.Examples.PopupControl.ContextMenuExample;

public partial class ContextMenuView : ContentView
{
    private RadPopup popup;
    private PopupViewModel popupViewModel;

	public ContextMenuView()
	{
		InitializeComponent();
        InitializePopup();
	}

    private void InitializePopup()
    {
        this.popupViewModel = new PopupViewModel();
        this.popupViewModel.ClosePopup += (sender, args) => this.popup.IsOpen = false;
        this.popup = new RadPopup();
        this.popup.Content = (View)this.Resources["popupContent"];
        this.popup.Content.BindingContext = this.popupViewModel;
        this.popup.OutsideBackgroundColor = Color.FromArgb("#3C000000");
    }

    private void RadButton_Clicked(object sender, System.EventArgs e)
    {
        this.popup.IsOpen = true;
        if(DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            this.popup.Content.WidthRequest = Application.Current.MainPage.Width;
        }
        if(DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.popup.PlacementTarget = this.ListView;
            this.popup.Placement = PlacementMode.Center;
        }
    }

    private void RadButton_ClosePopup(object sender, System.EventArgs e)
    {
        this.popup.IsOpen = false;
    }
}