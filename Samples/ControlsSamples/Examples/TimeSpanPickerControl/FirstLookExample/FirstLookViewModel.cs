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
                    BackgroundColorKey = "PickerBackgroundColor1",
                    BorderColorKey = "PickerBorderColor1",
                    SelectedBorderColorKey = "PickerSelectedBorderColor1",
                    ImageSource = "comedy.png",
                    Label = "Comedy"
                },
                new CardItem()
                {
                    BackgroundColorKey = "PickerBackgroundColor2",
                    BorderColorKey = "PickerBorderColor2",
                    SelectedBorderColorKey = "PickerSelectedBorderColor2",
                    ImageSource = "drama.png",
                    Label = "Drama"
                },
                new CardItem()
                {
                    BackgroundColorKey = "PickerBackgroundColor3",
                    BorderColorKey = "PickerBorderColor3",
                    SelectedBorderColorKey = "PickerSelectedBorderColor3",
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
