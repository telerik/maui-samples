using System.Collections.ObjectModel;
using Telerik.Documents.SpreadsheetStreaming;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class OrdersViewModel : ViewModelBase
{
    private bool isLoaded = false;
#if MACCATALYST || WINDOWS
    private OrderEditViewModel orderViewModel;
    private bool isEditPopupOpen;
#endif
    private bool isPopupOpen;
    private Order currentOrder;
    private Order selectedOrder;

    public OrdersViewModel()
    {
        this.CreateNewCommand = new Command(this.CreateNewCommandExecuted);
        this.TogglePopupCommand = new Command(() => this.IsPopupOpen = !this.IsPopupOpen);
        this.ExportToXlsxCommand = new Command(this.ExportToXlsx);
        this.ExportToCsvCommand = new Command(this.ExportToCsv);

#if MACCATALYST || WINDOWS
        this.SaveModalCommand = new Command(this.SaveModalCommandExecuted);
        this.CloseModalCommand = new Command(this.CloseModalCommandExecuted);
        this.EditCommand = new Command(this.EditCommandExecuted);
        this.OrderViewModel = services.GetService(typeof(OrderEditViewModel)) as OrderEditViewModel;
#else
        this.CanCreateNew = true;
        this.CanNavigateBack = true;
        this.NavigateBackContextName = "More";
        this.Title = "Orders";
#endif
    }

    public ObservableCollection<Order> Orders { get; } = new();

#if MACCATALYST || WINDOWS
    public OrderEditViewModel OrderViewModel
    {
        get => this.orderViewModel;
        set => this.UpdateValue(ref this.orderViewModel, value);
    }

    public bool IsEditPopupOpen
    {
        get => this.isEditPopupOpen;
        set => this.UpdateValue(ref this.isEditPopupOpen, value);
    }
#endif

    public bool IsPopupOpen
    {
        get => isPopupOpen;
        set => this.UpdateValue(ref isPopupOpen, value);
    }

    public Order SelectedOrder
    {
        get => this.selectedOrder;
        set
        {
            if (this.UpdateValue(ref this.selectedOrder, value) && this.selectedOrder != null)
            {
#if !(MACCATALYST || WINDOWS)
                var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
                service.NavigateToAsync<OrderDetailViewModel>(this.selectedOrder);
#endif
            }
        }
    }

    public Command EditCommand { get; set; }

    public Command SaveModalCommand { get; set; }

    public Command CloseModalCommand { get; set; }

    public Command TogglePopupCommand { get; set; }

    public Command ExportToXlsxCommand { get; set; }

    public Command ExportToCsvCommand { get; set; }

    public override async void OnAppearing()
    {
        await this.LoadOrdersAsync();
#if !(MACCATALYST || WINDOWS)
        // Clear selection when returning to the page from details page
        this.SelectedOrder = null;
#endif
    }

    private async Task LoadOrdersAsync()
    {
        if (this.IsBusy || this.isLoaded)
        {
            return;
        }

        try
        {
            if (this.Orders.Count == 0)
            {
                this.IsBusy = true;
                this.IsBusyMessage = "loading orders...";

                var orders = await this.services.GetService<RemoteOrderService>()?.GetItemsAsync()!;
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        this.Orders.Add(order);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading orders, check network connection and try again. Details: \r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = "";
            this.IsBusy = false;
            this.isLoaded = true;
        }
    }

    private async void CreateNewCommandExecuted()
    {
#if MACCATALYST || WINDOWS
        this.orderViewModel.SelectedOrder = new Order();
        this.Title = "Create Order";
        await this.orderViewModel.LoadData();
        this.IsEditPopupOpen = true;
#else
        var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
        await service.NavigateToAsync<OrderEditViewModel>();
#endif
    }

#if MACCATALYST || WINDOWS
    private async void EditCommandExecuted(object obj)
    {
        if (obj is Order order)
        {
            this.currentOrder = order;
            var editedOrder = this.currentOrder.Copy();

            this.orderViewModel.SelectedOrder = editedOrder;
            this.orderViewModel.SelectedDeliveryService = editedOrder?.DeliveryService;

            this.Title = "Edit Order";
            await this.orderViewModel.LoadData();
            this.DeleteContextName = "Order";

            this.IsEditPopupOpen = true;
        }
    }
#endif

    private async void ExportToXlsx(object obj)
    {
        this.IsBusy = true;
        this.IsBusyMessage = $"exporting to .xlsx...";

        using (MemoryStream stream = new MemoryStream())
        {
            using (IWorkbookExporter workbookExporter = SpreadExporter.CreateWorkbookExporter(SpreadDocumentFormat.Xlsx, stream))
            {
                using (IWorksheetExporter worksheetExporter = workbookExporter.CreateWorksheetExporter("Orders"))
                {
                    ExportColumnWidths(worksheetExporter);
                    ExportDocumentHeaderRow(worksheetExporter);
                    ExportData(worksheetExporter);
                }
            }

            await this.services.GetService<IFileViewerService>().View(stream, "Orders.xlsx");
        }

        this.IsBusyMessage = string.Empty;
        this.IsBusy = false;
    }

    private async void ExportToCsv(object obj)
    {
        this.IsBusy = true;
        this.IsBusyMessage = $"exporting to .csv...";

        using (MemoryStream stream = new MemoryStream())
        {
            using (IWorkbookExporter workbookExporter = SpreadExporter.CreateWorkbookExporter(SpreadDocumentFormat.Csv, stream))
            {
                using (IWorksheetExporter worksheetExporter = workbookExporter.CreateWorksheetExporter("Orders"))
                {
                    ExportColumnWidths(worksheetExporter);
                    ExportDocumentHeaderRow(worksheetExporter);
                    ExportData(worksheetExporter);
                }
            }

            await this.services.GetService<IFileViewerService>().View(stream, "Orders.csv");
        }

        this.IsBusyMessage = string.Empty;
        this.IsBusy = false;
    }

    private void ExportColumnWidths(IWorksheetExporter worksheetExporter)
    {
        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(200);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(200);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(200);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(150);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }

        using (var column = worksheetExporter.CreateColumnExporter())
        {
            column.SetWidthInPixels(100);
        }
    }

    private static void ExportDocumentHeaderRow(IWorksheetExporter worksheetExporter)
    {
        using IRowExporter row = worksheetExporter.CreateRowExporter();

        row.SetHeightInPixels(40);

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("CustomerId");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("EmployeeId");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("ProductId");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("TotalPrice");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("Quantity");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("OrderDate");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("DeliveryService");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("Street");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("City");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("State");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("Country");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("ZipCode");
        }

        using (var cell = row.CreateCellExporter())
        {
            cell.SetValue("Notes");
        }
    }

    private void ExportData(IWorksheetExporter worksheetExporter)
    {
        foreach (Order course in this.Orders)
        {
            using IRowExporter rowExporter = worksheetExporter.CreateRowExporter();

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.CustomerId);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.EmployeeId);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.ProductId);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue((double)course.TotalPrice);
                cellExporter.SetFormat(new SpreadCellFormat
                {
                    NumberFormat = "$ 0",
                    HorizontalAlignment = SpreadHorizontalAlignment.Right
                });
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.Quantity);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue((DateTime)course.OrderDate);
                cellExporter.SetFormat(new SpreadCellFormat
                {
                    NumberFormat = "MM/dd/yyyy",
                    HorizontalAlignment = SpreadHorizontalAlignment.Right
                });
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.DeliveryService);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.Street);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.City);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.State);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.Country);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.ZipCode);
            }

            using (var cellExporter = rowExporter.CreateCellExporter())
            {
                cellExporter.SetValue(course.Notes);
            }
        }
    }

#if MACCATALYST || WINDOWS
    private void SaveModalCommandExecuted()
    {
        if (this.IsEditPopupOpen)
        {
            this.orderViewModel?.SaveCommand?.Execute(null);

            // NOTE: Commented code is applicable for when app is not in read-only mode
            // this.IsEditPopupOpen = false;
            // this.currentOrder?.CopyFrom(this.orderViewModel.SelectedOrder);

            this.currentOrder = null;
        }
    }

    private void CloseModalCommandExecuted()
    {
        if (this.IsEditPopupOpen)
        {
            this.IsEditPopupOpen = false;
            this.currentOrder = null;
        }
    }
#endif
}