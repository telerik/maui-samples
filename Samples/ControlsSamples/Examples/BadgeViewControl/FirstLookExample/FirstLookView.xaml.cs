using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Dispatching;
using Telerik.Maui.Controls;
using Telerik.AppUtils.Services;
using QSF.Services;

namespace QSF.Examples.BadgeViewControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    private IDispatcherTimer pollingTimer;

    public FirstLookView()
    {
        this.InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        FirstLookViewModel vm = this.BindingContext as FirstLookViewModel;

        if (!DependencyService.Get<ITestingService>().IsAppUnderTest)
        {
            this.pollingTimer = this.Dispatcher.CreateTimer();
            this.pollingTimer.Interval = TimeSpan.FromSeconds(2);
            this.pollingTimer.Tick += (s, e) => vm.UpdateUnreadMessages();
            this.pollingTimer.IsRepeating = true;

            this.Loaded += (s, e) => this.pollingTimer.Start();
            this.Unloaded += (s, e) => this.pollingTimer.Stop();
        }
    }
}