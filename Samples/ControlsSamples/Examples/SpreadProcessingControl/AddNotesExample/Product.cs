using System;
using Telerik.Maui.Controls;

namespace QSF.Examples.SpreadProcessingControl.AddNotesExample
{
    public class Product : NotifyPropertyChangedBase
    {
        private int id;
        private string name;
        private double unitPrice;
        private int quantity;
        private DateTime date;
        private double subTotal;

        public Product(int id, string name, double unitPrice, int quantity, DateTime date)
        {
            this.ID = id;
            this.Name = name;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
            this.Date = date;
            this.SubTotal = this.quantity * this.unitPrice;
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                if (this.unitPrice != value)
                {
                    this.unitPrice = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get { return this.quantity; }
            set
            {
                if (this.quantity != value)
                {
                    this.quantity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if (this.date != value)
                {
                    this.date = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double SubTotal
        {
            get { return this.subTotal; }
            set
            {
                if (this.subTotal != value)
                {
                    this.subTotal = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
