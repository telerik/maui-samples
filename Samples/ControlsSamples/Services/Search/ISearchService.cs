using System.Collections.Generic;

namespace QSF.Services
{
    public interface ISearchService
    {
        public IEnumerable<SearchResult> Search(string text);
    }
}
