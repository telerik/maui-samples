using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.ChatPickersCategory.ChooseCardExample;

public partial class ChooseCard : RadContentView
{
	public ChooseCard()
	{
		InitializeComponent();

        // >> chat-chatpicker-cardpicker-pickeritem
        PickerItem pickerItem = new PickerItem();
        var context = new CardPickerContext { Cards = this.GetCards(pickerItem) };
        pickerItem.Context = context;
        chat.Items.Add(new TextMessage { Text = "Select a destination" });
        chat.Items.Add(pickerItem);
        // << chat-chatpicker-cardpicker-pickeritem
    }

    // >> chat-chatpicker-cardpicker-getcards
    private IEnumerable<CardContext> GetCards(ChatItem chatItem)
    {
        List<CardContext> cards = new List<CardContext>()
            {
                new BasicCardContext() {
                    Title ="Rome",
                    Subtitle ="Italy",
                    Description ="Italy's capital is one of the world's most romantic and inspiring cities",
                    Actions = GetCardActions(chatItem, "Rome")},
                new BasicCardContext() {
                    Title ="Barcelona",
                    Subtitle ="Spain",
                    Description ="Barcelona is an enchanting seaside city with remarkable architecture",
                    Actions = GetCardActions(chatItem, "Barcelona")}
            };
        return cards;
    }

    private ICollection<CardActionContext> GetCardActions(ChatItem chatItem, string Title)
    {
        var selectAction = new CardActionContext
        {
            Text = "Select",
            Command = new Command(() =>
            {
                var index = chat.Items.IndexOf(chatItem);
                chat.Items.RemoveAt(index);
                chat.Items.Add(new TextMessage { Author = this.chat.Author, Text = "You selected " + Title });
            }),
        };
        return new List<CardActionContext>() { selectAction };
    }
    // << chat-chatpicker-cardpicker-getcards
}