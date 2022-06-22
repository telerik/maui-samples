using System;
using Telerik.Maui.Controls.Compatibility.Chart;

namespace CryptoTracker.Formatters
{
    internal class ShortPriceFormatter : ILabelFormatter
    {
        public Type ValueType => typeof(double);

        public object ConvertionContext { get; set; }

        public string FormatValue(object value)
        {
            var num = (double)value;
            return this.FormatNumber(num);
        }

        private string FormatNumber(double num)
        {
            if (num <= 0.001)
            {
                return String.Empty;
            }

            if (num >= 1000000)
            {
                return (num / 1000000).ToString("0.#") + "M";
            }

            if (num >= 1000)
            {
                return (num / 1000.0).ToString("0.#") + "K";
            }

            if (num >= 10)
            {
                return num.ToString("F0");
            }

            if (num > 1)
            {
                return num.ToString("F1");
            }

            return num.ToString("F2");
        }
    }

}
