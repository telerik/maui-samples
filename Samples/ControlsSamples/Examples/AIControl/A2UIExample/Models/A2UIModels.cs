#nullable enable

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QSF.Examples.AIControl.A2UIExample.Models;

/// <summary>
/// Top-level A2UI v0.9 message envelope. Exactly one of the payload properties
/// Spec: https://a2ui.org/specification/v0_9/server_to_client.json
/// </summary>
public class A2UIMessage
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = "v0.9";

    [JsonPropertyName("createSurface")]
    public A2UICreateSurfacePayload? CreateSurface { get; set; }

    [JsonPropertyName("updateComponents")]
    public A2UIUpdateComponentsPayload? UpdateComponents { get; set; }

    [JsonPropertyName("updateDataModel")]
    public A2UIUpdateDataModelPayload? UpdateDataModel { get; set; }

    [JsonPropertyName("deleteSurface")]
    public A2UIDeleteSurfacePayload? DeleteSurface { get; set; }
}

public class A2UICreateSurfacePayload
{
    [JsonPropertyName("surfaceId")]
    public string SurfaceId { get; set; } = string.Empty;

    [JsonPropertyName("catalogId")]
    public string CatalogId { get; set; } = "telerik-maui/v1";

    [JsonPropertyName("theme")]
    public JsonElement? Theme { get; set; }
}

public class A2UIUpdateComponentsPayload
{
    [JsonPropertyName("surfaceId")]
    public string SurfaceId { get; set; } = string.Empty;

    [JsonPropertyName("components")]
    public List<A2UIComponent> Components { get; set; } = new List<A2UIComponent>();
}

public class A2UIUpdateDataModelPayload
{
    [JsonPropertyName("surfaceId")]
    public string SurfaceId { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; set; } = "/";

    [JsonPropertyName("value")]
    public JsonElement Value { get; set; }
}

public class A2UIDeleteSurfacePayload
{
    [JsonPropertyName("surfaceId")]
    public string SurfaceId { get; set; } = string.Empty;
}

public class A2UIComponent
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("component")]
    public string Component { get; set; } = string.Empty;

    [JsonPropertyName("children")]
    public List<string>? Children { get; set; }

    [JsonPropertyName("child")]
    public string? Child { get; set; }

    [JsonPropertyName("colSpan")]
    public int? ColSpan { get; set; }

    [JsonPropertyName("rowSpan")]
    public int? RowSpan { get; set; }

    [JsonPropertyName("headerText")]
    public string? HeaderText { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("header")]
    public CardHeader? Header { get; set; }

    [JsonPropertyName("body")]
    public CardBody? Body { get; set; }

    [JsonPropertyName("imageSrc")]
    public string? ImageSrc { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("value")]
    public JsonElement? Value { get; set; }

    [JsonPropertyName("variant")]
    public string? Variant { get; set; }

    [JsonPropertyName("placeholder")]
    public string? Placeholder { get; set; }

    [JsonPropertyName("enableTime")]
    public bool? EnableTime { get; set; }

    [JsonPropertyName("options")]
    public List<ChoiceOption>? Options { get; set; }

    [JsonPropertyName("min")]
    public double? Min { get; set; }

    [JsonPropertyName("max")]
    public double? Max { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("action")]
    public A2UIAction? Action { get; set; }

    [JsonPropertyName("series")]
    public List<ChartSeriesSpec>? Series { get; set; }

    [JsonPropertyName("categoryAxes")]
    public ChartCategoryAxesSpec? CategoryAxes { get; set; }

    [JsonPropertyName("valueAxes")]
    public ChartValueAxesSpec? ValueAxes { get; set; }

    [JsonPropertyName("tooltip")]
    public ChartTooltipSpec? Tooltip { get; set; }

    [JsonPropertyName("legend")]
    public ChartLegendSpec? Legend { get; set; }

    [JsonPropertyName("columns")]
    public List<GridColumnSpec>? Columns { get; set; }

    [JsonPropertyName("data")]
    public List<Dictionary<string, JsonElement>>? GridData { get; set; }

    [JsonPropertyName("height")]
    public string? Height { get; set; }

    [JsonPropertyName("width")]
    public string? Width { get; set; }

    [JsonPropertyName("pageable")]
    public bool? Pageable { get; set; }

    [JsonPropertyName("pageSize")]
    public int? PageSize { get; set; }

    [JsonPropertyName("page")]
    public int? Page { get; set; }

    [JsonPropertyName("resizable")]
    public bool? Resizable { get; set; }

    [JsonPropertyName("reorderable")]
    public bool? Reorderable { get; set; }

    [JsonPropertyName("groupable")]
    public bool? Groupable { get; set; }

    [JsonPropertyName("sortable")]
    public bool? Sortable { get; set; }

    [JsonPropertyName("sortMode")]
    public string? SortMode { get; set; }

    [JsonPropertyName("filterMode")]
    public string? FilterMode { get; set; }
}

public class ChartCategoryAxisSpec
{
    [JsonPropertyName("categories")]
    public List<string>? Categories { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}

public class ChartCategoryAxesSpec
{
    [JsonPropertyName("categoryAxis")]
    public ChartCategoryAxisSpec? CategoryAxis { get; set; }
}

public class ChartValueAxisSpec
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
}

public class ChartValueAxesSpec
{
    [JsonPropertyName("valueAxis")]
    public ChartValueAxisSpec? ValueAxis { get; set; }
}

public class ChartTooltipSpec
{
    [JsonPropertyName("visible")]
    public bool? Visible { get; set; }
}

public class ChartLegendSpec
{
    [JsonPropertyName("visible")]
    public bool? Visible { get; set; }
}

public class ChartSeriesSpec
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("data")]
    public List<double>? Data { get; set; }

    [JsonPropertyName("field")]
    public string? Field { get; set; }

    [JsonPropertyName("categoryField")]
    public string? CategoryField { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("stack")]
    public bool? Stack { get; set; }
}

public class GridColumnSpec
{
    [JsonPropertyName("field")]
    public string Field { get; set; } = string.Empty;

    [JsonPropertyName("fieldType")]
    public string? FieldType { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("filterable")]
    public bool? Filterable { get; set; }

    [JsonPropertyName("sortable")]
    public bool? Sortable { get; set; }

    [JsonPropertyName("groupable")]
    public bool? Groupable { get; set; }

    [JsonPropertyName("lockable")]
    public bool? Lockable { get; set; }

    [JsonPropertyName("width")]
    public string? Width { get; set; }

    [JsonPropertyName("visible")]
    public bool? Visible { get; set; }
}

public class ChoiceOption
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class A2UIAction
{
    [JsonPropertyName("event")]
    public A2UIActionEvent? Event { get; set; }
}

public class A2UIActionEvent
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("context")]
    public JsonElement? Context { get; set; }
}

public class CreateSurfaceFunction
{
    [JsonPropertyName("surfaceId")]
    public string SurfaceId { get; set; } = "surface";

    [JsonPropertyName("components")]
    public List<A2UIComponent> Components { get; set; } = new List<A2UIComponent>();

    [JsonPropertyName("dataModel")]
    public DataModelInit? DataModel { get; set; }
}

public class DataModelInit
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = "/form";

    [JsonPropertyName("value")]
    public JsonElement Value { get; set; }
}

public class CardHeader
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
}

public class CardBody
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
