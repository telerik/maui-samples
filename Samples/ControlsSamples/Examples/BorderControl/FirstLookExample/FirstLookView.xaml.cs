using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;

namespace QSF.Examples.BorderControl.FirstLookExample
{
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
            if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            {
                this.contentView.ControlTemplate = (ControlTemplate)this.contentView.Resources["HorizontalControlTemplate"];
            }
            else if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                this.contentView.ControlTemplate = (ControlTemplate)this.contentView.Resources["VerticalControlTemplate"];
            }
        }
    }
}
