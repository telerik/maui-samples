using System;
using System.Collections;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.SelectionCategory;
public class ViewModel : NotifyPropertyChangedBase
{
    private DataGridSelectionMode selectedMode;
    private DataGridSelectionUnit selectedUnit;   
    private bool isSelectAllEnabled;

    public ViewModel()
    {           
        this.GridSource = new ObservableCollection<Person>()
        {
            new Person { Name = "Kiko", Age = 23, Department = "Production" },
            new Person { Name = "Jerry", Age = 23, Department = "Accounting & Finance"},
            new Person { Name = "Ethan", Age = 51, Department = "Marketing" },
            new Person { Name = "Isabella", Age = 25, Department = "Marketing" },
            new Person { Name = "Joshua", Age = 45, Department = "Production" },
            new Person { Name = "Logan", Age = 26, Department = "Production"},
            new Person { Name = "Aaron", Age = 32, Department = "Production" },
            new Person { Name = "Elena", Age = 37, Department = "Accounting & Finance"},
            new Person { Name = "Ross", Age = 30, Department = "Marketing" }
        };

        this.SelectionModeSource = Enum.GetValues(typeof(DataGridSelectionMode));
        this.SelectionUnitSource = Enum.GetValues(typeof(DataGridSelectionUnit));
        
        this.SelectedMode = DataGridSelectionMode.Single;
        this.SelectedUnit = DataGridSelectionUnit.Cell;
    }

    public ObservableCollection<Person> GridSource { get; set; }
    public IList SelectionModeSource { get; set; }
    public IList SelectionUnitSource { get; set; }

    public DataGridSelectionMode SelectedMode
    {
        get
        {
            return this.selectedMode;
        }
        set
        {
            if (this.selectedMode != value)
            {
                this.selectedMode = value;
                this.IsSelectAllEnabled = this.selectedMode == DataGridSelectionMode.Multiple;
                this.OnPropertyChanged("SelectedMode");
            }
        }
    }

    public DataGridSelectionUnit SelectedUnit
    {
        get
        {
            return this.selectedUnit;
        }
        set
        {
            if (this.selectedUnit != value)
            {
                this.selectedUnit = value;
                this.OnPropertyChanged("SelectedUnit");
            }
        }
    }

    public bool IsSelectAllEnabled
    {
        get
        {
            return this.isSelectAllEnabled;
        }
        set
        {
            if (this.isSelectAllEnabled != value)
            {
                this.isSelectAllEnabled = value;
                this.OnPropertyChanged("IsSelectAllEnabled");
            }
        }
    }
}