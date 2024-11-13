using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Examples.TemplatedPickerControl.Common;
using QSF.Examples.TemplatedPickerControl.FirstLookExample;
using Telerik.Maui.Controls;

namespace QSF.Examples.TemplatedPickerControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : RadContentView
{
    private ColorAndSizeViewModel templatedPickerDropDownViewModel;
    private ColorAndSizeViewModel templatedPickerPopupViewModel;

    public ThemingView()
    {
        InitializeComponent();

        this.templatedPickerDropDownViewModel = new ColorAndSizeViewModel();
        this.templatedPickerDropDown.BindingContext = this.templatedPickerDropDownViewModel;
        this.templatedPickerDropDown.DropDownSettings.AcceptCommand = this.templatedPickerDropDownViewModel.AcceptCommand;
        this.templatedPickerDropDown.DropDownSettings.CancelCommand = this.templatedPickerDropDownViewModel.CancelCommand;

        this.templatedPickerPopupViewModel = new ColorAndSizeViewModel();
        this.templatedPickerPopup.BindingContext = this.templatedPickerPopupViewModel;
        this.templatedPickerPopup.PopupSettings.AcceptCommand = this.templatedPickerPopupViewModel.AcceptCommand;
        this.templatedPickerPopup.PopupSettings.CancelCommand = this.templatedPickerPopupViewModel.CancelCommand;
    }
}
