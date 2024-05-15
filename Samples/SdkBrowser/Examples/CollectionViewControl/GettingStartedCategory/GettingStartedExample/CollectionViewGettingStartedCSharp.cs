using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.GettingStartedCategory.GettingStartedExample;

public class CollectionViewGettingStartedCSharp : ContentView
{
	public CollectionViewGettingStartedCSharp()
	{
        // >> collectionview-getting-started-csharp
        var collectionView = new RadCollectionView 
        { 
            ItemsSource = new ViewModel().Locations,
            DisplayMemberPath = "City"
        };
        // << collectionview-getting-started-csharp
        this.Content = collectionView;
    }
}