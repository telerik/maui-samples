using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.EventsCategory.PressedEventExample;

public partial class Pressed : ContentView
{
    int count = 0;
	public Pressed()
	{
		InitializeComponent();
	}

	// >> templatedbutton-pressed-event
	private void OnTemplatedButtonPressed(object sender, System.EventArgs e)
	{
        count++;

        if (count == 1)
        {
            this.label.Text = $"TemplatedButton is pressed {count} time";
        }
        else
        {
            this.label.Text = $"TemplatedButton is pressed {count} times";
        }
    }
    // << templatedbutton-pressed-event
}