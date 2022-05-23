using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QSF.Layouts;

public class QGridLayoutManager : ILayoutManager
{
    private static readonly IReadOnlyList<IGridRowDefinition> singleRowDefinition = new List<IGridRowDefinition> { new QGridRowDefinition() };
    private static readonly IReadOnlyList<IGridColumnDefinition> singleColDefinition = new List<IGridColumnDefinition> { new QGridColumnDefinition() };

    private readonly IGridLayout layout;

    public QGridLayoutManager(IGridLayout layout)
    {
        this.layout = layout;
    }

    public Size Measure(double widthConstraint, double heightConstraint)
    {
        GridInfo gridInfo = this.GetGridInfo();
        this.MeasureChildren(gridInfo);
        return gridInfo.desiredSize;
    }

    public Size ArrangeChildren(Rect bounds)
    {
        GridInfo gridInfo = this.GetGridInfo();
        this.MeasureChildren(gridInfo);
        Size arrangeSize = this.ArrangeChildren(gridInfo, bounds);
        return arrangeSize;
    }

    private IReadOnlyList<IGridRowDefinition> GetRowDefinitions()
    {
        IReadOnlyList<IGridRowDefinition> definitions = this.layout.RowDefinitions;
        return definitions.Count > 0 ? definitions : singleRowDefinition;
    }

    private IReadOnlyList<IGridColumnDefinition> GetColDefinitions()
    {
        IReadOnlyList<IGridColumnDefinition> definitions = this.layout.ColumnDefinitions;
        return definitions.Count > 0 ? definitions : singleColDefinition;
    }

    private GridInfo GetGridInfo()
    {
        IReadOnlyList<IGridRowDefinition> rowDefinitions = this.GetRowDefinitions();
        IReadOnlyList<IGridColumnDefinition> colDefinitions = this.GetColDefinitions();
        int rows = rowDefinitions.Count;
        int cols = colDefinitions.Count;
        CellInfo[,] cells = new CellInfo[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                CellInfo cellInfo = new CellInfo(r, c, rowDefinitions[r].Height, colDefinitions[c].Width);
                cells[r, c] = cellInfo;
            }
        }

        foreach (IView child in this.layout)
        {
            int row = this.layout.GetRow(child);
            int col = this.layout.GetColumn(child);

            if (0 <= row && row < rows &&
                0 <= col && col < cols)
            {
                cells[row, col].views.Add(child);
            }
        }

