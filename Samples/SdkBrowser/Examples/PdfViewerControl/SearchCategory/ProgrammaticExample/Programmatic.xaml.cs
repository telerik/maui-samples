using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.PdfViewerControl.SearchCategory.ProgrammaticExample;

public partial class Programmatic : ContentView
{
	public Programmatic()
	{
		InitializeComponent(); 
        
        // >> pdfviewer-search-docusment-loading
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(Programmatic).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-overview.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
        // << pdfviewer-search-docusment-loading
    }

    // >> pdfviewer-entry-textchanged
    private void OnSearchEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        this.pdfViewer.SearchSettings.SearchAsync(this.searchEntry.Text, this.pdfViewer.SearchSettings.SearchOptions);
    }
    // << pdfviewer-entry-textchanged
}