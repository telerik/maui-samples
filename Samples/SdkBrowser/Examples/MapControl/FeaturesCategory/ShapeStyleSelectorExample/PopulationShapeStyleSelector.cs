using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Map;
using Telerik.Maui.Controls.Compatibility.ShapefileReader;

namespace SDKBrowserMaui.Examples.MapControl.FeaturesCategory.ShapeStyleSelectorExample
{ 
    // >> map-shapesstyleselector-code
    public class PopulationShapeStyleSelector : MapShapeStyleSelector
    {
        public MapShapeStyle HighPopulationShapeStyle { get; set; }
        public MapShapeStyle MediumPopulationShapeStyle { get; set; }
        public MapShapeStyle LowPopulationShapeStyle { get; set; }

        public override MapShapeStyle SelectStyle(object shape, BindableObject container)
        {
            var attributesShape = shape as IShape;
            if (attributesShape != null)
            {
                var populationText = attributesShape.GetAttribute("POP_CNTRY").ToString();
                int population;
                if (int.TryParse(populationText, out population))
                {
                    if (population > 20000000)
                    {
                        return this.HighPopulationShapeStyle;
                    }
                    else if (population < 1000000)
                    {
                        return this.LowPopulationShapeStyle;
                    }

                    return this.MediumPopulationShapeStyle;
                }
            }

            return null;
        }
    }
    // << map-shapesstyleselector-code
}