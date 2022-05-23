﻿using QSF.Services;
using QSF.ViewModels;
using QSF.ExampleUtilities;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.TimePickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private DateTime? selectedDate;
        private TimeSpan? selectedTime;

        public FirstLookViewModel()
        {
            this.SendOrderCommand = new Command(this.SendOrder, this.CanSendOrder);
            this.SelectedDate = DateTime.Now;
            this.SelectedTime = DateTime.Now.TimeOfDay;
            this.Cards = new List<CardItem>()
            {
                new CardItem()
                {
                    Color = Color.FromArgb("#ECF6FE"),
                    ImageSource = "cleaning.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FEF2F1"),
                    ImageSource = "carpetcleaning.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FFF4EB"),
                    ImageSource = "homepainting.png"
                }
            };
        } 
        
        public IList<CardItem> Cards { get; set; }

        public DateTime? SelectedDate
        {
            get => this.selectedDate;
            set
            {
                if (this.UpdateValue(ref this.selectedDate, value))
                {
                    this.SendOrderCommand.ChangeCanExecute();
                }
            }
        }

        public TimeSpan? SelectedTime
        {
            get => this.selectedTime;
            set
            {
                if (this.UpdateValue(ref this.selectedTime, value))
                {
                    this.SendOrderCommand.ChangeCanExecute();
                }
            }
        }

        public TimeSpan CurrentTime => DateTime.Now.TimeOfDay;

        public Command SendOrderCommand { get; }

        public void SendOrder()
        {
            var toastMessage = DependencyService.Get<IToastMessageService>();
            toastMessage.ShortAlert("Order sent");
            this.SelectedDate = null;
            this.SelectedTime = null;
        }

        private bool CanSendOrder()
        {
            return this.SelectedDate != null && this.SelectedTime != null;
        }
    }
}
