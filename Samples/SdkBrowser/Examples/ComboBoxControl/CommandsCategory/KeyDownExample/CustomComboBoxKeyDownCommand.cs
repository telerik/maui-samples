using Telerik.Maui;
using Telerik.Maui.Controls.ComboBox;

namespace SDKBrowserMaui.Examples.ComboBoxControl.CommandsCategory.KeyDownExample;

// >> combobox-custom-keydowncommand
public class CustomComboBoxKeyDownCommand : ComboBoxKeyDownCommand
{
    public override void Execute(object parameter)
    {
        var keyboardInfo = (KeyboardInfo)parameter;
        var comboBox = this.ComboBox;

        if (comboBox != null && !comboBox.IsDropDownOpen &&
            (keyboardInfo.key == RadKeyboardKey.Delete || keyboardInfo.key == RadKeyboardKey.Back))
        {
            var clearSelectionCommand = comboBox.ClearSelectionCommand;
            if (clearSelectionCommand != null && clearSelectionCommand.CanExecute(null))
            {
                clearSelectionCommand.Execute(null);
            }

            keyboardInfo.Cancel = true;
            return;
        }

        base.Execute(parameter);
    }
}
// << combobox-custom-keydowncommand
