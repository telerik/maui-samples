namespace QSF.Examples.AIPromptControl.FirstLookExample;

public class AiItem
{
    internal SuggestionId suggestionId;
    internal Format format;
    internal Language language;

    public string inputText;
    public string response;
    public string responseHtml;

    public AiItem(string inputText, string response, string responseHtml)
    {
        this.inputText = inputText;
        this.response = response;
        this.responseHtml = responseHtml;
    }

    internal AiItem(string inputText, string response, string responseHtml, SuggestionId suggestionId, Format format, Language language)
        : this(inputText, response, responseHtml)
    {
        this.suggestionId = suggestionId;
        this.format = format;
        this.language = language;
    }
}