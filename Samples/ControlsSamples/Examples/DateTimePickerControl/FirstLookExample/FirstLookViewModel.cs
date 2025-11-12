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
                    BackgroundColorKey = "PickerBackgroundColor1",
                    BorderColorKey = "PickerBorderColor1",
                    SelectedBorderColorKey = "PickerSelectedBorderColor1",
                    ImageSource = "train.png",
                    Label = "Train"
                },
                new CardItem()
                {
                    BackgroundColorKey = "PickerBackgroundColor2",
                    BorderColorKey = "PickerBorderColor2",
                    SelectedBorderColorKey = "PickerSelectedBorderColor2",
                    ImageSource = "car.png",
                    Label = "Car (Taxi)"
                },
                new CardItem()
                {
                    BackgroundColorKey = "PickerBackgroundColor3",
                    BorderColorKey = "PickerBorderColor3",
                    SelectedBorderColorKey = "PickerSelectedBorderColor3",
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
