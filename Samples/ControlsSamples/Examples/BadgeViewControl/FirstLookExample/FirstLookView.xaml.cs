using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Dispatching;
using System;
using Telerik.Maui.Controls;

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

        this.pollingTimer = this.Dispatcher.CreateTimer();
        this.pollingTimer.Interval = TimeSpan.FromSeconds(2);
        this.pollingTimer.Tick += (s, e) => vm.UpdateUnreadMessages();
        this.pollingTimer.IsRepeating = true;

        this.Loaded += (s, e) => this.pollingTimer.Start();
        this.Unloaded += (s, e) => this.pollingTimer.Stop();
    }
}