using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models;

namespace TelerikCRM.Maui.ViewModels;

public class WelcomeViewModel : ViewModelBase
{
    public ObservableCollection<WelcomeCard> WelcomeCards { get; } = new()
    {
        new WelcomeCard
        {
            Title = ViewModelBase.IsMobile ? "Telerik CRM" : "Welcome",
            Subtitle = "Welcome to the Telerik .NET MAUI CRM!\nThis is quick intro that will familiarize you\nwith the main features of the app.",
            IconSource = ImageSource.FromFile("onboarding.png")
        },
        new WelcomeCard
        {
            Title = "Employees",
            Subtitle = "Explore & manage employees.\nDrill down into employee details.\nMonitor Sales & Orders statistics.",
            IconSource = ImageSource.FromFile("onboarding_employees.png")
        },
        new WelcomeCard
        {
            Title = "Customers",
            Subtitle = "Drill down into customer details.\nExplore full history of related orders.\nReview order details.",
            IconSource = ImageSource.FromFile("onboarding_customers.png")
        },
        new WelcomeCard
        {
            Title = "Products",
            Subtitle = "Explore & manage products.\nDrill down into relevant details\n- stock quantity, price, etc.",
            IconSource = ImageSource.FromFile("onboarding_products.png")
        },
        new WelcomeCard
        {
            Title = "Orders",
            Subtitle = "Handle orders like a pro with the DataGrid.\nShowcase the related entities.\nExport the data in a convenient format.",
            IconSource = ImageSource.FromFile("onboarding_orders.png")
        },
    };
}