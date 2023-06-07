using Microsoft.Maui.Controls;
using QSF.Examples.WordsProcessingControl.Converters;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.IO;
using System.Windows.Input;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.Model;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample;

public class ExportViewModel : ExampleViewModel
{
    private readonly IFileViewerService fileViewerService;
    private readonly IResourceService resourceService;

    private string htmlText;
    private string resourceName;

    public ExportViewModel()
    {
        FixedExtensibilityManager.JpegImageConverter = new SkiaImageConverter();
        this.fileViewerService = DependencyService.Get<IFileViewerService>();
        this.resourceService = DependencyService.Get<IResourceService>();

        this.SaveAsTextCommand = new Command<IRichTextContext>(this.SaveAsText);
        this.SaveAsHtmlCommand = new Command<IRichTextContext>(this.SaveAsHtml);
        this.SaveAsDocxCommand = new Command<IRichTextContext>(this.SaveAsDocx);
        this.SaveAsRtfCommand = new Command<IRichTextContext>(this.SaveAsRtf);
        this.SaveAsPdfCommand = new Command<IRichTextContext>(this.SaveAsPdf);
    }

    public ExportViewModel(string resourceName) : this()
    {
        this.ResourceName = resourceName;
    }

    public string HtmlText
    {
        get { return this.htmlText; }
        set
        {
            if (this.htmlText != value)
            {
                this.htmlText = value;
                this.OnPropertyChanged();
            }
        }
    }

    public string ResourceName
    {
        get
        {
            return this.resourceName;
        }
        set
        {
            if (this.resourceName != value)
            {
                this.resourceName = value;
                this.OnPropertyChanged();
                this.UpdateHtmlText(resourceName);
            }
        }
    }

    public ICommand SaveAsTextCommand { get; }

    public ICommand SaveAsHtmlCommand { get; }

    public ICommand SaveAsDocxCommand { get; }

    public ICommand SaveAsRtfCommand { get; }

    public ICommand SaveAsPdfCommand { get; }

    private void SaveAsText(IRichTextContext richTextContext)
    {
        this.ExportHtml(richTextContext, DocumentType.Txt);
    }

    private void SaveAsHtml(IRichTextContext richTextContext)
    {
        this.ExportHtml(richTextContext, DocumentType.Html);
    }

    private void SaveAsDocx(IRichTextContext richTextContext)
    {
        this.ExportHtml(richTextContext, DocumentType.Docx);
    }

    private void SaveAsRtf(IRichTextContext richTextContext)
    {
        this.ExportHtml(richTextContext, DocumentType.Rtf);
    }

    private void SaveAsPdf(IRichTextContext richTextContext)
    {
        this.ExportHtml(richTextContext, DocumentType.Pdf);
    }

    private async void ExportHtml(IRichTextContext richTextContext, DocumentType documentType)
    {
        var htmlText = await richTextContext.GetHtmlAsync();

        RadFlowDocument flowDocument = DocumentHelper.CreateFlowDocument(htmlText);

        IFormatProvider<RadFlowDocument> formatProvider = DocumentHelper.GetFormatProvider(documentType);
        string extension = Enum.GetName(typeof(DocumentType), documentType).ToLower();
        string exampleName = "RichTextEditor_ImportExportExample." + extension;

        using (MemoryStream stream = new MemoryStream())
        {
            formatProvider.Export(flowDocument, stream);
            stream.Seek(0, SeekOrigin.Begin);
            await this.fileViewerService.View(stream, exampleName);
        }
    }

    private void UpdateHtmlText(string resourceName)
    {
        var importProvider = DocumentHelper.GetFormatProvider(resourceName);

        using (var resourceStream = this.resourceService.GetResourceStream(resourceName))
        {
            var flowDocument = importProvider.Import(resourceStream);
            var exportProvider = new HtmlFormatProvider();
            this.HtmlText = exportProvider.Export(flowDocument);
        }
    }
}
