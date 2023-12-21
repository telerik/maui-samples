using Microsoft.Maui.Controls;
using System.Linq;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.SelectionCategory.MultipleSelectionExample;

public partial class MultipleSelection : ContentView
{
	public MultipleSelection()
	{
		InitializeComponent();
        this.BindingContext = new ViewModel();
	}

    // >> combobox-multipleselection-selectionchanged
    private void RadComboBox_SelectionChanged(object sender, ComboBoxSelectionChangedEventArgs e)
    {
        this.lastActionLabel.Text = (e.AddedItems.Count() > 0) ? "Selected " +  (e.AddedItems.First() as City).Name : 
                                                                 "Unselected " + (e.RemovedItems.First() as City).Name;
    }
    // << combobox-multipleselection-selectionchanged
}