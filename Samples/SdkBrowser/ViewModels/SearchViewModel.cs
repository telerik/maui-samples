using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKBrowserMaui.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using SDKBrowserMaui.Common;

namespace SDKBrowserMaui.ViewModels
{
    public class SearchViewModel : ExamplesViewModelBase
    {
        private string searchText;
        private string log;
        private Func<object, bool> searchFilter;
        private Func<object, object> searchGrouping;

        public SearchViewModel()
        { 
            this.SearchFilter = item => this.PassesFilter(item);
            this.SearchGrouping = item => this.ExtractGroup(item);
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

        public Func<object, bool> SearchFilter
        {
            get
            {
                return this.searchFilter;
            }
            private set
            {
                if (this.searchFilter != value)
                {
                    this.searchFilter = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Func<object, object> SearchGrouping
        {
            get
            {
                return this.searchGrouping;
            }
            private set
            {
                if (this.searchGrouping != value)
                {
                    this.searchGrouping = value;
                    this.OnPropertyChanged();
                }
            }
        }

        protected virtual bool PassesFilter(object item, params string[] tokens)
        {
            var example = (Example)item;
            var category = example.Category;
            var control = category.Control;
            var comparison = StringComparison.OrdinalIgnoreCase;

            return tokens.All(token =>
                example.Name.IndexOf(token, comparison) >= 0 ||
                example.Title.IndexOf(token, comparison) >= 0 ||
                control.Name.IndexOf(token, comparison) >= 0 ||
                control.Title.IndexOf(token, comparison) >= 0);
        }

        protected virtual object ExtractGroup(object item)
        {
            var example = (Example)item;
            var category = example.Category;
            var control = category.Control;

            return control.Title;
        }

        private void OnSearchChanged()
        {
            if (this.IsSpecialSearch())
            {
                this.NavigateToExampleOrApplyThemeBySearcText();
            }

            var tokens = this.SearchText.Split(default(char[]),
                StringSplitOptions.RemoveEmptyEntries);

            this.SearchFilter = item => this.PassesFilter(item, tokens);
        }

        private bool IsSpecialSearch()
        {
            return !string.IsNullOrEmpty(this.searchText)
                && this.searchText.Contains(".")
                && this.searchText.StartsWith("#")
                && this.searchText.EndsWith("#");
        }

        private void NavigateToExampleOrApplyThemeBySearcText()
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
                    backdoorService.TryNavigateToExample(controlName, exampleName);
                }
            }
        }
    }
}
