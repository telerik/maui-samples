using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace QSF.Examples.AutoCompleteControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private bool isClearButtonVisible = true;
    private string noResultsMessage;
    private string placeholder;
    private int searchThreshold;
    private bool showSuggestionView = true;
    private AutoCompletePopupPosition popupPosition;
    private AutoCompleteCompletionMode completionMode;
    private AutoCompleteSuggestMode suggestMode = AutoCompleteSuggestMode.Suggest;
    private AutoCompleteDisplayMode displayMode;

    public ConfigurationViewModel()
    {
        this.Cities = new ObservableCollection<City>()
        {
            new City("New York", "USA"),
            new City("Tokyo", "Japan"),
            new City("Sofia", "Bulgaria"),
            new City("Paris", "France"),
            new City("London", "UK"),
        };

        this.Placeholder = "Find City";
        this.NoResultsMessage = "Not Found.";
    }

    public ObservableCollection<City> Cities { get; set; }

    public bool IsClearButtonVisible
    {
        get => this.isClearButtonVisible;
        set => this.UpdateValue(ref this.isClearButtonVisible, value);
    }

    public string Placeholder
    {
        get => this.placeholder;
        set => this.UpdateValue(ref this.placeholder, value);
    }

    public string NoResultsMessage
    {
        get => this.noResultsMessage;
        set => this.UpdateValue(ref this.noResultsMessage, value);
    }

    public int SearchThreshold
    {
        get => this.searchThreshold;
        set => this.UpdateValue(ref this.searchThreshold, value);
    }

    public bool ShowSuggestionView
    {
        get => this.showSuggestionView;
        set => this.UpdateValue(ref this.showSuggestionView, value);
    }

    public AutoCompletePopupPosition PopupPosition
    {
        get => this.popupPosition;
        set => this.UpdateValue(ref this.popupPosition, value);
    }

    public AutoCompleteCompletionMode CompletionMode
    {
        get => this.completionMode;
        set => this.UpdateValue(ref this.completionMode, value);
    }

    public AutoCompleteSuggestMode SuggestMode
    {
        get => this.suggestMode;
        set => this.UpdateValue(ref this.suggestMode, value);
    }

    public AutoCompleteDisplayMode DisplayMode
    {
        get => this.displayMode;
        set => this.UpdateValue(ref this.displayMode, value);
    }
}
