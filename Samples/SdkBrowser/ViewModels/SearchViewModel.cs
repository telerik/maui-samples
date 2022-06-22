using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKBrowserMaui.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using SDKBrowserMaui.Common;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView.Commands;

namespace SDKBrowserMaui.ViewModels
{
    public class SearchViewModel : ExamplesViewModelBase
    {
        private string searchText;
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
                this.NavigateToExampleBySearchedText();
            }

            var tokens = this.SearchText.Split(default(char[]),
                StringSplitOptions.RemoveEmptyEntries);

            this.SearchFilter = item => this.PassesFilter(item, tokens);

            this.NavigateToExampleOnSearch();
        }

        private bool IsSpecialSearch()
        {
            return !string.IsNullOrEmpty(this.searchText)
                && this.searchText.Contains(".")
                && this.searchText.StartsWith("#")
                && this.searchText.EndsWith("#");
        }

        private void NavigateToExampleBySearchedText()
        {
            string trimmedText = this.searchText.Substring(1, this.searchText.Length - 2);
            List<string> searchTextFragments = trimmedText.Split(".").ToList<string>();

            if (searchTextFragments.Count == 2)
            {
                string controlName = searchTextFragments[0];
                string exampleName = searchTextFragments[1];

                var backdoorService = DependencyService.Get<IBackdoorService>();
                backdoorService.TryNavigateToExample(controlName, exampleName);
            }
        }

        private void NavigateToExampleOnSearch()
        {
            var strings = this.SearchText.Split(';');
            if (strings.Length > 1)
            {
                var controlName = strings[0].Trim();
                var exampleName = strings[1].Trim();
                if (exampleName.EndsWith("#"))
                {
                    exampleName = exampleName.Substring(0, exampleName.Length - 1);
                    var backdoorService = DependencyService.Get<IBackdoorService>();
                    if (backdoorService != null)
                    {
                        backdoorService.TryNavigateToExample(controlName, exampleName);
                    }
                }
            }
        }
    }
}
