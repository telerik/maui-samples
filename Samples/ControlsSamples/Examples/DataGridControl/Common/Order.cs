﻿using System;
using System.Xml.Serialization;
using QSF.ViewModels;
using Telerik.Maui.Controls.Compatibility.Common.DataAnnotations;

namespace QSF.Examples.DataGridControl.Common
{
    public class Order : ViewModelBase
    {
        private int orderID;
        private string shipName;
        private string customerID;
        private int employeeID;
        private string employeeImage;
        private DateTime orderDate;
        private DateTime requiredDate;
        private DateTime shippedDate;
        private double shipVia;
        private double freight;
        private string shipAddress;
        private string shipCity;
        private string shipPostalCode;
        private string shipCountry;
        private string shipRegion;

        [XmlAttribute(AttributeName = "OrderID")]
        [DisplayOptions(Header = "Order ID", PlaceholderText = "Order ID")]
        public int OrderID
        {
            get => this.orderID;
            set => this.UpdateValue(ref this.orderID, value);
        }

        [XmlAttribute(AttributeName = "ShipName")]
        [DisplayOptions(Header = "Ship Name", PlaceholderText = "Ship Name")]
        public string ShipName
        {
            get => this.shipName;
            set => this.UpdateValue(ref this.shipName, value);
        }

        [XmlAttribute(AttributeName = "CustomerID")]
        [DisplayOptions(Header = "Customer ID", PlaceholderText = "Customer ID")]
        public string CustomerID
        {
            get => this.customerID;
            set => this.UpdateValue(ref this.customerID, value);
        }

        [XmlAttribute(AttributeName = "EmployeeID")]
        public int EmployeeID
        {
            get => this.employeeID;
            set => this.UpdateValue(ref this.employeeID, value);
        }

        [XmlAttribute(AttributeName = "EmployeeImage")]
        public string EmployeeImage
        {
            get => this.employeeImage;
            set => this.UpdateValue(ref this.employeeImage, value);
        }

        [XmlAttribute(AttributeName = "OrderDate")]
        [DisplayOptions(Header = "Order Date", PlaceholderText = "Order Date")]
        public DateTime OrderDate
        {
            get => this.orderDate;
            set => this.UpdateValue(ref this.orderDate, value);
        }

        [XmlAttribute(AttributeName = "RequiredDate")]
        public DateTime RequiredDate
        {
            get => this.requiredDate;
            set => this.UpdateValue(ref this.requiredDate, value);
        }

        [XmlAttribute(AttributeName = "ShippedDate")]
        [DisplayOptions(Header = "Shipped Date", PlaceholderText = "Shipped Date")]
        public DateTime ShippedDate
        {
            get => this.shippedDate;
            set => this.UpdateValue(ref this.shippedDate, value);
        }

        [XmlAttribute(AttributeName = "ShipVia")]
        [DisplayOptions(Header = "Ship Via", PlaceholderText = "Ship Via")]
        public double ShipVia
        {
            get => this.shipVia;
            set => this.UpdateValue(ref this.shipVia, value);
        }

        [XmlAttribute(AttributeName = "Freight")]
        [DisplayOptions(Header = "Freight", PlaceholderText = "Freight")]
        public double Freight
        {
            get => this.freight;
            set => this.UpdateValue(ref this.freight, value);
        }

        [XmlAttribute(AttributeName = "ShipAddress")]
        public string ShipAddress
        {
            get => this.shipAddress;
            set => this.UpdateValue(ref this.shipAddress, value);
        }

        [XmlAttribute(AttributeName = "ShipCity")]
        public string ShipCity
        {
            get => this.shipCity;
            set => this.UpdateValue(ref this.shipCity, value);
        }

        [XmlAttribute(AttributeName = "ShipPostalCode")]
        public string ShipPostalCode
        {
            get => this.shipPostalCode;
            set => this.UpdateValue(ref this.shipPostalCode, value);
        }

        [XmlAttribute(AttributeName = "ShipCountry")]
        public string ShipCountry
        {
            get => this.shipCountry;
            set => this.UpdateValue(ref this.shipCountry, value);
        }

        [XmlAttribute(AttributeName = "ShipRegion")]
        public string ShipRegion
        {
            get => this.shipRegion;
            set => this.UpdateValue(ref this.shipRegion, value);
        }
    }
}
