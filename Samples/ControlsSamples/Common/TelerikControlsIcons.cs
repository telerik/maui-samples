using System.Collections.Generic;

namespace QSF.Common;

internal static class TelerikControlsIcons
{
    private static Dictionary<string, string> ControlsIcons = new()
    {
        { "Accordion", char.ConvertFromUtf32(0xe000) },
        { "AutoComplete", char.ConvertFromUtf32(0xe001) },
        { "BadgeView", char.ConvertFromUtf32(0xe002) },
        { "Barcode", char.ConvertFromUtf32(0xe003) },
        { "Border", char.ConvertFromUtf32(0xe004) },
        { "BusyIndicator", char.ConvertFromUtf32(0xe005) },
        { "Button", char.ConvertFromUtf32(0xe006) },
        { "Calendar", char.ConvertFromUtf32(0xe007) },
        { "Chart", char.ConvertFromUtf32(0xe008) },
        { "Chat", char.ConvertFromUtf32(0xe00f) },
        { "CheckBox", char.ConvertFromUtf32(0xe010) },
        { "ComboBox", char.ConvertFromUtf32(0xe011) },
        { "DataForm", char.ConvertFromUtf32(0xe012) },
        { "DataGrid", char.ConvertFromUtf32(0xe013) },
        { "DatePicker", char.ConvertFromUtf32(0xe014) },
        { "DateTimePicker", char.ConvertFromUtf32(0xe015) },
        { "DockLayout", char.ConvertFromUtf32(0xe016) },
        { "Entry", char.ConvertFromUtf32(0xe017) },
        { "Expander", char.ConvertFromUtf32(0xe018) },
        { "Gauge", char.ConvertFromUtf32(0xe019) },
        { "ImageEditor", char.ConvertFromUtf32(0xe01a) },
        { "ItemsControl", char.ConvertFromUtf32(0xe01b) },
        { "ListPicker", char.ConvertFromUtf32(0xe01c) },
        { "ListView", char.ConvertFromUtf32(0xe01d) },
        { "Map", char.ConvertFromUtf32(0xe01e) },
        { "MaskedEntry", char.ConvertFromUtf32(0xe01f) },
        { "NavigationView", char.ConvertFromUtf32(0xe020) },
        { "NumericInput", char.ConvertFromUtf32(0xe021) },
        { "Path", char.ConvertFromUtf32(0xe022) },
        { "PdfProcessing", char.ConvertFromUtf32(0xe023) },
        { "PdfViewer", char.ConvertFromUtf32(0xe024) },
        { "Popup", char.ConvertFromUtf32(0xe025) },
        { "ProgressBar", char.ConvertFromUtf32(0xe026) },
        { "RangeSlider", char.ConvertFromUtf32(0xe027) },
        { "Rating", char.ConvertFromUtf32(0xe028) },
        { "RichTextEditor", char.ConvertFromUtf32(0xe029) },
        { "Scheduler", char.ConvertFromUtf32(0xe02a) },
        { "SegmentedControl", char.ConvertFromUtf32(0xe02b) },
        { "SideDrawer", char.ConvertFromUtf32(0xe02c) },
        { "SignaturePad", char.ConvertFromUtf32(0xe02d) },
        { "Slider", char.ConvertFromUtf32(0xe03e) },
        { "SlideView", char.ConvertFromUtf32(0xe02e) },
        { "SpreadProcessing", char.ConvertFromUtf32(0xe02f) },
        { "SpreadStreamProcessing", char.ConvertFromUtf32(0xe02f) },
        { "TabView", char.ConvertFromUtf32(0xe030) },
        { "TemplatedPicker", char.ConvertFromUtf32(0xe031) },
        { "TimePicker", char.ConvertFromUtf32(0xe032) },
        { "TimeSpanPicker", char.ConvertFromUtf32(0xe033) },
        { "Toolbar", char.ConvertFromUtf32(0xe034) },
        { "TreeView", char.ConvertFromUtf32(0xe035) },
        { "WordsProcessing", char.ConvertFromUtf32(0xe036) },
        { "WrapLayout", char.ConvertFromUtf32(0xe037) },
        { "ZipLibrary", char.ConvertFromUtf32(0xe038) }
    };

    private static Dictionary<string, string> ChartExamplesIcons = new()
    {
        { "AreaSeries", char.ConvertFromUtf32(0xe008) },
        { "BarSeries", char.ConvertFromUtf32(0xe009) },
        { "FinancialSeries", char.ConvertFromUtf32(0xe00a) },
        { "FinancialIndicators", char.ConvertFromUtf32(0xe00b) },
        { "LineSeries", char.ConvertFromUtf32(0xe00c) },
        { "NullValues", char.ConvertFromUtf32(0xe00d) },
        { "ScatterSeries", char.ConvertFromUtf32(0xe00e) },
        { "PieDonutSeries", char.ConvertFromUtf32(0xe00f) }
    };

    public static string GetIcon(string name)
    {
        if (ControlsIcons.ContainsKey(name))
        {
            return ControlsIcons[name];
        }

        return string.Empty;
    }

    public static string GetExampleIcon(Example example)
    {
        var exampleName = example.Name;
        var controlName = example.ControlName;

        if (exampleName.Equals("Configuration"))
        {
            return char.ConvertFromUtf32(0xe03c);
        }
        else if (exampleName.Equals("Customization"))
        {
            return char.ConvertFromUtf32(0xe03d);
        }
        else
        {
            if (controlName.Equals("Chart"))
            {
                return ChartExamplesIcons[exampleName];
            }
            else
            {
                return ControlsIcons.ContainsKey(controlName) ? ControlsIcons[controlName] : string.Empty;
            }
        }
    }
}
