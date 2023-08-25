using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SlideViewControl.CommandsCategory.CommandsExample;

public partial class Commands : RadContentView
{
    public Commands()
    {
        InitializeComponent();

        this.slideView.BindingContext = new ViewModel();
    }
}