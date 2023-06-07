using System;
using System.IO;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample;

internal static class DocumentHelper
{
    public static IFormatProvider<RadFlowDocument> GetFormatProvider(string resourceName)
    {
        var extension = Path.GetExtension(resourceName).ToLower();
        switch (extension)
        {
            case ".docx":
                return new DocxFormatProvider();
            case ".rtf":
                return new RtfFormatProvider();
            case ".html":
                return new HtmlFormatProvider();
            case ".txt":
                return new TxtFormatProvider();
            case ".pdf":
                return new PdfFormatProvider();
            default:
                throw new NotSupportedException($"Cannot import from {extension}.");
        }
    }

    public static IFormatProvider<RadFlowDocument> GetFormatProvider(DocumentType documentType)
    {
        IFormatProvider<RadFlowDocument> formatProvider = null;
        switch (documentType)
        {
            case DocumentType.Docx:
                formatProvider = new DocxFormatProvider();
                break;
            case DocumentType.Pdf:
                formatProvider = new PdfFormatProvider();
                break;
            case DocumentType.Rtf:
                formatProvider = new RtfFormatProvider();
                break;
            case DocumentType.Html:
                formatProvider = new HtmlFormatProvider();
                break;
            case DocumentType.Txt:
                formatProvider = new TxtFormatProvider();
                break;
        }

        return formatProvider;
    }

    public static RadFlowDocument CreateFlowDocument(string htmlContent)
    {
        var importProvider = new HtmlFormatProvider();
        return importProvider.Import(htmlContent);
    }
}
