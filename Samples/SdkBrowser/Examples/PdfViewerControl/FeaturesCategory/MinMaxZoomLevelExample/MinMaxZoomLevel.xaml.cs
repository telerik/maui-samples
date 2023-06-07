using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Fixed.Model;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.PdfViewerControl.FeaturesCategory.MinMaxZoomLevelExample;

public partial class MinMaxZoomLevel : ContentView
{
	public MinMaxZoomLevel()
	{
		InitializeComponent();

        // >> pdfviewer-key-features-source-uri
        Uri uri = this.GetUri();
        this.pdfViewer.Source = uri;
        // << pdfviewer-key-features-source-uri

        // >> pdfviewer-key-features-source-byte
        byte[] bytes = this.GetBytes();
        this.pdfViewer.Source = bytes;
        // << pdfviewer-key-features-source-byte

        //this.ImportFixedDocument();

        // >> pdfviewer-key-features-stream
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(MinMaxZoomLevel).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-processing.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
        // << pdfviewer-key-features-stream

        // >> pdfviewer-key-features-propertychanged
        this.pdfViewer.PropertyChanged += PdfViewer_PropertyChanged;
        // << pdfviewer-key-features-propertychanged
    }

    // >> pdfviewer-key-features-propertychanged-handler
    private void PdfViewer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Document")
        {
            var loadedDocument = this.pdfViewer.Document as RadFixedDocument;
        }
    }
    // << pdfviewer-key-features-propertychanged-handler

    // >> pdfviewer-key-features-source-fixed-method
    private void ImportFixedDocument()
    {
        Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.PdfFormatProvider provider = new Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.PdfFormatProvider();
        Assembly assembly = typeof(MinMaxZoomLevel).Assembly;
        string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-processing.pdf"));
        using (Stream stream = assembly.GetManifestResourceStream(fileName))
        {
            RadFixedDocument document = provider.Import(stream);
            this.pdfViewer.Source = document;
        }
    }
    // << pdfviewer-key-features-source-fixed-method

    private Uri GetUri()
    {
        return null;
    }

    private byte[] GetBytes()
    {
        return null;
    }
}