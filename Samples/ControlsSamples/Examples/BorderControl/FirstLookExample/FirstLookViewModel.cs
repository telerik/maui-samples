using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Examples.BorderControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public FirstLookViewModel()
        {
            this.Avatar = "border_user1.png";
            this.Person1Avatar = "person_8.png";
            this.Person2Avatar = "person_6.png";
            this.Person1Name = "Melanie Powers, MBA";
            this.Person2Name = "Yordan Bratkov";
            this.Person1Description = "Francis is a great and reliable developer! She is very responsive to her tasks, delivers in scope, and provides timely execution. She is a professional, a team player and a pleasure to work with!";
            this.Person2Description = "Francis is a very creative and fun-loving person with plenty of insights into web development.";

            this.ConnectCommand = new Command(this.ConnectRequest);
        }

        public string Avatar { get; set; }
        public string Person1Avatar { get; set; }
        public string Person2Avatar { get; set; }
        public string Person1Name { get; set; }
        public string Person2Name { get; set; }
        public string Person1Description { get; set; }
        public string Person2Description { get; set; }
        

        public Command ConnectCommand { get; }

        private void ConnectRequest()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Connection request successfully sent. Awaiting approval...");
        }
    }
}

