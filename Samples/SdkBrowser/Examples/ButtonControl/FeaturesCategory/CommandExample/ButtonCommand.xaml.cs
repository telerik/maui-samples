using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ButtonControl.FeaturesCategory.CommandExample;

public partial class ButtonCommand : ContentView
{
    int count = 0;

    public ButtonCommand()
    {
        InitializeComponent();

        // >> button-command
        this.button.Command = new Command(
            execute: () =>
            {
                count++;
                if (count == 1)
                    button.Text = $"Clicked {count} time";
                else
                    button.Text = $"Clicked {count} times";
            },
            canExecute: () =>
            {
                return true;
            });
        // << button-command
    }
}