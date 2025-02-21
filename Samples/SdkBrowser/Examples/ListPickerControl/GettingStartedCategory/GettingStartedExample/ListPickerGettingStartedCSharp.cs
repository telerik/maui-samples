﻿using Microsoft.Maui;
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
                DisplayMemberPath = "Name"
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
