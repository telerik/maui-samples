using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SmoothItemView : NavigationItemView
{
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), typeof(string), typeof(SmoothItemView));

    public SmoothItemView()
    {
        this.InitializeComponent();
    }

    public string Description
    {
        get { return (string)this.GetValue(DescriptionProperty); }
        set { this.SetValue(DescriptionProperty, value); }
    }
}