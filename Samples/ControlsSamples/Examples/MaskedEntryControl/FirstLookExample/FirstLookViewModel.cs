using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Examples.MaskedEntryControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private string email;
        private string phone;
        private int? ip;
        private int? zipCode;
        private double? currency;

        public FirstLookViewModel()
        {
            this.SubmitCommand = new Command(this.OnSubmitRequest);
        }

        public Command SubmitCommand { get; private set; }

        public string Email 
        {
            get
            {
                return this.email;
            }
            set
            {
                this.UpdateValue(ref this.email, value);
            }
        }

        public string Phone 
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.UpdateValue(ref this.phone, value);
            }
        }

        public int? IP 
        {
            get
            {
                return this.ip;
            }
            set
            {
                this.UpdateValue(ref this.ip, value);
            }
        }

        public int? ZipCode 
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                this.UpdateValue(ref this.zipCode, value);
            }
        }

        public double? Currency 
        {
            get
            {
                return this.currency;
            }
            set
            {
                this.UpdateValue(ref this.currency, value);
            }
        }

        private void OnSubmitRequest()
        {
            string registerMessage;

            if (string.IsNullOrEmpty(this.Email) || string.IsNullOrEmpty(this.Phone))
            {
                registerMessage = "Email and Phone fields are mandatory!";
            }
            else
            {
                registerMessage = "Your Account has been created.";
                this.Email = string.Empty;
                this.Phone = string.Empty;
                this.IP = null;
                this.ZipCode = null;
                this.Currency = null;
            }
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert(registerMessage);
        }
    }
}
