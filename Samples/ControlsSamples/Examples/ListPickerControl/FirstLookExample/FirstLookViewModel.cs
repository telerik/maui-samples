using QSF.Services;
using QSF.ViewModels;
using QSF.ExampleUtilities;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System;

namespace QSF.Examples.ListPickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private string selectedGenre;

        public FirstLookViewModel()
        {
            this.SendRequestCommand = new Command(this.SendRequest, this.CanSendRequest);
            this.SelectedGenre = "Jazz";
            this.Cards = new List<CardItem>()
            {
                new CardItem()
                {
                    Color = Color.FromArgb("#ECF6FE"),
                    ImageSource = "melodicchillout.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FEF2F1"),
                    ImageSource = "melodicprogressive.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FFF4EB"),
                    ImageSource = "vocalchillout.png"
                }
            };
            this.Genres = new ObservableItemCollection<string>()
            {
                "Alternative Rock",
                "New Wave",
                "Jazz",
                "Pop Rock",
                "Punk Rock",
                "Progressive House",
            };
        }

        public IList<CardItem> Cards { get; }

        public Command SendRequestCommand { get; }

        public ObservableItemCollection<string> Genres { get; set; }

        public string SelectedGenre
        {
            get => this.selectedGenre;
            set
            {
                if (this.UpdateValue(ref this.selectedGenre, value))
                {
                    this.OnGenreChange();
                }
            }
        }

        public void OnGenreChange()
        {
            this.SendRequestCommand.ChangeCanExecute();
        }

        private void SendRequest()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert($"Playing {this.SelectedGenre}...");
            this.SelectedGenre = null;
        }

        private bool CanSendRequest()
        {
            return this.SelectedGenre != null;
        }
    }
}
