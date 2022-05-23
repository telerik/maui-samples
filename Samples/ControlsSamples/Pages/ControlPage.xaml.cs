using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace QSF.Pages;

public partial class ControlPage : ContentPage
{
    public ControlPage()
    {
        this.InitializeComponent();

        if (this.Content != null)
        {
            this.Content.PropertyChanged += this.OnPropertyChanged;
        }
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
