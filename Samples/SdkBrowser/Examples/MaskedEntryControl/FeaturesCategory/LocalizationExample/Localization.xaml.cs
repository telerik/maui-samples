using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui;
using Telerik.Maui.Controls;
using Telerik.Maui.MaskedEntry;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace SDKBrowserMaui.Examples.MaskedEntryControl.FeaturesCategory.LocalizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Localization : RadContentView
    {
        public Localization()
        {   
            TelerikLocalizationManager.Manager.ResourceManager = MaskResource.ResourceManager;

            InitializeComponent();
        }
    }
}