using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QSF.Examples.ChatControl.AIIntegrationExample;

public static class TelerikAIService
{
    private const string chatUrl = "/service/v2/ai/chat";
    private const string ErrorText = "Something went wrong! Please, try again with a different prompt.";
    private const string BasePromptFormat =
            "You are a helpful AI assistant. Help me with the following request: " +
            "{0}" +
            ". Provide a clear and concise response. Make the response informative and helpful.";

    private static readonly HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://demos.telerik.com") };
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static async Task<string> GenerateChatId(CancellationToken cancellationToken = default)
    {
        string chatId = string.Empty;
        string conversationsUrl = $"{chatUrl}/conversations";
        using HttpResponseMessage createChatResponse = await httpClient.PostAsync(conversationsUrl, null, cancellationToken).ConfigureAwait(false);

        if (!createChatResponse.IsSuccessStatusCode)
        {
            return chatId;
        }

        string responseContent = await createChatResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        using JsonDocument parsedContent = JsonDocument.Parse(responseContent);
        return parsedContent.RootElement.GetProperty("chatId").GetString();
    }

    public static async Task DeleteChatId(string chatId, CancellationToken cancellationToken = default)
    {
        string deleteChatUrl = $"{chatUrl}/conversations/{chatId}";
        await httpClient.DeleteAsync(deleteChatUrl, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<string> GetAISuggestionAsync(string chatId, string prompt, ObservableCollection<AttachedFileData> attachedFiles, CancellationToken cancellationToken = default)
    {
        using MultipartFormDataContent form = new MultipartFormDataContent();
        form.Add(new StringContent(string.Format(BasePromptFormat, prompt)), "Message");
        await AppendFiles(form, attachedFiles).ConfigureAwait(false);
        string promptUrl = $"{chatUrl}/{chatId}?stream=true";
        using HttpResponseMessage response = await httpClient.PostAsync(promptUrl, form, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            return ErrorText;
        }

        string suggestion = await ExtractMessageText(response, cancellationToken).ConfigureAwait(false);
        return suggestion;
    }

    private static async Task AppendFiles(MultipartFormDataContent form, ObservableCollection<AttachedFileData> attachedFiles)
    {
        if (attachedFiles != null)
        {
            foreach (AttachedFileData file in attachedFiles)
            {
                string fileExtension = Path.GetExtension(file.Name)?.ToLowerInvariant();
                Stream stream = await file.GetStream().ConfigureAwait(false);
                System.Net.Http.StreamContent streamContent = new System.Net.Http.StreamContent(stream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                form.Add(streamContent, "Images", file.Name);
            }
        }
    }

    private static async Task<string> ExtractMessageText(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
    {
        using Stream stream = await responseMessage.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        if (stream == null)
        {
            return null;
        }

        using StreamReader reader = new StreamReader(stream);
        string messageText = string.Empty;
        string line;

        while ((line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false)) != null &&
            !cancellationToken.IsCancellationRequested)
        {
            if (string.IsNullOrWhiteSpace(line) || line == "[DONE]")
            {
                break;
            }

            if (line.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                line = line.Substring(5).Trim();
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string message = null;

            try
            {
                using JsonDocument doc = JsonDocument.Parse(line);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("status", out JsonElement statusElem))
                {
                    if (statusElem.GetString() == "streaming")
                    {
                        message = root.TryGetProperty("message", out JsonElement msgElem) ? msgElem.GetString() : null;
                    }
                    else if (statusElem.GetString() == "error")
                    {
                        message = ErrorText;
                    }
                }
            }
            catch
            {
                // Ignore non-JSON lines
                continue;
            }

            messageText += message;
        }
        
        return messageText;
    }
}