using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls.Compatibility.Chart;

namespace SDKBrowser.Examples.ChartControl.PieChartCategory.PieChartPercentageExample
{
    public class PercentageLabelFormatter : BindableObject, ILabelFormatter
    {
        public static readonly BindableProperty TotalProperty =
            BindableProperty.Create("Total", typeof(double), typeof(PercentageLabelFormatter), 0.0);

        public double Total
        {
            get
            {
                return (double)this.GetValue(TotalProperty);
            }
            set
            {
                this.SetValue(TotalProperty, value);
            }
        }

        public Type ValueType
        {
            get
            {
                return typeof(double);
            }
        }

        public object ConvertionContext
        {
            get
            {
                return null;
            }
        }

        public string FormatValue(object value)
        {
            return string.Format("{0:P}", (double)value / this.Total);
        }
    }
}
