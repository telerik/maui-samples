using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls.Data;

namespace QSF.Examples.SideDrawerControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private string selectedCategory;
    private FilterCategories filter;
    private readonly DelegateFilterDescriptor filterDescriptor = new DelegateFilterDescriptor();


    public FirstLookViewModel()
    {
        this.Items = new ObservableCollection<Item>
        {
            new Item{ Category = "Editors", Title = "AutoComplete" },
            new Item{ Category = "Editors", Title = "ComboBox" },
            new Item{ Category = "Editors", Title = "DatePicker" },
            new Item{ Category = "Editors", Title = "DateTimePicker" },
            new Item{ Category = "Editors", Title = "Editor" },
            new Item{ Category = "Editors", Title = "Entry" },
            new Item{ Category = "Editors", Title = "ListPicker" },
            new Item{ Category = "Editors", Title = "MaskedEntry" },
            new Item{ Category = "Editors", Title = "NumericInput" },
            new Item{ Category = "Editors", Title = "RangeSlider" },
            new Item{ Category = "Editors", Title = "RichTextEditor" },
            new Item{ Category = "Editors", Title = "TemplatedPicker" },
            new Item{ Category = "Editors", Title = "TimePicker" },
            new Item{ Category = "Editors", Title = "TimeSpanPicker" },
            new Item{ Category = "Editors", Title = "SignaturePad" },
            new Item{ Category = "Editors", Title = "Slider" },
            new Item{ Category = "Data Controls", Title = "CollectionView" },
            new Item{ Category = "Data Controls", Title = "DataForm" },
            new Item{ Category = "Data Controls", Title = "DataGrid" },
            new Item{ Category = "Data Controls", Title = "DataPager" },
            new Item{ Category = "Data Controls", Title = "ItemsControl" },
            new Item{ Category = "Data Controls", Title = "TreeDataGrid" },
            new Item{ Category = "Data Visualization", Title = "Barcode" },
            new Item{ Category = "Data Visualization", Title = "Chart" },
            new Item{ Category = "Data Visualization", Title = "Gauge" },
            new Item{ Category = "Data Visualization", Title = "Map" },
            new Item{ Category = "Data Visualization", Title = "ProgressBar" },
            new Item{ Category = "Data Visualization", Title = "Rating" },
            new Item{ Category = "Navigation and Layouts", Title = "Accordion" },
            new Item{ Category = "Navigation and Layouts", Title = "BottomSheet" },
            new Item{ Category = "Navigation and Layouts", Title = "DockLayout" },
            new Item{ Category = "Navigation and Layouts", Title = "Expander" },
            new Item{ Category = "Navigation and Layouts", Title = "NavigationView" },
            new Item{ Category = "Navigation and Layouts", Title = "SideDrawer" },
            new Item{ Category = "Navigation and Layouts", Title = "SlideView" },
            new Item{ Category = "Navigation and Layouts", Title = "TabView" },
            new Item{ Category = "Navigation and Layouts", Title = "TreeView" },
            new Item{ Category = "Navigation and Layouts", Title = "WrapLayout" },
            new Item{ Category = "Chatbots", Title = "Chat" },
            new Item{ Category = "Buttons", Title = "Button" },
            new Item{ Category = "Buttons", Title = "CheckBox" },
            new Item{ Category = "Buttons", Title = "SegmentedControl" },
            new Item{ Category = "Buttons", Title = "SpeechToTextButton" },
            new Item{ Category = "Buttons", Title = "TemplatedButton" },
            new Item{ Category = "Buttons", Title = "ToggleButton" },
            new Item{ Category = "Interactivity and UX", Title = "AIPrompt" },
            new Item{ Category = "Interactivity and UX", Title = "BadgeView" },
            new Item{ Category = "Interactivity and UX", Title = "Border" },
            new Item{ Category = "Interactivity and UX", Title = "BusyIndicator" },
            new Item{ Category = "Interactivity and UX", Title = "GridSplitter" },
            new Item{ Category = "Interactivity and UX", Title = "Path" },
            new Item{ Category = "Interactivity and UX", Title = "Popup" },
            new Item{ Category = "Interactivity and UX", Title = "Skeleton" },
            new Item{ Category = "Interactivity and UX", Title = "Toolbar" },
        };

        this.Categories = new ObservableCollection<string>
        {
            "Buttons",
            "Chatbots",
            "Data Controls",
            "Data Visualization",
            "Editors",
            "Interactivity and UX",
            "Navigation and Layouts",
        };

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

    public FilterDescriptorCollection FilterDescriptors { get; set; }

    public Command NavigateToProductPageCommand { get; private set; }

    public ObservableCollection<Item> Items { get; set; }

    public ObservableCollection<string> Categories { get; set; }
    
    private void OnCategoryChanged()
    {
        if (this.FilterDescriptors == null)
        {
            return;
        }

        this.filterDescriptor.Filter = new FilterCategories(this.selectedCategory);

        if (!this.FilterDescriptors.Contains(this.filterDescriptor))
        {
            this.FilterDescriptors.Add(this.filterDescriptor);
        }
    }
}

public class FilterCategories : IFilter
{
    private string filterCategory;

    public FilterCategories(string filterCategory)
    {
        this.filterCategory = filterCategory;
    }

    public bool PassesFilter(object item)
    {
        var category = (Item)item;
        return category.Category == this.filterCategory;
    }
}
