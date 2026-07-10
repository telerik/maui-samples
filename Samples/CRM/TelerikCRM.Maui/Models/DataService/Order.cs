namespace TelerikCRM.Maui.Models.DataService;

public class Order : ServiceModelBase<Order>
{
    private string customerId;
    private string employeeId;
    private string productId;
    private double totalPrice;
    private int quantity;
    private DateTime orderDate = DateTime.Today;
    private string deliveryService;
    private string street;
    private string city;
    private string state;
    private string country;
    private string zipCode;
    private string notes;

    public string CustomerId
    {
        get => this.customerId;
        set => this.SetProperty(ref this.customerId, value);
    }

    public string EmployeeId
    {
        get => this.employeeId;
        set => this.SetProperty(ref this.employeeId, value);
    }

    public string ProductId
    {
        get => this.productId;
        set => this.SetProperty(ref this.productId, value);
    }

    public double TotalPrice
    {
        get => this.totalPrice;
        set => this.SetProperty(ref this.totalPrice, value);
    }

    public int Quantity
    {
        get => this.quantity;
        set => this.SetProperty(ref this.quantity, value);
    }

    public DateTime OrderDate
    {
        get => this.orderDate;
        set => this.SetProperty(ref this.orderDate, value);
    }

    public string DeliveryService
    {
        get => this.deliveryService;
        set => this.SetProperty(ref this.deliveryService, value);
    }

    public string Street
    {
        get => this.street;
        set => this.SetProperty(ref this.street, value);
    }

    public string City
    {
        get => this.city;
        set => this.SetProperty(ref this.city, value);
    }

    public string State
    {
        get => this.state;
        set => this.SetProperty(ref this.state, value);
    }

    public string Country
    {
        get => this.country;
        set => this.SetProperty(ref this.country, value);
    }

    public string ZipCode
    {
        get => this.zipCode;
        set => this.SetProperty(ref this.zipCode, value);
    }

    public string Notes
    {
        get => this.notes;
        set => this.SetProperty(ref this.notes, value);
    }

    public override bool Equals(Order other)
        => other != null &&
           other.Id == this.Id &&
           other.CustomerId == this.CustomerId &&
           other.EmployeeId == this.EmployeeId;

    public override Order Copy()
    {
        var order = new Order();
        order.CopyFrom(this);

        return order;
    }

    public override void CopyFrom(Order other)
    {
        this.CustomerId = other.customerId;
        this.EmployeeId = other.employeeId;
        this.ProductId = other.productId;
        this.TotalPrice = other.totalPrice;
        this.Quantity = other.quantity;
        this.OrderDate = other.orderDate;
        this.DeliveryService = other.deliveryService;
        this.Street = other.street;
        this.City = other.city;
        this.State = other.state;
        this.Country = other.country;
        this.ZipCode = other.zipCode;
        this.Notes = other.notes;
    }
}