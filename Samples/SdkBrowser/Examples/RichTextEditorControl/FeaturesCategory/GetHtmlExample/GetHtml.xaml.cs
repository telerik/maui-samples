using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls.RichTextEditor;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.FeaturesCategory.GetHtmlExample;

public partial class GetHtml : ContentView
{
	public GetHtml()
	{
		InitializeComponent();
        
        // >> richtexteditor-keyfeatures-fromstream
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(GetHtml).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("richtexteditor-htmlsource.html"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.richTextEditor.Source = RichTextSource.FromStream(streamFunc);
        // << richtexteditor-keyfeatures-fromstream
    }

    private async void GetHTML_Clicked(object sender, EventArgs e)
    {
        // >> richtexteditor-keyfeatures-gethtml
        var htmlString = await this.richTextEditor.GetHtmlAsync();
        // << richtexteditor-keyfeatures-gethtml

        await Application.Current.MainPage.DisplayAlert("Html content", htmlString, "OK");
    }
}