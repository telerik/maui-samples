using Microsoft.Maui.Controls;
using QSF.Examples.ChatControl.TravelAssistanceExample.Models;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.TravelAssistanceExample;

public class CustomChatItemTemplateSelector : ChatItemTemplateSelector
{
    public DataTemplate FlightTemplate { get; set; }
    public DataTemplate SummaryTemplate { get; set; }
    public DataTemplate WaitingForBotTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        ChatItem chatItem = item as ChatItem;
        FlightChatContext context = chatItem?.Data as FlightChatContext;
        if (context != null)
        {
            return this.FlightTemplate;
        }

        Summary summary = chatItem?.Data as Summary;
        if (summary != null)
        {
            return this.SummaryTemplate;
        }

        string text = chatItem?.Data as string;
        if (text == TravelBotViewModel.WaitingForBot)
        {
            return this.WaitingForBotTemplate;
        }

        return base.OnSelectTemplate(item, container);
    }
}