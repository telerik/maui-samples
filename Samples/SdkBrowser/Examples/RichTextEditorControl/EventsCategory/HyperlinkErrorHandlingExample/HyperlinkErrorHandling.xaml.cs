using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.EventsCategory.HyperlinkErrorHandlingExample;

public partial class HyperlinkErrorHandling : ContentView
{
	public HyperlinkErrorHandling()
	{
		InitializeComponent();

        var htmlSource = @"<h5>Check the following links:</h5>
                        <p><a href='https://www.telerik.com/maui-ui' target='_blank'>Telerik.com - valid Url</a></p>
                        <p><a href='www.google.com'>Google - not absolute Url</a></p>
                        <p><a href='http:/www.wvtesting.com'>Wrong format of Url</a></p>";
        this.richTextEditor.Source = RichTextSource.FromString(htmlSource);
    }

    // >> richtexteditor-hyperlinkerrorhandling-code
    private void RichTextEditor_OpenHyperlinkError(object sender, OpenHyperlinkErrorEventArgs e)
    {
        e.Handled = true;
        Application.Current.MainPage.DisplayAlert(string.Format("Error opening {0}", e.Url), e.Error.Message, "Ok");
    }
    // << richtexteditor-hyperlinkerrorhandling-code
}