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
            new Item{ Category = "Editors", Title = "DatePicker" },
            new Item{ Category = "Editors", Title = "Entry" },
            new Item{ Category = "Editors", Title = "ListPicker" },
            new Item{ Category = "Editors", Title = "MaskedEntry" },
            new Item{ Category = "Editors", Title = "NumericInput" },
            new Item{ Category = "Editors", Title = "TemplatedPicker" },
            new Item{ Category = "Editors", Title = "TimePicker" },
            new Item{ Category = "Editors", Title = "TimeSpanPicker" },
            new Item{ Category = "Data Controls", Title = "DataGrid" },
            new Item{ Category = "Data Controls", Title = "ListView" },
            new Item{ Category = "Data Controls", Title = "ItemsControl" },
            new Item{ Category = "Data Visualization", Title = "Barcode" },
            new Item{ Category = "Data Visualization", Title = "Chart" },
            new Item{ Category = "Data Visualization", Title = "Gauge" },
            new Item{ Category = "Data Visualization", Title = "Map" },
            new Item{ Category = "Data Visualization", Title = "Rating" },
            new Item{ Category = "Navigation and Layouts", Title = "DockLayout" },
            new Item{ Category = "Navigation and Layouts", Title = "SideDrawer" },
            new Item{ Category = "Navigation and Layouts", Title = "TabView" },
            new Item{ Category = "Navigation and Layouts", Title = "WrapLayout" },
            new Item{ Category = "Buttons", Title = "Button" },
            new Item{ Category = "Buttons", Title = "CheckBox" },
            new Item{ Category = "Buttons", Title = "SegmentedControl" },
            new Item{ Category = "Interactivity and UX", Title = "BadgeView" },
            new Item{ Category = "Interactivity and UX", Title = "Border" },
            new Item{ Category = "Interactivity and UX", Title = "BusyIndicator" },
            new Item{ Category = "Interactivity and UX", Title = "Path" },
            new Item{ Category = "Interactivity and UX", Title = "Popup" }
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
