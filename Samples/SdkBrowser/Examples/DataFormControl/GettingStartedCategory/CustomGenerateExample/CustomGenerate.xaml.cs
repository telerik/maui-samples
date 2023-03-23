using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory;


namespace SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory.CustomGenerateExample;

public partial class CustomGenerate : ContentView
{
	public CustomGenerate()
	{
		InitializeComponent();
        this.dataForm.BindingContext = new CustomGenerateViewModel();

        // >> dataform-groupgenerated-event
        this.dataForm.GroupGenerated += this.OnGroupGenerated;
        // << dataform-groupgenerated-event

        // >> dataform-editorgenerated-event
        this.dataForm.EditorGenerated += this.OnEditorGenerated;
        // << dataform-editorgenerated-event
    }

    // >> dataform-ongroups-generated
    private void OnGroupGenerated(object sender, DataFormGroupGeneratedEventArgs eventArgs)
    {
        switch (eventArgs.GroupName)
        {
            case "Details":
                eventArgs.Group.HeaderText = "Additonal Details";
                break;
            case "Ignored":
                eventArgs.Group = null;
                break;
        }
    }
    // << dataform-ongroups-generated

    // >> dataform-oneditors-generated
    private void OnEditorGenerated(object sender, DataFormEditorGeneratedEventArgs eventArgs)
    {
        switch (eventArgs.PropertyName)
        {
            case "FirstName":
                eventArgs.Editor.HeaderText = "First Name";
                break;
            case "LastName":
                eventArgs.Editor.HeaderText = "Last Name";
                break;
            case "StartDate":
                eventArgs.Editor = new DataFormRadDatePickerEditor
                {
                    PropertyName = "StartDate",
                    HeaderText = "Start Date"
                };
                break;
            case "EndDate":
                eventArgs.Editor = new DataFormRadTimePickerEditor
                {
                    PropertyName = "EndDate",
                    HeaderText = "Time",
                };
                break;
            case "Accommodation":
                eventArgs.Editor = new DataFormRadComboBoxEditor
                {
                    PropertyName = "Accommodation",
                    HeaderText = "Accommodation",
                };
                break;
        }
    }
    // << dataform-oneditors-generated
}