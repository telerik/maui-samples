using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Pages;
using Microsoft.Maui.Controls;
using SDKBrowserMaui.ViewModels;

namespace SDKBrowserMaui.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation navigation;

        public NavigationService()
        {
            this.navigation = Application.Current.Windows[0].Page.Navigation;
        }

        public Task NavigateToAsync<TViewModel>(params object[] arguments)
        {
            var page = this.CreatePage<TViewModel>(arguments);

            return this.navigation.PushAsync(page);
        }

        public async Task NavigateToExampleAsync<TViewModel>(Example example, bool popToMain = false, bool isAnimated = true)
        {
            var page = this.CreatePage<TViewModel>(example);

            await this.navigation.PushAsync(page, isAnimated);

            if (popToMain)
            {
                // Purge the navigation stack...
                for (int i = 1; i < this.navigation.NavigationStack.Count - 1; i++)
                {
                    this.navigation.RemovePage(this.navigation.NavigationStack[i]);
                }
            }
        }

        public Task NavigateToRootAsync(bool isAnimated = true)
        {
            return this.navigation.PopToRootAsync(isAnimated);
        }

        public Task NavigateBackAsync(bool isAnimated = true)
        {
            return this.navigation.PopAsync(isAnimated);
        }

        private Page CreatePage<TViewModel>(params object[] arguments)
        {
            var viewModelType = typeof(TViewModel);
            var viewModelName = viewModelType.FullName;
            var viewName = viewModelName.Replace("ViewModel", "Page");
            var viewType = Type.GetType(viewName);
            Page view;
            {
                view = (Page)Activator.CreateInstance(viewType);
            }
            var viewModel = Activator.CreateInstance(viewModelType, arguments);

            view.BindingContext = viewModel;

            return view;
        }
    }
}

