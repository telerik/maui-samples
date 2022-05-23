using QSF.Converters;

namespace QSF.ViewModels;

public class HighlightedSearchResult
{
    public HighlightedSearchResult(HighlightedText highlightedText, HighlightedSearchResultType resultType, string controlName, string controlDisplayName, string exampleName = null, string exampleDisplayName = null, string description = null)
    {
        this.HighlightedText = highlightedText;
        this.ResultType = resultType;
        this.ResultTypeText = EnumToStringConverter.Convert(resultType);
        this.ControlName = controlName;
        this.ControlDisplayName = controlDisplayName;
        this.ExampleName = exampleName;
        this.ExampleDisplayName = exampleDisplayName;
        this.Description = description;
    }

    public HighlightedText HighlightedText { get; }
    public HighlightedSearchResultType ResultType { get; }
    public string ResultTypeText { get; }
    public string ControlName { get; }
    public string ControlDisplayName { get; }
    public string ExampleName { get; }
    public string ExampleDisplayName { get; }
    public string Description { get; }
    public string Text => this.HighlightedText.Text;
}
