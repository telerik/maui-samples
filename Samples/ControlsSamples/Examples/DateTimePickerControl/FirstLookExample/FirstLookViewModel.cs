using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Services;
using QSF.ViewModels;
using QSF.ExampleUtilities;
using System;
using System.Collections.Generic;

namespace QSF.Examples.DateTimePickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private DateTime? date;

        public FirstLookViewModel()
        {
            this.SendRequestCommand = new Command(this.SendRequest, this.CanSendRequest);
            this.Date = DateTime.Now;
            this.Cards = new List<CardItem>()
            {
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#ECF6FE"),
                    BorderColor = Color.FromArgb("#D6EBFC"),
                    SelectedBorderColor = Color.FromArgb("#0E88F2"),
                    ImageSource = "train.png",
                    Label = "Train"
                },
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#FEF2F1"),
                    BorderColor = Color.FromArgb("#FEE5E3"),
                    SelectedBorderColor = Color.FromArgb("#F85446"),
                    ImageSource = "car.png",
                    Label = "Car (Taxi)"
                },
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#FFF4EB"),
                    BorderColor = Color.FromArgb("#FFE9D8"),
                    SelectedBorderColor = Color.FromArgb("#FFAC3E"),
                    ImageSource = "flight.png",
                    Label = "Plane"
                }
            };
        }

        public IList<CardItem> Cards { get; }

        public Command SendRequestCommand { get; }

        public DateTime? Date 
        { 
            get => this.date;
            set
            {
                if (this.UpdateValue(ref this.date, value))
                {
                    this.OnDateTimeChange();
                }
            }
        }

        public void OnDateTimeChange()
        {
            this.SendRequestCommand.ChangeCanExecute();
        }

        private void SendRequest()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Transfer requested");
            this.Date = null;
        }

        private bool CanSendRequest()
        {
            return this.Date != null;
        }
    }
}
