using Telerik.Documents.Primitives;

namespace QSF.Examples.PdfProcessingControl.FirstLookExample
{
    public class ExampleDocumentSizes
    {

        public static readonly double DefaultLeftIndent = 50;
        public static readonly double DefaultLineHeight = 16;

        public static readonly Point FunnelCenter = new Point(300, 280);

        public static readonly double FunnelTopWidth = 230;
        public static readonly double FunnelBottomWidth = 70;
        public static readonly double FunnelHeight = 145;
        public static readonly double FunnelBlockHeight = 45;

        public static readonly Point EllipseCenter = new Point(FunnelCenter.X, 350 - 78);
        public static readonly Size EllipseRadiuses = new Size(119, 13);

        public static readonly Point ArrowStart = new Point(FunnelCenter.X, FunnelCenter.Y + FunnelHeight + 15);
        public static readonly double ArrowLength = 21;
        public static readonly double ArrowLineWidth = 4;

        public static Point[] GetSubFunnelBlockContourPoints(double percentStart, double percentEnd)
        {
            double topWidth = GetFunnelWidth(percentStart);
            double bottomWidth = GetFunnelWidth(percentEnd);
            double topHeight = percentStart * FunnelHeight;
            double bottomHeight = percentEnd * FunnelHeight;

            Point[] contour =
            {
                new Point(FunnelCenter.X - topWidth / 2, FunnelCenter.Y + topHeight),
                new Point(FunnelCenter.X + topWidth / 2, FunnelCenter.Y + topHeight),
                new Point(FunnelCenter.X + bottomWidth / 2, FunnelCenter.Y + bottomHeight),
                new Point(FunnelCenter.X -bottomWidth / 2, FunnelCenter.Y + bottomHeight),
            };

            return contour;
        }

        public static double GetPercentHeight(double funnelHeight)
        {
            return funnelHeight / ExampleDocumentSizes.FunnelHeight;
        }

        private static double GetFunnelWidth(double percentHeight)
        {
            return (1 - percentHeight) * FunnelTopWidth + percentHeight * FunnelBottomWidth;
        }
    }
}
