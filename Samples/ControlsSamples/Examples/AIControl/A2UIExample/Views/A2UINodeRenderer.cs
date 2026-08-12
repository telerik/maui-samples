using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Examples.AIControl.A2UIExample.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Chart;
using Telerik.Maui.Controls.DataGrid;

namespace QSF.Examples.AIControl.A2UIExample.Views;

/// <summary>
/// Recursive A2UI node renderer. Builds .NET MAUI views dynamically from A2UI component descriptors.
/// </summary>
public class A2UINodeRenderer
{
    private readonly Dictionary<string, A2UIComponent> componentMap;
    private readonly Dictionary<string, JsonElement> dataModel;
    private readonly Action<string, JsonElement> onValueChanged;
    private readonly Action<string> onAction;

    public A2UINodeRenderer(Dictionary<string, A2UIComponent> componentMap, Dictionary<string, JsonElement> dataModel, Action<string, JsonElement> onValueChanged, Action<string> onAction)
    {
        this.componentMap = componentMap;
        this.dataModel = dataModel;
        this.onValueChanged = onValueChanged;
        this.onAction = onAction;
    }

    public View Render(string id)
    {
        if (!this.componentMap.TryGetValue(id, out var comp))
        {
            return new Label { Text = $"[Unknown component: {id}]" };
        }

        return comp.Component switch
        {
            "TileLayout" => this.RenderTileLayout(comp),
            "Form" => this.RenderForm(comp),
            "Card" => this.RenderCard(comp),
            "TextField" => this.RenderTextField(comp),
            "CheckBox" => this.RenderCheckBox(comp),
            "DateTimePicker" => this.RenderDateTimePicker(comp),
            "Select" => this.RenderSelect(comp),
            "Button" => this.RenderButton(comp),
            "Grid" => this.RenderGrid(comp),
            "Chart" => this.RenderChart(comp),
            _ => this.RenderChildren(comp)
        };
    }

    private View RenderTileLayout(A2UIComponent comp)
    {
        var children = comp.Children;
        if (children == null || children.Count == 0)
        {
            return new Label { Text = "[Empty TileLayout]" };
        }

        var tiles = new List<View>();

        foreach (var tileId in children)
        {
            if (!this.componentMap.TryGetValue(tileId, out var tile))
            {
                continue;
            }

            var tileContainer = new VerticalStackLayout { Spacing = 8 };

#if !(WINDOWS || MACCATALYST)
            if (!string.IsNullOrEmpty(tile.HeaderText))
            {
                tileContainer.Children.Add(new Label
                {
                    Text = tile.HeaderText,
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(0, 8, 0, 4)
                });
            }
#endif

            var innerChildIds = tile.Children;
            if (innerChildIds != null)
            {
                foreach (var innerChildId in innerChildIds)
                {
                    tileContainer.Children.Add(this.Render(innerChildId));
                }
            }

            var border = new RadBorder
            {
                CornerRadius = new Thickness(8),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(12),
                Content = tileContainer
            };

            tiles.Add(border);
        }

#if WINDOWS || MACCATALYST
        int maxColumns = 3;
        var container = new VerticalStackLayout { Spacing = 16 };

        var headers = new List<string?>();
        foreach (var tileId in children)
        {
            if (!this.componentMap.TryGetValue(tileId, out var tile))
            {
                headers.Add(null);
                continue;
            }

            headers.Add(tile.HeaderText);
        }

        for (int i = 0; i < tiles.Count; i += maxColumns)
        {
            int itemsInRow = Math.Min(maxColumns, tiles.Count - i);
            var rowGrid = new Grid
            {
                ColumnSpacing = 16,
                RowSpacing = 4,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star }
                }
            };

            for (int c = 0; c < itemsInRow; c++)
            {
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            for (int c = 0; c < itemsInRow; c++)
            {
                var header = headers[i + c];
                if (!string.IsNullOrEmpty(header))
                {
                    var headerLabel = new Label
                    {
                        Text = header,
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(16, 8, 0, 0)
                    };

                    Grid.SetRow(headerLabel, 0);
                    Grid.SetColumn(headerLabel, c);
                    rowGrid.Children.Add(headerLabel);
                }

                var tile = tiles[i + c];
                tile.VerticalOptions = LayoutOptions.Fill;
                Grid.SetRow(tile, 1);
                Grid.SetColumn(tile, c);
                rowGrid.Children.Add(tile);
            }

            container.Children.Add(rowGrid);
        }

