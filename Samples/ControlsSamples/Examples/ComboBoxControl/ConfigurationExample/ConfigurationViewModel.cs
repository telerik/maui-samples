using System.Collections.ObjectModel;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using Telerik.Maui.Controls;

namespace QSF.Examples.ComboBoxControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private bool isEditable;
    private bool openOnFocus = true;
    private bool isDropDownClosedOnSelection = true;
    private bool isDropDownOpen;
    private bool isClearButtonVisible = true;
    private string placeholder;
    private SearchMode searchMode;
    private ComboBoxSelectionMode selectionMode;

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
    }

    public ObservableCollection<City> Cities { get; set; }

    public bool IsEditable
    {
        get => this.isEditable;
        set => this.UpdateValue(ref this.isEditable, value);
    }

    public bool OpenOnFocus
    {
        get => this.openOnFocus;
        set => this.UpdateValue(ref this.openOnFocus, value);
    }

    public bool IsDropDownClosedOnSelection
    {
        get => this.isDropDownClosedOnSelection;
        set => this.UpdateValue(ref this.isDropDownClosedOnSelection, value);
    }

    public bool IsDropDownOpen
    {
        get => this.isDropDownOpen;
        set => this.UpdateValue(ref this.isDropDownOpen, value);
    }

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

    public SearchMode SearchMode
    {
        get => this.searchMode;
        set => this.UpdateValue(ref this.searchMode, value);
    }

    public ComboBoxSelectionMode SelectionMode
    {
        get => this.selectionMode;
        set => this.UpdateValue(ref this.selectionMode, value);
    }
}