using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace QSF.Converters;

[ContentProperty(nameof(Items))]
public class RandomItemConverter : IValueConverter
{
    private static readonly Random random = new Random();

    private readonly IList<object> items = new List<object>();
    private int index = -1;
    private Dictionary<object, object> valueToItemSet = new Dictionary<object, object>();

    public IList<object> Items { get { return this.items; } }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.items.Count == 0)
        {
            return null;
        }

        if (this.index == -1)
        {
            this.index = random.Next() % this.items.Count;
        }

        object item;
        if (!this.valueToItemSet.TryGetValue(value, out item))
        {
            this.index++;
            item = this.items[this.index % this.items.Count];
            this.valueToItemSet[value] = item;
        }

        return item;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
