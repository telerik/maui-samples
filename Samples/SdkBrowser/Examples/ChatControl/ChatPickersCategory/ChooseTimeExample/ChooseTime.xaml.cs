using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.ChatPickersCategory.ChooseTimeExample;

public partial class ChooseTime : RadContentView
{
	public ChooseTime()
	{
		InitializeComponent();

        // >> chat-chatpicker-timepicker
        TimePickerContext context = new TimePickerContext
        {
            StartTime = TimeSpan.FromHours(1),
            EndTime = TimeSpan.FromHours(5),
        };
        PickerItem pickerItem = new PickerItem { Context = context };
        chat.Items.Add(new TextMessage { Text = "Select a time" });
        chat.Items.Add(pickerItem);
        context.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == "SelectedValue")
            {
                if (context.SelectedValue != null)
                {
                    chat.Items.Remove(pickerItem);
                    chat.Items.Add(new TextMessage { Author = chat.Author, Text = "" + context.SelectedValue });
                }
            }
        };
        // << chat-chatpicker-timepicker
    }
}