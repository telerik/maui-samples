using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Dispatching;
using System;
using Telerik.Maui.Controls;
using QSF.Services.Interfaces;

namespace QSF.Examples.BadgeViewControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : RadContentView, INavigationView
    {
        private IDispatcherTimer pollingTimer;

        public FirstLookView()
        {
            this.InitializeComponent();
            this.InitializePollingTimer();
        }

        private void InitializePollingTimer()
        {
            this.pollingTimer = this.Dispatcher.CreateTimer();
            this.pollingTimer.Interval = TimeSpan.FromSeconds(2);
            this.pollingTimer.Tick += this.OnPollingTimerTick;
            this.pollingTimer.IsRepeating = true;
        }

        private void OnPollingTimerTick(object sender, EventArgs eventArgs)
        {
            var viewModel = (FirstLookViewModel)this.BindingContext;
            viewModel.UpdateUnreadMessages();
        }

        public void OnAppearing()
        {
            this.pollingTimer.Start();
        }

        public void OnDisappearing()
        {
            this.pollingTimer.Stop();
        }
    }
}