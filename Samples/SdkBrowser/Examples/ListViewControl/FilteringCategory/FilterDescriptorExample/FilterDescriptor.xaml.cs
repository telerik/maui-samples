﻿using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataControls;

namespace SDKBrowserMaui.Examples.ListViewControl.FilteringCategory.FilterDescriptorExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterDescriptor : RadContentView
    {
        public FilterDescriptor()
        {
            InitializeComponent();
            // >> listview-add-filter
            this.listView.FilterDescriptors.Add(new Telerik.Maui.Controls.Compatibility.DataControls.ListView.ListViewDelegateFilterDescriptor { Filter = this.AgeFilter });
            // << listview-add-filter
        }

        // >> listview-age-filter
        private bool AgeFilter(object arg)
        {
            var age = ((Person)arg).Age;
            return age >= 25 && age <= 35;
        }
        // << listview-age-filter
    }
}