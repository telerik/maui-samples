using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.EventsCategory.IsToggleChangedEventExample;

public partial class IsToggleChanged : ContentView
{
    public IsToggleChanged()
    {
        InitializeComponent();
    }

    // >> togglebutton-events-togglechanged-handler
    private void ToggleButtonIsToggledChanged(object sender, Telerik.Maui.Controls.ValueChangedEventArgs<bool?> e)
    {
        this.label.Text = "IsToggled: " + e.NewValue;
    }
    // << togglebutton-events-togglechanged-handler
}