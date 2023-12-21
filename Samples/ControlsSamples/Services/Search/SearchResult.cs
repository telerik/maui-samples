namespace QSF.Services
{
    public class SearchResult
    {
        public SearchResult(SearchResultType resultType, string controlName, string controlDisplayName, string exampleName, string exampleDisplayName, string icon, int firstCharIndex, int lastCharIndex)
        {
            this.ResultType = resultType;
            this.ControlName = controlName;
            this.ControlDisplayName = controlDisplayName;
            this.ExampleName = exampleName;
            this.ExampleDisplayName = exampleDisplayName;
            this.Icon = icon;
            this.FirstCharIndex = firstCharIndex;
            this.LastCharIndex = lastCharIndex;
        }

        public SearchResultType ResultType { get; }

        public string ControlName { get; }

        public string ControlDisplayName { get; }

        public string ExampleName { get; }

        public string ExampleDisplayName { get; }

        public string Icon { get; }

        public int FirstCharIndex { get; }

        public int LastCharIndex { get; }
    }
}
