using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ListViewControl.CellTypesCategory.TemplateCellSelectorExample
{
    // >> listview-itemtemplateselector-customitemtemplateselector
    public class CustomItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Template1 { get; set; }
        public DataTemplate Template2 { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var book = item as DataItem;
            if (book.IsSpecial)
            {
                return this.Template2;
            }

            return this.Template1;
        }
    }
    // << listview-itemtemplateselector-customitemtemplateselector
}
