using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Documents.Core.Fonts;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Graphics;

namespace QSF.Examples.PdfProcessingControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private readonly IResourceService resourceService;
        private readonly IFileViewerService fileViewerService;
        private FixedContentEditor editor;

        public FirstLookViewModel()
        {
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
            page.Size = new Size(600, 800);
            this.editor = new FixedContentEditor(page);
            this.editor.Position.Translate(40, 50);

            using (Stream stream = this.resourceService.GetResourceStream("banner.jpg"))
            {
                this.editor.DrawImage(stream, new Size(530, 80));
            }

            this.editor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, 160);
            double maxWidth = page.Size.Width - ExampleDocumentSizes.DefaultLeftIndent * 2;

            this.DrawDescription(maxWidth);

            using (this.editor.SaveProperties())
            {
                using (this.editor.SavePosition())
                {
                    this.DrawFunnelFigure();
                }
            }

            return document;
        }

        private void SetTextProperties(Block block, ColorBase color, double fontSize, FontFamily fontFamily, bool isBold = false, bool isUnderlined = false)
        {
            block.GraphicProperties.FillColor = color;
            block.HorizontalAlignment = HorizontalAlignment.Left;
            block.TextProperties.FontSize = fontSize;
            FontWeight fontWeight = isBold ? FontWeights.Bold : FontWeights.Normal;
            block.TextProperties.TrySetFont(fontFamily, FontStyles.Normal, fontWeight);
            block.TextProperties.UnderlinePattern = isUnderlined ? UnderlinePattern.Single : UnderlinePattern.None;
        }

        private void DrawDescription(double maxWidth)
        {
            Block block = new Block();

            this.SetTextProperties(block, new RgbColor(155, 199, 5), 18, new FontFamily("Helvetica"));
            block.InsertText("Thank you for choosing Telerik RadPdfProcessing!");
            this.editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            block = new Block();
            this.SetTextProperties(block, RgbColors.Black, 25, new FontFamily("Helvetica"));
            block.InsertLineBreak();
            this.SetTextProperties(block, RgbColors.Black, 11, new FontFamily("Helvetica"), true);
            block.InsertText("RadPdfProcessing");

            this.SetTextProperties(block, RgbColors.Black, 11, new FontFamily("Helvetica"));
            block.InsertText(" is a document processing library that enables your application to import and export files to and from PDF format. The document model is entirely independent from UI and allows you to generate sleek documents with differently formatted text, images, shapes and more.");
            this.editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            double currentTopOffset = 480;
            this.editor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, currentTopOffset);
            block = new Block();
            this.SetTextProperties(block, RgbColors.Black, 25, new FontFamily("Helvetica"), true);
            block.InsertLineBreak();
            this.SetTextProperties(block, RgbColors.Black, 13, new FontFamily("Times-Roman"), true);
            block.InsertText("RadPdfProcessing");
            this.SetTextProperties(block, RgbColors.Black, 13, new FontFamily("Times-Roman"));
            block.InsertText(" was built with performance and stability in mind. The document automation is fast and has a low memory footprint even with large amounts of data.");
            this.editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += ExampleDocumentSizes.DefaultLineHeight * 3;
            this.editor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, currentTopOffset);

            block = new Block();
            this.SetTextProperties(block, RgbColors.Black, 25, new FontFamily("Helvetica"));
            block.InsertLineBreak();
            this.SetTextProperties(block, RgbColors.Black, 11, new FontFamily("Helvetica"), false, true);
            block.InsertText("The intuitive API allows you to swiftly generate documents from scratch. Designed with the user in mind, RadPdfProcessing is straightforward and easy to use.");
            this.editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += ExampleDocumentSizes.DefaultLineHeight * 2;
            this.editor.Position.Translate(ExampleDocumentSizes.DefaultLeftIndent, currentTopOffset);

            block = new Block();
            this.SetTextProperties(block, new RgbColor(45, 178, 0), 35, new FontFamily("Courier"));
            block.InsertLineBreak();

            this.SetTextProperties(block, new RgbColor(45, 178, 0), 12, new FontFamily("Courier"));
            DateTime date = DateTime.Now;
            block.InsertText(string.Format("{0} {1}", date.ToLongDateString(), date.ToLongTimeString()));
            block.InsertLineBreak();
            block.InsertText("by XAMARIN Team");

            currentTopOffset += ExampleDocumentSizes.DefaultLineHeight * 2;

            this.editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));
            this.DrawLogo();
        }

        private void DrawLogo()
        {
            using (Stream stream = this.resourceService.GetResourceStream("teleriklogo.jpg"))
            {
                double imageWidth = 200;
                double imageHeight = 63;
                this.editor.Position.Translate(ExampleDocumentSizes.FunnelCenter.X - (imageWidth / 2), ExampleDocumentSizes.FunnelCenter.Y + ExampleDocumentSizes.FunnelHeight + ExampleDocumentSizes.ArrowLength + 220);

                this.editor.DrawImage(stream, new Size(imageWidth, imageHeight));
            }
        }

        private void DrawFunnelFigure()
        {
            this.editor.Position.Translate(0, 0);
            this.editor.GraphicProperties.IsStroked = false;
            this.editor.GraphicProperties.FillColor = new RgbColor(147, 208, 237);

            this.editor.DrawEllipse(ExampleDocumentSizes.EllipseCenter, ExampleDocumentSizes.EllipseRadiuses.Width, ExampleDocumentSizes.EllipseRadiuses.Height);

            double funelBlockGap = (ExampleDocumentSizes.FunnelHeight - 3 * ExampleDocumentSizes.FunnelBlockHeight) / 2;

            double startPercent = 0;
            double endPercent = ExampleDocumentSizes.GetPercentHeight(ExampleDocumentSizes.FunnelBlockHeight);
            this.DrawFunnelBlock(startPercent, endPercent);

            startPercent = ExampleDocumentSizes.GetPercentHeight(ExampleDocumentSizes.FunnelBlockHeight + funelBlockGap);
            endPercent = ExampleDocumentSizes.GetPercentHeight(2 * ExampleDocumentSizes.FunnelBlockHeight + funelBlockGap);
            this.DrawFunnelBlock(startPercent, endPercent);

            startPercent = ExampleDocumentSizes.GetPercentHeight(2 * ExampleDocumentSizes.FunnelBlockHeight + 2 * funelBlockGap);
            endPercent = ExampleDocumentSizes.GetPercentHeight(3 * ExampleDocumentSizes.FunnelBlockHeight + 2 * funelBlockGap);
            this.DrawFunnelBlock(startPercent, endPercent);

            this.editor.Position.Translate(ExampleDocumentSizes.ArrowStart.X - 18, ExampleDocumentSizes.ArrowStart.Y + ExampleDocumentSizes.ArrowLength + 5);
            this.editor.TextProperties.FontSize = 18;
            this.editor.GraphicProperties.FillColor = new RgbColor(41, 156, 206);
            this.editor.TextProperties.TrySetFont(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Bold);
            this.editor.DrawText("PDF");
            this.DrawArrow();
        }

        private void DrawArrow()
        {
            this.editor.Position.Translate(ExampleDocumentSizes.ArrowStart.X, ExampleDocumentSizes.ArrowStart.Y);

            this.editor.GraphicProperties.IsStroked = true;
            this.editor.GraphicProperties.StrokeThickness = 1;
            this.editor.GraphicProperties.StrokeColor = new RgbColor(191, 191, 191);
            this.editor.GraphicProperties.IsFilled = true;
            this.editor.GraphicProperties.FillColor = this.editor.GraphicProperties.StrokeColor;

            PathGeometry arrow = new PathGeometry();
            PathFigure figure = arrow.Figures.AddPathFigure();

            figure.StartPoint = new Point();
            figure.Segments.AddLineSegment(new Point(2, 0));
            figure.Segments.AddLineSegment(new Point(2, 13));
            figure.Segments.AddLineSegment(new Point(7, 7));
            figure.Segments.AddLineSegment(new Point(7, 13));
            figure.Segments.AddLineSegment(new Point(0, ExampleDocumentSizes.ArrowLength));
            figure.Segments.AddLineSegment(new Point(-7, 13));
            figure.Segments.AddLineSegment(new Point(-7, 7));
            figure.Segments.AddLineSegment(new Point(-2, 13));
            figure.Segments.AddLineSegment(new Point(-2, 0));

            figure.IsClosed = true;

            this.editor.DrawPath(arrow);
        }

        private void DrawFunnelBlock(double startPercentHeight, double endPercentHeight)
        {
            Point[] contourPoints = ExampleDocumentSizes.GetSubFunnelBlockContourPoints(startPercentHeight, endPercentHeight);
            this.editor.GraphicProperties.IsStroked = false;
            PathGeometry path = new PathGeometry();
            PathFigure figure = path.Figures.AddPathFigure();
            figure.StartPoint = contourPoints[0];
            figure.IsClosed = true;
            string funnelBlockText;
            double textYOffset = 0;

            if (startPercentHeight == 0)
            {
                funnelBlockText = "IMAGES";
                this.editor.GraphicProperties.FillColor = new RgbColor(37, 160, 219);
                ArcSegment arc = figure.Segments.AddArcSegment();
                arc.Point = contourPoints[1];
                arc.IsLargeArc = false;
                arc.SweepDirection = Telerik.Windows.Documents.Fixed.Model.Graphics.SweepDirection.Counterclockwise;
                arc.RadiusX = ExampleDocumentSizes.EllipseRadiuses.Width;
                arc.RadiusY = ExampleDocumentSizes.EllipseRadiuses.Height;
                textYOffset = arc.RadiusY / 2;
                figure.Segments.AddLineSegment(contourPoints[2]);
                figure.Segments.AddLineSegment(contourPoints[3]);
                textYOffset = ExampleDocumentSizes.EllipseRadiuses.Height;
            }
            else if (endPercentHeight == 1)
            {
                funnelBlockText = "FONTS";
                this.editor.GraphicProperties.FillColor = new RgbColor(42, 180, 0);
                figure.Segments.AddLineSegment(contourPoints[1]);
                figure.Segments.AddLineSegment(contourPoints[2]);
                ArcSegment arc = figure.Segments.AddArcSegment();
                arc.Point = contourPoints[3];
                arc.IsLargeArc = false;
                arc.SweepDirection = Telerik.Windows.Documents.Fixed.Model.Graphics.SweepDirection.Clockwise;
                arc.RadiusX = ExampleDocumentSizes.EllipseRadiuses.Width;
                arc.RadiusY = ExampleDocumentSizes.EllipseRadiuses.Height + 100;
            }
            else
            {
                funnelBlockText = "SHAPES";
                this.editor.GraphicProperties.FillColor = new RgbColor(255, 127, 0);
                figure.Segments.AddLineSegment(contourPoints[1]);
                figure.Segments.AddLineSegment(contourPoints[2]);
                figure.Segments.AddLineSegment(contourPoints[3]);
            }

            this.editor.DrawPath(path);

            using (this.editor.SavePosition())
            {
                Size textSize = new Size(contourPoints[1].X - contourPoints[0].X, ExampleDocumentSizes.FunnelBlockHeight - textYOffset);
                this.editor.Position.Translate(contourPoints[0].X, contourPoints[0].Y + textYOffset);
                DrawCenteredText(this.editor, funnelBlockText, textSize);
            }
        }

        private static void DrawCenteredText(FixedContentEditor editor, string text, Size size)
        {
            Block block = new Block();

            block.TextProperties.TrySetFont(new FontFamily("Arial"));
            block.HorizontalAlignment = HorizontalAlignment.Center;
            block.VerticalAlignment = VerticalAlignment.Center;
            block.GraphicProperties.FillColor = RgbColors.White;
            block.TextProperties.FontSize = 16;
            block.InsertText(text);

            editor.DrawBlock(block, size);
        }
    }
}
