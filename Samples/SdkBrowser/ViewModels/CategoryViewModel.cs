﻿using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SDKBrowserMaui.ViewModels
{
    public class CategoryViewModel : SearchViewModel
    {
        public CategoryViewModel(Category category)
        {
            this.IsBackVisible = true;
            this.HeaderTitle = String.Concat(category.Title, " - ", category.Control.Name);
            this.Examples = new ObservableCollection<Example>();

            foreach (var example in category.Examples)
            {
                this.Examples.Add(example);
            }
        }

        public ObservableCollection<Example> Examples { get; private set; }

        public void NavigateTo(Example example)
        {
            if (example == null)
            {
                return;
            }

            var navigationService = DependencyService.Get<INavigationService>();
            navigationService.NavigateToAsync<ExampleViewModel>(example);
        }
    }
}
