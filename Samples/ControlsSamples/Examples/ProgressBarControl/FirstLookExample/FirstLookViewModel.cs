using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace QSF.Examples.ProgressBarControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private string orderImage;
    private int orderProgress;
    private int animationDuration = 800;
    private string placeOrderButtonText = "Place Your Order";
    private string statusText = "Proceed to order placement";
    private bool isOrderInProgress = false;

    public FirstLookViewModel()
    {
	    this.OrderImages = new List<string>
        {
            "tiramisu.png",
            "belgian_chocolate.png",
            "american_pancakes.png",
            "blueberry_waffle.png"
        };

        this.OrderImage = this.OrderImages.First();
        this.PlaceOrderCommand = new Command(PlaceOrder, CanPlaceOrder);
    }

	public List<string> OrderImages { get; }

	public Command PlaceOrderCommand { get; }

	public string OrderImage
	{
		get { return this.orderImage; }
		set { this.UpdateValue(ref this.orderImage, value); }
    }

    public int OrderProgress
	{
		get { return this.orderProgress; }
		set { this.UpdateValue(ref this.orderProgress, value); }
    }

    public int AnimationDuration
	{
		get { return this.animationDuration; }
		set { this.UpdateValue(ref this.animationDuration, value); }
    }

    public string PlaceOrderButtonText
    {
        get { return this.placeOrderButtonText; }
        set { this.UpdateValue(ref this.placeOrderButtonText, value); }
    }

    public string StatusText
	{
		get { return this.statusText; }
		set { this.UpdateValue(ref this.statusText, value); }
    }

    private bool CanPlaceOrder()
    {
        return this.isOrderInProgress == false;
    }

    private void PlaceOrder()
    {
        this.ResetOrder();
		this.AnimationDuration = 800;

		Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
        {
            this.OrderProgress += 5;

			if (this.OrderProgress == 100)
			{
				this.CompleteOrder();
				return false;
			}

			int imageIndex = this.OrderProgress / 25;
			this.OrderImage = this.OrderImages[imageIndex];

			return true;
        });
    }

    public void ResetOrder()
    {
        this.AnimationDuration = 0;
        this.OrderProgress = 0;
        this.isOrderInProgress = true;
        this.PlaceOrderCommand.ChangeCanExecute();
        this.PlaceOrderButtonText = "Placing...";
        this.StatusText = "Placing Your Order...";
    }

    public void CompleteOrder()
    {
        this.isOrderInProgress = false;
        this.PlaceOrderCommand.ChangeCanExecute();
        this.PlaceOrderButtonText = "Place Another Order";
        this.StatusText = "Your order is placed successfully.";
    }
}
