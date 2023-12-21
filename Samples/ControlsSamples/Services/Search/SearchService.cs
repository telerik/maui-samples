using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace QSF.Services
{
    /// <summary>
    /// The SearchService is responsible for searching text into control and examples names and descriptions.
    /// </summary>
    public class SearchService : ISearchService
    {
        public IEnumerable<SearchResult> Search(string text)
        {
            var controlService = DependencyService.Get<IControlsService>();

            var allControls = controlService.GetAllControls();

            var lowerText = text.ToLowerInvariant();

            var strings = lowerText.Split(';');

            if (strings.Length > 1)
            {
                var controlLowerText = strings[0].Trim();
                var exampleLowerText = strings[1].Trim();
                if (exampleLowerText.EndsWith("#"))
                {
                    exampleLowerText = exampleLowerText.Substring(0, exampleLowerText.Length - 1);
                }

                var control = allControls.FirstOrDefault(c => c.DisplayName?.ToLower() == controlLowerText
                                                            || c.Name?.ToLower() == controlLowerText
                                                            || c.DescriptionHeader?.ToLower() == controlLowerText);
                if (control != null)
                {
                    var example = control.Examples.FirstOrDefault(e => e.DisplayName.ToLower() == exampleLowerText 
                                                            || e.Description.ToLower() == exampleLowerText);
                    if (example != null)
                    {
                        yield return new SearchResult(SearchResultType.Example, control.Name, control.DisplayName, example.Name, example.DisplayName, example.Icon, 0, exampleLowerText.Length);
                    }
                }
            }
            else
            {
                foreach (var control in allControls)
                {
                    var nameIndex = control.DisplayName.ToLowerInvariant().IndexOf(lowerText);
                    var descriptionIndex = control.FullDescription.ToLowerInvariant().IndexOf(lowerText);
                    if (nameIndex >= 0 || (lowerText.Length > 3 && descriptionIndex >= 0))
                    {
                        yield return new SearchResult(SearchResultType.Control, control.Name, control.DisplayName, null, null, control.Icon, nameIndex, nameIndex + text.Length);
                    }

                    foreach (var example in control.Examples)
                    {
                        var exampleNameIndex = example.DisplayName.ToLowerInvariant().IndexOf(lowerText);
                        var exampleDescriptionIndex = example.Description.ToLowerInvariant().IndexOf(lowerText);
                        if (nameIndex >= 0 || exampleNameIndex >= 0 || (lowerText.Length > 3 && exampleDescriptionIndex >= 0))
                        {
                            yield return new SearchResult(SearchResultType.Example, control.Name, control.DisplayName, example.Name, example.DisplayName, example.Icon, exampleNameIndex, exampleNameIndex + text.Length);
                        }
                    }
                }
            }
        }
    }
}
