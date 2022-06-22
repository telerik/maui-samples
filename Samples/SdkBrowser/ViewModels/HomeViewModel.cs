using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView.Commands;

namespace SDKBrowserMaui.ViewModels
{
    public class HomeViewModel : ExamplesViewModelBase
    {
        public HomeViewModel()
        {
            this.Controls = new ObservableCollection<Control>();

            var configurationService = DependencyService.Get<IConfigurationService>();
            var configuration = configurationService.Configuration;

            foreach (var control in configuration.Controls)
            {
                this.Controls.Add(control);
            }
        }

        public ObservableCollection<Control> Controls { get; private set; }
    }
}
