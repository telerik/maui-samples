using SDKBrowser.Examples.DataGridControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Compatibility.DataGrid;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.DecorationsCategory.GridLinesExample;
public class ViewModel : NotifyPropertyChangedBase
{
    private string gridLinesColor;
    private GridLinesVisibility gridLinesVisibility;
    private double gridLinesThickness;

    public ViewModel()
    {
        var source = new ObservableCollection<Person>();
        source.Add(new Person() { Name = "Kiko", Age = 23, Gender = Gender.Other });
        source.Add(new Person() { Name = "Jerry", Age = 23, Gender = Gender.Male });
        source.Add(new Person() { Name = "Ethan", Age = 51, Gender = Gender.Male });
        source.Add(new Person() { Name = "Isabella", Age = 23, Gender = Gender.Female });
        source.Add(new Person() { Name = "Joshua", Age = 51, Gender = Gender.Male });
        source.Add(new Person() { Name = "Logan", Age = 51, Gender = Gender.Female });
        source.Add(new Person() { Name = "Aaron", Age = 23, Gender = Gender.Other });

        this.GridSource = source;

        this.LinesThicknessPickerSource = new List<double>() { 1, 2, 3 };
        this.LinesColorPickerSource = new List<string>() { "Gray", "Red", "Green" };
        this.LinesVisibilityPickerSource = Enum.GetValues(typeof(GridLinesVisibility));

        this.GridLinesVisibility = GridLinesVisibility.Both;
        this.GridLinesColor = "Gray";
        this.GridLinesThickness = 1d;
    }

    public ObservableCollection<Person> GridSource { get; set; }

    public IList LinesThicknessPickerSource { get; set; }

    public IList LinesColorPickerSource { get; set; }

    public IList LinesVisibilityPickerSource { get; set; }

    public string GridLinesColor
    {
        get
        {
            return this.gridLinesColor;
        }
        set
        {
            this.gridLinesColor = value;
            this.OnPropertyChanged("GridLinesColor");
        }
    }

    public double GridLinesThickness
    {
        get
        {
            return this.gridLinesThickness;
        }
        set
        {
            this.gridLinesThickness = value;
            this.OnPropertyChanged("GridLinesThickness");
        }
    }

    public GridLinesVisibility GridLinesVisibility
    {
        get
        {
            return this.gridLinesVisibility;
        }
        set
        {
            this.gridLinesVisibility = value;
            this.OnPropertyChanged("GridLinesVisibility");
        }
    }
}
