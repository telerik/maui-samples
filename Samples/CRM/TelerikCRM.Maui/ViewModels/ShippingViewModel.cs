using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Scheduler;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class ShippingViewModel : ViewModelBase
{
    private DateTime _calendarDisplayDate = DateTime.Now;
    private ObservableCollection<Appointment> shippingAppointments;

    public ShippingViewModel()
    {
#if !(MACCATALYST || WINDOWS)
        this.CanNavigateBack = true;
        this.NavigateBackContextName = "More";
        this.Title = "Shipping";
#endif
    }

    public ObservableCollection<Appointment> ShippingAppointments
    {
        get => this.shippingAppointments;
        set => this.UpdateValue(ref this.shippingAppointments, value);
    }

    public DateTime CalendarDisplayDate
    {
        get => this._calendarDisplayDate;
        set => this.UpdateValue(ref this._calendarDisplayDate, value);
    }

    public async Task LoadShippingDataAsync()
    {
        try
        {
            this.IsBusy = true;

            this.IsBusyMessage = "loading orders...";

            var orders = await this.services.GetService<RemoteOrderService>()?.GetItemsAsync()!;
            if (orders == null)
            {
                return;
            }

            this.IsBusyMessage = "creating appointments...";

            var tempList = new List<Appointment>();

            for (int i = 0; i < orders.Count - 1; i++)
            {
                var order = orders[i];

                var start = order.OrderDate.Date.AddHours(i);
                var end = start.AddHours(1);

                tempList.Add(new Appointment
                {
                    UniqueId = order.Id,
                    Start = start,
                    End = end,
                    Subject = order.DeliveryService,
                    Body = $"Order Total: {order.TotalPrice:C2}",
                    IsAllDay = false
                });
            }

            this.ShippingAppointments = new ObservableCollection<Appointment>(tempList);
            this.CalendarDisplayDate = this.ShippingAppointments.Min(appointment => appointment.Start);
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading shipping data, check your network connection and try again. Details: \r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = "";
            this.IsBusy = false;
        }
    }
}