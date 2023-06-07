using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.SuggestedActionsExample;

public partial class SuggestedActions : RadContentView
{
	public SuggestedActions()
	{
		InitializeComponent();
        // >> chat-suggested-actions-code
        this.chat.Items.Add(new TextMessage { Text = "Hello" });
        this.chat.Items.Add(new TextMessage { Text = "Hi", Author = this.chat.Author });
        this.chat.Items.Add(new TextMessage { Text = "How can I help you?" });
        this.chat.Items.Add(new TextMessage { Text = "I need help with buying some tickets.", Author = this.chat.Author });
        this.chat.Items.Add(new TimeBreak() { Text = "Last read (Time Break)" });
        this.chat.Items.Add(new TextMessage { Text = "Here's a list of possible actions:" });

        var suggestedActionsItem = new SuggestedActionsItem();
        suggestedActionsItem.Actions = this.GetSuggestedActions(suggestedActionsItem);
        this.chat.Items.Add(suggestedActionsItem);
        // << chat-suggested-actions-code
    }

    // >> chat-suggested-actions-collection
    private ICollection<SuggestedAction> GetSuggestedActions(SuggestedActionsItem suggestedActionsItem)
    {
        var actions = new List<SuggestedAction>();

        for (int i = 0; i < 5; i++)
        {
            int actionIndex = i;
            actions.Add(new SuggestedAction
            {
                Text = "Action " + i,
                Command = new Command(() =>
                {
                    this.chat.Items.Remove(suggestedActionsItem);
                    this.chat.Items.Add(new TextMessage { Author = this.chat.Author, Text = "Action " + actionIndex });
                }),
            });
        }
        return actions;
    }
    // << chat-suggested-actions-collection
}