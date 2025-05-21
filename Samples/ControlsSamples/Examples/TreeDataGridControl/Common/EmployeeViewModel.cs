using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

[XmlType("Employee")]
public class EmployeeViewModel : ViewModelBase
{
    private string name;
    private string title;
    private string city;
    private string phone;
    private string head;
    private string type;

    public EmployeeViewModel()
    {
        this.Children = new ObservableCollection<EmployeeViewModel>();
    }

    [XmlAttribute(AttributeName = "Name")]
    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    [XmlAttribute(AttributeName = "Title")]
    public string Title
    {
        get => this.title;
        set => this.UpdateValue(ref this.title, value);
    }

    [XmlAttribute(AttributeName = "City")]
    public string City
    {
        get => this.city;
        set => this.UpdateValue(ref this.city, value);
    }

    [XmlAttribute(AttributeName = "Phone")]
    public string Phone
    {
        get => this.phone;
        set => this.UpdateValue(ref this.phone, value);
    }

    [XmlAttribute(AttributeName = "Type")]
    public string Type
    {
        get => this.type;
        set => this.UpdateValue(ref this.type, value);
    }

    [XmlArray("Children")]
    [XmlArrayItem("Employee")]
    public ObservableCollection<EmployeeViewModel> Children { get; private set; }
}
