using Telerik.Maui.Controls;

namespace QSF.Examples.TreeViewControl.CustomizationExample;

public partial class CustomizationView : RadContentView
{
    public CustomizationView()
    {
        this.InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        this.treeView.ExpandAll();
    }
}
