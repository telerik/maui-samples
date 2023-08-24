using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private static readonly Random random = new();
    private static readonly List<string> cardTitles = new() { "Fitness Salad", "Naked Beef Bowl", "Buddha Bowl" };
    private static readonly List<string> cardSubtitles = new() { "Calories: 315", "Calories: 370", "Calories: 336" };
    private static readonly List<string> cardDescriptions = new()
    {
        "Chicken fillet, eggs, tomatoes, cucumber, avocado, sweet corn, red cabbage, lettuce",
        "Boneless beef sirloin steak, sautéed spinach, red onions, chilli pepper, fresh salad",
        "Chickpea, sweet potato, tomatoes, avocado, watermelon radish, red cabbage, microgreens"
    };
    private static readonly List<string> cardImages = new()
    {
        "chat_configuration_image.png",
        "chat_configuration_image_1.png",
        "chat_configuration_image_2.png",
    };

    private readonly List<string> randomTextMessages = new() { "This is a message", "The message", "Message content" };
    private readonly List<string> selectedSuggestionsCache = new List<string>();
    private readonly string showTypingIndicatorText = "Show Typing Indicator";
    private readonly string hideTypingIndicatorText = "Hide Typing Indicator";
    private readonly Author authorBot;
    private Author authorMe;

    private string currentAuthorName;
    private string message;
    private MessageType messageType;
    private MessageRenderMode messageRenderMode = MessageRenderMode.Inline;
    private bool canChangeMessageRenderMode;

    private PickerContext pickerContext;
    private string pickerHeaderText;
    private bool isPickerVisible;
    private bool arePickerButtonsVisible;

    private ObservableCollection<object> selectedSuggestions;

    private string timeBreakText = "Unread";

    private bool isTypingIndicatorVisible;
    private string typingIndicatorText;
    private string toggleTypingIndicatorText;

    public ConfigurationViewModel()
    {
        this.authorMe = new Author() { Name = "Me" };
        this.authorBot = new Author() { Name = "Bot" };
        this.currentAuthorName = this.authorMe.Name;

        this.SuggestedActions = new ObservableCollection<string> { "Yes", "No", "Ok", "Thank you", "I'll get back to you" };
        this.selectedSuggestionsCache.Add(this.SuggestedActions[0]);
        this.selectedSuggestionsCache.Add(this.SuggestedActions[1]);
        this.selectedSuggestionsCache.Add(this.SuggestedActions[2]);

        this.typingIndicatorText = "Someone is typing";
        this.toggleTypingIndicatorText = this.showTypingIndicatorText;
        this.TypingIndicatorAuthors = new ObservableCollection<Author>() { this.GetChatAuthor() };

        this.Items = new ObservableCollection<ChatItem>
        {
            new TextMessage() { Author = this.authorBot, Text = "Welcome to the RadChat configurator!" },
            new TextMessage() { Author = this.authorBot, Text = "You can add new messages and customize the control by using the toolbox on the right." }
        };

        this.SendMessageCommand = new Command(this.OnSendMessageCommandExecuted);
        this.AddMessageCommand = new Command(this.OnAddMessageCommandExecuted);
        this.PickerOkCommand = new Command(this.OnPickerOkCommandExecuted, this.CanExecutePickerOkCommand);
        this.PickerCancelCommand = new Command(this.OnPickerCancelCommandExecuted);
        this.AddSuggestedActionsCommand = new Command(this.OnAddSuggestedActionsCommandExecuted, this.CanExecuteAddSuggestedActionsCommand);
        this.AddTimeBreakCommand = new Command(this.OnAddTimeBreakCommandExecuted, this.CanExecuteAddTimeBreakCommand);
        this.ToggleTypingIndicatorCommand = new Command(this.OnToggleTypingIndicatorCommandExecuted, this.CanExecuteToggleTypingIndicatorCommand);
    }

    public string Me { get; } = "Me";

    public string Bot { get; } = "Bot";

    public ObservableCollection<string> SuggestedActions { get; set; }

    public ObservableCollection<Author> TypingIndicatorAuthors { get; set; }

    public IEnumerable<MessageType> MessageTypes { get; } = Enum.GetValues(typeof(MessageType)).Cast<MessageType>();

    public IList<ChatItem> Items { get; set; }

    public ICommand AddMessageCommand { get; set; }

    public ICommand PickerOkCommand { get; set; }

    public ICommand PickerCancelCommand { get; set; }

    public Command AddSuggestedActionsCommand { get; set; }

    public ICommand AddTimeBreakCommand{ get; set; }

    public ICommand ToggleTypingIndicatorCommand{ get; set; }

    public ICommand SendMessageCommand{ get; set; }

    public Author AuthorMe
    {
        get => this.authorMe;
        set => this.UpdateValue(ref this.authorMe, value);
    }

    public string CurrentAuthorName
    {
        get => this.currentAuthorName;
        set => this.UpdateValue(ref this.currentAuthorName, value);
    }

    public string Message
    {
        get => this.message;
        set => this.UpdateValue(ref this.message, value);
    }

    public MessageType MessageType
    {
        get => this.messageType;
        set
        {
            if (this.UpdateValue(ref this.messageType, value))
            {
                this.CanChangeMessageRenderMode = this.messageType != MessageType.Text;
            }
        }
    }

    public MessageRenderMode MessageRenderMode
    {
        get => this.messageRenderMode;
        set => this.UpdateValue(ref this.messageRenderMode, value);
    }

    public bool CanChangeMessageRenderMode
    {
        get => this.canChangeMessageRenderMode;
        set
        {
            if (this.UpdateValue(ref this.canChangeMessageRenderMode, value))
            {
                if (!this.canChangeMessageRenderMode)
                {
                    this.MessageRenderMode = MessageRenderMode.Inline;
                }
            }
        }
    }

    public PickerContext PickerContext
    {
        get => this.pickerContext;
        set => this.UpdateValue(ref this.pickerContext, value);
    }

    public string PickerHeaderText
    {
        get => this.pickerHeaderText;
        set => this.UpdateValue(ref this.pickerHeaderText, value);
    }

    public bool IsPickerVisible
    {
        get => this.isPickerVisible;
        set => this.UpdateValue(ref this.isPickerVisible, value);
    }

    public bool ArePickerButtonsVisible
    {
        get => this.arePickerButtonsVisible;
        set => this.UpdateValue(ref this.arePickerButtonsVisible, value);
    }

    public ObservableCollection<object> SelectedSuggestions
    {
        get => this.selectedSuggestions;
        set
        {
            if (this.UpdateValue(ref this.selectedSuggestions, value))
            {
                if (this.selectedSuggestions != null)
                {
                    foreach (var suggestion in this.selectedSuggestionsCache)
                    {
                        this.selectedSuggestions.Add(suggestion);
                    }

                    this.selectedSuggestions.CollectionChanged += this.SelectedSuggestionsChanged;
                }
            }
        }
    }

    public string TimeBreakText
    {
        get => this.timeBreakText;
        set
        {
            if (this.UpdateValue(ref this.timeBreakText, value))
            {
                ((Command)this.AddTimeBreakCommand).ChangeCanExecute();
            }
        }
    }

    public bool IsTypingIndicatorVisible
    {
        get => this.isTypingIndicatorVisible;
        set
        {
            if (this.UpdateValue(ref this.isTypingIndicatorVisible, value))
            {
                this.ToggleTypingIndicatorText = this.isTypingIndicatorVisible ? this.hideTypingIndicatorText : this.showTypingIndicatorText;
            }

            ((Command)this.ToggleTypingIndicatorCommand).ChangeCanExecute();
        }
    }

    public string TypingIndicatorText
    {
        get => this.typingIndicatorText;
        set
        {
            if (this.UpdateValue(ref this.typingIndicatorText, value))
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    this.IsTypingIndicatorVisible = false;
                }
                else
                {
                    ((Command)this.ToggleTypingIndicatorCommand).ChangeCanExecute();
                }
            }
        }
    }

    public string ToggleTypingIndicatorText
    {
        get => this.toggleTypingIndicatorText;
        set => this.UpdateValue(ref this.toggleTypingIndicatorText, value);
    }

    internal Author GetChatAuthor()
    {
        return this.CurrentAuthorName == this.authorMe.Name ? this.authorMe : this.authorBot;
    }

    private void OnSendMessageCommandExecuted()
    {
        if (String.IsNullOrEmpty(this.Message))
        {
            return;
        }

        TextMessage item = new()
        {
            Author = this.GetChatAuthor(),
            Text = this.Message
        };

        this.Items.Add(item);
        this.Message = String.Empty;
    }

    private void OnAddMessageCommandExecuted(object arg)
    {
        bool renderInline = this.MessageRenderMode == MessageRenderMode.Inline;
        Author author = this.GetChatAuthor();

        switch (this.messageType)
        {
            case MessageType.Text:
                this.AddTextMessage(author);
                return;
            case MessageType.List:
                this.AddListMessage(author, renderInline);
                return;
            case MessageType.Calendar:
                this.AddCalendarMessage(author, renderInline);
                return;
            case MessageType.Timespan:
                this.AddTimeSpanMessage(author, renderInline);
                return;
            case MessageType.Image:
                this.AddImageMessage(author, renderInline);
                return;
            case MessageType.Card:
                this.AddCardMessage(author, renderInline);
                return;
            default:
                return;
        }
    }

    private void ShowChatPicker(PickerContext context, string header, bool showButtons = true)
    {
        this.PickerContext = context;
        this.PickerHeaderText = header;
        this.IsPickerVisible = true;
        this.ArePickerButtonsVisible = showButtons;

        ((Command)this.PickerOkCommand).ChangeCanExecute();

        context.PropertyChanged += (s, e) =>
        {
            string property = e.PropertyName;

            if (context is ItemPickerContext && property == nameof(ItemPickerContext.SelectedItem))
            {
                ((Command)this.PickerOkCommand).ChangeCanExecute();
            }
            else if (context is DatePickerContext && property == nameof(DatePickerContext.SelectedDate))
            {
                ((Command)this.PickerOkCommand).ChangeCanExecute();
            }
            else if (context is TimePickerContext && property == nameof(TimePickerContext.SelectedValue))
            {
                ((Command)this.PickerOkCommand).ChangeCanExecute();
            }
        };
    }

    private void HideChatPicker()
    {
        this.PickerContext = null;
        this.IsPickerVisible = false;
    }

    private void AddTextMessage(Author author)
    {
        this.Items.Add(new TextMessage()
        {
            Author = author,
            Text = this.randomTextMessages[random.Next(0, this.randomTextMessages.Count)]
        });
    }

    private void AddListMessage(Author author, bool renderInline)
    {
        var coffeeTypes = new List<string>
        {
            "Caffe Latte",
            "Caffe Mocha",
            "Frappuccino",
            "Cuban Espresso",
            "Iced Coffee",
            "Americano"
        };
        var pickerContext = new ItemPickerContext { ItemsSource = coffeeTypes };
        string promptString = "Pick your favorite coffee type:";

        if (renderInline)
        {
            PickerItem pickerItem = new() { Context = pickerContext };
            this.Items.Add(new TextMessage { Author = author, Text = promptString });
            this.Items.Add(pickerItem);
            pickerContext.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedItem" && pickerContext.SelectedItem != null)
                {
                    this.Items.Remove(pickerItem);
                    this.Items.Add(new TextMessage { Author = author, Text = "" + pickerContext.SelectedItem });
                    this.HideChatPicker();
                }
            };
        }
        else
        {
            this.ShowChatPicker(pickerContext, promptString);
        }
    }

    private void AddCalendarMessage(Author author, bool renderInline)
    {
        var pickerContext = new DatePickerContext { MinDate = new DateTime(2000, 1, 1), MaxDate = DateTime.Now };
        string promptString = "Pick a date";

        if (renderInline)
        {
            PickerItem pickerItem = new() { Context = pickerContext };
            this.Items.Add(new TextMessage { Text = promptString, Author = author });
            this.Items.Add(pickerItem);
            pickerContext.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedDate" && pickerContext.SelectedDate != null)
                {
                    this.Items.Remove(pickerItem);
                    this.Items.Add(new TextMessage { Author = author, Text = "" + pickerContext.SelectedDate });
                    this.HideChatPicker();
                }
            };
        }
        else
        {
            this.ShowChatPicker(pickerContext, promptString);
        }
    }

    private void AddTimeSpanMessage(Author author, bool renderInline)
    {
        var pickerContext = new TimePickerContext { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(17) };
        string promptString = "Pick a time";

        if (renderInline)
        {
            PickerItem pickerItem = new() { Context = pickerContext };
            this.Items.Add(new TextMessage { Text = promptString, Author = author });
            this.Items.Add(pickerItem);
            pickerContext.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedValue" && pickerContext.SelectedValue != null)
                {
                    this.Items.Remove(pickerItem);
                    this.Items.Add(new TextMessage { Author = author, Text = "" + pickerContext.SelectedValue });
                    this.HideChatPicker();
                }
            };
        }
        else
        {
            this.ShowChatPicker(pickerContext, promptString);
        }
    }

    private ICollection<CardActionContext> GetCardActions(ChatItem chatItem, string cardActionResultText)
    {
        bool isInline = this.MessageRenderMode == MessageRenderMode.Inline;
        var selectAction = new CardActionContext
        {
            Text = "Pick",
            Command = new Command(() =>
            {
                if (isInline)
                {
                    var index = this.Items.IndexOf(chatItem);
                    this.Items.RemoveAt(index);
                }
                else
                {
                    this.HideChatPicker();
                }

                this.Items.Add(new TextMessage { Author =  this.GetChatAuthor(), Text = cardActionResultText });
            }),
        };
        return new List<CardActionContext>() { selectAction };
    }

    private List<CardContext> GetImageCards(ChatItem chatItem)
    {
        List<CardContext> imageCards = new();

        for (int i = 0; i <= 2; i++)
        {
            imageCards.Add(new ImageCardContext()
            {
                Title = cardTitles[i],
                Subtitle = cardSubtitles[i],
                Description = cardDescriptions[i],
                Actions = this.GetCardActions(chatItem, cardTitles[i]),
                Image = cardImages[i]
            });
        }

        return imageCards;
    }

    private List<CardContext> GetCards(ChatItem chatItem)
    {
        List<CardContext> cards = new();

        for (int i = 0; i <= 2; i++)
        {
            cards.Add(new BasicCardContext()
            {
                Title = cardTitles[i],
                Subtitle = cardSubtitles[i],
                Description = cardDescriptions[i],
                Actions = this.GetCardActions(chatItem, cardTitles[i])
            });
        }

        return cards;
    }

    private void AddCardMessageCore(Author author, bool renderInline, bool includeImage)
    {
        PickerItem pickerItem = new();
        var pickerContext = new CardPickerContext() { Cards = includeImage ? this.GetImageCards(pickerItem) : this.GetCards(pickerItem) };
        string promptString = "Pick a dish";
        pickerItem.Context = pickerContext;

        if (renderInline)
        {
            this.Items.Add(new TextMessage { Author = author, Text = promptString });
            this.Items.Add(pickerItem);
        }
        else
        {
            this.ShowChatPicker(pickerContext, promptString, false);
        }
    }

    private void AddImageMessage(Author author, bool renderInline)
    {
        this.AddCardMessageCore(author, renderInline, true);
    }

    private void AddCardMessage(Author author, bool renderInline)
    {
        this.AddCardMessageCore(author, renderInline, false);
    }

    private bool CanExecutePickerOkCommand()
    {
        var pickerContext = this.PickerContext;
        if (pickerContext is null)
        {
            return false;
        }

        if (pickerContext is ItemPickerContext itemPickerContext)
        {
            return !String.IsNullOrEmpty(itemPickerContext.SelectedItem?.ToString());
        }

        if (pickerContext is DatePickerContext datePickerContext)
        {
            return !String.IsNullOrEmpty(datePickerContext.SelectedDate?.ToString());
        }

        if (pickerContext is TimePickerContext timePickerContext)
        {
            return !String.IsNullOrEmpty(timePickerContext.SelectedValue?.ToString());
        }

        return false;
    }

    private void OnPickerOkCommandExecuted()
    {
        string text = string.Empty;
        var pickerContext = this.PickerContext;

        if (pickerContext is ItemPickerContext itemPickerContext)
        {
            text += itemPickerContext.SelectedItem.ToString();
        }

        if (pickerContext is DatePickerContext datePickerContext)
        {
            text += datePickerContext.SelectedDate;
        }

        if (pickerContext is TimePickerContext timePickerContext)
        {
            text += timePickerContext.SelectedValue;
        }

        this.HideChatPicker();
        this.Items.Add(new TextMessage { Text = text, Author = this.GetChatAuthor() });
    }

    private void OnPickerCancelCommandExecuted()
    {
        this.HideChatPicker();
    }

    private void SelectedSuggestionsChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.AddSuggestedActionsCommand.ChangeCanExecute();

        var action = e.Action;
        if (action == NotifyCollectionChangedAction.Add)
        {
            var item = (string)e.NewItems[0];
            if (!this.selectedSuggestionsCache.Contains(item))
            {
                this.selectedSuggestionsCache.Add(item);
            }
        }
        else if (action == NotifyCollectionChangedAction.Remove)
        {
            this.selectedSuggestionsCache.Remove((string)e.OldItems[0]);
        }
    }

    private ICollection<SuggestedAction> GetSuggestedActions(SuggestedActionsItem suggestedActionsItem)
    {
        var actions = new List<SuggestedAction>();

        foreach (var action in this.SelectedSuggestions)
        {
            string actionText = action.ToString();
            actions.Add(new SuggestedAction
            {
                Text = actionText,
                Command = new Command(() =>
                {
                    this.Items.Remove(suggestedActionsItem);
                    this.Items.Add(new TextMessage { Author = this.GetChatAuthor(), Text = actionText });
                }),
            });
        }

        return actions;
    }


    private void OnAddSuggestedActionsCommandExecuted()
    {
        var suggestedActionsItem = new SuggestedActionsItem();
        suggestedActionsItem.Actions = this.GetSuggestedActions(suggestedActionsItem);
        this.Items.Add(suggestedActionsItem);
    }

    private bool CanExecuteAddSuggestedActionsCommand() => this.selectedSuggestions?.Count > 0;

    private void OnAddTimeBreakCommandExecuted()
    {
        this.Items.Add(new TimeBreak() { Text = this.TimeBreakText });
    }

    private bool CanExecuteAddTimeBreakCommand() => !String.IsNullOrWhiteSpace(this.timeBreakText);

    private void OnToggleTypingIndicatorCommandExecuted()
    {
        this.IsTypingIndicatorVisible = !this.isTypingIndicatorVisible;
    }

    private bool CanExecuteToggleTypingIndicatorCommand()
    {
        if (!this.IsTypingIndicatorVisible)
        {
            return !String.IsNullOrWhiteSpace(this.TypingIndicatorText);
        }
        else
        {
            return true;
        }
    }
}