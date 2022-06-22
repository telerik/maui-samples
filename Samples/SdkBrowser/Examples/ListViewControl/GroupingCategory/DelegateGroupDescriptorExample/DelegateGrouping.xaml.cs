using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataControls;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView;

namespace SDKBrowserMaui.Examples.ListViewControl.GroupingCategory.DelegateGroupDescriptorExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DelegateGrouping : RadContentView
    {
        // >> listview-grouping-delegategroupdescriptor-settingdelegate
        public DelegateGrouping()
        {
            InitializeComponent();

            var delegateDescriptor = new ListViewDelegateGroupDescriptor
            {
                KeyExtractor = FirstLetterKeyExtractor
            };

            listView.GroupDescriptors.Add(delegateDescriptor);
        }

        private object FirstLetterKeyExtractor(object arg)
        {
            var item = arg as City;
            return item?.Name.Substring(0, 1);
        }
        // << listview-grouping-delegategroupdescriptor-settingdelegate
    }
}