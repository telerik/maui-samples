using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using QSF.Examples.TabViewControl.ConfigurationExample;

namespace QSF.Examples.TabViewControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : RadContentView
{
    public ThemingView()
    {
        InitializeComponent();

        this.BindingContext = new ConfigurationViewModel();
    }
}