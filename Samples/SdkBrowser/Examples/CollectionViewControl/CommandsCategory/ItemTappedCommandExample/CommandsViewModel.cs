using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Windows.Input;

namespace SDKBrowserMaui.Examples.CollectionViewControl.CommandsCategory.ItemTappedCommandExample;

// >> collectionview-itemtap-command-viewmodel
public class CommandsViewModel
{
    public CommandsViewModel()
    {
        Source = new List<string> { "Tom", "Anna", "Peter", "Teodor", "Martin" };
        MyTapCommand = new Command((item) =>
        {
            var tappedItem = item;
            Application.Current.Windows[0].Page.DisplayAlert("", "You have tapped on: " + tappedItem, "OK");
        });
    }

    public List<string> Source { get; set; }

    public ICommand MyTapCommand { get; set; }
}
// << collectionview-itemtap-command-viewmodel
