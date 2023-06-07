using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.ChatPickersCategory.PickerInsideChatExample;

public partial class PickerInsideChat : RadContentView
{
    public PickerInsideChat()
    {
        InitializeComponent();

        // >> chat-chatpicker-overlay-code
        DatePickerContext context = new DatePickerContext
        {
            MinDate = new DateTime(2019, 1, 1),
            MaxDate = new DateTime(2019, 2, 2),
            SelectedDate = new DateTime(2019, 1, 16)
        };

        context.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == "SelectedDate")
            {
                if (context.SelectedDate != null)
                {
                    chat.Items.Add(new TextMessage { Author = this.chat.Author, Text = "" + context.SelectedDate });
                    (chat.Picker as RadChatPicker).IsVisible = false;
                }
            }
        };
        (chat.Picker as RadChatPicker).Context = context;
        // << chat-chatpicker-overlay-code
    }
}