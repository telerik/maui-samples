using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.PdfViewerControl.ToolbarCategory.CustomToolbarExample;

public partial class CustomToolbar : ContentView
{
	public CustomToolbar()
	{
		InitializeComponent();

        // >> pdfviewer-toolbar-customcommand
        this.BindingContext = new ViewModel();

        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(CustomToolbar).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-processing.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
        // << pdfviewer-toolbar-customcommand
    }
}