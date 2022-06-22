using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView.Commands;

namespace SDKBrowserMaui.ViewModels
{
    public class ExamplesViewModelBase : PageViewModel
    {
        public ExamplesViewModelBase()
        {
            this.Examples = new ObservableCollection<Example>();

            var configurationService = DependencyService.Get<IConfigurationService>();
            var configuration = configurationService.Configuration;

            foreach (var control in configuration.Controls)
            {
                foreach (var category in control.Categories)
                {
                    foreach (var example in category.Examples)
                    {
                        this.Examples.Add(example);
                    }
                }
            }

            this.NextCommand = new Command<ItemTapCommandContext>(this.OnNextCommand);
        }

        public ObservableCollection<Example> Examples { get; protected set; }

        public Command<ItemTapCommandContext> NextCommand { get; private set; }

        private void OnNextCommand(ItemTapCommandContext context)
        {
            var navigationService = DependencyService.Get<INavigationService>();

            var control = context.Item as Control;

            if (control != null)
            {
                if (control.Categories.Count > 1)
                {
                    navigationService.NavigateToAsync<ControlViewModel>(control);
                }
                else if (control.Categories.Count > 0)
                {
                    var category = control.Categories[0];

                    if (category.Examples.Count > 1)
                    {
                        navigationService.NavigateToAsync<CategoryViewModel>(category);
                    }
                    else if (category.Examples.Count > 0)
                    {
                        var example = category.Examples[0];

                        navigationService.NavigateToAsync<ExampleViewModel>(example);
                    }
                }
            }
            else
            {
                var example = context.Item as Example;

                if (example != null)
                {
                    navigationService.NavigateToAsync<ExampleViewModel>(example);
                }
            }
        }
    }
}
