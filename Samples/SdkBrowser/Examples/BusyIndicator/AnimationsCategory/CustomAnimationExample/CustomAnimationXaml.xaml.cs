using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Dispatching;
using System;
using Telerik.Maui;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common;

namespace SDKBrowserMaui.Examples.BusyIndicatorControl.AnimationsCategory.CustomAnimationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomAnimationXaml : RadContentView
    {
        public CustomAnimationXaml()
        {
            InitializeComponent();

            // >> busyindicator-animations-code
            RadDoubleAnimation annimation = new RadDoubleAnimation() { Duration = 800, From = 0.1, To = 1, PropertyPath = "Opacity", Target = radBusyIndicator.BusyContent, RepeatForever = true, AutoReverse = true };
            this.radBusyIndicator.Animations.Add(annimation);

            Application.Current.Windows[0].Page.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(5000),
                () =>
                {
                    this.radBusyIndicator.IsBusy = false;
                    return false;
                });
            // << busyindicator-animations-code
        }
    }
}