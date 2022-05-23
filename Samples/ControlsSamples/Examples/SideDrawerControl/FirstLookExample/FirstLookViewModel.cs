using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace QSF.Examples.SideDrawerControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private string selectedCategory;
    private Func<object, bool> filterCondition;

    public FirstLookViewModel()
    {
        this.Items = new ObservableCollection<Item>
        {
            new Item{ Category = "Editors", Title = "DatePicker", Icon = "datepicker.png" },
            new Item{ Category = "Editors", Title = "Entry", Icon = "entry.png" },
            new Item{ Category = "Editors", Title = "ListPicker", Icon = "listpicker.png" },
            new Item{ Category = "Editors", Title = "MaskedEntry", Icon = "maskedentry.png" },
            new Item{ Category = "Editors", Title = "NumericInput", Icon = "numericinput.png" },
            new Item{ Category = "Editors", Title = "TemplatedPicker", Icon = "templatedpicker.png" },
            new Item{ Category = "Editors", Title = "TimePicker", Icon = "timepicker.png" },
            new Item{ Category = "Editors", Title = "TimeSpanPicker", Icon = "timespanpicker.png" },
            new Item{ Category = "Data Controls", Title = "DataGrid", Icon = "datagrid.png" },
            new Item{ Category = "Data Controls", Title = "ListView", Icon = "listview.png" },
            new Item{ Category = "Data Controls", Title = "ItemsControl", Icon = "itemscontrol.png" },
            new Item{ Category = "Data Visualization", Title = "Barcode", Icon = "barcode.png" },
            new Item{ Category = "Data Visualization", Title = "Charts", Icon = "chartbar.png" },
            new Item{ Category = "Data Visualization", Title = "Gauge", Icon = "gauge.png" },
            new Item{ Category = "Data Visualization", Title = "Map", Icon = "map.png" },
            new Item{ Category = "Data Visualization", Title = "Rating", Icon = "rating.png" },
            new Item{ Category = "Navigation and Layouts", Title = "DockLayout", Icon = "docklayout.png" },
            new Item{ Category = "Navigation and Layouts", Title = "SideDrawer", Icon = "sidedrawer.png" },
            new Item{ Category = "Navigation and Layouts", Title = "TabView", Icon = "tabview.png" },
            new Item{ Category = "Navigation and Layouts", Title = "WrapLayout", Icon = "wraplayout.png" },
            new Item{ Category = "Buttons", Title = "Button", Icon = "button.png" },
            new Item{ Category = "Buttons", Title = "Checkbox", Icon = "checkbox.png" },
            new Item{ Category = "Buttons", Title = "SegmentedControl", Icon = "segmentedcontrol.png" },
            new Item{ Category = "Interactivity and UX", Title = "BadgeView", Icon = "badgeview.png" },
            new Item{ Category = "Interactivity and UX", Title = "Border", Icon = "border.png" },
            new Item{ Category = "Interactivity and UX", Title = "BusyIndicator", Icon = "busyindicator.png" },
            new Item{ Category = "Interactivity and UX", Title = "Path", Icon = "path.png" },
            new Item{ Category = "Interactivity and UX", Title = "Popup", Icon = "popup.png" }

        };

        this.Categories = new ObservableCollection<string>
        {
            "Editors",
            "Data Controls",
            "Data Visualization",
            "Navigation and Layouts",
            "Buttons",
            "Interactivity and UX"
        };

        this.SelectedCategory = this.Categories.FirstOrDefault();

        this.NavigateToProductPageCommand = new Command(() => {
            Launcher.OpenAsync("https://www.telerik.com/maui-ui");
        });
    }

    public string SelectedCategory
    {
        get { return this.selectedCategory; }
        set
        {
            if (this.selectedCategory != value)
            {
                this.selectedCategory = value;
                this.OnPropertyChanged();
                this.OnCategoryChanged();
            }
        }
    }

    public Command NavigateToProductPageCommand { get; private set; }

    public ObservableCollection<Item> Items { get; set; }

    public ObservableCollection<string> Categories { get; set; }

    public Func<object, bool> FilterCondition
    {
        get { return this.filterCondition; }
        private set
        {
            if (this.filterCondition != value)
            {
                this.filterCondition = value;
                this.OnPropertyChanged();
            }
        }
    }
    
    private void OnCategoryChanged()
    {
        var filterCategory = this.selectedCategory;

        this.FilterCondition = value =>
        {
            var recipe = (Item)value;

            return recipe.Category == filterCategory;
        };
    }
}
