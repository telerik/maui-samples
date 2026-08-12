using Microsoft.Maui.Controls;
using QSF.Examples.AIControl.A2UIExample.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace QSF.Examples.AIControl.A2UIExample.Views;

/// <summary>
/// Top-level A2UI v0.9 renderer. Accepts A2UI messages, builds component map
/// and data model, determines root, then delegates rendering to A2UINodeRenderer.
/// </summary>
public class A2UITelerikRenderer : ContentView
{
    private const string SyntheticRootId = "__synthetic-root";

    private Dictionary<string, A2UIComponent> componentMap = new Dictionary<string, A2UIComponent>();
    private Dictionary<string, JsonElement> dataModel;
    private string? rootId;
    private Action<Dictionary<string, object?>>? onSubmit;

    public void SetMessages(List<A2UIMessage> messages, Action<Dictionary<string, object?>>? onSubmit = null)
    {
        this.onSubmit = onSubmit;
        this.componentMap.Clear();
        this.rootId = null;

        var newDataModel = new Dictionary<string, JsonElement>();
        foreach (var message in messages)
        {
            var updateComponentsAction = message.UpdateComponents;
            if (updateComponentsAction != null)
            {
                foreach (var component in updateComponentsAction.Components)
                {
                    this.componentMap[component.Id] = component;
                }
            }

            var updateDataModelAction = message.UpdateDataModel;
            if (updateDataModelAction != null)
            {
                FlattenInto(newDataModel, updateDataModelAction.Path, updateDataModelAction.Value);
            }
        }

        if (this.componentMap.ContainsKey("root"))
        {
            this.rootId = "root";
        }
        else if (this.componentMap.Count > 0)
        {
            var childIds = this.componentMap.Values
                .SelectMany(c => c.Children ?? new List<string>())
                .Concat(this.componentMap.Values.Where(c => c.Child is not null).Select(c => c.Child!))
                .ToHashSet();

            var rootCandidates = this.componentMap.Keys
                .Where(id => !childIds.Contains(id))
                .ToList();

            if (rootCandidates.Count == 1)
            {
                this.rootId = rootCandidates[0];
            }
            else if (rootCandidates.Count > 1)
            {
                var allDashboard = rootCandidates.All(id => this.componentMap.TryGetValue(id, out var c) && c.Component is "Chart" or "Grid" or "TileLayoutItem" or "Card");

                this.componentMap[SyntheticRootId] = new A2UIComponent
                {
                    Id = SyntheticRootId,
                    Component = allDashboard ? "TileLayout" : "Form",
                    Children = rootCandidates
                };

                this.rootId = SyntheticRootId;
            }
            else
            {
                this.rootId = this.componentMap.Keys.FirstOrDefault();
            }
        }

        this.dataModel = newDataModel;

        this.BuildContent();
    }

    private static object? GetNativeValue(JsonElement el) => el.ValueKind switch
    {
        JsonValueKind.String => el.GetString(),
        JsonValueKind.Number => el.TryGetDouble(out var d) ? d : null,
        JsonValueKind.True => true,
        JsonValueKind.False => false,
        _ => null
    };

    private static void FlattenInto(Dictionary<string, JsonElement> dict, string basePath, JsonElement value)
    {
        dict[basePath] = value;

        if (value.ValueKind == JsonValueKind.Object)
        {
            foreach (var prop in value.EnumerateObject())
            {
                var childPath = basePath.TrimEnd('/') + "/" + prop.Name;
                FlattenInto(dict, childPath, prop.Value);
            }
        }
    }

    private void BuildContent()
    {
        if (this.rootId == null)
        {
            this.Content = null;
            return;
        }

        var renderer = new A2UINodeRenderer(this.componentMap, this.dataModel, this.OnValueChanged, this.OnActionTriggered);
        this.Content = renderer.Render(this.rootId);
    }

    private void OnValueChanged(string path, JsonElement value)
        => this.dataModel[path] = value;

    private void OnActionTriggered(string eventName)
    {
        if (eventName != "submit_form")
        {
            return;
        }

        var values = this.dataModel
            .Where(x => x.Value.ValueKind is not (JsonValueKind.Object or JsonValueKind.Array))
            .ToDictionary(x => x.Key, kv => (object?)GetNativeValue(kv.Value));

        this.onSubmit?.Invoke(values);
    }
}
