using System;
using Microsoft.Maui;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using QSF.Services;

namespace QSF
{
    [Activity(
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        Name = "com.telerik.ControlsSamples.MainActivity",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : MauiAppCompatActivity
	{
        protected override void OnCreate(Bundle bundle)
        {
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;

            base.OnCreate(bundle);

            this.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            this.Window.SetStatusBarColor(App.ApplicationAccentColor.ToPlatform());

            this.Window.InsetsController.SetSystemBarsAppearance(
                (int)Android.Views.WindowInsetsControllerAppearance.LightNavigationBars,
                (int)Android.Views.WindowInsetsControllerAppearance.LightNavigationBars);

            this.Window.InsetsController.SetSystemBarsAppearance(
                (int)Android.Views.WindowInsetsControllerAppearance.LightStatusBars,
                0);

            Microsoft.Maui.ApplicationModel.Platform.Init(this, bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Microsoft.Maui.ApplicationModel.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}