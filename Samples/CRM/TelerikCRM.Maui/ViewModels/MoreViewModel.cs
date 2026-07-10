#if ANDROID || IOS
using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class MoreViewModel : ViewModelBase
{
    public MoreViewModel()
    {
        this.Pages = new()
        {
            new PageModel
            {
                Title = "Orders",
                Type = typeof(OrdersViewModel)
            },
            new PageModel
            {
                Title = "Shipping",
                Type = typeof(ShippingViewModel)
            },
            new PageModel
            {
                Title = "About",
                Type = typeof(AboutViewModel)
            },
        };

        this.ItemTapCommand = new Command(this.ItemTapped);
        this.SearchCommand = new Command(this.SearchCommandExecuted);
    }

    public ObservableCollection<PageModel> Pages { get; private set; }

    public Command ItemTapCommand { get; set; }

    private async void ItemTapped(object item)
    {
        if (item is PageModel pageModel)
        {
            var service = this.services.GetService<INavigationService>();

            var type = pageModel.Type;
            if (type == typeof(OrdersViewModel))
            {
                await service.NavigateToAsync<OrdersViewModel>();
            }
            else if (type == typeof(ShippingViewModel))
            {
                await service.NavigateToAsync<ShippingViewModel>();   
            }
            else if (type == typeof(AboutViewModel))
            {
                await service.NavigateToAsync<AboutViewModel>();
            }
        }
    }
}
#endif