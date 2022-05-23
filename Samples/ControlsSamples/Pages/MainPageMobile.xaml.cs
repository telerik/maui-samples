using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.ComponentModel;

namespace QSF.Pages;

public partial class MainPageMobile : ContentPage
{
    public MainPageMobile()
    {
        this.InitializeComponent();

        this.BindingContext = new HomeViewModel();
    }

    private void Search_Clicked(object sender, EventArgs e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm.NavigateToSearch();
    }

    private void controlsListView_ItemTapped(object sender, Telerik.XamarinForms.DataControls.ListView.ItemTapEventArgs e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm?.SelectControlCommand?.Execute(e.Item);
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
#if __ANDROID__
        //TODO: Remove this when NavigationPage starts using the Maui Handler instead of the old compat Renderer.
        if (e.PropertyName == nameof(this.Width) || e.PropertyName == nameof(this.Height))
        {
            this.Frame = this.Content.Frame;
        }
#endif
    }
}
