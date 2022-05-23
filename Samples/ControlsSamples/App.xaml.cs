﻿using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using QSF.Pages;
using QSF.Services;
using Telerik.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

namespace QSF;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
        this.InitializeDependencies();

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.MainPage = new NavigationPage(new MainPageDesktop());
        }
        else
        { 
            this.MainPage = new NavigationPage(new MainPageMobile());
        }
    }

    private void InitializeDependencies()
    {
        DependencyService.Register<IConfigurationService, ConfigurationService>();
        DependencyService.Register<IResourceService, AssemblyResourceService>();
        DependencyService.Register<IFileViewerService, FileViewerService>();
        DependencyService.Register<INavigationService, NavigationService>();
        DependencyService.Register<IExampleService, ExampleService>();
        DependencyService.Register<IControlsService, ControlsService>();
        DependencyService.Register<ISearchService, SearchService>();
        DependencyService.Register<IToastMessageService, ToastMessageService>();
        DependencyService.Register<ISerializationService, SerializationService>();
    }

    public static void DisplayAlert(string text)
    {
        var popup = new RadPopup();
        popup.IsModal = true;
        popup.Placement = PlacementMode.Center;
        popup.OutsideBackgroundColor = Color.FromArgb("#6F000000");

        var border = new RadBorder();
        border.CornerRadius = new Thickness(8);
        border.BackgroundColor = Color.FromArgb("#F1F1F1");

        var grid = new Grid();
        grid.Padding = new Thickness(10);
        grid.WidthRequest = 200;
        grid.HeightRequest = 150;
        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
        grid.RowDefinitions.Add(new RowDefinition() { Height = 30 });

        var label = new Microsoft.Maui.Controls.Label();
        label.Text = text;
        label.VerticalOptions = LayoutOptions.Start;
        label.HorizontalOptions = LayoutOptions.Start;
        label.TextColor = Colors.Black;
        label.LineBreakMode = LineBreakMode.WordWrap;
        Grid.SetRow(label, 0);
        grid.Children.Add(label);

        var okButton = new RadButton();
        okButton.Padding = new Thickness(2);
        if (DeviceInfo.Platform != DevicePlatform.WinUI)
        {
            okButton.WidthRequest = 100;
        }
        okButton.HorizontalOptions = LayoutOptions.Center;
        okButton.VerticalOptions = LayoutOptions.End;
        okButton.BackgroundColor = Color.FromArgb("#674bb2");
        okButton.TextColor = Colors.White;
        okButton.Text = "OK";
        okButton.Clicked += (sender, args) =>
        {
            popup.IsOpen = false;
        };

        Grid.SetRow(okButton, 1);
        grid.Children.Add(okButton);
        border.Content = grid;
        popup.Content = border;
        popup.IsOpen = true;
    }
}
