using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.TreeView;

namespace SDKBrowserMaui.Examples.TreeViewControl.LoadChildrenOnDemandCategory.LoadChildrenOnDemandCommandExample
{
    // >> load-children-on-demand-command-viewmodel
    public class LoadingViewModel : NotifyPropertyChangedBase
    {
        private const int loadDelay = 1000;
        public LoadingViewModel()
        {
            this.Items = new ObservableCollection<Category>()
            {
                new Category { Name = "Products"},
                new Category { Name = "Purchase"},
                new Category { Name = "Support"},
                new Category { Name = "Community"}
            };

            this.LoadChildrenOnDemand = new Command<TreeViewLoadChildrenOnDemandCommandContext>(this.LoadOnDemandExecute);
        }

        public ObservableCollection<Category> Items { get; set; }
        public ICommand LoadChildrenOnDemand { get; set; }

        private async void LoadOnDemandExecute(TreeViewLoadChildrenOnDemandCommandContext commandContext)
        {
            if (commandContext.Item is Category category)
            {
                await Task.Delay(loadDelay);

                ObservableCollection<string> children = await Task.Run(() => this.LoadChildren(category));
                category.Children = children;
            }
            commandContext.Finish();

        }
        private ObservableCollection<string> LoadChildren(Category category)
        {
            Dictionary<string, ObservableCollection<string>> allItems = new Dictionary<string, ObservableCollection<string>>();
            allItems.Add("Products", new ObservableCollection<string>() { "Telerik UI for .NET MAUI", "Telerik UI for WPF", "Telerik Reporting", "Telerik UI for Xamarin", "Telerik UI for WinUI" });
            allItems.Add("Purchase", new ObservableCollection<string>() { "Buy now", "License Agreement", "Policies" });
            allItems.Add("Support", new ObservableCollection<string>() { "Support Center", "Knowledge Base", "Demos", "Tutorials" });
            allItems.Add("Community", new ObservableCollection<string>() { "Learning Resources", "Blogs", "Forums" });

            var result = new ObservableCollection<string>();
            bool hasChildren = allItems.TryGetValue(category.Name, out result);

            return result;
        }
    }
    // << load-children-on-demand-command-viewmodel
}
