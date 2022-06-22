using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls.Compatibility.Map;

namespace QSF.Examples.MapControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();

            var source = MapSource.FromResource("QSF.Examples.MapControl.FirstLookExample.usa.shp", typeof(FirstLookView));
            var dataSource = MapSource.FromResource("QSF.Examples.MapControl.FirstLookExample.usa.dbf", typeof(FirstLookView));
            this.shapeReader.Source = source;
            this.shapeReader.DataSource = dataSource;
#if WINDOWS
            this.map.WidthRequest = 500;
#endif
            this.BindingContext = new FirstLookViewModel();
        }

        private void OnReadCompleted(object sender, System.EventArgs e)
        {
            var bestView = this.shapeLayer.GetBestView();
            this.map.SetView(bestView);

            this.shapeReader.ReadCompleted -= this.OnReadCompleted;
        }

        private void OnSizeChanged(object sender, System.EventArgs e)
        {
            this.popup.HorizontalOffset = this.Width / 2 - this.popup.Content.WidthRequest / 2 - 24;
        }
    }
}