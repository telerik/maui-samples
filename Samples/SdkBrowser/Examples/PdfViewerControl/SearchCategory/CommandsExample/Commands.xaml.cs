using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.PdfViewerControl.SearchCategory.CommandsExample;

public partial class Commands : ContentView
{
	public Commands()
	{
		InitializeComponent();
        // >> pdfviewer-search-docusment-loading
        Func <CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(Commands).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-overview.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
        // << pdfviewer-search-docusment-loading
    }
}