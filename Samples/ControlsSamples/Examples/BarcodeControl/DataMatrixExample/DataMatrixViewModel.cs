using QSF.ViewModels;

namespace QSF.Examples.BarcodeControl.DataMatrixExample
{
    public class DataMatrixViewModel : ExampleViewModel
    {
        public string GTN { get { return "GTN(01): 05060478880006 "; } }

        public string ProdDate { get { return "Prod Date: 2021-05-12 "; } }

        public string SerialN { get { return "Serial: OGA1245004 "; } }

        public string Batch { get { return "Batch: B123CR890J "; } }

        public string Manufacturer { get { return "Medic4YourHealth"; } }

        public string Address { get { return "Hippocrateslaan 69"; } }

        public string City { get { return "5644 DV Eindhoven"; } }

        public string PhoneNumber { get { return "+31-655-5422-22"; } }

        public string WebSite { get { return "www.medic4yourhealth.nl"; } }

        public string Value
        {
            get { return $"{GTN}\n{ProdDate}\n{SerialN}\n{Batch}"; }
        }
    }
}
