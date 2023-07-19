using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Documents.Common.Model;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Primitives;
using TelerikDocumentsFlowModel = Telerik.Windows.Documents.Flow.Model.Styles;

namespace QSF.Examples.WordsProcessingControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private static readonly ThemableColor GreenColor = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 92, 230, 0));
        private readonly IResourceService resourceService;
        private readonly IFileViewerService fileViewerService;
        private ICommand generateCommand = null;
        private bool includeFooter = true;
        private bool includeHeader = true;

        private string selectedExportFormat;
        private IEnumerable<string> exportFormats;

        public ICommand GenerateCommand
        {
            get
            {
                return this.generateCommand;
            }
            set
            {
                if (this.generateCommand != value)
                {
                    this.generateCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                if (!object.Equals(this.selectedExportFormat, value))
                {
                    this.selectedExportFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IncludeHeader
        {
            get
            {
                return this.includeHeader;
            }
            set
            {
                if (this.includeHeader != value)
                {
                    this.includeHeader = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IncludeFooter
        {
            get
            {
                return this.includeFooter;
            }
            set
            {
                if (this.includeFooter != value)
                {
                    this.includeFooter = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IEnumerable<string> ExportFormats
        {
            get
            {
                if (this.exportFormats == null)
                {
                    this.exportFormats = new string[] { "Docx", "Rtf", "Html", "Txt" };
                }

                return this.exportFormats;
            }
        }

        public FirstLookViewModel()
        {
            this.GenerateCommand = new Command(async () => await this.Generate());
            this.resourceService = DependencyService.Get<IResourceService>();
            this.fileViewerService = DependencyService.Get<IFileViewerService>();
            this.SelectedExportFormat = this.ExportFormats.ElementAt(0);
        }

        private async Task Generate()
        {
            RadFlowDocument document = this.CreateDocument();
            IFormatProvider<RadFlowDocument> formatProvider = null;
            string exampleName = null;

            switch (selectedExportFormat)
            {
                case "Docx":
                    formatProvider = new DocxFormatProvider();
                    exampleName = "example.docx";
                    break;
                case "Rtf":
                    formatProvider = new RtfFormatProvider();
                    exampleName = "example.rtf";
                    break;
                case "Html":
                    formatProvider = new HtmlFormatProvider();
                    exampleName = "example.html";
                    break;
                case "Txt":
                    formatProvider = new TxtFormatProvider();
                    exampleName = "example.txt";
                    break;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                formatProvider.Export(document, stream);

                stream.Seek(0, SeekOrigin.Begin);

                await this.fileViewerService.View(stream, exampleName);
            }
        }

        private RadFlowDocument CreateDocument()
        {
            RadFlowDocument document = new RadFlowDocument();
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);
            editor.ParagraphFormatting.TextAlignment.LocalValue = TelerikDocumentsFlowModel.Alignment.Justified;

            editor.InsertLine("Dear Telerik User,");
            editor.InsertText("We’re happy to introduce the Telerik RadWordsProcessing component for .NET MAUI. High performance library that enables you to read, write and manipulate documents in DOCX, RTF and plain text format. The document model is independent from UI and ");
            Run run = editor.InsertText("does not require");
            run.Underline.Pattern = TelerikDocumentsFlowModel.UnderlinePattern.Single;
            editor.InsertLine(" Microsoft Office.");

            editor.InsertText("The current community preview version comes with full rich-text capabilities including ");
            editor.InsertText("bold, ").FontWeight = FontWeights.Bold;
            editor.InsertText("italic, ").FontStyle = FontStyles.Italic;
            editor.InsertText("underline,").Underline.Pattern = TelerikDocumentsFlowModel.UnderlinePattern.Single;
            editor.InsertText(" font sizes and ").FontSize = 20;
            editor.InsertText("colors ").ForegroundColor = GreenColor;

            editor.InsertLine("as well as text alignment and indentation. Other options include tables, hyperlinks, inline and floating images. Even more sweetness is added by the built-in styles and themes.");

            editor.InsertText("Here at Telerik we strive to provide the best services possible and fulfill all needs you as a customer may have. We would appreciate any feedback you send our way through the ");
            editor.InsertHyperlink("public forums", "http://www.telerik.com/forums", false, "Telerik Forums");
            editor.InsertLine(" or support ticketing system.");

            editor.InsertLine("We hope you’ll enjoy RadWordsProcessing as much as we do. Happy coding!");
            run = editor.InsertText("Kind regards,");
            run.Paragraph.Properties.SpacingBefore.LocalValue = 30;

            this.CreateSignature(editor);

            this.CreateHeader(editor);

            this.CreateFooter(editor);

            return document;
        }

        private void CreateSignature(RadFlowDocumentEditor editor)
        {
            Table signatureTable = editor.InsertTable(1, 2);

            signatureTable.Rows[0].Cells[0].Borders = new TelerikDocumentsFlowModel.TableCellBorders(
                                                      new TelerikDocumentsFlowModel.Border(TelerikDocumentsFlowModel.BorderStyle.None),
                                                      new TelerikDocumentsFlowModel.Border(TelerikDocumentsFlowModel.BorderStyle.None),
                                                      new TelerikDocumentsFlowModel.Border(4, TelerikDocumentsFlowModel.BorderStyle.Single, GreenColor),
                                                      new TelerikDocumentsFlowModel.Border(TelerikDocumentsFlowModel.BorderStyle.None));

            signatureTable.Rows[0].Cells[0].PreferredWidth = new TelerikDocumentsFlowModel.TableWidthUnit(140);

            Paragraph paragraphWithImage = signatureTable.Rows[0].Cells[0].Blocks.AddParagraph();
            paragraphWithImage.Spacing.SpacingAfter = 0;

            editor.MoveToParagraphStart(paragraphWithImage);

            using (Stream stream = this.resourceService.GetResourceStream("progressLogo.png"))
            {
                editor.InsertImageInline(stream, "png", new Telerik.Documents.Primitives.Size(93, 42));
            }

            signatureTable.Rows[0].Cells[1].PreferredWidth = new TelerikDocumentsFlowModel.TableWidthUnit(140);
            signatureTable.Rows[0].Cells[1].Padding = new Padding(12, 0, 0, 0);

            Paragraph cellParagraph = signatureTable.Rows[0].Cells[1].Blocks.AddParagraph();
            cellParagraph.Spacing.SpacingAfter = 0;

            editor.MoveToParagraphStart(cellParagraph);
            editor.CharacterFormatting.FontSize.LocalValue = 12;
            editor.InsertText("Jane Doe").FontWeight = FontWeights.Bold;
            editor.InsertParagraph().Spacing.SpacingAfter = 0;
            editor.InsertText("Support Officer");
        }

        private void CreateFooter(RadFlowDocumentEditor editor)
        {
            if (this.IncludeFooter)
            {
                Footer footer = editor.Document.Sections.First().Footers.Add();
                Paragraph paragraph = footer.Blocks.AddParagraph();
                paragraph.TextAlignment = TelerikDocumentsFlowModel.Alignment.Right;

                editor.MoveToParagraphStart(paragraph);
                editor.InsertHyperlink("telerik.com", "http://www.telerik.com", false, "Telerik Site");
            }
        }

        private void CreateHeader(RadFlowDocumentEditor editor)
        {
            if (this.IncludeHeader)
            {
                Header header = editor.Document.Sections.First().Headers.Add();

                editor.MoveToParagraphStart(header.Blocks.AddParagraph());

                using (Stream stream = this.resourceService.GetResourceStream("progressLogo.png"))
                {
                    editor.InsertImageInline(stream, "png", new Telerik.Documents.Primitives.Size(660, 265));
                }
            }
        }
    }
}
