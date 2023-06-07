using Newtonsoft.Json;
using QSF.Examples.ChatControl.TravelAssistanceExample.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QSF.Examples.ChatControl;

public class AzureChatBotService
{
    private HttpClient httpClient;
    private Conversation lastConversation;
    private static string BotBaseAddress = "https://directline.botframework.com/v3/directline/conversations/";
    private string directLineKey;
    private string botId;
    private Action<Activity> messageReceivedAction;
    private string watermark = null;

    public AzureChatBotService(string directLineKey, string botId, Action<Activity> messageReceivedAction)
    {
        this.directLineKey = directLineKey;
        this.messageReceivedAction = messageReceivedAction;
        this.botId = botId;

        this.httpClient = new HttpClient();
        this.httpClient.Timeout = Timeout.InfiniteTimeSpan;
        this.httpClient.BaseAddress = new Uri(AzureChatBotService.BotBaseAddress);
        this.httpClient.DefaultRequestHeaders.Accept.Clear();
        this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.directLineKey);
        this.SetupConversation();
    }

    private async void SetupConversation()
    {
        Conversation conversation = new Conversation();
        HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(conversation), Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await this.httpClient.PostAsync("/v3/directline/conversations", contentPost);
            if (response.IsSuccessStatusCode)
            {
                string conversationInfo = await response.Content.ReadAsStringAsync();
                this.lastConversation = JsonConvert.DeserializeObject<Conversation>(conversationInfo);
                await Task.Delay(2000);
                await this.ReadBotMessagesAsync();
            }
        }
        catch
        {
        }
    }

    public async void SendMessageAsync(string username, string messageText)
    {
        Activity messageToSend = new Activity()
        {
            From = new Sender()
            {
                Id = username
            },
            Text = messageText,
            Type = "message"
        };

        StringContent contentPost = new StringContent(JsonConvert.SerializeObject(messageToSend), Encoding.UTF8, "application/json");
        string conversationUrl = AzureChatBotService.BotBaseAddress + this.lastConversation.ConversationId + "/activities";

        try
        {
            await this.httpClient.PostAsync(conversationUrl, contentPost);
            await this.ReadBotMessagesAsync();
        }
        catch
        {
        }
    }

    private async Task ReadBotMessagesAsync()
    {
        string conversationUrl = AzureChatBotService.BotBaseAddress + this.lastConversation.ConversationId + "/activities?watermark=" + this.watermark;
        using (HttpResponseMessage messagesReceived = await this.httpClient.GetAsync(conversationUrl, HttpCompletionOption.ResponseContentRead))
        {
            string messagesReceivedData = await messagesReceived.Content.ReadAsStringAsync();
            ActivityRoot messagesRoot = JsonConvert.DeserializeObject<ActivityRoot>(messagesReceivedData);

            if (messagesRoot != null)
            {
                this.watermark = messagesRoot.Watermark;
                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach (Activity activity in messagesRoot.Activities)
                    {
                        if (activity.From.Id == this.botId)
                        {
                            this.messageReceivedAction?.Invoke(activity);
                        }
                    }
                });
            }
        }
    }
}