using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SlideViewControl.TemplatesCategory.ItemTemplateExample;

public partial class ItemTemplate : RadContentView
{
    public ItemTemplate()
    {
        InitializeComponent();

        slideView.BindingContext = new ViewModel();
    }
}