        return new GridInfo(rowDefinitions, colDefinitions, cells);
    }

    private void MeasureChildren(GridInfo gridInfo)
    {
        this.MeasureCells(gridInfo);
        this.CalculateDesiredRowHeights(gridInfo);
        this.CalculateDesiredColumnWidths(gridInfo);
        gridInfo.desiredSize = new Size(gridInfo.colDesiredSize.Sum(), gridInfo.rowDesiredSize.Sum());
    }

    private void MeasureCells(GridInfo gridInfo)
    {
        for (int r = 0; r < gridInfo.rows; r++)
        {
            for (int c = 0; c < gridInfo.cols; c++)
            {
                CellInfo cellInfo = gridInfo.cells[r, c];
                cellInfo.cellDesiredSize = this.MeasureCell(cellInfo);
            }
        }
    }

    private Size MeasureCell(CellInfo cellInfo)
    {
        double w = this.GetMeasureSize(cellInfo.colLength);
        double h = this.GetMeasureSize(cellInfo.rowLength);
        double maxWidth = 0;
        double maxHeight = 0;

        foreach (IView child in cellInfo.views)
        {
            Size size = child.Measure(w, h);

            if (maxWidth < size.Width)
            {
                maxWidth = size.Width;
            }

            if (maxHeight < size.Height)
            {
                maxHeight = size.Height;
            }
        }

        return new Size(maxWidth, maxHeight);
    }

    private double GetMeasureSize(GridLength gridLength)
    {
        return gridLength.IsAbsolute ? gridLength.Value : double.PositiveInfinity;
    }

    private void CalculateDesiredRowHeights(GridInfo gridInfo)
    {
        for (int r = 0; r < gridInfo.rows; r++)
        {
            GridLength gridLength = gridInfo.rowDefinitions[r].Height;

            if (gridLength.IsAbsolute)
            {
                gridInfo.rowDesiredSize[r] = gridLength.Value;
            }
            else
            {
                double maxSize = 0;

                for (int c = 0; c < gridInfo.cols; c++)
                {
                    CellInfo cellInfo = gridInfo.cells[r, c];
                    maxSize = Math.Max(maxSize, cellInfo.cellDesiredSize.Height);
                }

                gridInfo.rowDesiredSize[r] = maxSize;
            }
        }

        double desiredSizeOfOneStar = 0;

        for (int r = 0; r < gridInfo.rows; r++)
        {
            GridLength gridLength = gridInfo.rowDefinitions[r].Height;

            if (gridLength.IsStar && gridLength.Value > 0)
            {
                desiredSizeOfOneStar = Math.Max(desiredSizeOfOneStar, gridInfo.rowDesiredSize[r] / gridLength.Value);
            }
        }

        for (int r = 0; r < gridInfo.rows; r++)
        {
            GridLength gridLength = gridInfo.rowDefinitions[r].Height;

            if (gridLength.IsStar && gridLength.Value > 0)
            {
                gridInfo.rowDesiredSize[r] = gridLength.Value * desiredSizeOfOneStar;
            }
        }
    }

    private void CalculateDesiredColumnWidths(GridInfo gridInfo)
    {
        for (int c = 0; c < gridInfo.cols; c++)
        {
            GridLength gridLength = gridInfo.colDefinitions[c].Width;

            if (gridLength.IsAbsolute)
            {
                gridInfo.colDesiredSize[c] = gridLength.Value;
            }
            else
            {
                double maxSize = 0;

                for (int r = 0; r < gridInfo.rows; r++)
                {
                    CellInfo cellInfo = gridInfo.cells[r, c];
                    maxSize = Math.Max(maxSize, cellInfo.cellDesiredSize.Width);
                }

                gridInfo.colDesiredSize[c] = maxSize;
            }
        }

        double desiredSizeOfOneStar = 0;

        for (int c = 0; c < gridInfo.cols; c++)
        {
            GridLength gridLength = gridInfo.colDefinitions[c].Width;

            if (gridLength.IsStar && gridLength.Value > 0)
            {
                desiredSizeOfOneStar = Math.Max(desiredSizeOfOneStar, gridInfo.colDesiredSize[c] / gridLength.Value);
            }
        }

        for (int c = 0; c < gridInfo.cols; c++)
        {
            GridLength gridLength = gridInfo.colDefinitions[c].Width;

            if (gridLength.IsStar && gridLength.Value > 0)
            {
                gridInfo.colDesiredSize[c] = gridLength.Value * desiredSizeOfOneStar;
            }
        }
    }

    private Size ArrangeChildren(GridInfo gridInfo, Rect bounds)
    {
        this.CalculateRowsStartAndLength(gridInfo, bounds);
        this.CalculateColsStartAndLength(gridInfo, bounds);

        for (int r = 0; r < gridInfo.rows; r++)
        {
            for (int c = 0; c < gridInfo.cols; c++)
            {
                CellInfo cellInfo = gridInfo.cells[r, c];

                foreach (IView child in cellInfo.views)
                {
                    Rect childBounds = new Rect(gridInfo.colArrangeStart[c], gridInfo.rowArrangeStart[r], gridInfo.colArrangeLength[c], gridInfo.rowArrangeLength[r]);
                    child.Arrange(childBounds);
                }
            }
        }

        double right = gridInfo.colArrangeStart[gridInfo.cols - 1] + gridInfo.colArrangeLength[gridInfo.cols - 1];
        double bottom = gridInfo.rowArrangeStart[gridInfo.rows - 1] + gridInfo.rowArrangeLength[gridInfo.rows - 1];

        return new Size(right - bounds.X, bottom - bounds.Y);
    }

    private void CalculateRowsStartAndLength(GridInfo gridInfo, Rect bounds)
    {
        gridInfo.rowArrangeStart = new double[gridInfo.rows];
        gridInfo.rowArrangeLength = new double[gridInfo.rows];

        double autoAndAbsoluteDesiredSize = this.GetAutoAndAbsoluteDesiredHeight(gridInfo);
        double excess = Math.Max(0, bounds.Height - autoAndAbsoluteDesiredSize);
        double stars = gridInfo.rowDefinitions.Where(d => d.Height.IsStar).Sum(d => d.Height.Value);
        if (stars == 0)
        {
            stars = 1;
        }

        double start = bounds.Y;
        for (int i = 0; i < gridInfo.rows; i++)
        {
            gridInfo.rowArrangeStart[i] = start;

            GridLength gridLength = gridInfo.rowDefinitions[i].Height;
            double length = gridLength.IsStar ? (excess * gridLength.Value / stars) : gridInfo.rowDesiredSize[i];
            gridInfo.rowArrangeLength[i] = length;

            start += length;
        }
    }

    private void CalculateColsStartAndLength(GridInfo gridInfo, Rect bounds)
    {
        gridInfo.colArrangeStart = new double[gridInfo.cols];
        gridInfo.colArrangeLength = new double[gridInfo.cols];

        double autoAndAbsoluteDesiredSize = this.GetAutoAndAbsoluteDesiredWidth(gridInfo);
        double excess = Math.Max(0, bounds.Width - autoAndAbsoluteDesiredSize);
        double stars = gridInfo.colDefinitions.Where(d => d.Width.IsStar).Sum(d => d.Width.Value);
        if (stars == 0)
        {
            stars = 1;
        }

        double start = bounds.X;
        for (int i = 0; i < gridInfo.cols; i++)
        {
            gridInfo.colArrangeStart[i] = start;

            GridLength gridLength = gridInfo.colDefinitions[i].Width;
            double length = gridLength.IsStar ? (excess * gridLength.Value / stars) : gridInfo.colDesiredSize[i];
            gridInfo.colArrangeLength[i] = length;

            start += length;
        }
    }

    private double GetAutoAndAbsoluteDesiredWidth(GridInfo gridInfo)
    {
        double size = 0;

        for (int i = 0; i < gridInfo.cols; i++)
        {
            if (!gridInfo.colDefinitions[i].Width.IsStar)
            {
                size += gridInfo.colDesiredSize[i];
            }
        }

        return size;
    }

    private double GetAutoAndAbsoluteDesiredHeight(GridInfo gridInfo)
    {
        double size = 0;

        for (int i = 0; i < gridInfo.rows; i++)
        {
            if (!gridInfo.rowDefinitions[i].Height.IsStar)
            {
                size += gridInfo.rowDesiredSize[i];
            }
        }

        return size;
    }

    class QGridRowDefinition : IGridRowDefinition
    {
        private readonly GridLength gridLength = new GridLength(1, GridUnitType.Star);

        GridLength IGridRowDefinition.Height => this.gridLength;
    }

    class QGridColumnDefinition : IGridColumnDefinition
    {
        private readonly GridLength gridLength = new GridLength(1, GridUnitType.Star);

        GridLength IGridColumnDefinition.Width => this.gridLength;
    }

    class CellInfo
    {
        internal int row;
        internal int col;
        internal GridLength rowLength;
        internal GridLength colLength;
        internal List<IView> views;
        internal Size cellDesiredSize;

        internal CellInfo(int row, int col, GridLength rowLength, GridLength colLength)
        {
            this.row = row;
            this.col = col;
            this.rowLength = rowLength;
            this.colLength = colLength;
            this.views = new List<IView>();
        }
    }

    class GridInfo
    {
        internal int rows;
        internal int cols;
        internal IReadOnlyList<IGridRowDefinition> rowDefinitions;
        internal IReadOnlyList<IGridColumnDefinition> colDefinitions;
        internal CellInfo[,] cells;
        internal double[] rowDesiredSize;
        internal double[] colDesiredSize;
        internal Size desiredSize;
        internal double[] rowArrangeStart;
        internal double[] colArrangeStart;
        internal double[] rowArrangeLength;
        internal double[] colArrangeLength;

        internal GridInfo(IReadOnlyList<IGridRowDefinition> rowDefinitions, IReadOnlyList<IGridColumnDefinition> colDefinitions, CellInfo[,] cells)
        {
            this.rows = rowDefinitions.Count;
            this.cols = colDefinitions.Count;
            this.rowDefinitions = rowDefinitions;
            this.colDefinitions = colDefinitions;
            this.cells = cells;
            this.rowDesiredSize = new double[this.rows];
            this.colDesiredSize = new double[this.cols];
        }
    }
}
