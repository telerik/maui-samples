using Microsoft.Maui.Controls;
using QSF.Examples.PdfViewerControl.Common;

namespace QSF.Examples.PdfViewerControl.ConfigurationExample;

public partial class ConfigurationView : ContentView
{
    public ConfigurationView()
    {
        this.InitializeComponent();

        this.pdfViewer.Source = ResourceHelper.GetStreamFromEmbeddedResource("pdf_file.pdf");
    }
}