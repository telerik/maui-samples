using System.Collections.Generic;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.ChatPickersCategory.ChooseItemExample;

public partial class ChooseItem : RadContentView
{
	public ChooseItem()
	{
		InitializeComponent();

        // >> chat-chatpicker-itempicker
        ItemPickerContext context = new ItemPickerContext
        {
            ItemsSource = new List<string>() { "2 days", "5 days", "7 days", "Another period" }
        };
        PickerItem pickerItem = new PickerItem { Context = context, HeaderText = "Select an item" };

        chat.Items.Add(pickerItem);
        context.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == "SelectedItem")
            {
                if (context.SelectedItem != null)
                {
                    chat.Items.Remove(pickerItem);
                    chat.Items.Add(new TextMessage { Author = chat.Author, Text = "" + context.SelectedItem });
                }
            }
        };
        // << chat-chatpicker-itempicker
    }
}