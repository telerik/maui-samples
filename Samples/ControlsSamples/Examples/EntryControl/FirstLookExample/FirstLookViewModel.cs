using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Examples.EntryControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private string username;
        private string emailAddress;
        private string password;
        private string repeatPassword;

        public FirstLookViewModel()
        {
            this.RegisterCommand = new Command(this.Register);
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                UpdateValue(ref this.username, value);
            }
        }

        public string EmailAddress
        {
            get
            {
                return this.emailAddress;
            }
            set
            {
                UpdateValue(ref this.emailAddress, value);
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                UpdateValue(ref this.password, value);
            }
        }

        public string RepeatPassword
        {
            get
            {
                return this.repeatPassword;
            }
            set
            {
                UpdateValue(ref this.repeatPassword, value);
            }
        }

        public Command RegisterCommand { get; }

        private void Register()
        {
            string registerMessage;

            if (string.IsNullOrEmpty(this.Username) ||
               string.IsNullOrEmpty(this.EmailAddress) ||
               string.IsNullOrEmpty(this.Password) ||
               string.IsNullOrEmpty(this.RepeatPassword))
            {
                registerMessage = "All fields are mandatory!";
            }
            else if(this.RepeatPassword != Password)
            {
                registerMessage = "Passwords do not match";
                this.Password = string.Empty;
                this.RepeatPassword = string.Empty;
            }
            else
            {
                registerMessage = "Your Account has been created.";
                this.Username = string.Empty;
                this.EmailAddress = string.Empty;
                this.Password = string.Empty;
                this.RepeatPassword = string.Empty;
            }
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert(registerMessage);
        }
    }
}
