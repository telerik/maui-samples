using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Telerik.Maui.Controls;

namespace QSF.Examples.DataGridControl.Common;

public class Employee : ViewModelBase
{
    private readonly UIState uiState;
	
    private int id;
    private string name;
    private string businessGroup;
    private string phone;
    private string image;
    private string email;
    private string country;
    private ObservableCollection<Order> orders;

    public Employee()
    {
        this.Orders = new ObservableCollection<Order>();
        this.uiState = new UIState();
	}

    [XmlAttribute(AttributeName = "ID")]
    public int Id
    {
        get => this.id;
        set => this.UpdateValue(ref this.id, value);
    }

    [XmlAttribute(AttributeName = "Name")]
    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    [XmlAttribute(AttributeName = "BusinessGroup")]
    public string BusinessGroup
    {
        get => this.businessGroup;
        set => this.UpdateValue(ref this.businessGroup, value);
    }

    [XmlAttribute(AttributeName = "Phone")]
    public string Phone
    {
        get => this.phone;
        set => this.UpdateValue(ref this.phone, value);
    }

    [XmlAttribute(AttributeName = "Image")]
    public string Image
    {
        get => this.image;
        set => this.UpdateValue(ref this.image, value);
    }

    [XmlAttribute(AttributeName = "Email")]
    public string Email
    {
        get => this.email;
        set => this.UpdateValue(ref this.email, value);
    }

    [XmlAttribute(AttributeName = "Country")]
    public string Country
    {
        get => this.country;
        set => this.UpdateValue(ref this.country, value);
    }

    [XmlArray("Orders")]
    [XmlArrayItem("Order")]
    public ObservableCollection<Order> Orders 
    {
        get => this.orders;
        set => this.UpdateValue(ref this.orders, value);
    }

	public UIState UIState
	{
		get => this.uiState;
	}
}
