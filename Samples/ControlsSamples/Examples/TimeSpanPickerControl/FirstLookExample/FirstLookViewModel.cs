using System;
using System.Collections.Generic;
using QSF.ViewModels;
using QSF.ExampleUtilities;
using QSF.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.TimeSpanPickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private TimeSpan? selectedTime;

        public FirstLookViewModel()
        {
            this.SendRequestCommand = new Command(this.SendRequest, this.CanSendRequest);
            this.SelectedTime = new TimeSpan(0, 2, 30, 0);
            this.Cards = new List<CardItem>()
            {
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#ECF6FE"),
                    BorderColor = Color.FromArgb("#D6EBFC"),
                    SelectedBorderColor = Color.FromArgb("#0E88F2"),
                    ImageSource = "comedy.png",
                    Label = "Comedy"
                },
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#FEF2F1"),
                    BorderColor = Color.FromArgb("#FEE5E3"),
                    SelectedBorderColor = Color.FromArgb("#F85446"),
                    ImageSource = "drama.png",
                    Label = "Drama"
                },
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#FFF4EB"),
                    BorderColor = Color.FromArgb("#FFE9D8"),
                    SelectedBorderColor = Color.FromArgb("#FFAC3E"),
                    ImageSource = "history.png",
                    Label = "History"
                }
            };
        }

        public IList<CardItem> Cards { get; set; }

        public TimeSpan? SelectedTime
        {
            get => this.selectedTime;
            set
            {
                if (this.UpdateValue(ref this.selectedTime, value))
                {
                    this.SendRequestCommand.ChangeCanExecute();
                }
            }
        }

        public Command SendRequestCommand { get; }

        public void SendRequest()
        {
            var toastMessage = DependencyService.Get<IToastMessageService>();
            toastMessage.ShortAlert("Displaying movies...");
            this.SelectedTime = null;
        }

        private bool CanSendRequest()
        {
            return this.SelectedTime != null;
        }
    }
}
