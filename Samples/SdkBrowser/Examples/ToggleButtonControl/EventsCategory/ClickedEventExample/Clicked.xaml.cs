using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.EventsCategory.ClickedEventExample;

public partial class Clicked : ContentView
{
    int count = 0;

	public Clicked()
	{
		InitializeComponent();
	}
    // >> togglebutton-clicked-event
    private void OnRadToggleButtonClicked(object sender, System.EventArgs e)
    {
        count++;

        if (count == 1)
        {
            this.label.Text = $"ToggleButton is clicked {count} time";
        }
        else
        {
            this.label.Text = $"ToggleButton is clicked {count} times";
        }
    }
    // << togglebutton-clicked-event
}