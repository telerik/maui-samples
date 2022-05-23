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
                    Color = Color.FromArgb("#ECF6FE"),
                    ImageSource = "train.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FEF2F1"),
                    ImageSource = "car.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FFF4EB"),
                    ImageSource = "flight.png"
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
            toastService.ShortAlert("Vehicle requested");
            this.Date = null;
        }

        private bool CanSendRequest()
        {
            return this.Date != null;
        }
    }
}
