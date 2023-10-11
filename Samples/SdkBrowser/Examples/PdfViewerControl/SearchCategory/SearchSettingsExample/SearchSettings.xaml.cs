using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.PdfViewerControl.SearchCategory.SearchSettingsExample;

public partial class SearchSettings : ContentView
{
	public SearchSettings()
	{
		InitializeComponent();
        // >> pdfviewer-search-settings-loading-document
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(SearchSettings).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-processing.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
        // << pdfviewer-search-settings-loading-document
    }
}