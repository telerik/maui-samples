using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QSF.Examples.AIControl.A2UIExample.Services;

public sealed class A2UILlmClient
{
    private const string AIServiceURL = "https://demos.telerik.com/service/v2/ai/a2ui/completion";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private static readonly string HeaderId = Guid.NewGuid().ToString("N");
    private static readonly HttpClient HttpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

    public async Task<string?> GenerateJsonAsync(string systemPrompt, string prompt, CancellationToken ct = default)
    {
        var payload = new[]
        {
            new
            {
                role = "system",
                contents = new[] { new { type = "text", text = systemPrompt } }
            },
            new
            {
                role = "user",
                contents = new[] { new { type = "text", text = prompt } }
            }
        };

        var serialized = JsonSerializer.Serialize(payload);

        using var request = new HttpRequestMessage(HttpMethod.Post, AIServiceURL);
        request.Headers.Add("telerik-user-id", HeaderId);

        var content = new StringContent(serialized, Encoding.UTF8, "application/json");
        request.Content = content;

        try
        {
            var response = await HttpClient.SendAsync(request, ct);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"A2UI LLM {(int)response.StatusCode}: {body}", null, response.StatusCode);
            }

            var responseContent = await response.Content.ReadAsStringAsync(ct);
            using var doc = JsonDocument.Parse(responseContent);
            return doc.RootElement
                .GetProperty("messages").EnumerateArray().LastOrDefault()
                .GetProperty("contents").EnumerateArray().LastOrDefault()
                .GetProperty("text").GetString();
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("The AI request timed out.");
        }
        catch
        {
            return null;
        }
    }
}
