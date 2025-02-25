﻿using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SDKBrowserMaui.ViewModels
{
    public class ControlViewModel : SearchViewModel
    {
        public ControlViewModel(Control control)
        {
            this.IsBackVisible = true;
            this.HeaderTitle = control.Title;
            this.Categories = new ObservableCollection<Category>();
            this.Examples = new ObservableCollection<Example>();

            foreach (var category in control.Categories)
            {
                this.Categories.Add(category);

                foreach (var example in category.Examples)
                {
                    this.Examples.Add(example);
                }
            }
        }

        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableCollection<Example> Examples { get; private set; }

        public void NavigateTo(Category category)
        {
            if (category == null)
            {
                return;
            }

            var navigationService = DependencyService.Get<INavigationService>();

            if (category.Examples.Count == 1)
            {
                var example = category.Examples[0];
                navigationService.NavigateToAsync<ExampleViewModel>(example);
            }
            else if (category.Examples.Count > 0)
            {
                navigationService.NavigateToAsync<CategoryViewModel>(category);
            }
        }
    }
}
