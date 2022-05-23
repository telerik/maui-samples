using Microsoft.Maui.Graphics;

namespace QSF.Examples.MapControl.FirstLookExample
{
    public class DensityItem
    {
        public DensityItem(string density, Color densityLevelColor)
        {
            this.Density = density;
            this.DensityLevelColor = densityLevelColor;
        }

        public string Density { get; set; }
        public Color DensityLevelColor { get; set; }
    }
}