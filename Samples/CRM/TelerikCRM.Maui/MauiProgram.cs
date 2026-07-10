using Microsoft.Maui.LifecycleEvents;
using Telerik.Maui.Controls.Compatibility;
using TelerikCRM.Maui.Services;
using TelerikCRM.Maui.ViewModels;

#if ANDROID || IOS
using PlatformViews = TelerikCRM.Maui.Views.Mobile;
#elif WINDOWS10_0_17763_0_OR_GREATER
using PlatformViews = TelerikCRM.Maui.Views.Desktop;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml.Media;
using WinUIEx;
#elif MACCATALYST
using PlatformViews = TelerikCRM.Maui.Views.Desktop;
using CoreGraphics;
using UIKit;

#endif

namespace TelerikCRM.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseTelerik()
            .RegisterDataServices()
            .RegisterViewModels()
            .RegisterViews()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("telerikfontexamples.ttf", "TelerikFontExamples");
            })
            .RegisterLifecycleEvents();

        return builder.Build();
    }

    public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<DatasyncClientService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddSingleton<IFileViewerService, FileViewerService>();
        builder.Services.AddTransient<RemoteCustomerService>();
        builder.Services.AddTransient<RemoteEmployeeService>();
        builder.Services.AddTransient<RemoteProductService>();
        builder.Services.AddTransient<RemoteOrderService>();

        return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<WelcomeViewModel>();

#if ANDROID || IOS
        builder.Services.AddSingleton<ProductDetailViewModel>();
        builder.Services.AddTransient<OrderDetailViewModel>();
        builder.Services.AddTransient<MoreViewModel>();
#endif
        builder.Services.AddSingleton<EmployeesViewModel>();
        builder.Services.AddSingleton<CustomersViewModel>();
        builder.Services.AddSingleton<ProductsViewModel>();
        builder.Services.AddSingleton<OrdersViewModel>();
        builder.Services.AddSingleton<ShippingViewModel>();
        builder.Services.AddTransient<AboutViewModel>();
        builder.Services.AddTransient<SearchViewModel>();

#if MACCATALYST || WINDOWS
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<CustomerDetailViewModel>();
        builder.Services.AddSingleton<EmployeeDetailViewModel>();
        builder.Services.AddTransient<EmployeeEditViewModel>();
        builder.Services.AddTransient<CustomerEditViewModel>();
        builder.Services.AddTransient<ProductEditViewModel>();
        builder.Services.AddTransient<OrderEditViewModel>();
        builder.Services.AddTransient<ImageEditorViewModel>();
#endif

        return builder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<PlatformViews.WelcomeView>();
        builder.Services.AddSingleton<PlatformViews.EmployeesView>();
        builder.Services.AddSingleton<PlatformViews.CustomersView>();
        builder.Services.AddSingleton<PlatformViews.ProductsView>();

#if ANDROID || IOS
        builder.Services.AddSingleton<PlatformViews.MoreView>();
        builder.Services.AddSingleton<PlatformViews.OrdersPage>();
        builder.Services.AddSingleton<PlatformViews.ShippingPage>();
        builder.Services.AddSingleton<PlatformViews.AboutPage>();
#else
        builder.Services.AddSingleton<PlatformViews.OrdersView>();
        builder.Services.AddSingleton<PlatformViews.ShippingView>();
        builder.Services.AddSingleton<PlatformViews.AboutView>();
#endif

        return builder;
    }

    public static MauiAppBuilder RegisterLifecycleEvents(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
#if WINDOWS10_0_17763_0_OR_GREATER
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    window.CenterOnScreen(1200,900);
                });
            });

#elif MACCATALYST
            events.AddiOS(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.SceneWillConnect((scene, session, options) =>
                {
                    if (scene is UIWindowScene { SizeRestrictions: { } } windowScene)
                    {
                        windowScene.SizeRestrictions.MaximumSize = new CGSize(1200, 900);
                        windowScene.SizeRestrictions.MinimumSize = new CGSize(600, 400);
                    }
                });

            });
#endif
        });

        return builder;
    }
}