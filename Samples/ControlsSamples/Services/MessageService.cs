using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace QSF.Services
{
    public class MessageService : IMessageService
    {
        public Task ShowMessage(string caption, string message)
        {
            return this.ShowMessage(caption, message, "OK");
        }

        public Task ShowMessage(string caption, string message, string button)
        {
            return Application.Current.MainPage.DisplayAlert(caption, message, button);
        }

        public Task<bool> ShowQuestion(string caption, string message)
        {
            return this.ShowQuestion(caption, message, "OK", "Cancel");
        }

        public Task<bool> ShowQuestion(string caption, string message, string accept, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(caption, message, accept, cancel);
        }
    }
}
