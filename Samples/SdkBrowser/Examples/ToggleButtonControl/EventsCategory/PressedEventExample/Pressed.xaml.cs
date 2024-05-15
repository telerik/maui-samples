using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.EventsCategory.PressedEventExample;

public partial class Pressed : ContentView
{
    int count = 0;
	public Pressed()
	{
		InitializeComponent();
	}
    // >> togglebutton-pressed-event
    private void OnToggleButtonPressed(object sender, System.EventArgs e)
    {
        count++;

        if (count == 1)
        {
            this.label.Text = $"ToggleButton is pressed {count} time";
        }
        else
        {
            this.label.Text = $"ToggleButton is pressed {count} times";
        }
    }
    // << togglebutton-pressed-event
}