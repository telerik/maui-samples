using Microsoft.Maui.Controls.Xaml;
using QSF.Examples.PdfViewerControl.Common;
using Telerik.Maui.Controls;

namespace QSF.Examples.PdfViewerControl.LargeFileExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LargeFileView : RadContentView
{
    public LargeFileView()
    {
        InitializeComponent();

        this.pdfViewer.Source = ResourceHelper.GetStreamFromEmbeddedResource("large_pdf_file.pdf");
    }
}
