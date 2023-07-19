using Microsoft.Maui.Controls;
using QSF.Examples.WordsProcessingControl.Converters;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Documents.Core.Fonts;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace QSF.Examples.PdfProcessingControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private readonly IResourceService resourceService;
        private readonly IFileViewerService fileViewerService;
        private FixedContentEditor pageEditor;

        public FirstLookViewModel()
        {
            FixedExtensibilityManager.JpegImageConverter = new SkiaImageConverter();

            this.Save = new Command(async (p) => await this.Export(p));
            this.resourceService = DependencyService.Get<IResourceService>();
            this.fileViewerService = DependencyService.Get<IFileViewerService>();
        }

        public ICommand Save { get; private set; }

        private async Task Export(object param)
        {
            RadFixedDocument document = this.CreateDocument();
            using (MemoryStream stream = new MemoryStream())
            {
                PdfFormatProvider pdfFormatProvider = new PdfFormatProvider();
                pdfFormatProvider.Export(document, stream);

                stream.Seek(0, SeekOrigin.Begin);

                await this.fileViewerService.View(stream, "example.pdf");
            }
        }

        public RadFixedDocument CreateDocument()
        {
            RadFixedDocument document = new RadFixedDocument();
            RadFixedPage page = document.Pages.AddPage();
            page.Size = new Size(ExampleDocumentSizes.PageWidth, ExampleDocumentSizes.PageHeight);
            this.pageEditor = new FixedContentEditor(page);
            this.pageEditor.Position.Translate(40, 55);

            using (Stream stream = this.resourceService.GetResourceStream("banner.png"))
            {
                this.pageEditor.DrawImage(stream, new Size(609, 92));
            }

            this.pageEditor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, 180);
            double maxWidth = page.Size.Width - ExampleDocumentSizes.DefaultLeftIndent * 2;

            this.DrawDescription(maxWidth);

            using (this.pageEditor.SaveProperties())
            {
                using (this.pageEditor.SavePosition())
                {
                    this.DrawFigure();
                }
            }

            return document;
        }

        private void SetTextProperties(Block block, ColorBase color, double fontSize, FontFamily fontFamily, double lineSpacing = 1, bool isBold = false, bool isUnderlined = false)
        {
            block.GraphicProperties.FillColor = color;
            block.HorizontalAlignment = HorizontalAlignment.Left;
            block.TextProperties.FontSize = fontSize;
            FontWeight fontWeight = isBold ? FontWeights.Bold : FontWeights.Normal;
            block.TextProperties.TrySetFont(fontFamily, FontStyles.Normal, fontWeight);
            block.TextProperties.UnderlinePattern = isUnderlined ? UnderlinePattern.Single : UnderlinePattern.None;
            block.LineSpacing = lineSpacing;
        }

        private void DrawDescription(double maxWidth)
        {
            Block block = new Block();

            this.SetTextProperties(block, new RgbColor(36, 151, 56), 22, new FontFamily("Helvetica"));
            block.InsertText("Thank you for choosing Telerik RadPdfProcessing!");
            this.pageEditor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            block = new Block();
            this.SetTextProperties(block, RgbColors.Black, 25, new FontFamily("Helvetica"));
            block.InsertLineBreak();
            this.SetTextProperties(block, RgbColors.Black, 13, new FontFamily("Helvetica-Bold"));
            block.InsertText("RadPdfProcessing");

            this.SetTextProperties(block, RgbColors.Black, 13, new FontFamily("Helvetica"));
            block.InsertText(" is a document processing library that enables your application to import and export files to and from PDF format. The document model is entirely independent from UI and allows you to generate sleek documents with differently formatted text, images, shapes and more.");
            this.pageEditor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            double currentTopOffset = 560;
            this.pageEditor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, currentTopOffset);
            block = new Block();
            this.SetTextProperties(block, RgbColors.Black, 25, new FontFamily("Times-Roman"), 1.2, true);
            block.InsertLineBreak();
            this.SetTextProperties(block, RgbColors.Black, 15, new FontFamily("Times-Bold"), 1.2);
            block.InsertText("RadPdfProcessing");
            this.SetTextProperties(block, RgbColors.Black, 15, new FontFamily("Times-Roman"), 1.2);
            block.InsertText(" was built with performance and stability in mind. The document automation is fast and has a low memory footprint even with large amounts of data.");
            this.pageEditor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += ExampleDocumentSizes.DefaultLineHeight * 4;
            this.pageEditor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, currentTopOffset);

            block = new Block();
            this.SetTextProperties(block, RgbColors.Black, 25, new FontFamily("Helvetica"), 1.2);
            block.InsertLineBreak();
            this.SetTextProperties(block, RgbColors.Black, 13, new FontFamily("Helvetica-Oblique"), 1.2, false, true);
            block.InsertText("The intuitive API allows you to swiftly generate documents from scratch. Designed with the user in mind, RadPdfProcessing is straightforward and easy to use.");
            this.pageEditor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += ExampleDocumentSizes.DefaultLineHeight * 2;
            this.pageEditor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, currentTopOffset);

            block = new Block();
            this.SetTextProperties(block, new RgbColor(36, 151, 56), 35, new FontFamily("Helvetica"), isBold: true);
            block.InsertLineBreak();

            currentTopOffset += ExampleDocumentSizes.DefaultLineHeight * 2;
            this.pageEditor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, currentTopOffset);
            this.pageEditor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));
            this.DrawLogo();
        }

        private void DrawLogo()
        {
            using (Stream stream = this.resourceService.GetResourceStream("progressLogo.png"))
            {
                double imageWidth = 160;
                double imageHeight = 37.84;
                this.pageEditor.Position.Translate((ExampleDocumentSizes.PageWidth / 2) - (imageWidth / 2), ExampleDocumentSizes.PageHeight - imageHeight - 50);

                this.pageEditor.DrawImage(stream, new Size(imageWidth, imageHeight));
            }
        }

        private void DrawFigure()
        {
            FormSource formSource = FigureCreator.Create(new Size(600, 800));

            pageEditor.Position.Translate(40, 280);
            pageEditor.DrawForm(formSource);
        }
    }
}
