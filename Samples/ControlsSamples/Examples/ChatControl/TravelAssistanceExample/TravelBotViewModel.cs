using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using QSF.Examples.ChatControl.TravelAssistanceExample.Converters;
using QSF.Examples.ChatControl.TravelAssistanceExample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.TravelAssistanceExample;

public class TravelBotViewModel : NotifyPropertyChangedBase
{
    public const string WaitingForBot = "WaitingForBot";

    private AzureChatBotService chatService;
    private bool isChatPickerVisible;
    private string pickerHeaderText;
    private PickerContext context;
    private bool isBusy;
    private Author me;
    private ChatMessage waitingForBotMessage;

    public TravelBotViewModel()
    {
        this.IsBusy = true;
        this.BotAuthor = new Author();
        this.BotAuthor.Name = "Botyo-BotTesting";

        this.BotAuthor.Avatar = "vacationbot.png";

        this.Me = new Author();
        this.Me.Name = "human";

        this.chatService = new AzureChatBotService("Y_ly-If6haE.cwA.PQE.ZwOOsq4MlHcD3_YLFI-t9oW6L6DXMMBoi67LBz9WaWA", this.BotAuthor.Name, this.OnMessageReceived);

        this.OkPickerCommand = new Command(this.OnOkPickerCommandExecuted, this.OnOkPickerCommandCanExecute);

        this.Items = new ObservableCollection<ChatItem>();
        this.Items.CollectionChanged += this.OnItemsCollectionChanged;

        this.waitingForBotMessage = new ChatMessage { Author = this.BotAuthor, Data = WaitingForBot, };
    }

    public ICommand OkPickerCommand { get; set; }

    public ObservableCollection<ChatItem> Items { get; set; }

    public Author BotAuthor { get; set; }

    public Author Me
    {
        get
        {
            return this.me;
        }
        set
        {
            this.UpdateValue(ref this.me, value);
        }
    }

    public bool IsChatPickerVisible
    {
        get
        {
            return this.isChatPickerVisible;
        }
        set
        {
            this.UpdateValue(ref this.isChatPickerVisible, value);
        }
    }

    public PickerContext Context
    {
        get
        {
            return this.context;
        }
        set
        {
            this.UpdateValue(ref this.context, value);
        }
    }

    public string PickerHeaderText
    {
        get
        {
            return this.pickerHeaderText;
        }
        set
        {
            this.UpdateValue(ref this.pickerHeaderText, value);
        }
    }

    public bool IsBusy
    {
        get
        {
            return this.isBusy;
        }
        set
        {
            this.UpdateValue(ref this.isBusy, value);
        }
    }

    private void OnMessageReceived(Activity activity)
    {
        if (this.IsBusy)
        {
            this.IsBusy = false;
        }

        if (activity.Type != "message")
        {
            return;
        }

        this.Items.Remove(this.waitingForBotMessage);

        if (!string.IsNullOrEmpty(activity.Text))
        {
            TextMessage textMessage = new TextMessage();
            textMessage.Text = activity.Text;
            textMessage.Author = this.BotAuthor;

            this.Items.Add(textMessage);
            this.HandleDateSelection(activity);
            this.HandleSuggestedActions(activity);
        }

        if (activity.AttachmentLayout == "carousel")
        {
            this.Items.Add(this.CreateCarouselPickerItem(activity));
        }
        else if (activity.AttachmentLayout == "list" || activity.Attachments != null)
        {
            this.RenderList(activity);
        }
    }