        return container;
#else
        var stack = new VerticalStackLayout { Spacing = 8 };

        foreach (var tile in tiles)
        {
            stack.Children.Add(tile);
        }

        return stack;
#endif
    }

    private View RenderForm(A2UIComponent comp)
    {
        var stack = new VerticalStackLayout
        {
            Spacing = 12,
            Padding = new Thickness(0, 4),
            MaximumWidthRequest = 500
        };

        var children = comp.Children;
        if (children != null)
        {
            foreach (var childId in children)
            {
                stack.Children.Add(this.Render(childId));
            }
        }

        return stack;
    }

    private View RenderCard(A2UIComponent comp)
    {
        var stack = new VerticalStackLayout { Spacing = 8, Padding = new Thickness(12) };

        if (comp.Header?.Title is { Length: > 0 } headerTitle)
        {
            stack.Children.Add(new Label
            {
                Text = headerTitle,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold
            });
        }

        if (comp.Body is not null)
        {
            if (comp.Body.Title is { Length: > 0 } bodyTitle)
            {
                stack.Children.Add(new Label
                {
                    Text = bodyTitle,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold
                });

                var separator = new BoxView { HeightRequest = 1, Margin = new Thickness(0, 4) };
                separator.SetDynamicResource(BoxView.ColorProperty, "SeparatorColor");
                stack.Children.Add(separator);
            }

            if (comp.Body.Content is { Length: > 0 } bodyContent)
            {
                stack.Children.Add(new Label { Text = bodyContent, FontSize = 13, LineBreakMode = LineBreakMode.WordWrap });
            }
        }

        var children = comp.Children;
        if (children != null)
        {
            foreach (var childId in children)
            {
                stack.Children.Add(this.Render(childId));
            }
        }

        var cardBorder = new RadBorder
        {
            CornerRadius = new Thickness(8),
            BorderThickness = new Thickness(1),
            Content = stack
        };

        cardBorder.SetDynamicResource(RadBorder.BorderColorProperty, "DefaultBorderColor");
        return cardBorder;
    }

    private View RenderTextField(A2UIComponent comp)
    {
        var stack = new VerticalStackLayout { Spacing = 4 };

        if (comp.Label is not null)
        {
            stack.Children.Add(new Label { Text = comp.Label, FontSize = 13, FontAttributes = FontAttributes.Bold });
        }

        View editor;

        if (comp.Variant == "multiline")
        {
            var entry = new RadEditor
            {
                Text = this.GetCurrentString(comp),
                Placeholder = comp.Placeholder ?? string.Empty,
                HeightRequest = 80,
                ReserveSpaceForErrorView = false
            };

            entry.TextChanged += (_, e) => this.SetStringValue(comp, e.NewTextValue);
            editor = entry;
        }
        else if (comp.Variant == "number")
        {
            var numInput = new RadNumericInput
            {
                Value = this.GetCurrentNumber(comp),
            };

            numInput.ValueChanged += (_, e) => this.SetNumberValue(comp, e.NewValue ?? 0);
            editor = numInput;
        }
        else
        {
            var entry = new RadEntry
            {
                Text = this.GetCurrentString(comp),
                Placeholder = comp.Placeholder ?? string.Empty,
                IsPassword = comp.Variant == "password",
                ReserveSpaceForErrorView = false
            };

            entry.TextChanged += (_, e) => this.SetStringValue(comp, e.NewTextValue);
            editor = entry;
        }

        stack.Children.Add(editor);
        return stack;
    }

    private View RenderCheckBox(A2UIComponent comp)
    {
        var layout = new HorizontalStackLayout { Spacing = 8 };

        var checkBox = new RadCheckBox
        {
            IsChecked = this.GetCurrentBool(comp)
        };

        checkBox.IsCheckedChanged += (_, e) => this.SetBoolValue(comp, e.NewValue ?? false);

        layout.Children.Add(checkBox);

        if (comp.Label is not null)
        {
            layout.Children.Add(new Label { Text = comp.Label, VerticalTextAlignment = TextAlignment.Center, FontSize = 13 });
        }

        return layout;
    }

    private View RenderDateTimePicker(A2UIComponent comp)
    {
        var stack = new VerticalStackLayout { Spacing = 4 };

        if (comp.Label is not null)
        {
            stack.Children.Add(new Label { Text = comp.Label, FontSize = 13, FontAttributes = FontAttributes.Bold });
        }

        if (comp.EnableTime == true)
        {
            var picker = new RadDateTimePicker
            {
                Date = this.GetCurrentDateTime(comp) ?? DateTime.Today,
                MinimumDate = DateTime.Today
            };

            picker.SelectionChanged += (_, _) => this.SetDateValue(comp, picker.Date);
            stack.Children.Add(picker);
        }
        else
        {
            var picker = new RadDatePicker
            {
                Date = this.GetCurrentDateTime(comp) ?? DateTime.Today,
                MinimumDate = DateTime.Today
            };

            picker.SelectionChanged += (_, _) => this.SetDateValue(comp, picker.Date);
            stack.Children.Add(picker);
        }

        return stack;
    }

    private View RenderSelect(A2UIComponent comp)
    {
        var stack = new VerticalStackLayout { Spacing = 4 };

        if (comp.Label is not null)
        {
            stack.Children.Add(new Label { Text = comp.Label, FontSize = 13, FontAttributes = FontAttributes.Bold });
        }

        var comboBox = new RadComboBox
        {
            ItemsSource = comp.Options ?? new List<ChoiceOption>(),
            DisplayMemberPath = "Label",
            Placeholder = "Select...",
            IsEditable = false,
        };

        var currentValue = this.GetCurrentString(comp);
        if (!string.IsNullOrEmpty(currentValue) && comp.Options is not null)
        {
            var selectedItem = comp.Options.FirstOrDefault(o => o.Value == currentValue);
            if (selectedItem is not null)
            {
                comboBox.SelectedItem = selectedItem;
            }
        }

        comboBox.SelectionChanged += (_, _) =>
        {
            if (comboBox.SelectedItem is ChoiceOption selected)
            {
                SetStringValue(comp, selected.Value);
            }
        };

        stack.Children.Add(comboBox);
        return stack;
    }

    private View RenderButton(A2UIComponent comp)
    {
        var label = this.GetButtonLabel(comp);

        var button = new RadButton
        {
            Text = label
        };

        button.Clicked += (_, _) =>
        {
            var eventName = comp.Action?.Event?.Name ?? "click";
            this.onAction(eventName);
        };

        if (Application.Current?.Resources.TryGetValue("AccentButtonStyle", out var accentButtonStyle) == true)
        {
            button.Style = (Style)accentButtonStyle;
            button.HorizontalOptions = LayoutOptions.Start;
        }

        return button;
    }

    private string GetButtonLabel(A2UIComponent comp)
    {
        if (comp.Child is { } childId && componentMap.TryGetValue(childId, out var child))
        {
            return child.Text ?? child.Label ?? "Submit";
        }

        return comp.Label ?? "Submit";
    }

    private View RenderGrid(A2UIComponent comp)
    {
        var grid = new Grid { RowSpacing = 8 };
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

        if (comp.Label is { Length: > 0 } gridLabel)
        {
            grid.Children.Add(new Label { Text = gridLabel, FontSize = 15, FontAttributes = FontAttributes.Bold });
        }

        var dataGrid = new RadDataGrid
        {
            AutoGenerateColumns = false,
            ItemsSource = GetGridData(comp),
            UserEditMode = DataGridUserEditMode.None
        };

#if WINDOWS || MACCATALYST
        dataGrid.HeightRequest = 300;
#endif

        foreach (var col in comp.Columns ?? new List<GridColumnSpec>())
        {
            if (col.Visible == false)
            {
                continue;
            }

            var fieldType = col.FieldType ?? "string";
            DataGridColumn column = fieldType switch
            {
                "number" => new DataGridNumericalColumn { PropertyName = col.Field, HeaderText = col.Title ?? col.Field },
                "boolean" => new DataGridBooleanColumn { PropertyName = col.Field, HeaderText = col.Title ?? col.Field },
                "date" => new DataGridDateColumn { PropertyName = col.Field, HeaderText = col.Title ?? col.Field },
                _ => new DataGridTextColumn { PropertyName = col.Field, HeaderText = col.Title ?? col.Field }
            };

            dataGrid.Columns.Add(column);
        }

        Grid.SetRow(dataGrid, 1);
        grid.Children.Add(dataGrid);

        var gridBorder = new RadBorder
        {
            CornerRadius = new Thickness(8),
            BorderThickness = new Thickness(1),
            Padding = new Thickness(12),
            Content = grid
        };

        gridBorder.SetDynamicResource(RadBorder.BorderColorProperty, "DefaultBorderColor");
        return gridBorder;
    }

    private static List<ExpandoObject> GetGridData(A2UIComponent comp)
    {
        var fieldTypes = (comp.Columns ?? new List<GridColumnSpec>()).ToDictionary(c => c.Field, c => c.FieldType ?? "string", StringComparer.OrdinalIgnoreCase);

        return (comp.GridData ?? new List<Dictionary<string, JsonElement>>())
            .Select(row =>
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (var kvp in row)
                {
                    var ft = fieldTypes.TryGetValue(kvp.Key, out var t) ? t : "string";
                    expando[kvp.Key] = kvp.Value.ValueKind switch
                    {
                        JsonValueKind.Number => (object)kvp.Value.GetDouble(),
                        JsonValueKind.True => (object)true,
                        JsonValueKind.False => (object)false,
                        JsonValueKind.String when ft == "date" => DateTime.TryParse(kvp.Value.GetString(), out var dt)
                            ? (object)dt : kvp.Value.GetString() ?? string.Empty,
                        _ => (object)(kvp.Value.GetString() ?? string.Empty)
                    };
                }

                return (ExpandoObject)expando;
            })
            .ToList();
    }

    private View RenderChart(A2UIComponent comp)
    {
        var stack = new VerticalStackLayout { Spacing = 8 };

        if (comp.Label is { Length: > 0 } chartLabel)
        {
            stack.Children.Add(new Label { Text = chartLabel, FontSize = 15, FontAttributes = FontAttributes.Bold });
        }

        var chart = new RadCartesianChart { HeightRequest = 300 };

        var categories = comp.CategoryAxes?.CategoryAxis?.Categories ?? new List<string>();
        chart.HorizontalAxis = new CategoricalAxis();
        chart.VerticalAxis = new NumericalAxis();

        foreach (var s in comp.Series ?? new List<ChartSeriesSpec>())
        {
            var data = (s.Data ?? new List<double>())
                .Select((value, index) => new ChartDataPoint
                {
                    Category = index < categories.Count ? categories[index] : index.ToString(),
                    Value = value
                })
                .ToList();

            CartesianSeries series = (s.Type ?? "Column") switch
            {
                "Line" => new LineSeries
                {
                    ItemsSource = data,
                    CategoryBinding = new PropertyNameDataPointBinding("Category"),
                    ValueBinding = new PropertyNameDataPointBinding("Value"),
                    DisplayName = s.Name
                },
                "Bar" => new BarSeries
                {
                    ItemsSource = data,
                    CategoryBinding = new PropertyNameDataPointBinding("Category"),
                    ValueBinding = new PropertyNameDataPointBinding("Value"),
                    DisplayName = s.Name
                },
                "Area" => new AreaSeries
                {
                    ItemsSource = data,
                    CategoryBinding = new PropertyNameDataPointBinding("Category"),
                    ValueBinding = new PropertyNameDataPointBinding("Value"),
                    DisplayName = s.Name
                },
                _ => new BarSeries
                {
                    ItemsSource = data,
                    CategoryBinding = new PropertyNameDataPointBinding("Category"),
                    ValueBinding = new PropertyNameDataPointBinding("Value"),
                    DisplayName = s.Name
                }
            };

            chart.Series.Add(series);
        }

        var legend = new RadLegend
        {
            LegendProvider = chart,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(8, 0, 0, 0)
        };

        var chartGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        chartGrid.Add(chart, 0, 0);
        chartGrid.Add(legend, 1, 0);
        stack.Children.Add(chartGrid);

        var chartBorder = new RadBorder
        {
            CornerRadius = new Thickness(8),
            BorderThickness = new Thickness(1),
            Padding = new Thickness(12),
            Content = stack
        };

        chartBorder.SetDynamicResource(RadBorder.BorderColorProperty, "DefaultBorderColor");
        return chartBorder;
    }

    private View RenderChildren(A2UIComponent comp)
    {
        var stack = new VerticalStackLayout { Spacing = 8 };

        foreach (var childId in comp.Children ?? new List<string>())
        {
            stack.Children.Add(this.Render(childId));
        }

        return stack;
    }

    private string? GetBindingPath(A2UIComponent comp)
    {
        var val = comp.Value;
        if (val is null)
        {
            return null;
        }

        var el = val.Value;
        if (el.ValueKind == JsonValueKind.Object && el.TryGetProperty("path", out var p))
        {
            return p.GetString();
        }

        return null;
    }

    private string GetCurrentString(A2UIComponent comp)
    {
        var path = this.GetBindingPath(comp);

        if (path is not null && dataModel.TryGetValue(path, out var dm))
        {
            return dm.ValueKind switch
            {
                JsonValueKind.String => dm.GetString() ?? string.Empty,
                JsonValueKind.Number => dm.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                _ => string.Empty
            };
        }

        if (comp.Value?.ValueKind == JsonValueKind.String)
        {
            return comp.Value.Value.GetString() ?? string.Empty;
        }

        return string.Empty;
    }

    private bool GetCurrentBool(A2UIComponent comp)
    {
        var path = this.GetBindingPath(comp);
        if (path is not null && dataModel.TryGetValue(path, out var dm))
        {
            return dm.ValueKind == JsonValueKind.True;
        }

        return comp.Value?.ValueKind == JsonValueKind.True;
    }

    private double GetCurrentNumber(A2UIComponent comp)
    {
        var path = this.GetBindingPath(comp);
        if (path is not null && this.dataModel.TryGetValue(path, out var dm) && dm.ValueKind == JsonValueKind.Number)
        {
            return dm.GetDouble();
        }

        if (comp.Value?.ValueKind == JsonValueKind.Number)
        {
            return comp.Value.Value.GetDouble();
        }

        return 0;
    }

    private DateTime? GetCurrentDateTime(A2UIComponent comp)
    {
        var str = this.GetCurrentString(comp);
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }

        if (DateTime.TryParse(str, out var dt) && dt > DateTime.MinValue)
        {
            return dt;
        }

        return null;
    }

    private void SetStringValue(A2UIComponent comp, string value)
    {
        var path = this.GetBindingPath(comp);
        if (path is null)
        {
            return;
        }

        this.onValueChanged(path, JsonSerializer.SerializeToElement(value));
    }

    private void SetBoolValue(A2UIComponent comp, bool value)
    {
        var path = this.GetBindingPath(comp);
        if (path is null)
        {
            return;
        }

        this.onValueChanged(path, JsonSerializer.SerializeToElement(value));
    }

    private void SetNumberValue(A2UIComponent comp, double value)
    {
        var path = this.GetBindingPath(comp);
        if (path is null)
        {
            return;
        }

        this.onValueChanged(path, JsonSerializer.SerializeToElement(value));
    }

    private void SetDateValue(A2UIComponent comp, DateTime? value)
    {
        var path = this.GetBindingPath(comp);
        if (path is null || value is null)
        {
            return;
        }

        this.onValueChanged(path, JsonSerializer.SerializeToElement(value.Value.ToString("O")));
    }
}

public class ChartDataPoint
{
    public string Category { get; set; } = string.Empty;
    public double Value { get; set; }
}
