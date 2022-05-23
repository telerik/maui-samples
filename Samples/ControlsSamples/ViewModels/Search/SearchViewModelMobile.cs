using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Services;
using System.Linq;

namespace QSF.ViewModels;

public class SearchViewModelMobile : PageViewModel
{
    private HighlightedSearchResult selectedSearchResult;

    public HighlightedSearchResult SelectedSearchResult
    {
        get
        {
            return this.selectedSearchResult;
        }
        set
        {
            if (this.UpdateValue(ref this.selectedSearchResult, value))
            {
                this.OnSelectedSearchResultChanged();
            }
        }
    }

    private void OnSelectedSearchResultChanged()
    {
        HighlightedSearchResult searchResult = this.selectedSearchResult;
        if (searchResult == null)
        {
            return;
        }

        IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
        Configuration configuration = configurationService.Configuration;

        Control control = configuration.Controls?.FirstOrDefault(c => c.Name == searchResult.ControlName);
        if (control == null)
        {
            return;
        }

        Example example = control.Examples?.FirstOrDefault(e => e.Name == searchResult.ExampleName);
        if (example != null)
        {
            this.NavigationService.NavigateToExampleAsync(example);
        }
        else
        {
            this.NavigationService.NavigateToAsync<ControlViewModel>(control);
        }
    }
}
