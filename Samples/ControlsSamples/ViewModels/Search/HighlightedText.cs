namespace QSF.ViewModels;

public class HighlightedText
{
    public HighlightedText(string text)
    {
        this.Text = text;
    }

    public HighlightedText(string text, int firstCharIndex, int lastCharIndex)
    {
        this.Text = text;
        this.HighlightStart = firstCharIndex;
        this.HighlightLength = lastCharIndex - firstCharIndex;
    }

    public string Text { get; }

    public int HighlightStart { get; }

    public int HighlightLength { get; }
}
