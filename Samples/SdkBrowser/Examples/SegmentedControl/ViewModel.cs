using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl;

// >> segmentedcontrol-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    public ViewModel()
    {
        this.FileCategories = new ObservableCollection<SegmentItem>
        {
            new SegmentItem { Icon = "\ue847", Category = "Books", Description = "Browse and manage your book collection, reading lists, and bookmarks." },
            new SegmentItem { Icon = "\ue866", Category = "Calendar", Description = "View and manage your upcoming events, appointments, and reminders." },
            new SegmentItem { Icon = "\ue869", Category = "Gallery", Description = "Explore and organize your photos, albums, and media files." },
            new SegmentItem { Icon = "\ue83d", Category = "Places", Description = "Discover and save your favorite locations, venues, and points of interest." },
        };
    }

    public ObservableCollection<SegmentItem> FileCategories { get; }
}
// << segmentedcontrol-viewmodel
