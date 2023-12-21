using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace QSF.Examples.SliderControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            this.sliderFirstLookContentView.SizeChanged += (s, e) =>
            {
                var contentViewWidth = this.sliderFirstLookContentView.Width;
                var rootGridRowDefinitions = this.rootGrid.RowDefinitions;
                var rootGridColumnDefinitions = this.rootGrid.ColumnDefinitions;

                rootGridRowDefinitions.Clear();
                rootGridColumnDefinitions.Clear();

                if (contentViewWidth <= 640)
                {
                    rootGridRowDefinitions.Add(new Microsoft.Maui.Controls.RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });
                    rootGridRowDefinitions.Add(new Microsoft.Maui.Controls.RowDefinition() { Height = Microsoft.Maui.GridLength.Star });
                    this.rootGrid.RowSpacing = 40;
                    this.rootGrid.SetRow(this.slidersGrid, 1);
                }
                else
                {
                    rootGridColumnDefinitions.Add(new Microsoft.Maui.Controls.ColumnDefinition() { Width = Microsoft.Maui.GridLength.Auto });
                    rootGridColumnDefinitions.Add(new Microsoft.Maui.Controls.ColumnDefinition() { Width = Microsoft.Maui.GridLength.Star });
                    this.rootGrid.ColumnSpacing = 40;
                    this.rootGrid.SetColumn(this.slidersGrid, 1);
                }
            };
        }
    }
}
