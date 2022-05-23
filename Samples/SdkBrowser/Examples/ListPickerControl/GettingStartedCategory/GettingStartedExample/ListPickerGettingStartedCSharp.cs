using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ListPickerControl.GettingStartedCategory.GettingStartedExample
{
    public class ListPickerGettingStartedCSharp : RadContentView
    {
        public ListPickerGettingStartedCSharp()
        {
            // >> listpicker-getting-started-csharp
            this.BindingContext = new PeopleViewModel();
            var listPicker = new RadListPicker()
            {
                Placeholder = "Pick a name!",
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    label.SetBinding(Label.TextProperty, new Binding(nameof(Person.Name)));

                    return label;
                }),
                DisplayMemberPath = "FullName"
            };
            listPicker.SetBinding(RadListPicker.ItemsSourceProperty, new Binding("Items"));
            // << listpicker-getting-started-csharp
            var panel = new VerticalStackLayout();
            panel.WidthRequest = 300;
            panel.HorizontalOptions = LayoutOptions.Center;
            panel.Children.Add(listPicker);
            this.Content = panel;
        }
    }
}
