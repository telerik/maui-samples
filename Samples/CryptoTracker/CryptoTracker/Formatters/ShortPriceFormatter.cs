using System;
using Telerik.XamarinForms.Chart;

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
            if (num >= 100000)
                return FormatNumber(num / 1000) + "K";

            if (num >= 1000)
                return (num / 1000D).ToString("0.#") + "K";

            if (num < 1)
            {
                return num.ToString("F2");
            }

            if (num > 10)
            {
                return num.ToString("F0");
            }
            else
            {
                return num.ToString("F1");
            }
        }
    }

}
