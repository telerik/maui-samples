using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models;

#if ANDROID || IOS
using PlatformViews = TelerikCRM.Maui.Views.Mobile;
#else
using PlatformViews = TelerikCRM.Maui.Views.Desktop;
#endif

namespace TelerikCRM.Maui.ViewModels;

public class MainPageViewModel : ViewModelBase
{

#if MACCATALYST || WINDOWS
    private PageModel selectedPage;
#endif

    public MainPageViewModel()
    {
        this.Pages = new()
        {
            new PageModel
            {
                Title = "Employees",
                Type = typeof(PlatformViews.EmployeesView)
            },
            new PageModel
            {
                Title = "Customers",
                Type = typeof(PlatformViews.CustomersView)
            },
            new PageModel
            {
                Title = "Products",
                Type = typeof(PlatformViews.ProductsView)
            },
#if !(ANDROID || IOS)
            new PageModel
            {
                Title = "Orders",
                Type = typeof(PlatformViews.OrdersView)
            },
            new PageModel
            {
                Title = "Shipping",
                Type = typeof(PlatformViews.ShippingView)
            },
#else
            new PageModel
            {
                Title = "More",
                Type = typeof(PlatformViews.MoreView)
            },
#endif
        };

#if MACCATALYST || WINDOWS
        this.SelectedPage = this.Pages.FirstOrDefault();
#endif
    }

    public ObservableCollection<PageModel> Pages { get; private set; }

#if MACCATALYST || WINDOWS
    public PageModel SelectedPage
    {
        get => this.selectedPage;
        set => this.UpdateValue(ref this.selectedPage, value);
    }
#endif
}