using System;
using Telerik.Maui.Controls.PdfViewer;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.PdfViewerControl.FeaturesCategory.SourceExceptionExample;

public partial class SourceException : ContentView
{
	public SourceException()
	{
		InitializeComponent();

        this.pdfViewer.Source = new Uri("a.b.c", UriKind.Relative);
    }

    // >> pdfviewer-sourceexception-eventhandler
    private void PdfViewerSourceException(object sender, SourceExceptionEventArgs e)
    {
        var error = e.Exception.Message;
    }
    // << pdfviewer-sourceexception-eventhandler
}