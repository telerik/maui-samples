using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.AggregatesCategory
{
    public class Data : NotifyPropertyChangedBase
    {
        private string name;
        private double price;
        private double deliveryPrice;
        private int quantity;

        public string Name
        {
            get => this.name;
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double Price
        {
            get => this.price;
            set
            {
                if (value != this.price)
                {
                    this.price = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double DeliveryPrice
        {
            get => this.deliveryPrice;
            set
            {
                if (value != this.deliveryPrice)
                {
                    this.deliveryPrice = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get => this.quantity;
            set
            {
                if (value != this.quantity)
                {
                    this.quantity = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}