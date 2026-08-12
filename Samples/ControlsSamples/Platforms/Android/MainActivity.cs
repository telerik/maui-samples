using System;
using Microsoft.Maui;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
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

#if NET10_0_OR_GREATER
            ViewCompat.SetOnApplyWindowInsetsListener(this.Window.DecorView, new ConsumeInsetsListener());
            if (Build.VERSION.SdkInt < BuildVersionCodes.VanillaIceCream)
            {
                this.Window.SetStatusBarColor(App.ApplicationAccentColor.ToPlatform());
            }
#else
            this.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            this.Window.SetStatusBarColor(App.ApplicationAccentColor.ToPlatform());
#endif

            this.Window.InsetsController.SetSystemBarsAppearance(
                (int)Android.Views.WindowInsetsControllerAppearance.LightNavigationBars,
                (int)Android.Views.WindowInsetsControllerAppearance.LightNavigationBars);

            this.Window.InsetsController.SetSystemBarsAppearance(
                0,
                (int)Android.Views.WindowInsetsControllerAppearance.LightStatusBars);

            Microsoft.Maui.ApplicationModel.Platform.Init(this, bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Microsoft.Maui.ApplicationModel.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

#if NET10_0_OR_GREATER
        private class ConsumeInsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
        {
            public WindowInsetsCompat OnApplyWindowInsets(Android.Views.View v, WindowInsetsCompat insets)
            {
                ViewCompat.OnApplyWindowInsets(v, insets);
                return WindowInsetsCompat.Consumed;
            }
        }
#endif
    }
}