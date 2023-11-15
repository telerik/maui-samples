using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.ComponentModel;
using Telerik.Maui.Controls;
using Telerik.Windows.Documents.Fixed.Model;
using QSF.Examples.PdfViewerControl.Common;

namespace QSF.Examples.PdfViewerControl.CustomizationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomizationView : RadContentView
{
    public CustomizationView()
    {
        InitializeComponent();

        this.pdfViewer.Source = ResourceHelper.GetStreamFromEmbeddedResource("pdf_file.pdf");
        this.pdfViewer.PropertyChanged += this.PdfViewerPropertyChanged;

        this.navigateToFirstPageBtn.Command = new Command(this.NavigateToFirstPageCommandExecuted, this.CanExecuteNavigateToFirstPageCommand);
        this.navigateToLastPageBtn.Command = new Command(this.NavigateToLastPageCommandExecuted, this.CanExecuteNavigateToLastPageCommand);
    }

    private bool CanExecuteNavigateToFirstPageCommand(object arg)
    {
        RadFixedDocument document = this.pdfViewer?.Document;
        return document != null && this.pdfViewer.VisiblePagesStartIndex > 0;
    }

    private void NavigateToFirstPageCommandExecuted(object obj)
    {
        this.pdfViewer.NavigateToPageCommand.Execute(0);
    }

    private bool CanExecuteNavigateToLastPageCommand(object arg)
    {
        RadFixedDocument document = this.pdfViewer?.Document;
        return document != null && this.pdfViewer.VisiblePagesStartIndex < document.Pages.Count - 1;
    }

    private void NavigateToLastPageCommandExecuted(object obj)
    {
        this.pdfViewer.NavigateToPageCommand.Execute(this.pdfViewer.Document.Pages.Count - 1);
    }

    private void PdfViewerPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.pdfViewer.VisiblePagesStartIndex) ||
            e.PropertyName == nameof(this.pdfViewer.Document))
        {
            ((Command)this.navigateToFirstPageBtn.Command).ChangeCanExecute();
            ((Command)this.navigateToLastPageBtn.Command).ChangeCanExecute();
        }
    }
}