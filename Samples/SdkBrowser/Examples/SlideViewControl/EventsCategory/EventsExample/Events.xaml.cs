using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SlideViewControl.EventsCategory.EventsExample;

public partial class Events : RadContentView
{
    public Events()
    {
        InitializeComponent();

        this.slideView.BindingContext = new ViewModel();
    }

    // >> slideview-events-current-item-changed-event
    private void OnCurrentItemChanged(object sender, Telerik.Maui.Controls.SlideView.CurrentItemChangedEventArgs e)
    {
        App.Current.MainPage.DisplayAlert("Current index changed to", " " + e.NewIndex, "OK");
    }
    // << slideview-events-current-item-changed-event
}