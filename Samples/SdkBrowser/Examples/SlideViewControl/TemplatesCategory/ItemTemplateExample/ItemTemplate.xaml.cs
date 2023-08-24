using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SlideViewControl.TemplatesCategory.ItemTemplateExample;

public partial class ItemTemplate : RadContentView
{
    public ItemTemplate()
    {
        InitializeComponent();

        this.slideView.BindingContext = new ItemTemplateViewModel();
    }
}