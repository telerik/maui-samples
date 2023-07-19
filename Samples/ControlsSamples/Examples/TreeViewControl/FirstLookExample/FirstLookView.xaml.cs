using Telerik.Maui.Controls;
using Telerik.Maui.Controls.ItemsView;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        this.InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

       this.treeView.ExpandAll();
    }

    private void OnItemTapped(object sender, ItemViewTappedEventArgs eventArgs)
    {
        this.treeView.SelectedItem = eventArgs.Item;

        eventArgs.Handled = true;
    }
}
