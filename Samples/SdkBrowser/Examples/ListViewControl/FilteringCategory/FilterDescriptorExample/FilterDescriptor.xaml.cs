using Microsoft.Maui.Controls.Xaml;
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

            this.listView.FilterDescriptors.Add(new Telerik.Maui.Controls.Compatibility.DataControls.ListView.ListViewDelegateFilterDescriptor { Filter = this.AgeFilter });
        }

        private bool AgeFilter(object arg)
        {
            var age = ((Person)arg).Age;
            return age >= 25 && age <= 35;
        }
    }
}