namespace QSF.Examples.BarcodeControl.SwissQRCodeExample
{
    public class Item
    {
        public Item(int nO, string name, string quantityType, int quantity, double price)
        {
            this.No = nO;
            this.Name = name;
            this.QuantityType = quantityType;
            this.Quantity = quantity;
            this.Price = price;
            this.Total = this.Price * this.Quantity;
        }

        public int No { get; set; }

        public string Name { get; set; }

        public string QuantityType { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double Total { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
