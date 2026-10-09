using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;
using Telerik.Maui.Data;

namespace Telerik.AppUtils.Services;

public class DataGridTestingService
{
    private const string InspectCommand = "DATAGRID_INSPECT";

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = false,
        Converters = { new JsonStringEnumConverter() }
    };
    private static readonly MethodInfo GetVisibleRowsMethod = typeof(HitTestService).GetMethod("GetVisibleRows", BindingFlags.Instance | BindingFlags.NonPublic)!;
    private static readonly MethodInfo GetVisibleCellsForRowMethod = typeof(HitTestService).GetMethod("GetVisibleCellsForRow", BindingFlags.Instance | BindingFlags.NonPublic)!;
    private static readonly MethodInfo GetColumnHeaderLayoutSlotMethod = typeof(HitTestService).GetMethod("GetColumnHeaderLayoutSlot", BindingFlags.Instance | BindingFlags.NonPublic)!;
    private static readonly MethodInfo GetCellTextMethod = typeof(DataGridColumn).GetMethod("GetCellText", BindingFlags.Instance | BindingFlags.NonPublic)!;
    private static readonly PropertyInfo? ModelProperty = typeof(RadDataGrid).GetProperty("Model", BindingFlags.Instance | BindingFlags.NonPublic);

    private static WeakReference<RadDataGrid>? dataGridRef;

    public static bool TryStart(RadDataGrid dataGrid)
    {
        var testingService = DependencyService.Get<ITestingService>();
        if (testingService == null || !testingService.IsAppUnderTest)
        {
            return false;
        }

        testingService.OnCommand += HandleCommand;
        dataGridRef = new WeakReference<RadDataGrid>(dataGrid);

        return true;
    }

    private static void HandleCommand(object? sender, TestCommandEventArgs e)
    {
        if (e.Command.Equals(InspectCommand, StringComparison.OrdinalIgnoreCase) || e.Command.StartsWith(InspectCommand + ":", StringComparison.OrdinalIgnoreCase))
        {
            var tcs = new TaskCompletionSource<string?>();
            e.Result = tcs.Task;

            if (dataGridRef != null && dataGridRef.TryGetTarget(out var dataGrid))
            {
                dataGrid.Dispatcher.Dispatch(() =>
                {
                    try
                    {
                        string json = BuildJson(dataGrid);
                        tcs.SetResult(json);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetResult("ERROR: " + ex.Message);
                    }
                });
            }
        }
    }

    private static string BuildJson(RadDataGrid dataGrid)
    {
        var dto = new DataGridDto
        {
            SelectionUnit = dataGrid.SelectionUnit,
            SelectedItemsCount = dataGrid.SelectedItems?.Count ?? 0,
            Width = dataGrid.Width,
            Height = dataGrid.Height,
        };

        var dataView = dataGrid.GetDataView();
        HitTestService hitTestService = dataGrid.HitTestService;
        DataGridCellInfo currentCellInfo = dataGrid.CurrentCell;

        Rect viewport = GetCellsViewport(dataGrid, frozen: false);
        Rect frozenViewport = GetCellsViewport(dataGrid, frozen: true);

        bool isFirstRow = true;

        var rows = (IEnumerable<(object item, int rowIndex, Rect layoutSlot)>)GetVisibleRowsMethod.Invoke(hitTestService, null)!;
        foreach (var row in rows)
        {
            if (row.item is IDataGroup group)
            {
                Rect? visibleGroupBounds = IntersectRect(row.layoutSlot, viewport);
                if (visibleGroupBounds != null)
                {
                    dto.Groups.Add(new GroupRowDto
                    {
                        Text = group.Key?.ToString() ?? string.Empty,
                        Level = group.Level,
                        IsExpanded = dataView.GetIsExpanded(group),
                        Bounds = row.layoutSlot,
                        VisibleBounds = visibleGroupBounds.Value,
                        IsFullyVisible = visibleGroupBounds.Value == row.layoutSlot,
                    });
                }

                continue;
            }

            int rowIndex = row.rowIndex;
            var cells = (IEnumerable<(DataGridCellInfo cellInfo, Rect layoutBounds)>)GetVisibleCellsForRowMethod.Invoke(hitTestService, new object[] { rowIndex })!;
            foreach (var cell in cells)
            {
                DataGridCellInfo info = cell.cellInfo;
                DataGridColumn column = info.Column;

                Rect cellViewport = column.IsFrozen ? frozenViewport : viewport;

                if (isFirstRow)
                {
                    Rect headerBounds = (Rect)GetColumnHeaderLayoutSlotMethod.Invoke(hitTestService, new object[] { column })!;
                    Rect? visibleHeaderBounds = IntersectRect(headerBounds, new Rect(cellViewport.X, headerBounds.Y, cellViewport.Width, headerBounds.Height));
                    if (visibleHeaderBounds != null)
                    {
                        dto.Columns.Add(new ColumnDto
                        {
                            HeaderText = column.HeaderText ?? string.Empty,
                            IsFrozen = column.IsFrozen,
                            Bounds = headerBounds,
                            VisibleBounds = visibleHeaderBounds.Value,
                            IsFullyVisible = visibleHeaderBounds.Value == headerBounds,
                        });
                    }
                }

                Rect? visibleBounds = IntersectRect(cell.layoutBounds, cellViewport);

                var cellDto = new CellDto
                {
                    Row = rowIndex,
                    Column = column.HeaderText ?? string.Empty,
                    Text = GetCellText(column, info.Value),
                    IsSelected = IsCellSelected(dataGrid, info),
                    IsFrozen = column.IsFrozen,
                    Bounds = cell.layoutBounds,
                    VisibleBounds = visibleBounds ?? Rect.Zero,
                    IsFullyVisible = visibleBounds == cell.layoutBounds,
                };

                // Cells realized only in the virtualization buffer are never on screen, so they must not
                // be reachable through the reported collection (e.g. Cells.Last()).
                if (visibleBounds != null)
                {
                    dto.Cells.Add(cellDto);
                }

                if (dto.CurrentCell == null && AreSameCell(info, currentCellInfo))
                {
                    dto.CurrentCell = cellDto;
                }
            }

            isFirstRow = false;
        }

        if (dto.CurrentCell == null && currentCellInfo != null)
        {
            dto.CurrentCell = new CellDto
            {
                Row = -1,
                Column = currentCellInfo.Column?.HeaderText,
                Text = GetCellText(currentCellInfo.Column, currentCellInfo.Value),
                IsSelected = IsCellSelected(dataGrid, currentCellInfo),
                IsFrozen = currentCellInfo.Column?.IsFrozen ?? false,
                Bounds = Rect.Zero,
            };
        }

        return JsonSerializer.Serialize(dto, SerializerOptions);
    }

    private static string GetCellText(DataGridColumn? column, object? value)
        => column == null ? string.Empty : (string?)GetCellTextMethod.Invoke(column, new[] { value }) ?? string.Empty;

    /// <summary>
    /// Returns the area of the cells panel that is actually on screen, expressed in the same
    /// (scroll content) coordinate space as the cell layout slots. This mirrors the viewport the
    /// grid itself uses in <c>GridModel.ForEachViewportDataItem</c>; it must not be approximated
    /// from the grid size, because the grid also hosts the group/search/header panels, and it must
    /// not be taken from the content panel, which is sized to the whole (virtualized) content.
    /// </summary>
    private static Rect GetCellsViewport(RadDataGrid dataGrid, bool frozen)
    {
        object? model = ModelProperty?.GetValue(dataGrid);
        if (model == null)
        {
            return new Rect(0, 0, dataGrid.Width, dataGrid.Height);
        }

        Type modelType = model.GetType();
        Size available = ReadSize(model, modelType, frozen ? "availableFrozenCellsSize" : "availableCellsSize");
        Size final = ReadSize(model, modelType, frozen ? "finalFrozenCellsSize" : "finalCellsSize");

        double offsetX = frozen ? 0 : ReadDouble(model, modelType, "PhysicalHorizontalOffset");
        double offsetY = ReadDouble(model, modelType, "PhysicalVerticalOffset");

        return new Rect(offsetX, offsetY, Math.Min(available.Width, final.Width), Math.Min(available.Height, final.Height));
    }

    private static Size ReadSize(object model, Type modelType, string fieldName)
    {
        object? value = modelType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(model);
        if (value == null)
        {
            return Size.Zero;
        }

        Type sizeType = value.GetType();
        double width = (double)sizeType.GetField("Width")!.GetValue(value)!;
        double height = (double)sizeType.GetField("Height")!.GetValue(value)!;

        return new Size(width, height);
    }

    private static double ReadDouble(object model, Type modelType, string propertyName)
        => (double?)modelType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(model) ?? 0;

    private static Rect? IntersectRect(Rect rect, Rect viewport)
    {
        double left = Math.Max(rect.Left, viewport.Left);
        double top = Math.Max(rect.Top, viewport.Top);
        double right = Math.Min(rect.Right, viewport.Right);
        double bottom = Math.Min(rect.Bottom, viewport.Bottom);

        if (right <= left || bottom <= top)
        {
            return null;
        }

        return new Rect(left, top, right - left, bottom - top);
    }

    private static bool AreSameCell(DataGridCellInfo? first, DataGridCellInfo? second)
    {
        if (first == null || second == null)
        {
            return false;
        }

        if (ReferenceEquals(first, second))
        {
            return true;
        }

        return ReferenceEquals(first.Item, second.Item)
            && string.Equals(first.Column?.HeaderText, second.Column?.HeaderText, StringComparison.Ordinal)
            && Equals(first.Value, second.Value);
    }

    private static bool IsCellSelected(RadDataGrid grid, DataGridCellInfo cellInfo)
    {
        if (cellInfo == null || grid?.SelectedItems == null)
        {
            return false;
        }

        if (grid.SelectionUnit == DataGridSelectionUnit.Cell)
        {
            return grid.SelectedItems.Contains(cellInfo);
        }

        return grid.SelectedItems.Contains(cellInfo.Item);
    }

    class ColumnDto
    {
        public string HeaderText { get; set; } = string.Empty;

        public bool IsFrozen { get; set; }

        public Rect Bounds { get; set; } = Rect.Zero;

        public Rect VisibleBounds { get; set; } = Rect.Zero;

        public bool IsFullyVisible { get; set; }
    }

    class CellDto
    {
        public int Row { get; set; }

        public string? Column { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsSelected { get; set; }

        public bool IsFrozen { get; set; }

        public Rect Bounds { get; set; } = Rect.Zero;

        public Rect VisibleBounds { get; set; } = Rect.Zero;

        public bool IsFullyVisible { get; set; }
    }

    class GroupRowDto
    {
        public string Text { get; set; } = string.Empty;

        public int Level { get; set; }

        public bool IsExpanded { get; set; }

        public Rect Bounds { get; set; } = Rect.Zero;

        public Rect VisibleBounds { get; set; } = Rect.Zero;

        public bool IsFullyVisible { get; set; }
    }

    class DataGridDto
    {
        public List<ColumnDto> Columns { get; set; } = new();

        public List<CellDto> Cells { get; set; } = new();

        public List<GroupRowDto> Groups { get; set; } = new();

        public CellDto? CurrentCell { get; set; }

        public DataGridSelectionUnit SelectionUnit { get; set; }

        public int SelectedItemsCount { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}