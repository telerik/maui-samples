using Microsoft.Maui.Controls.Xaml;
using QSF.Examples.PdfViewerControl.Common;
using Telerik.Maui.Controls;

namespace QSF.Examples.PdfViewerControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : RadContentView
{
    public ThemingView()
    {
        InitializeComponent();

        this.pdfViewer.Source = ResourceHelper.GetStreamFromEmbeddedResource("pdf_file.pdf");
    }
}