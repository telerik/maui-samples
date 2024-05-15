using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.EventsCategory.ReleasedEventExample;

public partial class Released : ContentView
{
    int count = 0;

	public Released()
	{
		InitializeComponent();
	}
    // >> togglebutton-released-event
    private void OnToggleButtonReleased(object sender, System.EventArgs e)
    {
        count++;

        if (count == 1)
        {
            this.label.Text = $"ToggleButton is released {count} time";
        }
        else
        {
            this.label.Text = $"ToggleButton is released {count} times";
        }
    }
    // << togglebutton-released-event
}