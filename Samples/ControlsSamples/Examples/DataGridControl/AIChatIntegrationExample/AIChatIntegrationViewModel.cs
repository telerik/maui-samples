using Microsoft.Maui.Controls;
using QSF.Examples.ChatControl;
using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls.BottomSheet;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public class AIChatIntegrationViewModel : ExampleViewModel
{
    private const string GreetingsMessage = "👋 Hi! I'm your AI assistant. I can help you control the shipment data grid. Try asking me to:\n\n• Sort by single column (e.g., \"sort by id\")\n• Multi-column sort (e.g., \"sort by country then city\")\n• Filter by order id or city\n\nWhat would you like to do?";
    private static readonly HttpClient HttpClient = new HttpClient();
    private ICommand visualizeChatCommand;
    private ICommand hideChatCommand;
    private ICommand sendMessageCommand;
    private ICommand suggestionTapCommand;
    private Func<string, string> aIRequestFunc;
    private Action<string> aIResponseAction;
    private ObservableCollection<string> suggestedPrompts;
    private ObservableCollection<BottomSheetState> bottomSheetStates;
    private bool isChatVisible = false;
    private bool isProcessingAIRequest = false;
    private bool isResetAllowed = false;
    private object prompt;

    public AIChatIntegrationViewModel()
    {
        this.Me = ConversationConstants.aiAuthorId;
        this.Bot = ConversationConstants.aiBotAuthorId;

        this.suggestedPrompts = this.CreateDefaultSuggestedPrompts();
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        this.visualizeChatCommand = new Command(() => this.IsChatVisible = true);
        this.hideChatCommand = new Command(() => this.IsChatVisible = false);
        this.sendMessageCommand = new Command(async () => await this.ExecuteSendMessageCommand(), () => this.CanExecuteSendMessageCommand());
        this.suggestionTapCommand = new Command((s) => this.ExecuteSuggestionTapped(s));

        var messages = new ObservableCollection<object> { new MessageItem { Author = this.Bot, Text = GreetingsMessage } };
        this.Messages = messages;
    }

    public object Me { get; }

    public object Bot { get; }

    public ObservableCollection<Order> Orders { get; private set; }

    public ObservableCollection<object> Messages { get; } = new ObservableCollection<object>();

    public ObservableCollection<string> SuggestedPrompts => this.suggestedPrompts;

    public ObservableCollection<BottomSheetState> BottomSheetStates
    {
        get => this.bottomSheetStates;
        set
        {
            if (this.UpdateValue(ref this.bottomSheetStates, value))
            {
                this.bottomSheetStates.Add(BottomSheetState.HiddenState);
                this.bottomSheetStates.Add(new BottomSheetState(BottomSheetState.PartialStateName, new BottomSheetLength(60, true)));
            }
        }
    }

    public Func<string, string> AIRequestFunc
    {
        get => this.aIRequestFunc;
        set => this.UpdateValue(ref this.aIRequestFunc, value);
    }

    public Action<string> AIResponseAction
    {
        get => this.aIResponseAction;
        set => this.UpdateValue(ref this.aIResponseAction, value);
    }

    public ICommand VisualizeChatCommand => this.visualizeChatCommand;

    public ICommand HideChatCommand => this.hideChatCommand;

    public ICommand SendMessageCommand => this.sendMessageCommand;

    public ICommand SuggestionTapCommand => this.suggestionTapCommand;

    public bool IsChatVisible
    {
        get => this.isChatVisible;
        set => this.UpdateValue(ref this.isChatVisible, value);
    }

    public bool IsProcessingAIRequest
    {
        get => this.isProcessingAIRequest;
        private set => this.UpdateValue(ref this.isProcessingAIRequest, value, this.OnIsProcessingAIRequestChanged);
    }

    public bool IsResetAllowed
    {
        get => this.isResetAllowed;
        set => this.UpdateValue(ref this.isResetAllowed, value);
    }

    public object Prompt
    {
        get => this.prompt;
        set => this.UpdateValue(ref this.prompt, value);
    }

    protected virtual ObservableCollection<string> CreateDefaultSuggestedPrompts()
    {
        return new ObservableCollection<string>
        {
            "Sort by city ascending",
            "Group by order date",
            "Filter by freight greater than $50.00",
            "Clear all filters, sorting and grouping",
            "Show Ships only from Germany and sort them by shipped date descending",
            "Lock order date column and sort it descending",
        };
    }

    private void OnIsProcessingAIRequestChanged(bool _) => (this.sendMessageCommand as Command)?.ChangeCanExecute();

    private bool CanExecuteSendMessageCommand()
    {
        if (this.isProcessingAIRequest)
        {
            return false;
        }

        string promptString = this.prompt as string;
        if (string.IsNullOrWhiteSpace(promptString))
        {
            return false;
        }

        return true;
    }

    private async Task ExecuteSendMessageCommand()
    {
        try
        {
            this.IsProcessingAIRequest = true;

            string promptStr = this.prompt as string;
            this.Prompt = string.Empty;

            this.Messages.Add(new MessageItem { Author = this.Me, Text = promptStr });

            var requestJson = this.AIRequestFunc?.Invoke(promptStr);
            var columnIdToHeaderMap = this.CacheColumnMappings(requestJson);

            var request = JsonSerializer.Deserialize<object>(requestJson);
            var requestResult = await HttpClient.PostAsJsonAsync("https://demos.telerik.com/service/v2/ai/grid/smart-state", request);
            var responseJson = await requestResult.Content.ReadAsStringAsync();

            var messages = this.ExtractResponseMessage(responseJson, columnIdToHeaderMap);
            this.Messages.Add(new MessageItem { Author = this.Bot, Text = string.Join("\n\n", messages) });

            this.AIResponseAction?.Invoke(responseJson);

            this.IsResetAllowed = true;
        }
        catch (Exception ex)
        {
            this.Messages.Add(new MessageItem { Author = this.Bot, Text = $"Failed to process request: {ex.Message} Try again." });
        }
        finally
        {
            this.IsProcessingAIRequest = false;
        }
    }

    private Dictionary<string, string> CacheColumnMappings(string requestJson)
    {
        var columnsIdToHeaderMap = new Dictionary<string, string>();

        try
        {
            var node = JsonNode.Parse(requestJson);
            var columns = node?["columns"]?.AsArray();
            
            if (columns != null)
            {
                foreach (var column in columns)
                {
                    var id = column?["id"]?.GetValue<string>();
                    var header = column?["header"]?.GetValue<string>();
                    
                    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(header))
                    {
                        columnsIdToHeaderMap[id] = header;
                    }
                }
            }

            return columnsIdToHeaderMap;
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private string ExtractResponseMessage(string responseJson, Dictionary<string, string> columnIdToHeaderMap)
    {
        try
        {
            var node = JsonNode.Parse(responseJson);
            var rootMessage = node?["message"]?.GetValue<string>();

            var commands = node?["commands"]?.AsArray();
            var commandMessages = commands?.Select(cmd => cmd?["message"]?.GetValue<string>()).Where(m => !string.IsNullOrWhiteSpace(m)) ?? Enumerable.Empty<string>();

            var allMessages = !string.IsNullOrWhiteSpace(rootMessage) ? new[] { rootMessage }.Concat(commandMessages) : commandMessages;
            var joinedMessage = string.Join("\n\n", allMessages);
            
            return this.ReplaceColumnIdsWithHeaders(joinedMessage, columnIdToHeaderMap);
        }
        catch
        {
            return string.Empty;
        }
    }

    private string ReplaceColumnIdsWithHeaders(string message, Dictionary<string, string> columnIdToHeaderMap)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return message;
        }

        var result = message;
        foreach (var kvp in columnIdToHeaderMap)
        {
            result = result.Replace(kvp.Key, kvp.Value);
        }
        
        return result;
    }

    private void ExecuteSuggestionTapped(object suggestion)
    {
        if (suggestion is string suggestionString)
        {
            this.Prompt = suggestionString;
        }
    }
}
