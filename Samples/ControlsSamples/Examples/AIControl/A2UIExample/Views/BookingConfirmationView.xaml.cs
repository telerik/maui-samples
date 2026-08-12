using System.Collections.Generic;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace QSF.Examples.AIControl.A2UIExample.Views;

public partial class BookingConfirmationView : ContentView
{
    public static readonly BindableProperty ResultProperty =
        BindableProperty.Create(nameof(Result), typeof(Dictionary<string, object>), typeof(BookingConfirmationView), null,
            propertyChanged: (b, o, n) => ((BookingConfirmationView)b).OnResultChanged(n));

    public BookingConfirmationView()
    {
        this.InitializeComponent();
    }

    public Dictionary<string, object>? Result
    {
        get => (Dictionary<string, object>?)this.GetValue(ResultProperty);
        set => this.SetValue(ResultProperty, value);
    }

    private void OnResultChanged(object newValue)
    {
        this.resultList.Children.Clear();

        if (newValue is not Dictionary<string, object> result)
        {
            return;
        }

        var entries = new List<(string Key, string Value)>();
        foreach (var item in result)
        {
            entries.Add((A2UIViewModel.FormatDisplayedKey(item.Key), A2UIViewModel.FormatSubmittedValue(item.Value)));
        }

        for (var i = 0; i < entries.Count; i++)
        {
            var row = new Grid
            {
                ColumnDefinitions = { new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star) },
                Padding = new Thickness(0, 12),
            };

            var keyLabel = new Label
            {
                Text = entries[i].Key,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
            };

            keyLabel.SetDynamicResource(Label.TextColorProperty, "PlaceholderColor");
            row.Add(keyLabel, 0, 0);

            row.Add(new Label
            {
                Text = entries[i].Value,
                FontSize = 15,
                HorizontalOptions = LayoutOptions.End,
                HorizontalTextAlignment = TextAlignment.End,
            }, 1, 0);

            this.resultList.Children.Add(row);

            if (i < entries.Count - 1)
            {
                var divider = new BoxView { HeightRequest = 1 };
                divider.SetDynamicResource(BoxView.ColorProperty, "SeparatorColor");
                this.resultList.Children.Add(divider);
            }
        }
    }
}
