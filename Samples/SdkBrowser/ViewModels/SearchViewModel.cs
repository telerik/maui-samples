using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Data;

namespace SDKBrowserMaui.ViewModels
{
    public class SearchViewModel : ExamplesViewModelBase
    {
        private string searchText;
        private string log;
        private CustomFilter filter;
        private readonly DelegateFilterDescriptor filterDescriptor = new DelegateFilterDescriptor();

        public SearchViewModel()
        { 
            this.searchText = string.Empty;
        }

        public string SearchText
        {
            get
            {
                return this.searchText;
            }
            set
            {
                if (this.searchText != value)
                {
                    this.searchText = value;
                    this.OnPropertyChanged();
                    this.OnSearchChanged();
                }
            }
        }

        public string Log
        {
            get
            {
                return this.log;
            }
            set
            {
                if (this.log != value)
                {
                    this.log = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FilterDescriptorCollection FilterDescriptors { get; set; }

        private void OnSearchChanged()
        {
            if (this.IsSpecialSearch())
            {
                this.NavigateToExampleOrApplyThemeBySearchText();
            }

            var tokens = this.SearchText.Split(default(char[]),
                StringSplitOptions.RemoveEmptyEntries);

            if (this.FilterDescriptors == null)
            {
                return;
            }

            this.filterDescriptor.Filter = new CustomFilter(tokens);

            if (!this.FilterDescriptors.Contains(this.filterDescriptor))
            {
                this.FilterDescriptors.Add(this.filterDescriptor);
            }
        }

        private bool IsSpecialSearch()
        {
            return !string.IsNullOrEmpty(this.searchText)
                && this.searchText.Contains(".")
                && this.searchText.StartsWith("#")
                && this.searchText.EndsWith("#");
        }

        private async Task NavigateToExampleOrApplyThemeBySearchText()
        {
            string trimmedText = this.searchText.Substring(1, this.searchText.Length - 2);
            List<string> searchTextFragments = trimmedText.Split(".").ToList<string>();

            var count = searchTextFragments.Count;
            if (count == 2)
            {
                var prefix = searchTextFragments[0];
                if (prefix.Contains(":"))
                {
                    string theme = prefix.Split(':').Last();
                    string swatch = searchTextFragments[1];

                    var themeViewModel = DependencyService.Get<ThemeSettingsViewModel>();
                    if (themeViewModel != null)
                    {
                        var definition = themeViewModel.ThemesCatalog.FirstOrDefault(t => t.Theme == theme && t.Swatch == swatch);
                        if (definition != null)
                        {
                            themeViewModel.CurrentTheme = definition;
                            this.Log = $"{theme}.{swatch}";
                        }
                    }
                }
                else
                {
                    string controlName = searchTextFragments[0];
                    string exampleName = searchTextFragments[1];

                    var backdoorService = DependencyService.Get<IBackdoorService>();
                    await backdoorService.NavigateToExampleAsync(controlName, exampleName);
                }
            }
        }
    }

    public class CustomGroupSearchResults : IKeyLookup
    {
        public object GetKey(object item)
        {
            var example = (Example)item;
            var category = example.Category;
            var control = category.Control;

            return control.Title;
        }
    }

    public class CustomFilter : IFilter
    {
        private string[] tokens;

        public CustomFilter(string[] tokens)
        {
            this.tokens = tokens;
        }

        public bool PassesFilter(object item)
        {
            var example = (Example)item;
            var category = example.Category;
            var control = category.Control;
            var comparison = StringComparison.OrdinalIgnoreCase;

            return this.tokens.All(token =>
                example.Name.IndexOf(token, comparison) >= 0 ||
                example.Title.IndexOf(token, comparison) >= 0 ||
                control.Name.IndexOf(token, comparison) >= 0 ||
                control.Title.IndexOf(token, comparison) >= 0);
        }
    }
}
