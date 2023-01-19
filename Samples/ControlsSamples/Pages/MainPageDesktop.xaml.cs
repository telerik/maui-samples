using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using System;

namespace QSF.Pages;

public partial class MainPageDesktop : ContentPage
{
    private bool isNavigationMenuOpen = true;
    private double navigationMenuDesiredWidth;

    public MainPageDesktop()
    {
        this.InitializeComponent();

        this.BindingContext = new HomeViewModel();

        TapGestureRecognizer overlayTap = new TapGestureRecognizer();
        overlayTap.Tapped += this.overlay_Tapped;
        this.overlay.GestureRecognizers.Add(overlayTap);

        TapGestureRecognizer burgerTap = new TapGestureRecognizer();
        burgerTap.Tapped += this.burgerView_Tapped;
        this.burgerView.GestureRecognizers.Add(burgerTap);

        this.Loaded += this.OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        this.Loaded -= this.OnLoaded;

        this.Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(2), () =>
        {
            this.IsNavigationMenuOpen = false;
        });
    }

    private bool IsNavigationMenuOpen
    {
        get
        {
            return this.isNavigationMenuOpen;
        }
        set
        {
            if (this.isNavigationMenuOpen != value)
            {
                this.isNavigationMenuOpen = value;
                this.OnIsNavigationMenuOpenChanged();
            }
        }
    }

    private void OnIsNavigationMenuOpenChanged()
    {
        if (this.navigationMenuDesiredWidth <= 0)
        {
            this.navigationMenuDesiredWidth = this.navigationMenu.Bounds.Width;
        }

        double oldWidth = this.navigationMenu.Bounds.Width;
        double newWidth = this.isNavigationMenuOpen ? this.navigationMenuDesiredWidth : 40;

        if (oldWidth != newWidth)
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                this.navigationMenu.Animate("width", new Animation(value =>
                {
                    this.navigationMenu.WidthRequest = value;
                }, oldWidth, newWidth, Easing.CubicInOut));
            }
            else
            {
                this.navigationMenu.WidthRequest = newWidth;
            }
        }

        this.overlay.IsVisible = this.isNavigationMenuOpen;
    }

    private void overlay_Tapped(object sender, EventArgs e)
    {
        this.IsNavigationMenuOpen = false;
    }

    private void burgerView_Tapped(object sender, EventArgs e)
    {
        this.IsNavigationMenuOpen ^= true;
    }

    private void controlsView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.controlsView.SelectedItem))
        {
            this.IsNavigationMenuOpen = false;
        }
    }
}
