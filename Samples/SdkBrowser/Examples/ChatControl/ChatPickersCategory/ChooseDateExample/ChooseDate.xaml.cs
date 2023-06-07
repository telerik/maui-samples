using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.ChatPickersCategory.ChooseDateExample;

public partial class ChooseDate : RadContentView
{
	public ChooseDate()
	{
		InitializeComponent();

        // >> chat-chatpicker-datepicker
        DatePickerContext context = new DatePickerContext
        {
            MinDate = new DateTime(2019, 1, 1),
            MaxDate = new DateTime(2019, 2, 2),
            SelectedDate = new DateTime(2019, 1, 16)
        };
        PickerItem pickerItem = new PickerItem { Context = context };
        chat.Items.Add(new TextMessage { Text = "Select a date", Author = chat.Author });
        chat.Items.Add(pickerItem);

        context.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == "SelectedDate")
            {
                if (context.SelectedDate != null)
                {
                    chat.Items.Remove(pickerItem);
                    chat.Items.Add(new TextMessage { Author = chat.Author, Text = "" + context.SelectedDate });
                }
            }
        };
        // << chat-chatpicker-datepicker
    }
}