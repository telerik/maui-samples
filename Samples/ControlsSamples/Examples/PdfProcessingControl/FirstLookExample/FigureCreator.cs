using System.Collections.Generic;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Graphics;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace QSF.Examples.PdfProcessingControl.FirstLookExample;

public static class FigureCreator
{
    internal static FormSource Create(Size size)
    {
        FormSource formSource = new FormSource
        {
            Size = size
        };

        FixedContentEditor formEditor = new FixedContentEditor(formSource);
        formEditor.Position.Translate(0, 0);

        DrawLeftCircle(formEditor);
        DrawRightCircle(formEditor);
        DrawFontsText(formEditor);
        DrawShapesText(formEditor);
        DrawImagesText(formEditor);
        DrawLinesAndDots(formEditor);

        return formSource;
    }

    private static void DrawLeftCircle(FixedContentEditor formEditor)
    {
        DrawPdfSheet(formEditor);
        ApplyMask(formEditor);
        DrawPdfSheetGreenCorner(formEditor);
        DrawOrangeRectangle(formEditor);
        DrawPdfText(formEditor);
        DrawGreenCircle(formEditor);
    }

    private static void DrawPdfSheet(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();
            PathFigure item = new PathFigure();
            item.StartPoint = new Point(59.21, 270.96);
            item.Segments.AddLineSegment(new Point(59.21, 60.2568));
            item.Segments.AddLineSegment(new Point(176.71, 60.2568));
            item.Segments.AddLineSegment(new Point(194.81, 77.5378));
            item.Segments.AddLineSegment(new Point(212.091, 94.0038));
            item.Segments.AddLineSegment(new Point(212.091, 270.096));
            item.Segments.AddLineSegment(new Point(59.21, 270.096));
            item.IsClosed = true;
            pathFigures.Add(item);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = new RgbColor(245, 245, 245);
            formEditor.DrawPath(pdfList);
        }
    }

    private static void ApplyMask(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();
            PathFigure item = new PathFigure();
            item.StartPoint = new Point(0, 300);
            item.Segments.AddLineSegment(new Point(0, 0));
            item.Segments.AddLineSegment(new Point(609, 0));
            item.Segments.AddLineSegment(new Point(609, 300));
            item.Segments.AddLineSegment(new Point(0, 300));
            item.Segments.AddLineSegment(new Point(135, 269));
            item.Segments.AddBezierSegment(new Point(204.036, 269), new Point(260, 213.036), new Point(260, 144));
            item.Segments.AddBezierSegment(new Point(260, 74.9644), new Point(204.036, 19), new Point(135, 19));
            item.Segments.AddBezierSegment(new Point(65.9644, 19), new Point(10, 74.9644), new Point(10, 144));
            item.Segments.AddBezierSegment(new Point(10, 213.036), new Point(65.9644, 269), new Point(135, 269));
            item.IsClosed = true;
            pathFigures.Add(item);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = RgbColors.White;
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawPdfSheetGreenCorner(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();
            PathFigure item = new PathFigure();
            item.StartPoint = new Point(176.711, 60.2568);
            item.Segments.AddLineSegment(new Point(212.089, 94.0048));
            item.Segments.AddLineSegment(new Point(176.711, 94.0048));
            item.Segments.AddLineSegment(new Point(176.711, 60.2568));
            item.IsClosed = true;
            pathFigures.Add(item);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = new RgbColor(152, 204, 0);
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawOrangeRectangle(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();
            PathFigure item = new PathFigure();
            item.StartPoint = new Point(162.836, 109.265);
            item.Segments.AddLineSegment(new Point(43.3838, 109.265));
            item.Segments.AddLineSegment(new Point(43.3838, 148.63));
            item.Segments.AddLineSegment(new Point(162.836, 148.63));
            item.Segments.AddLineSegment(new Point(162.836, 109.265));
            item.IsClosed = true;
            pathFigures.Add(item);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = new RgbColor(255, 144, 66);
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawPdfText(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();
            PathFigure p = new PathFigure();
            p.StartPoint = new Point(71.9543, 130.837);
            p.Segments.AddBezierSegment(new Point(76.0687, 130.837), new Point(78.7564, 128.714), new Point(78.7564, 124.732));
            p.Segments.AddBezierSegment(new Point(78.7564, 120.717), new Point(76.0687, 118.627), new Point(71.9543, 118.627));
            p.Segments.AddLineSegment(new Point(64.4222, 118.627));
            p.Segments.AddLineSegment(new Point(64.4222, 139));
            p.Segments.AddLineSegment(new Point(67.0435, 139));
            p.Segments.AddLineSegment(new Point(67.0435, 130.837));
            p.Segments.AddLineSegment(new Point(71.9543, 130.837));
            p.IsClosed = true;
            pathFigures.Add(p);

            PathFigure pHole = new PathFigure();
            pHole.StartPoint = new Point(76.1683, 124.732);
            pHole.Segments.AddBezierSegment(new Point(76.1683, 127.022), new Point(74.6751, 128.515), new Point(71.9211, 128.515));
            pHole.Segments.AddLineSegment(new Point(67.0435, 128.515));
            pHole.Segments.AddLineSegment(new Point(67.0435, 120.916));
            pHole.Segments.AddLineSegment(new Point(71.9211, 120.916));
            pHole.Segments.AddBezierSegment(new Point(74.6751, 120.916), new Point(76.1683, 122.41), new Point(76.1683, 124.732));
            pHole.IsClosed = true;
            pathFigures.Add(pHole);

            PathFigure d = new PathFigure();
            d.StartPoint = new Point(84.8653, 136.644);
            d.Segments.AddLineSegment(new Point(84.8653, 120.95));
            d.Segments.AddLineSegment(new Point(88.7475, 120.95));
            d.Segments.AddBezierSegment(new Point(93.3597, 120.95), new Point(96.0805, 124.035), new Point(96.0805, 128.813));
            d.Segments.AddBezierSegment(new Point(96.0805, 133.591), new Point(93.3597, 136.644), new Point(88.7475, 136.644));
            d.Segments.AddLineSegment(new Point(84.8653, 136.644));
            d.IsClosed = true;
            pathFigures.Add(d);

            PathFigure dHole = new PathFigure();
            dHole.StartPoint = new Point(82.244, 139);
            dHole.Segments.AddLineSegment(new Point(88.7807, 139));
            dHole.Segments.AddBezierSegment(new Point(94.7533, 139), new Point(98.6686, 135.018), new Point(98.6686, 128.813));
            dHole.Segments.AddBezierSegment(new Point(98.6686, 122.609), new Point(94.7533, 118.627), new Point(88.7807, 118.627));
            dHole.Segments.AddLineSegment(new Point(82.244, 118.627));
            dHole.Segments.AddLineSegment(new Point(82.244, 139));
            dHole.IsClosed = true;
            pathFigures.Add(dHole);

            PathFigure f = new PathFigure();
            f.StartPoint = new Point(104.955, 139);
            f.Segments.AddLineSegment(new Point(104.955, 130.141));
            f.Segments.AddLineSegment(new Point(114.18, 130.141));
            f.Segments.AddLineSegment(new Point(114.18, 127.818));
            f.Segments.AddLineSegment(new Point(104.955, 127.818));
            f.Segments.AddLineSegment(new Point(104.955, 120.95));
            f.Segments.AddLineSegment(new Point(114.777, 120.95));
            f.Segments.AddLineSegment(new Point(114.777, 118.627));
            f.Segments.AddLineSegment(new Point(102.334, 118.627));
            f.Segments.AddLineSegment(new Point(102.334, 139));
            f.Segments.AddLineSegment(new Point(104.955, 139));
            f.IsClosed = true;
            pathFigures.Add(f);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = new RgbColor(245, 245, 245);
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawGreenCircle(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();
            PathFigure item = new PathFigure();
            item.StartPoint = new Point(259.35, 144);
            item.Segments.AddBezierSegment(new Point(259.35, 212.677), new Point(203.677, 268.35), new Point(135, 268.35));
            item.Segments.AddBezierSegment(new Point(66.3234, 268.35), new Point(10.65, 212.677), new Point(10.65, 144));
            item.Segments.AddBezierSegment(new Point(10.65, 75.3234), new Point(66.3234, 19.65), new Point(135, 19.65));
            item.Segments.AddBezierSegment(new Point(203.677, 19.65), new Point(259.35, 75.3234), new Point(259.35, 144));
            item.IsClosed = true;
            pathFigures.Add(item);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.StrokeColor = new RgbColor(40, 165, 61);
            formEditor.GraphicProperties.StrokeThickness = 1.6;
            formEditor.GraphicProperties.IsStroked = true;
            formEditor.GraphicProperties.IsFilled = false;
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawRightCircle(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            formEditor.GraphicProperties.StrokeColor = new RgbColor(255, 169, 24);
            formEditor.GraphicProperties.StrokeThickness = 1.6;
            formEditor.GraphicProperties.IsStroked = true;
            formEditor.GraphicProperties.FillColor = RgbColors.White;

            using (formEditor.SaveGraphicProperties())
            {
                PathGeometry pdfList = new PathGeometry();
                List<PathFigure> pathFigures = new List<PathFigure>();
                PathFigure orangeCircle = new PathFigure();
                orangeCircle.StartPoint = new Point(598.35, 144);
                orangeCircle.Segments.AddBezierSegment(new Point(598.35, 212.677), new Point(542.677, 268.35), new Point(474, 268.35));
                orangeCircle.Segments.AddBezierSegment(new Point(405.323, 268.35), new Point(349.65, 212.677), new Point(349.65, 144));
                orangeCircle.Segments.AddBezierSegment(new Point(349.65, 75.3234), new Point(405.323, 19.65), new Point(474, 19.65));
                orangeCircle.Segments.AddBezierSegment(new Point(542.677, 19.65), new Point(598.35, 75.3234), new Point(598.35, 144));
                orangeCircle.IsClosed = true;
                pathFigures.Add(orangeCircle);

                pdfList.Figures.AddRange(pathFigures);
                formEditor.DrawPath(pdfList);
            }

            using (formEditor.SaveGraphicProperties())
            {
                PathGeometry pdfList = new PathGeometry();
                List<PathFigure> pathFigures = new List<PathFigure>();

                PathFigure bottomFigure = new PathFigure();
                bottomFigure.StartPoint = new Point(528.671, 163.063);
                bottomFigure.Segments.AddLineSegment(new Point(555.435, 180.031));
                bottomFigure.Segments.AddLineSegment(new Point(473.718, 231.827));
                bottomFigure.Segments.AddLineSegment(new Point(392, 180.031));
                bottomFigure.Segments.AddLineSegment(new Point(418.765, 163.063));
                pathFigures.Add(bottomFigure);

                PathFigure middleFigure = new PathFigure();
                middleFigure.StartPoint = new Point(528.671, 129.127);
                middleFigure.Segments.AddLineSegment(new Point(555.436, 146.095));
                middleFigure.Segments.AddLineSegment(new Point(473.718, 197.891));
                middleFigure.Segments.AddLineSegment(new Point(392, 146.095));
                middleFigure.Segments.AddLineSegment(new Point(418.765, 129.127));
                pathFigures.Add(middleFigure);

                PathFigure line1 = new PathFigure();
                line1.StartPoint = new Point(401.058, 151.412);
                line1.Segments.AddLineSegment(new Point(483.579, 100.105));
                pathFigures.Add(line1);
                PathFigure line2 = new PathFigure();
                line2.StartPoint = new Point(411.814, 158.146);
                line2.Segments.AddLineSegment(new Point(493.967, 107.068));
                pathFigures.Add(line2);
                PathFigure line3 = new PathFigure();
                line3.StartPoint = new Point(422.574, 164.887);
                line3.Segments.AddLineSegment(new Point(504.363, 114.033));
                pathFigures.Add(line3);
                PathFigure line4 = new PathFigure();
                line4.StartPoint = new Point(433.325, 171.617);
                line4.Segments.AddLineSegment(new Point(514.746, 120.992));
                pathFigures.Add(line4);
                PathFigure line5 = new PathFigure();
                line5.StartPoint = new Point(444.085, 178.353);
                line5.Segments.AddLineSegment(new Point(525.138, 127.958));
                pathFigures.Add(line5);
                PathFigure line6 = new PathFigure();
                line6.StartPoint = new Point(454.841, 185.087);
                line6.Segments.AddLineSegment(new Point(536.5, 134));
                pathFigures.Add(line6);
                PathFigure line7 = new PathFigure();
                line7.StartPoint = new Point(465.601, 191.826);
                line7.Segments.AddLineSegment(new Point(547, 141));
                pathFigures.Add(line7);

                pdfList.Figures.AddRange(pathFigures);
                formEditor.GraphicProperties.MiterLimit = 10;
                formEditor.DrawPath(pdfList);
            }

            using (formEditor.SaveGraphicProperties())
            {
                PathGeometry pdfList = new PathGeometry();
                List<PathFigure> pathFigures = new List<PathFigure>();
                PathFigure topFigure = new PathFigure();
                topFigure.StartPoint = new Point(392.031, 112.16);
                topFigure.Segments.AddLineSegment(new Point(473.748, 60.3638));
                topFigure.Segments.AddLineSegment(new Point(555.466, 112.16));
                topFigure.Segments.AddLineSegment(new Point(526.33, 130.623));
                topFigure.Segments.AddLineSegment(new Point(473.73, 163.97));
                topFigure.Segments.AddLineSegment(new Point(392.031, 112.16));
                topFigure.IsClosed = true;
                pathFigures.Add(topFigure);

                pdfList.Figures.AddRange(pathFigures);
                formEditor.GraphicProperties.MiterLimit = 10;
                formEditor.GraphicProperties.FillColor = new RgbColor(255, 169, 24);
                formEditor.DrawPath(pdfList);
            }
        }
    }

    private static void DrawFontsText(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();

            PathFigure f = new PathFigure();
            f.StartPoint = new Point(280.125, 185);
            f.Segments.AddLineSegment(new Point(280.125, 180.518));
            f.Segments.AddLineSegment(new Point(284.967, 180.518));
            f.Segments.AddLineSegment(new Point(284.967, 178.88));
            f.Segments.AddLineSegment(new Point(280.125, 178.88));
            f.Segments.AddLineSegment(new Point(280.125, 175.604));
            f.Segments.AddLineSegment(new Point(285.309, 175.604));
            f.Segments.AddLineSegment(new Point(285.309, 173.948));
            f.Segments.AddLineSegment(new Point(278.253, 173.948));
            f.Segments.AddLineSegment(new Point(278.253, 185));
            f.Segments.AddLineSegment(new Point(280.125, 185));
            f.IsClosed = true;
            pathFigures.Add(f);

            PathFigure o = new PathFigure();
            o.StartPoint = new Point(296.777, 179.474);
            o.Segments.AddBezierSegment(new Point(296.777, 176.162), new Point(294.653, 173.768), new Point(291.665, 173.768));
            o.Segments.AddBezierSegment(new Point(288.677, 173.768), new Point(286.571, 176.162), new Point(286.571, 179.474));
            o.Segments.AddBezierSegment(new Point(286.571, 182.768), new Point(288.677, 185.18), new Point(291.665, 185.18));
            o.Segments.AddBezierSegment(new Point(294.653, 185.18), new Point(296.777, 182.768), new Point(296.777, 179.474));
            o.IsClosed = true;
            pathFigures.Add(o);

            PathFigure oHole = new PathFigure();
            oHole.StartPoint = new Point(294.887, 179.474);
            oHole.Segments.AddBezierSegment(new Point(294.887, 181.814), new Point(293.609, 183.506), new Point(291.665, 183.506));
            oHole.Segments.AddBezierSegment(new Point(289.721, 183.506), new Point(288.461, 181.814), new Point(288.461, 179.474));
            oHole.Segments.AddBezierSegment(new Point(288.461, 177.134), new Point(289.721, 175.442), new Point(291.665, 175.442));
            oHole.Segments.AddBezierSegment(new Point(293.609, 175.442), new Point(294.887, 177.134), new Point(294.887, 179.474));
            oHole.IsClosed = true;
            pathFigures.Add(oHole);

            PathFigure n = new PathFigure();
            n.StartPoint = new Point(307.518, 173.948);
            n.Segments.AddLineSegment(new Point(305.718, 173.948));
            n.Segments.AddLineSegment(new Point(305.718, 181.616));
            n.Segments.AddLineSegment(new Point(300.39, 173.948));
            n.Segments.AddLineSegment(new Point(298.626, 173.948));
            n.Segments.AddLineSegment(new Point(298.626, 185));
            n.Segments.AddLineSegment(new Point(300.426, 185));
            n.Segments.AddLineSegment(new Point(300.426, 176.954));
            n.Segments.AddLineSegment(new Point(306.078, 185));
            n.Segments.AddLineSegment(new Point(307.518, 185));
            n.Segments.AddLineSegment(new Point(307.518, 173.948));
            n.IsClosed = true;
            pathFigures.Add(n);

            PathFigure t = new PathFigure();
            t.StartPoint = new Point(309.28, 175.604);
            t.Segments.AddLineSegment(new Point(312.952, 175.604));
            t.Segments.AddLineSegment(new Point(312.952, 185));
            t.Segments.AddLineSegment(new Point(314.824, 185));
            t.Segments.AddLineSegment(new Point(314.824, 175.604));
            t.Segments.AddLineSegment(new Point(318.496, 175.604));
            t.Segments.AddLineSegment(new Point(318.496, 173.948));
            t.Segments.AddLineSegment(new Point(309.28, 173.948));
            t.Segments.AddLineSegment(new Point(309.28, 175.604));
            t.IsClosed = true;
            pathFigures.Add(t);

            PathFigure s = new PathFigure();
            s.StartPoint = new Point(319.815, 184.208);
            s.Segments.AddBezierSegment(new Point(320.589, 184.748), new Point(321.849, 185.18), new Point(323.325, 185.18));
            s.Segments.AddBezierSegment(new Point(325.575, 185.18), new Point(327.285, 184.064), new Point(327.285, 182.048));
            s.Segments.AddBezierSegment(new Point(327.285, 180.446), new Point(326.493, 179.474), new Point(324.765, 178.844));
            s.Segments.AddLineSegment(new Point(322.983, 178.196));
            s.Segments.AddBezierSegment(new Point(322.065, 177.872), new Point(321.543, 177.548), new Point(321.543, 176.738));
            s.Segments.AddBezierSegment(new Point(321.543, 175.838), new Point(322.371, 175.406), new Point(323.487, 175.406));
            s.Segments.AddBezierSegment(new Point(324.783, 175.406), new Point(325.791, 175.892), new Point(326.709, 176.594));
            s.Segments.AddLineSegment(new Point(326.709, 174.668));
            s.Segments.AddBezierSegment(new Point(325.827, 174.092), new Point(324.693, 173.768), new Point(323.451, 173.768));
            s.Segments.AddBezierSegment(new Point(321.417, 173.768), new Point(319.725, 174.83), new Point(319.725, 176.774));
            s.Segments.AddBezierSegment(new Point(319.725, 178.592), new Point(320.841, 179.384), new Point(322.317, 179.924));
            s.Segments.AddLineSegment(new Point(323.919, 180.5));
            s.Segments.AddBezierSegment(new Point(324.909, 180.878), new Point(325.467, 181.22), new Point(325.467, 182.084));
            s.Segments.AddBezierSegment(new Point(325.467, 183.074), new Point(324.585, 183.524), new Point(323.325, 183.524));
            s.Segments.AddBezierSegment(new Point(322.029, 183.524), new Point(320.823, 183.056), new Point(319.815, 182.246));
            s.Segments.AddLineSegment(new Point(319.815, 184.208));
            s.IsClosed = true;
            pathFigures.Add(s);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = new RgbColor(40, 165, 61);
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawShapesText(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();

            PathFigure s = new PathFigure();
            s.StartPoint = new Point(273.926, 136.208);
            s.Segments.AddBezierSegment(new Point(274.7, 136.748), new Point(275.96, 137.18), new Point(277.436, 137.18));
            s.Segments.AddBezierSegment(new Point(279.686, 137.18), new Point(281.396, 136.064), new Point(281.396, 134.048));
            s.Segments.AddBezierSegment(new Point(281.396, 132.446), new Point(280.604, 131.474), new Point(278.876, 130.844));
            s.Segments.AddLineSegment(new Point(277.094, 130.196));
            s.Segments.AddBezierSegment(new Point(276.176, 129.872), new Point(275.654, 129.548), new Point(275.654, 128.738));
            s.Segments.AddBezierSegment(new Point(275.654, 127.838), new Point(276.482, 127.406), new Point(277.598, 127.406));
            s.Segments.AddBezierSegment(new Point(278.894, 127.406), new Point(279.902, 127.892), new Point(280.82, 128.594));
            s.Segments.AddLineSegment(new Point(280.82, 126.668));
            s.Segments.AddBezierSegment(new Point(279.938, 126.092), new Point(278.804, 125.768), new Point(277.562, 125.768));
            s.Segments.AddBezierSegment(new Point(275.528, 125.768), new Point(273.836, 126.83), new Point(273.836, 128.774));
            s.Segments.AddBezierSegment(new Point(273.836, 130.592), new Point(274.952, 131.384), new Point(276.428, 131.924));
            s.Segments.AddLineSegment(new Point(278.03, 132.5));
            s.Segments.AddBezierSegment(new Point(279.02, 132.878), new Point(279.578, 133.22), new Point(279.578, 134.084));
            s.Segments.AddBezierSegment(new Point(279.578, 135.074), new Point(278.696, 135.524), new Point(277.436, 135.524));
            s.Segments.AddBezierSegment(new Point(276.14, 135.524), new Point(274.934, 135.056), new Point(273.926, 134.246));
            s.Segments.AddLineSegment(new Point(273.926, 136.208));
            s.IsClosed = true;
            pathFigures.Add(s);

            PathFigure h = new PathFigure();
            h.StartPoint = new Point(290.74, 125.948);
            h.Segments.AddLineSegment(new Point(290.74, 130.502));
            h.Segments.AddLineSegment(new Point(285.178, 130.502));
            h.Segments.AddLineSegment(new Point(285.178, 125.948));
            h.Segments.AddLineSegment(new Point(283.306, 125.948));
            h.Segments.AddLineSegment(new Point(283.306, 137));
            h.Segments.AddLineSegment(new Point(285.178, 137));
            h.Segments.AddLineSegment(new Point(285.178, 132.158));
            h.Segments.AddLineSegment(new Point(290.74, 132.158));
            h.Segments.AddLineSegment(new Point(290.74, 137));
            h.Segments.AddLineSegment(new Point(292.612, 137));
            h.Segments.AddLineSegment(new Point(292.612, 125.948));
            h.Segments.AddLineSegment(new Point(290.74, 125.948));
            h.IsClosed = true;
            pathFigures.Add(h);

            PathFigure a = new PathFigure();
            a.StartPoint = new Point(298.45, 125.948);
            a.Segments.AddLineSegment(new Point(294.202, 137));
            a.Segments.AddLineSegment(new Point(296.092, 137));
            a.Segments.AddLineSegment(new Point(297.226, 133.94));
            a.Segments.AddLineSegment(new Point(301.456, 133.94));
            a.Segments.AddLineSegment(new Point(302.59, 137));
            a.Segments.AddLineSegment(new Point(304.498, 137));
            a.Segments.AddLineSegment(new Point(300.25, 125.948));
            a.Segments.AddLineSegment(new Point(298.45, 125.948));
            a.IsClosed = true;
            pathFigures.Add(a);

            PathFigure aHole = new PathFigure();
            aHole.StartPoint = new Point(299.332, 128.234);
            aHole.Segments.AddLineSegment(new Point(300.844, 132.32));
            aHole.Segments.AddLineSegment(new Point(297.82, 132.32));
            aHole.Segments.AddLineSegment(new Point(299.332, 128.234));
            aHole.IsClosed = true;
            pathFigures.Add(aHole);

            PathFigure p = new PathFigure();
            p.StartPoint = new Point(310.353, 132.878);
            p.Segments.AddBezierSegment(new Point(312.639, 132.878), new Point(314.133, 131.672), new Point(314.133, 129.422));
            p.Segments.AddBezierSegment(new Point(314.133, 127.136), new Point(312.639, 125.948), new Point(310.353, 125.948));
            p.Segments.AddLineSegment(new Point(306.087, 125.948));
            p.Segments.AddLineSegment(new Point(306.087, 137));
            p.Segments.AddLineSegment(new Point(307.959, 137));
            p.Segments.AddLineSegment(new Point(307.959, 132.878));
            p.Segments.AddLineSegment(new Point(310.353, 132.878));
            p.IsClosed = true;
            pathFigures.Add(p);

            PathFigure pHole = new PathFigure();
            pHole.StartPoint = new Point(312.279, 129.422);
            pHole.Segments.AddBezierSegment(new Point(312.279, 130.484), new Point(311.613, 131.258), new Point(310.317, 131.258));
            pHole.Segments.AddLineSegment(new Point(307.959, 131.258));
            pHole.Segments.AddLineSegment(new Point(307.959, 127.568));
            pHole.Segments.AddLineSegment(new Point(310.317, 127.568));
            pHole.Segments.AddBezierSegment(new Point(311.613, 127.568), new Point(312.279, 128.324), new Point(312.279, 129.422));
            pHole.IsClosed = true;
            pathFigures.Add(pHole);

            PathFigure e = new PathFigure();
            e.StartPoint = new Point(315.896, 125.948);
            e.Segments.AddLineSegment(new Point(315.896, 137));
            e.Segments.AddLineSegment(new Point(323.114, 137));
            e.Segments.AddLineSegment(new Point(323.114, 135.326));
            e.Segments.AddLineSegment(new Point(317.732, 135.326));
            e.Segments.AddLineSegment(new Point(317.732, 132.158));
            e.Segments.AddLineSegment(new Point(322.61, 132.158));
            e.Segments.AddLineSegment(new Point(322.61, 130.52));
            e.Segments.AddLineSegment(new Point(317.732, 130.52));
            e.Segments.AddLineSegment(new Point(317.732, 127.604));
            e.Segments.AddLineSegment(new Point(323.114, 127.604));
            e.Segments.AddLineSegment(new Point(323.114, 125.948));
            e.Segments.AddLineSegment(new Point(315.896, 125.948));
            e.IsClosed = true;
            pathFigures.Add(e);

            PathFigure s2 = new PathFigure();
            s2.StartPoint = new Point(324.692, 136.208);
            s2.Segments.AddBezierSegment(new Point(325.466, 136.748), new Point(326.726, 137.18), new Point(328.202, 137.18));
            s2.Segments.AddBezierSegment(new Point(330.452, 137.18), new Point(332.162, 136.064), new Point(332.162, 134.048));
            s2.Segments.AddBezierSegment(new Point(332.162, 132.446), new Point(331.37, 131.474), new Point(329.642, 130.844));
            s2.Segments.AddLineSegment(new Point(327.86, 130.196));
            s2.Segments.AddBezierSegment(new Point(326.942, 129.872), new Point(326.42, 129.548), new Point(326.42, 128.738));
            s2.Segments.AddBezierSegment(new Point(326.42, 127.838), new Point(327.248, 127.406), new Point(328.364, 127.406));
            s2.Segments.AddBezierSegment(new Point(329.66, 127.406), new Point(330.668, 127.892), new Point(331.586, 128.594));
            s2.Segments.AddLineSegment(new Point(331.586, 126.668));
            s2.Segments.AddBezierSegment(new Point(330.704, 126.092), new Point(329.57, 125.768), new Point(328.328, 125.768));
            s2.Segments.AddBezierSegment(new Point(326.294, 125.768), new Point(324.602, 126.83), new Point(324.602, 128.774));
            s2.Segments.AddBezierSegment(new Point(324.602, 130.592), new Point(325.718, 131.384), new Point(327.194, 131.924));
            s2.Segments.AddLineSegment(new Point(328.796, 132.5));
            s2.Segments.AddBezierSegment(new Point(329.786, 132.878), new Point(330.344, 133.22), new Point(330.344, 134.084));
            s2.Segments.AddBezierSegment(new Point(330.344, 135.074), new Point(329.462, 135.524), new Point(328.202, 135.524));
            s2.Segments.AddBezierSegment(new Point(326.906, 135.524), new Point(325.7, 135.056), new Point(324.692, 134.246));
            s2.Segments.AddLineSegment(new Point(324.692, 136.208));
            s2.IsClosed = true;
            pathFigures.Add(s2);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = new RgbColor(40, 165, 61);
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawImagesText(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            PathGeometry pdfList = new PathGeometry();
            List<PathFigure> pathFigures = new List<PathFigure>();

            PathFigure i = new PathFigure();
            i.StartPoint = new Point(277.172, 77.948);
            i.Segments.AddLineSegment(new Point(275.3, 77.948));
            i.Segments.AddLineSegment(new Point(275.3, 89));
            i.Segments.AddLineSegment(new Point(277.172, 89));
            i.Segments.AddLineSegment(new Point(277.172, 77.948));
            i.IsClosed = true;
            pathFigures.Add(i);

            PathFigure m = new PathFigure();
            m.StartPoint = new Point(284.97, 86.624);
            m.Segments.AddLineSegment(new Point(288.354, 81.26));
            m.Segments.AddLineSegment(new Point(288.354, 89));
            m.Segments.AddLineSegment(new Point(290.19, 89));
            m.Segments.AddLineSegment(new Point(290.19, 77.948));
            m.Segments.AddLineSegment(new Point(288.516, 77.948));
            m.Segments.AddLineSegment(new Point(284.952, 83.708));
            m.Segments.AddLineSegment(new Point(281.37, 77.948));
            m.Segments.AddLineSegment(new Point(279.624, 77.948));
            m.Segments.AddLineSegment(new Point(279.624, 89));
            m.Segments.AddLineSegment(new Point(281.46, 89));
            m.Segments.AddLineSegment(new Point(281.46, 81.26));
            m.Segments.AddLineSegment(new Point(284.898, 86.624));
            m.Segments.AddLineSegment(new Point(284.97, 86.624));
            m.IsClosed = true;
            pathFigures.Add(m);

            PathFigure a = new PathFigure();
            a.StartPoint = new Point(296.016, 77.948);
            a.Segments.AddLineSegment(new Point(291.768, 89));
            a.Segments.AddLineSegment(new Point(293.658, 89));
            a.Segments.AddLineSegment(new Point(294.792, 85.94));
            a.Segments.AddLineSegment(new Point(299.022, 85.94));
            a.Segments.AddLineSegment(new Point(300.156, 89));
            a.Segments.AddLineSegment(new Point(302.064, 89));
            a.Segments.AddLineSegment(new Point(297.816, 77.948));
            a.Segments.AddLineSegment(new Point(296.016, 77.948));
            a.IsClosed = true;
            pathFigures.Add(a);

            PathFigure aHole = new PathFigure();
            aHole.StartPoint = new Point(296.898, 80.234);
            aHole.Segments.AddLineSegment(new Point(298.41, 84.32));
            aHole.Segments.AddLineSegment(new Point(295.386, 84.32));
            aHole.Segments.AddLineSegment(new Point(296.898, 80.234));
            aHole.IsClosed = true;
            pathFigures.Add(aHole);

            PathFigure g = new PathFigure();
            g.StartPoint = new Point(307.949, 87.524);
            g.Segments.AddBezierSegment(new Point(305.771, 87.524), new Point(304.493, 85.832), new Point(304.493, 83.474));
            g.Segments.AddBezierSegment(new Point(304.493, 81.098), new Point(305.969, 79.442), new Point(308.129, 79.442));
            g.Segments.AddBezierSegment(new Point(309.335, 79.442), new Point(310.325, 79.838), new Point(311.261, 80.666));
            g.Segments.AddLineSegment(new Point(311.261, 78.722));
            g.Segments.AddBezierSegment(new Point(310.487, 78.128), new Point(309.407, 77.768), new Point(308.129, 77.768));
            g.Segments.AddBezierSegment(new Point(304.961, 77.768), new Point(302.603, 80.09), new Point(302.603, 83.474));
            g.Segments.AddBezierSegment(new Point(302.603, 86.84), new Point(304.637, 89.18), new Point(307.913, 89.18));
            g.Segments.AddBezierSegment(new Point(309.317, 89.18), new Point(310.793, 88.694), new Point(311.855, 87.542));
            g.Segments.AddLineSegment(new Point(311.855, 83.114));
            g.Segments.AddLineSegment(new Point(308.039, 83.114));
            g.Segments.AddLineSegment(new Point(308.039, 84.68));
            g.Segments.AddLineSegment(new Point(310.127, 84.68));
            g.Segments.AddLineSegment(new Point(310.127, 86.804));
            g.Segments.AddBezierSegment(new Point(309.533, 87.29), new Point(308.795, 87.524), new Point(307.949, 87.524));
            g.IsClosed = true;
            pathFigures.Add(g);

            PathFigure e = new PathFigure();
            e.StartPoint = new Point(313.972, 77.948);
            e.Segments.AddLineSegment(new Point(313.972, 89));
            e.Segments.AddLineSegment(new Point(321.19, 89));
            e.Segments.AddLineSegment(new Point(321.19, 87.326));
            e.Segments.AddLineSegment(new Point(315.808, 87.326));
            e.Segments.AddLineSegment(new Point(315.808, 84.158));
            e.Segments.AddLineSegment(new Point(320.686, 84.158));
            e.Segments.AddLineSegment(new Point(320.686, 82.52));
            e.Segments.AddLineSegment(new Point(315.808, 82.52));
            e.Segments.AddLineSegment(new Point(315.808, 79.604));
            e.Segments.AddLineSegment(new Point(321.19, 79.604));
            e.Segments.AddLineSegment(new Point(321.19, 77.948));
            e.Segments.AddLineSegment(new Point(313.972, 77.948));
            e.IsClosed = true;
            pathFigures.Add(e);

            PathFigure s = new PathFigure();
            s.StartPoint = new Point(322.768, 88.208);
            s.Segments.AddBezierSegment(new Point(323.542, 88.748), new Point(324.802, 89.18), new Point(326.278, 89.18));
            s.Segments.AddBezierSegment(new Point(328.528, 89.18), new Point(330.238, 88.06), new Point(330.238, 86.048));
            s.Segments.AddBezierSegment(new Point(330.238, 84.446), new Point(329.446, 83.474), new Point(327.718, 82.844));
            s.Segments.AddLineSegment(new Point(325.936, 82.196));
            s.Segments.AddBezierSegment(new Point(325.018, 81.872), new Point(324.496, 81.548), new Point(324.496, 80.738));
            s.Segments.AddBezierSegment(new Point(324.496, 79.838), new Point(325.324, 79.406), new Point(326.44, 79.406));
            s.Segments.AddBezierSegment(new Point(327.736, 79.406), new Point(328.744, 79.892), new Point(329.662, 80.594));
            s.Segments.AddLineSegment(new Point(329.662, 78.668));
            s.Segments.AddBezierSegment(new Point(328.78, 78.092), new Point(327.646, 77.768), new Point(326.404, 77.768));
            s.Segments.AddBezierSegment(new Point(324.37, 77.768), new Point(322.678, 78.83), new Point(322.678, 80.774));
            s.Segments.AddBezierSegment(new Point(322.678, 82.592), new Point(323.794, 83.384), new Point(325.27, 83.924));
            s.Segments.AddLineSegment(new Point(326.872, 84.5));
            s.Segments.AddBezierSegment(new Point(327.862, 84.878), new Point(328.42, 85.22), new Point(328.42, 86.084));
            s.Segments.AddBezierSegment(new Point(328.42, 87.074), new Point(327.538, 87.524), new Point(326.278, 87.524));
            s.Segments.AddBezierSegment(new Point(324.982, 87.524), new Point(323.776, 87.056), new Point(322.768, 86.246));
            s.Segments.AddLineSegment(new Point(322.768, 88.208));
            s.IsClosed = true;
            pathFigures.Add(s);

            pdfList.Figures.AddRange(pathFigures);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.GraphicProperties.FillColor = new RgbColor(40, 165, 61);
            formEditor.DrawPath(pdfList);
        }
    }

    private static void DrawLinesAndDots(FixedContentEditor formEditor)
    {
        using (formEditor.SaveGraphicProperties())
        {
            formEditor.GraphicProperties.FillColor = new RgbColor(152, 204, 0);
            formEditor.GraphicProperties.StrokeColor = new RgbColor(152, 204, 0);
            formEditor.GraphicProperties.IsStroked = false;
            formEditor.DrawCircle(new Point(249, 146), 5);
            formEditor.DrawCircle(new Point(359, 146), 5);
            formEditor.DrawCircle(new Point(239, 100), 5);
            formEditor.DrawCircle(new Point(367, 100), 5);
            formEditor.DrawCircle(new Point(239, 192), 5);
            formEditor.DrawCircle(new Point(367, 192), 5);

            formEditor.GraphicProperties.IsStroked = true;
            formEditor.DrawLine(new Point(254, 146.5), new Point(359, 146.5));
            formEditor.DrawLine(new Point(242, 100.5), new Point(367, 100.5));
            formEditor.DrawLine(new Point(242, 192), new Point(367, 192.5));
        }
    }
}
