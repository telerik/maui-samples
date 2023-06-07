using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls.PdfViewer;

namespace SDKBrowserMaui.Examples.PdfViewerControl.FeaturesCategory.PageElementsLoadedExample;

public partial class PageElementsLoaded : ContentView
{
	public PageElementsLoaded()
	{
		InitializeComponent();

        this.pdfViewer.PageElementsLoaded += OnPageElementsLoaded;

        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(PageElementsLoaded).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-processing.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
    }


    // >> pdfviewer-page-element-loaded-event
    private void OnPageElementsLoaded(object sender, PageElementsLoadedEventArgs e)
    {
        foreach (var item in e.Page.Content)
        {
            if (item is Telerik.Windows.Documents.Fixed.Model.Graphics.Path path)
            {
                if (path.StrokeThickness == 0)
                {
                    path.StrokeThickness = 5;
                }
            }
        }
    }
    // << pdfviewer-page-element-loaded-event
}