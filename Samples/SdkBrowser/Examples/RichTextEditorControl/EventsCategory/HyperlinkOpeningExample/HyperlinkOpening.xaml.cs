using System;
using Microsoft.Maui.Controls;
using SDKBrowserMaui.Behaviors;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.EventsCategory.HyperlinkOpeningExample;

public partial class HyperlinkOpening : ContentView
{
    public HyperlinkOpening()
    {
        InitializeComponent();

        var htmlSource = @"<h5>Check the following links:</h5>
                        <p><a href='https://www.telerik.com/maui-ui'>Telerik.com - launched by default</a></p>
                        <p><a href='myapp://open'>Custom scheme - handled by the app</a></p>
                        <p><a href='file:///etc/hosts'>File scheme - blocked by default</a></p>";
        this.richTextEditor.Source = RichTextSource.FromString(htmlSource);
        this.richTextEditor.Behaviors.Add(new PickImageBehavior());
        this.grid.Behaviors.Add(new AndroidKeyboardPaddingBehavior());
    }

    // >> richtexteditor-hyperlinkopening-code
    // By default, the editor launches only http, https, mailto and tel links.
    // Handle the event to take over links with other schemes, such as the app's own deep links.
    private async void RichTextEditor_HyperlinkOpening(object sender, HyperlinkOpeningEventArgs e)
    {
        if (Uri.TryCreate(e.Url, UriKind.Absolute, out Uri uri) && uri.Scheme == "myapp")
        {
            e.Handled = true;

            await Application.Current.Windows[0].Page.DisplayAlertAsync("Custom link", $"The app handles: {uri}", "OK");
        }
    }
    // << richtexteditor-hyperlinkopening-code
}
