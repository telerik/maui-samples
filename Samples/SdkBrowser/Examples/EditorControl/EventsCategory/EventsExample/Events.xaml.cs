using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.EditorControl.EventsCategory.EventsExample;

public partial class Events : ContentView
{
    public Events()
    {
        InitializeComponent();
    }

    // >> editor-events-text-changing
    private void OnTextChanging(object sender, Telerik.Maui.Controls.TextChangingEventArgs e)
    {
        this.eventLabel.Text = $"Text changing from '{e.OldText}' to '{e.NewText}'";
    }
    // << editor-events-text-changing

    // >> editor-events-completed
    private void OnCompleted(object sender, System.EventArgs e)
    {
        var editor = sender as RadEditor;
        if (editor.Text is not null)
        {
            editor.Text = string.Empty;
            this.eventLabel.Text = string.Empty;
        }
    }
    // << editor-events-completed
}