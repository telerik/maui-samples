using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;
using Telerik.Maui.Controls.Compatibility.ShapefileReader;

namespace QSF.Examples.MapControl.FirstLookExample
{
    public class DensityShapeStyleSelector : MapShapeStyleSelector
    {
        public MapShapeStyle HighDensityStyle { get; set; }
        public MapShapeStyle MediumHighDensityStyle { get; set; }
        public MapShapeStyle MediumDensityStyle { get; set; }
        public MapShapeStyle MediumLowDensityStyle { get; set; }
        public MapShapeStyle LowDensityStyle { get; set; }
        public MapShapeStyle LowerDensityStyle { get; set; }
        public MapShapeStyle LowestDensityStyle { get; set; }

        public override MapShapeStyle SelectStyle(object shape, BindableObject container)
        {
            var state = shape as IShape;
            if (state != null)
            {
                var densityText = state.GetAttribute("POP_DENSIT").ToString();
                int density;
                if (int.TryParse(densityText, out density))
                {
                    if (density > 1000)
                    {
                        return this.HighDensityStyle;
                    }

                    if (density <= 1000 && density >= 500)
                    {
                        return this.MediumHighDensityStyle;
                    }

                    if (density <= 500 && density >= 200)
                    {
                        return this.MediumDensityStyle;
                    }

                    if (density <= 200 && density >= 100)
                    {
                        return this.MediumLowDensityStyle;
                    }

                    if (density <= 100 && density >= 50)
                    {
                        return this.LowDensityStyle;
                    }

                    if (density <= 50 && density >= 20)
                    {
                        return this.LowerDensityStyle;
                    }

                    return this.LowestDensityStyle;
                }
            }

            return null;
        }
    }
}
