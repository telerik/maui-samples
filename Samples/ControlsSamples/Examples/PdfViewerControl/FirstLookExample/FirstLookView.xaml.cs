using Microsoft.Maui.Controls.Xaml;
using QSF.Examples.PdfViewerControl.Common;
using Telerik.Maui.Controls;

namespace QSF.Examples.PdfViewerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : RadContentView
    {
       
        public FirstLookView()
        {
            InitializeComponent();

            this.pdfViewer.Source = ResourceHelper.GetStreamFromEmbeddedResource("pdf_file.pdf");
        }
    }
}