using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using QSF.ViewModels;


namespace QSF.Pages
{
    public partial class ExamplePage : ContentPage
    {
        public ExamplePage()
        {
            InitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
            await vm.NavigationService.NavigateBackAsync();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
#if __ANDROID__
            //TODO: Remove this when NavigationPage starts using the Maui Handler instead of the old compat Renderer.
            if (e.PropertyName == nameof(this.Width) || e.PropertyName == nameof(this.Height))
            {
                this.Frame = this.Content.Frame;
            }
#endif
        }
    }
}
