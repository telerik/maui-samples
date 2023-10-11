using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.PdfViewerControl.SearchCategory.CustomToolbarExample;

public partial class CustomToolbar : ContentView
{
    public CustomToolbar()
	{
		InitializeComponent();
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(CustomToolbar).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-overview.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
        this.pdfViewer.SearchSettings = new Telerik.Maui.Controls.PdfViewer.PdfViewerSearchSettings { TextSearchTrigger = Telerik.Maui.Controls.PdfViewer.PdfViewerSearchTrigger.TextChanged };
    }

    private void entrySearchToolbar_TextChanged(object sender, TextChangedEventArgs e)
    {
        this.pdfViewer.SearchSettings.SearchAsync(this.entrySearchToolbar.Text, this.pdfViewer.SearchSettings.SearchOptions);
    }
}