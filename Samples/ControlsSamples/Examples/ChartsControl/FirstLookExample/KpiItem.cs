using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.ChartsControl.FirstLookExample;

public class KpiItem
{
    private static readonly PathGeometryConverter GeometryConverter = new PathGeometryConverter();

    public string Title { get; set; }

    public string Subtitle { get; set; } = "vs previous month";

    public Geometry Icon { get; private set; }

    public string IconData
    {
        set => this.Icon = (Geometry)GeometryConverter.ConvertFromInvariantString(value);
    }

    public Color IconColor { get; set; }

    public Color IconBackgroundColor { get; set; }

    public string Value { get; set; }

    public string Trend { get; set; }

    public bool IsPositive { get; set; }
}
