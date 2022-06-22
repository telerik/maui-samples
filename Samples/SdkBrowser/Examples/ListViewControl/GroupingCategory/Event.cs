using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common;

namespace SDKBrowserMaui.Examples.ListViewControl.GroupingCategory
{
    // >> listview-grouping-reorderitems-businessobject
    public class Event : NotifyPropertyChangedBase
    {
        public string content;
        public string day;
        public string category;

        public string Content
        {
            get { return this.content; }
            set { this.UpdateValue(ref this.content, value); }

        }
        public string Day
        {
            get { return this.day; }
            set { this.UpdateValue(ref this.day, value); }

        }
        public string Category
        {
            get { return this.category; }
            set { this.UpdateValue(ref this.category, value); }

        }
    }
    // << listview-grouping-reorderitems-businessobject
}
