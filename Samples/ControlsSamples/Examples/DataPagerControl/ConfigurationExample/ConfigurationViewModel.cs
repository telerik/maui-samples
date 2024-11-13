using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui.Controls.DataPager;

namespace QSF.Examples.DataPagerControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private int minNumericButtonsCount = 3;
    private int maxNumericButtonsCount = 7;
    private DataPagerEllipsisMode ellipsisMode = DataPagerEllipsisMode.After;
    private double itemSpacing = 16;
    private DataPagerDisplayMode displayMode;
    private bool isFirstPageButtonDisplayed = true;
    private bool isPrevPageButtonDisplayed = true;
    private bool areNumericButtonsDisplayed = true;
    private bool isNavigationComboBoxDisplayed = true;
    private bool isNextPageButtonDisplayed = true;
    private bool isLastPageButtonDisplayed = true;
    private bool isPageSizesViewDisplayed = true;
    private bool isNavigationViewDisplayed = true;
    private bool isFirstPageButtonDisplayEnabled = true;
    private bool isPrevPageButtonDisplayEnabled = true;
    private bool areNumericButtonsDisplayEnabled = true;
    private bool isNavigationComboBoxDisplayEnabled = true;
    private bool isNextPageButtonDisplayEnabled = true;
    private bool isLastPageButtonDisplayEnabled = true;
    private bool isPageSizesViewDisplayEnabled = true;
    private bool isNavigationViewDisplayEnabled = true;
    private bool isNoneDisplayed = false;

    public ConfigurationViewModel()
    {
        this.displayMode = DataPagerDisplayMode.FirstPageButton |
            DataPagerDisplayMode.PrevPageButton |
            DataPagerDisplayMode.NumericButtons |
            DataPagerDisplayMode.NavigationComboBox |
            DataPagerDisplayMode.NextPageButton |
            DataPagerDisplayMode.LastPageButton |
            DataPagerDisplayMode.PageSizesView |
            DataPagerDisplayMode.NavigationView;
    }

    public IEnumerable<int> Source { get; } = Enumerable.Range(1, 100).ToList();

    public IEnumerable<DataPagerEllipsisMode> EllipsisModes { get; } = Enum.GetValues(typeof(DataPagerEllipsisMode)).Cast<DataPagerEllipsisMode>();

    public IEnumerable<DataPagerDisplayMode> DisplayModes { get; } = Enum.GetValues(typeof(DataPagerDisplayMode)).Cast<DataPagerDisplayMode>();

    public int MinNumericButtonsCount
    {
        get => this.minNumericButtonsCount;
        set => this.UpdateValue(ref this.minNumericButtonsCount, value);
    }

    public int MaxNumericButtonsCount
    {
        get => this.maxNumericButtonsCount;
        set => this.UpdateValue(ref this.maxNumericButtonsCount, value);
    }

    public DataPagerEllipsisMode EllipsisMode
    {
        get => this.ellipsisMode;
        set => this.UpdateValue(ref this.ellipsisMode, value);
    }

    public double ItemSpacing
    {
        get => this.itemSpacing;
        set => this.UpdateValue(ref this.itemSpacing, value);
    }

    public DataPagerDisplayMode DisplayMode
    {
        get => this.displayMode;
        set => this.UpdateValue(ref this.displayMode, value);
    }

    public bool IsFirstPageButtonDisplayed
    {
        get => this.isFirstPageButtonDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isFirstPageButtonDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }

    public bool IsPrevPageButtonDisplayed
    {
        get => this.isPrevPageButtonDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isPrevPageButtonDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }
    
    public bool AreNumericButtonsDisplayed
    {
        get => this.areNumericButtonsDisplayed;
        set
        {
            if (this.UpdateValue(ref this.areNumericButtonsDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }
    
    public bool IsNavigationComboBoxDisplayed
    {
        get => this.isNavigationComboBoxDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isNavigationComboBoxDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }
    
    public bool IsNextPageButtonDisplayed
    {
        get => this.isNextPageButtonDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isNextPageButtonDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }
    
    public bool IsLastPageButtonDisplayed
    {
        get => this.isLastPageButtonDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isLastPageButtonDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }

    public bool IsPageSizesViewDisplayed
    {
        get => this.isPageSizesViewDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isPageSizesViewDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }

    public bool IsNavigationViewDisplayed
    {
        get => this.isNavigationViewDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isNavigationViewDisplayed, value))
            {
                this.UpdateDisplayMode();
            }
        }
    }

    public bool IsFirstPageButtonDisplayEnabled
    {
        get => this.isFirstPageButtonDisplayEnabled;
        set => this.UpdateValue(ref this.isFirstPageButtonDisplayEnabled, value);
    }

    public bool IsPrevPageButtonDisplayEnabled
    {
        get => this.isPrevPageButtonDisplayEnabled;
        set => this.UpdateValue(ref this.isPrevPageButtonDisplayEnabled, value);
    }

    public bool AreNumericButtonsDisplayEnabled
    {
        get => this.areNumericButtonsDisplayEnabled;
        set => this.UpdateValue(ref this.areNumericButtonsDisplayEnabled, value);
    }

    public bool IsNavigationComboBoxDisplayEnabled
    {
        get => this.isNavigationComboBoxDisplayEnabled;
        set => this.UpdateValue(ref this.isNavigationComboBoxDisplayEnabled, value);
    }

    public bool IsNextPageButtonDisplayEnabled
    {
        get => this.isNextPageButtonDisplayEnabled;
        set => this.UpdateValue(ref this.isNextPageButtonDisplayEnabled, value);
    }

    public bool IsLastPageButtonDisplayEnabled
    {
        get => this.isLastPageButtonDisplayEnabled;
        set => this.UpdateValue(ref this.isLastPageButtonDisplayEnabled, value);
    }

    public bool IsPageSizesViewDisplayEnabled
    {
        get => this.isPageSizesViewDisplayEnabled;
        set => this.UpdateValue(ref this.isPageSizesViewDisplayEnabled, value);
    }

    public bool IsNavigationViewDisplayEnabled
    {
        get => this.isNavigationViewDisplayEnabled;
        set => this.UpdateValue(ref this.isNavigationViewDisplayEnabled, value);
    }

    public bool IsNoneDisplayed
    {
        get => this.isNoneDisplayed;
        set
        {
            if (this.UpdateValue(ref this.isNoneDisplayed, value))
            {
                this.UpdateDisplayMode();

                this.IsFirstPageButtonDisplayEnabled = !value;
                this.IsPrevPageButtonDisplayEnabled = !value;
                this.AreNumericButtonsDisplayEnabled = !value;
                this.IsNavigationComboBoxDisplayEnabled = !value;
                this.IsNextPageButtonDisplayEnabled = !value;
                this.IsLastPageButtonDisplayEnabled = !value;
                this.IsPageSizesViewDisplayEnabled = !value;
                this.IsNavigationViewDisplayEnabled = !value;
            }
        }
    }

    private void UpdateDisplayMode()
    {
        if (this.IsNoneDisplayed)
        {
            this.DisplayMode = DataPagerDisplayMode.None;
        }
        else
        {
            var mode = DataPagerDisplayMode.None;

            if (this.isFirstPageButtonDisplayed)
            {
                mode |= DataPagerDisplayMode.FirstPageButton;
            }

            if (this.isPrevPageButtonDisplayed)
            {
                mode |= DataPagerDisplayMode.PrevPageButton;
            }

            if (this.areNumericButtonsDisplayed)
            {
                mode |= DataPagerDisplayMode.NumericButtons;
            }

            if (this.isNavigationComboBoxDisplayed)
            {
                mode |= DataPagerDisplayMode.NavigationComboBox;
            }

            if (this.isNextPageButtonDisplayed)
            {
                mode |= DataPagerDisplayMode.NextPageButton;
            }

            if (this.isLastPageButtonDisplayed)
            {
                mode |= DataPagerDisplayMode.LastPageButton;
            }

            if (this.isPageSizesViewDisplayed)
            {
                mode |= DataPagerDisplayMode.PageSizesView;
            }

            if (this.isNavigationViewDisplayed)
            {
                mode |= DataPagerDisplayMode.NavigationView;
            }

            this.DisplayMode = mode;
        }
    }
}