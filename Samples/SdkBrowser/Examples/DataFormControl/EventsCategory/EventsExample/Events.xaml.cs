using Microsoft.Maui.Controls;
using System.ComponentModel;
using Telerik.Maui.Controls;
using SDKBrowserMaui.Examples.DataFormControl.EditorsCategory.EditorsExample;

namespace SDKBrowserMaui.Examples.DataFormControl.EventsCategory.EventsExample;

public partial class Events : ContentView
{
	public Events()
	{
		InitializeComponent();
        this.BindingContext = new EditorsViewModel();
	}

    // >> dataform-property-changed
    private void OnDataFormPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
    {
        switch (eventArgs.PropertyName)
        {
            case nameof(this.dataForm.HasPendingChanges):
                this.logOutput.Text += $"Pending changes: {dataForm.HasPendingChanges}\n";
                break;
            case nameof(this.dataForm.HasValidationErrors):
                this.logOutput.Text += $"Validation errors: {dataForm.HasValidationErrors}\n";
                break;
        }
    }
    // << dataform-property-changed

    // >> dataform-validation-completed
    private void OnDataFormValidationCompleted(object sender, DataFormObjectValidationCompletedEventArgs eventArgs)
    {
        this.logOutput.Text += $"Form validation completed:\n" +
                               $"Errors: {eventArgs.HasValidationErrors}\n";
    }
    // << dataform-validation-completed

    // >> dataform-editor-validation-completed
    private void OnDataFormEditorValidationCompleted(object sender, DataFormEditorValidationCompletedEventArgs eventArgs)
    {
        this.logOutput.Text += $"Editor validation completed:\n" +
                               $"Property: {eventArgs.PropertyName}\n" +
                               $"Errors: {eventArgs.HasValidationErrors}\n";
    }
    // << dataform-editor-validation-completed

    // >> dataform-editor-value-changed
    private void OnDataFormEditorValueChanged(object sender, DataFormEditorValueChangedEventArgs eventArgs)
    {
        this.logOutput.Text += $"Editor value changed:\n" +
                               $"Property: {eventArgs.PropertyName}\n" +
                               $"Value: {eventArgs.EditorValue}\n";
    }
    // << dataform-editor-value-changed
}