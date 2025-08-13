using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls.CollectionView;

namespace SDKBrowserMaui.Examples.CollectionViewControl.CommandsCategory.GroupTappedCommandExample;

// >> collectionview-grouptapcommand-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        this.People = new ObservableCollection<Person>()
        {
            new Person { Name = "Kiko", Age = 23, Department = "Production" },
            new Person { Name = "Jerry", Age = 23, Department = "Accounting and Finance" },
            new Person { Name = "Ethan", Age = 51, Department = "Marketing" },
            new Person { Name = "Isabella", Age = 25, Department = "Marketing" },
            new Person { Name = "Joshua", Age = 45, Department = "Production" },
            new Person { Name = "Logan", Age = 26, Department = "Production"},
            new Person { Name = "Aaron", Age = 32, Department = "Production" },
            new Person { Name = "Elena", Age = 37, Department = "Accounting and Finance" },
            new Person { Name = "Ross", Age = 30, Department = "Marketing" },
        };

        this.MyGroupTapCommand = new Command((item) =>
        {
            var tappedGroup = (GroupContext)item;
            Application.Current.Windows[0].Page.DisplayAlert("", $"You have tapped on group {tappedGroup.Key}", "OK");
        });
    }

    public ObservableCollection<Person> People { get; set; }

    public ICommand MyGroupTapCommand { get; set; }
}
// << collectionview-grouptapcommand-viewmodel