    private void OnItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (ChatItem chatItem in e.NewItems)
            {
                TextMessage textMessage = chatItem as TextMessage;
                if (textMessage != null && textMessage.Author == this.Me)
                {
                    this.DispatchSendMessageToBot(textMessage.Text);
                }
            }
        }
    }

    private void DispatchSendMessageToBot(string text)
    {
        App.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () =>
        {
            this.Items.Add(this.waitingForBotMessage);
            this.chatService.SendMessageAsync(this.Me.Name, text);
        });
    }

    private void HandleSuggestedActions(Activity activity)
    {
        if (activity.SuggestedActions != null)
        {
            ItemPickerContext itemPickerContext = null;
            if (activity.Text == "Here are some recommended locations for you:")
            {
                List<TravelCardAction> travelActions = new List<TravelCardAction>();
                foreach (CardAction action in activity.SuggestedActions.Actions)
                {
                    travelActions.Add(new TravelCardAction() { DestinationName = action.Title });
                }

                itemPickerContext = new ItemPickerContext { ItemsSource = travelActions };

                this.Context = itemPickerContext;
                this.PickerHeaderText = "Select a Location";
                this.IsChatPickerVisible = true;
            }
            else
            {
                var suggestedActionsItem = new SuggestedActionsItem();
                var actions = activity.SuggestedActions.Actions.Select(a => new SuggestedAction()
                {
                    Text = a.Title,
                    Command = new Command(new Action(() =>
                    {
                        this.Items.Remove(suggestedActionsItem);
                        this.Items.Add(new TextMessage { Author = this.Me, Text = a.Title });
                    }))
                }).ToList();
                suggestedActionsItem.Actions = actions;

                this.Items.Add(suggestedActionsItem);
            }
        }
    }

    private void HandleDateSelection(Activity activity)
    {
        if (activity.Text == "When do you want your vacation to start?" || activity.Text.Contains("This doesn't seem to be a valid date."))
        {
            this.Items.Add(this.CreateDatePickerChatItem());
        }
    }

    private ChatItem CreateDatePickerChatItem()
    {
        DatePickerContext datePickercontext = new DatePickerContext();
        PickerItem pickerItem = new PickerItem();
        pickerItem.Data = datePickercontext;
        pickerItem.Context = datePickercontext;

        datePickercontext.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(DatePickerContext.SelectedDate))
            {
                if (datePickercontext.SelectedDate != null)
                {
                    this.Items.Remove(pickerItem);

                    string selectedDate = datePickercontext.SelectedDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    this.Items.Add(new TextMessage() { Author = this.Me, Text = selectedDate });
                }
            }
        };

        return pickerItem;
    }

    private bool OnOkPickerCommandCanExecute(object arg)
    {
        if (this.context != null)
        {
            return this.context.CanExecuteOk();
        }

        return true;
    }

    private void OnOkPickerCommandExecuted(object obj)
    {
        ItemPickerContext itemPickerContext = this.Context as ItemPickerContext;
        if (itemPickerContext != null && itemPickerContext.SelectedItem != null)
        {
            string selectedItem;
            if (itemPickerContext.SelectedItem is TravelCardAction)
            {
                selectedItem = ((TravelCardAction)itemPickerContext.SelectedItem).DestinationName;
            }
            else
            {
                selectedItem = ((AirlineCompany)itemPickerContext.SelectedItem).Value;
            }

            TextMessage pickerMessage = new TextMessage();
            pickerMessage.Text = selectedItem;
            pickerMessage.Author = this.Me;

            this.Items.Add(pickerMessage);
        }

        this.IsChatPickerVisible = false;
    }

    private ChatItem CreateCarouselPickerItem(Activity activity)
    {
        CardPickerContext cardContext = new CardPickerContext();
        List<CardContext> imageCards = new List<CardContext>();
        PickerItem cardPickerItem = new PickerItem();

        foreach (Attachment attachment in activity.Attachments)
        {
            if (attachment.ContentType != "application/vnd.microsoft.card.hero")
            {
                continue;
            }

            ImageCardContext imageContext = this.CreateImageContext(cardPickerItem, attachment);
            imageCards.Add(imageContext);
        }

        cardContext.Cards = imageCards;
        cardPickerItem.Context = cardContext;

        return cardPickerItem;
    }

    private ImageCardContext CreateImageContext(PickerItem cardPickerItem, Attachment attachment)
    {
        HeroCard card = JsonConvert.DeserializeObject<HeroCard>(attachment.Content.ToString());
        ImageCardContext imageContext = new ImageCardContext();

        if (card.Images != null && card.Images.Count > 0)
        {
            imageContext.Image = card.Images[0].Url;
        }

        imageContext.Title = card.Title;
        imageContext.Subtitle = card.Subtitle;
        imageContext.Description = card.Text;

        foreach (CardAction action in card.Buttons)
        {
            CardAction currentAction = action;
            CardActionContext actionContext = new CardActionContext()
            {
                Text = currentAction.Title,
                Command = new Command(() =>
                {
                    this.Items.Remove(cardPickerItem);
                    this.Items.Add(new TextMessage() { Author = this.Me, Text = currentAction.Value.ToString() });
                })
            };

            imageContext.Actions.Add(actionContext);
        }

        return imageContext;
    }

    private void RenderList(Activity activity)
    {
        foreach (Attachment attachment in activity.Attachments)
        {
            if (attachment.ContentType == "application/vnd.microsoft.card.hero")
            {
                this.RenderHeroCard(attachment);
            }
            else if (attachment.ContentType == "application/vnd.microsoft.card.adaptive")
            {
                this.RenderAdaptiveCard(attachment);
            }
        }
    }

    private void RenderAdaptiveCard(Attachment attachment)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Converters.Add(new JsonAdaptiveElementToObjectConverter());

        AdaptiveCard adaptiveCard = JsonConvert.DeserializeObject<AdaptiveCard>(attachment.Content.ToString(), settings);
        if (adaptiveCard.TType == "Flight")
        {
            string passengerName = ((AdaptiveTextBlock)adaptiveCard.Body[1]).Text;
            string outDepartureCity = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[0].Items[0]).Text;
            string outDepartureAirport = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[0].Items[1]).Text;
            DateTime outDepartureDateTime = DateTime.ParseExact(((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[0].Items[2]).Text
                + " " + ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[0].Items[3]).Text, "d MMM yyyy hh:mm tt", CultureInfo.InvariantCulture);
            string outArrivalCity = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[2].Items[0]).Text;
            string outArrivalAirport = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[2].Items[1]).Text;
            DateTime outArrivalDateTime = DateTime.ParseExact(((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[2].Items[2]).Text
                + " " + ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[2].Items[3]).Text, "d MMM yyyy hh:mm tt", CultureInfo.InvariantCulture);
            string inDepartureCity = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[0].Items[0]).Text;
            string inDepartureAirport = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[0].Items[1]).Text;
            DateTime inDepartureDateTime = DateTime.ParseExact(((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[0].Items[2]).Text
                + " " + ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[0].Items[3]).Text, "d MMM yyyy hh:mm tt", CultureInfo.InvariantCulture);
            string inArrivalCity = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[2].Items[0]).Text;
            string inArrivalAirport = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[2].Items[1]).Text;
            DateTime inArrivalDateTime = DateTime.ParseExact(((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[2].Items[2]).Text
                + " " + ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[4]).Columns[2].Items[3]).Text, "d MMM yyyy hh:mm tt", CultureInfo.InvariantCulture);
            string planeImage = ((AdaptiveImage)((AdaptiveColumnSet)adaptiveCard.Body[3]).Columns[1].Items[0]).Url.OriginalString;
            string total = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[5]).Columns[1].Items[0]).Text;

            List<FlightInfo> flights = new List<FlightInfo>()
            {
                new FlightInfo()
                {
                    DepartureCity = outDepartureCity,
                    DepartureAirport = outDepartureAirport,
                    DepartureDate = outDepartureDateTime,
                    ArrivalCity = outArrivalCity,
                    ArrivalAirport = outArrivalAirport,
                    ArrivalDate = outArrivalDateTime,
                    PlaneImageUrl = planeImage
                },
                new FlightInfo()
                {
                    DepartureCity = inDepartureCity,
                    DepartureAirport = inDepartureAirport,
                    DepartureDate = inDepartureDateTime,
                    ArrivalCity = inArrivalCity,
                    ArrivalAirport = inArrivalAirport,
                    ArrivalDate = inArrivalDateTime,
                    PlaneImageUrl = planeImage
                }
            };

            FlightChatContext context = new FlightChatContext()
            {
                Flights = flights,
                PassangerName = passengerName,
                TotalTicketPrice = total
            };

            ChatItem flightItem = new ChatItem();
            flightItem.Data = context;

            this.Items.Add(flightItem);
        }
        else if (adaptiveCard.TType == "Summary")
        {
            string summaryTitle = ((AdaptiveTextBlock)adaptiveCard.Body[1]).Text;
            summaryTitle = summaryTitle.Replace("($100.00 pp/night)", string.Empty);

            Summary summary = new Summary();
            summary.Image = ((AdaptiveImage)adaptiveCard.Body[0]).Url.OriginalString;
            summary.Title = summaryTitle;
            summary.Hotel = ((AdaptiveTextBlock)adaptiveCard.Body[3]).Text;

            if (adaptiveCard.Body.Count > 6)
            {
                summary.Flights = new List<string>()
                {
                    ((AdaptiveTextBlock)adaptiveCard.Body[5]).Text,
                    ((AdaptiveTextBlock)adaptiveCard.Body[6]).Text
                };
            }

            summary.TotalPrice = ((AdaptiveTextBlock)((AdaptiveColumnSet)adaptiveCard.Body[adaptiveCard.Body.Count - 1]).Columns[1].Items[0]).Text;

            ChatItem pickerItem = new ChatItem { Data = summary };
            this.Items.Add(pickerItem);
        }
    }

    private void RenderHeroCard(Attachment attachment)
    {
        HeroCard heroCard = JsonConvert.DeserializeObject<HeroCard>(attachment.Content.ToString());

        if (heroCard.Buttons == null || heroCard.Buttons.Count == 0)
        {
            return;
        }

        List<AirlineCompany> airlineCards = new List<AirlineCompany>();
        foreach (CardAction action in heroCard.Buttons)
        {
            AirlineCompany airline = new AirlineCompany();
            airline.Name = action.Title;
            airline.CompanyLogo = action.Image;
            airline.Value = action.Value.ToString();
            airlineCards.Add(airline);
        }

        ItemPickerContext itemPickerContext = new ItemPickerContext { ItemsSource = airlineCards };
        this.Context = itemPickerContext;
        this.PickerHeaderText = "Select an Airline Company";
        this.IsChatPickerVisible = true;
    }
}