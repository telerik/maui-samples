using System;
using System.Collections.Generic;
using System.Linq;

namespace SDKBrowserMaui.Examples.DataGridControl.SearchCategory.SemanticSearchExample;

// >> datagrid-semantic-search-mock-service
/// <summary>
/// A mock embedding service that simulates semantic search by mapping
/// search terms to semantically related concepts using a synonym dictionary.
/// In a real application, this would be replaced by an AI embedding model
/// (e.g., OpenAI, Azure AI, or a local ONNX model).
/// </summary>
public static class LocalEmbeddingService
{
    // A dictionary mapping concepts to related terms.
    // This simulates the "understanding" that a real embedding model would provide.
    private static readonly Dictionary<string, string[]> ConceptMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // Food & drink concepts
        ["drink"] = ["beverages", "soft drinks", "coffees", "teas", "beers", "ales", "liquid", "refreshment"],
        ["food"] = ["snacks", "grains", "cereals", "meat", "poultry", "seafood", "produce", "dairy", "confections", "frozen foods", "baby food"],
        ["sweet"] = ["confections", "desserts", "candies", "sweet breads", "ice cream", "sugar"],
        ["healthy"] = ["health", "wellness", "produce", "fruit", "vegetables", "organic"],
        ["cooking"] = ["condiments", "sauces", "seasonings", "spreads", "relishes", "grains", "pasta", "cereal"],
        ["animal"] = ["pet supplies", "meat", "poultry", "seafood", "fish", "dogs", "cats"],

        // Entertainment concepts
        ["entertainment"] = ["movies", "music", "books", "dvds", "blu-rays", "streaming", "cds", "vinyl", "records"],
        ["reading"] = ["books", "genres", "topics", "literature"],
        ["watch"] = ["movies", "dvds", "blu-rays", "streaming media", "film"],
        ["listen"] = ["music", "cds", "vinyl records", "audio", "songs"],

        // Technology concepts
        ["technology"] = ["electronics", "computers", "phones", "devices", "automotive", "accessories"],
        ["gadget"] = ["electronics", "computers", "phones", "devices"],
        ["computer"] = ["electronics", "phones", "devices", "office supplies"],

        // Personal & household concepts
        ["wear"] = ["clothing", "apparel", "men's", "women's", "children's", "fashion"],
        ["fashion"] = ["clothing", "apparel", "men's", "women's", "children's"],
        ["clean"] = ["household", "cleaning products", "paper goods", "toiletries"],
        ["home"] = ["household", "cleaning products", "paper goods", "frozen foods", "frozen dinners"],
        ["hygiene"] = ["personal care", "toiletries", "cleaning", "health"],
        ["work"] = ["office supplies", "school supplies", "automotive"],
        ["kids"] = ["baby", "baby food", "diapers", "children's", "clothing", "toys"],
        ["car"] = ["automotive", "parts", "accessories"],
    };

    /// <summary>
    /// Determines whether the given cell text is semantically related to the search query.
    /// This simulates what a real AI embedding service would do by computing vector similarity.
    /// </summary>
    /// <param name="searchQuery">The user's search text.</param>
    /// <param name="cellText">The text content of the cell being evaluated.</param>
    /// <returns><c>true</c> if the cell text is semantically related to the search query.</returns>
    public static bool IsSemanticMatch(string searchQuery, string cellText)
    {
        if (string.IsNullOrWhiteSpace(searchQuery) || string.IsNullOrWhiteSpace(cellText))
        {
            return false;
        }

        string lowerQuery = searchQuery.ToLowerInvariant();
        string lowerCell = cellText.ToLowerInvariant();

        // Direct substring match (baseline).
        if (lowerCell.Contains(lowerQuery) || lowerQuery.Contains(lowerCell))
        {
            return true;
        }

        // Check each word in the query against the concept map.
        string[] queryWords = lowerQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in queryWords)
        {
            // Look up the concept and check if any related terms appear in the cell text.
            if (ConceptMap.TryGetValue(word, out string[] relatedTerms))
            {
                if (relatedTerms.Any(term => lowerCell.Contains(term) || term.Contains(lowerCell)))
                {
                    return true;
                }
            }

            // Reverse lookup: check if the cell text matches a concept whose related terms contain the query word.
            foreach (var concept in ConceptMap)
            {
                bool cellMatchesConcept = lowerCell.Contains(concept.Key) ||
                                          concept.Value.Any(t => lowerCell.Contains(t));

                bool queryMatchesConcept = concept.Key.Contains(word) ||
                                           concept.Value.Any(t => t.Contains(word));

                if (cellMatchesConcept && queryMatchesConcept)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
// << datagrid-semantic-search-mock-service
