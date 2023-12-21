using QSF.ViewModels;
using System.Windows.Input;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using QSF.ExampleUtilities;
using System.Collections.Generic;
using QSF.Services;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private ColorViewModel selectedColor;
        private SizeViewModel selectedSize;
        private Color defaultDeselectedColor;
        private Color defaultSelectedColor;
        private Color defaultDeselectedTextColor;
        private Color defaultSelectedTextColor;
        private string highlightedValue;
        private string selectedValue;

        public FirstLookViewModel()
        {
            this.defaultDeselectedColor = Color.FromArgb("#EAEAEA");
            this.defaultSelectedColor = Color.FromArgb("#919191");
            this.defaultDeselectedTextColor = Colors.Black;
            this.defaultSelectedTextColor = Colors.White;
            this.HighlightedValue = string.Empty;
            this.SelectedValue = string.Empty;

            this.XS = new SizeViewModel("XS");
            this.S = new SizeViewModel("S");
            this.M = new SizeViewModel("M");
            this.L = new SizeViewModel("L");
            this.XL = new SizeViewModel("XL");
            this.XXL = new SizeViewModel("XXL");

            this.Blue = new ColorViewModel("Blue", "#007AFF");
            this.Yellow = new ColorViewModel("Yellow", "#F3C163");
            this.Purple = new ColorViewModel("Purple", "#CE3A6D");
            this.Orange = new ColorViewModel("Orange", "#EE6C4D");
            this.LightGray = new ColorViewModel("LightGray", "#EAEAEA");
            this.DarkGray = new ColorViewModel("DarkGray", "#919191");

            this.SendRequestCommand = new Command(this.SendRequest, this.CanSendRequest);

            this.SelectSizeCommand = new Command<SizeViewModel>(
                execute: (SizeViewModel arg) =>
                {
                    if (this.SelectedSize != null)
                    {
                        this.SelectedSize.BackgroundColor = this.defaultDeselectedColor;
                        this.SelectedSize.TextColor = this.defaultDeselectedTextColor;
                    }
                    arg.BackgroundColor = this.defaultSelectedColor;
                    arg.TextColor = this.defaultSelectedTextColor;
                    this.SelectedSize = arg;

                    if (this.HighlightedValue != null && this.HighlightedValue.Contains(", "))
                    {
                        this.HighlightedValue = arg.Name + this.HighlightedValue.Substring(this.HighlightedValue.IndexOf(','));
                    }
                    else
                    {
                        this.HighlightedValue = arg.Name + ", ";
                    }
                });

            this.SelectColorCommand = new Command<ColorViewModel>(
                execute: (ColorViewModel arg) =>
                {
                    if (this.SelectedColor != null)
                    {
                        this.SelectedColor.BorderColor = Colors.Transparent;
                    }
                    arg.BorderColor = arg.Color;
                    this.SelectedColor = arg;

                    this.HighlightedValue = this.HighlightedValue.Substring(0, this.HighlightedValue.IndexOf(',') + 2) + arg.Name;
                });

            this.AcceptCommand = new Command(Accept);
            this.CancelCommand = new Command(Cancel);

            this.SelectSizeCommand.Execute(this.XS);
            this.SelectColorCommand.Execute(this.Blue);
            this.Cards = new List<CardItem>()
            {
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#ECF6FE"),
                    BorderColor = Color.FromArgb("#D6EBFC"),
                    SelectedBorderColor = Color.FromArgb("#0E88F2"),
                    ImageSource = "top.png"
                },
            };

            this.Accept();
        }
        public IList<CardItem> Cards { get; }

        public ColorViewModel Blue { get; private set; }
        public ColorViewModel Yellow { get; private set; }
        public ColorViewModel Purple { get; private set; }
        public ColorViewModel Orange { get; private set; }
        public ColorViewModel LightGray { get; private set; }
        public ColorViewModel DarkGray { get; private set; }
        public ColorViewModel SelectedColor
        {
            get { return this.selectedColor; }
            set { UpdateValue(ref this.selectedColor, value); }
        }
        public ColorViewModel LastAcceptedColor { get; set; }

        public SizeViewModel XS { get; private set; }
        public SizeViewModel S { get; private set; }
        public SizeViewModel M { get; private set; }
        public SizeViewModel L { get; private set; }
        public SizeViewModel XL { get; private set; }
        public SizeViewModel XXL { get; private set; }
        public SizeViewModel SelectedSize
        {
            get { return this.selectedSize; }
            set { UpdateValue(ref this.selectedSize, value); }
        }
        public SizeViewModel LastAcceptedSize { get; set; }

        public ICommand SelectColorCommand { get; set; }
        public ICommand SelectSizeCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Command SendRequestCommand { get; set; }

        public string HighlightedValue
        {
            get { return this.highlightedValue; }
            set { UpdateValue(ref this.highlightedValue, value); }
        }
        public string SelectedValue
        {
            get { return this.selectedValue; }
            set
            {
                UpdateValue(ref this.selectedValue, value);
                this.SendRequestCommand?.ChangeCanExecute();
            }
        }

        private void Accept()
        {
            this.SelectedValue = this.HighlightedValue;
            this.LastAcceptedSize = this.SelectedSize;
            this.LastAcceptedColor = this.SelectedColor;
            this.SendRequestCommand.ChangeCanExecute();
        }

        private void Cancel()
        {
            if (!string.IsNullOrEmpty(this.SelectedValue))
            {
                this.HighlightedValue = this.SelectedValue;
                if (this.LastAcceptedSize != null)
                {
                    this.SelectSizeCommand.Execute(this.LastAcceptedSize);
                }
                if (this.LastAcceptedColor != null)
                {
                    this.SelectColorCommand.Execute(this.LastAcceptedColor);
                }
            }
            else
            {
                this.SelectSizeCommand.Execute(this.XS);
                this.SelectColorCommand.Execute(this.Blue);
            }
        }

        private void SendRequest()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert($"{this.SelectedValue} Women's Top Added to Basket");
            this.SelectSizeCommand.Execute(this.XS);
            this.SelectColorCommand.Execute(this.Blue);
            this.LastAcceptedColor = null;
            this.LastAcceptedSize = null;
            this.SelectedValue = null;
        }

        private bool CanSendRequest()
        {
            return !string.IsNullOrEmpty(this.SelectedValue);
        }
    }
}
