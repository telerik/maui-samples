using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.EventsCategory.ReleasedEventExample;

public partial class Released : ContentView
{
    int count = 0;
	public Released()
	{
		InitializeComponent();
	}

	// >> templatedbutton-released-event
    private void OnTemplatedButtonReleased(object sender, System.EventArgs e)
    {
        count++;

        if (count == 1)
        {
            this.label.Text = $"TemplatedButton is released {count} time";
        }
        else
        {
            this.label.Text = $"TemplatedButton is released {count} times";
        }
    }
    // << templatedbutton-released-event
}