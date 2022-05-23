using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.LocalizationCategory.CustomLocalizationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomLocalization : RadContentView
{
    public CustomLocalization()
    {
        // >> datagrid-setting-the-custom-manager-csharp
        TelerikLocalizationManager.Manager = new CustomTelerikLocalizationManager();
        
        this.InitializeComponent();
        // << datagrid-setting-the-custom-manager-csharp
        this.BindingContext = new LocalizationViewModel();
    }
}

// >> datagrid-custom-localizationmanager-csharp
public class CustomTelerikLocalizationManager : TelerikLocalizationManager
{
    public override string GetString(string key)
    {
        if (key == "DataGrid_DistinctValues_SelectAll")
        {
            return "Wählen Sie Alle";
        }

        if (key == "DataGrid_TextOperator_IsEmpty")
        {
            return "Ist leer";
        }

        if (key == "DataGrid_TextOperator_IsNotEmpty")
        {
            return "Ist nicht leer";
        }

        if (key == "DataGrid_Filter_ShowRowsWithValueThat")
        {
            return "Zeilen mit folgendem Wert anzeigen:";
        }

        return base.GetString(key);
    }
}
// << datagrid-custom-localizationmanager-csharp