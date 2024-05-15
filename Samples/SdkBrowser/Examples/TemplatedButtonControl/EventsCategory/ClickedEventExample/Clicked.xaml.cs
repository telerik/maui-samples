using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.EventsCategory.ClickedEventExample;

public partial class Clicked : ContentView
{
	int count = 0;
	public Clicked()
	{
		InitializeComponent();
	}

	// >> templatedbutton-clicked-event
    private void OnRadTemplatedButtonClicked(object sender, System.EventArgs e)
    {
        count++;
        
        if (count == 1)
        {
            this.label.Text = $"TemplatedButton is clicked {count} time";
        }
        else
        {
            this.label.Text = $"TemplatedButton is clicked {count} times";
        }
    }
    // << templatedbutton-clicked-event
}