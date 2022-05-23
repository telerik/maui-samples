using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QSF.ViewModels;
using QSF.Views;
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
            this.SelectedTime = new TimeSpan(1, 6, 30, 15);
            this.Cards = new List<CardItem>()
            {
                new CardItem()
                {
                    Color = Color.FromArgb("#ECF6FE"),
                    ImageSource = "economyclass.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FEF2F1"),
                    ImageSource = "firstclass.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FFF4EB"),
                    ImageSource = "businessclass.png"
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
            toastMessage.ShortAlert("Displaying flights...");
            this.SelectedTime = null;
        }

        private bool CanSendRequest()
        {
            return this.SelectedTime != null;
        }
    }
}
