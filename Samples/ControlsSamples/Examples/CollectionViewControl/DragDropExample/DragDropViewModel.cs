using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace QSF.Examples.CollectionViewControl.DragDropExample;

public class DragDropViewModel : ExampleViewModel
{
    public DragDropViewModel()
    {
        this.InProgressItems = new ObservableCollection<WorkItem>
        {
            new WorkItem
            {
                Title = "Add design for the new demos of CollectionView",
                Description = "Create design for the standalone demos that will showcase the CollectionView's new features.",
                Assignee = "Eva Lawson",
                AssigneeAvatarColorKey = "AvatarColor1",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor2"], (Color)App.Current.Resources["AccentColor3"], (Color)App.Current.Resources["AccentColor4"] },
            },
            new WorkItem
            {
                Title = "Create demos for the new features of CollectionView",
                Description = "Showcase the CollectionView's new features - reordering, grid layout, header and footer, etc.",
                Assignee = "Layton Buck",
                AssigneeAvatarColorKey = "AvatarColor2",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor3"] },
            },
            new WorkItem
            {
                Title = "Create tests for the new features of CollectionView",
                Description = "Cover the new CollectionView features with tests.",
                Assignee = "Chester Harvey",
                AssigneeAvatarColorKey = "AvatarColor3",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor2"], (Color)App.Current.Resources["AccentColor3"], (Color)App.Current.Resources["AccentColor4"] },
            },
            new WorkItem
            {
                Title = "Write documentation for the new features of CollectionView",
                Description = "Document the CollectionView's new features - reordering, grid layout, header and footer, etc.",
                Assignee = "Jenny Fuller",
                AssigneeAvatarColorKey = "AvatarColor4",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor2"], (Color)App.Current.Resources["AccentColor3"] },
            },
            new WorkItem
            {
                Title = "Expose DataGrid column reordering event",
                Description = "Implement notification that a column reordering is in progress and info about which column is being dragged and add option to cancel the operation.",
                Assignee = "Ashley Robertson",
                AssigneeAvatarColorKey = "AvatarColor5",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor4"] },
            },
            new WorkItem
            {
                Title = "Expose DataGrid column resizing event",
                Description = "Expose similar events to Telerik WPF DataGrid's ColumnWidthChanging and ColumnWidthChanged events.",
                Assignee = "Ashley Robertson",
                AssigneeAvatarColorKey = "AvatarColor5",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor3"], (Color)App.Current.Resources["AccentColor4"] },
            },
            new WorkItem
            {
                Title = "Add how to article for DataGrid's busy indicator customization",
                Description = "Add information on how to customize the busy indicator of the DataGrid component.",
                Assignee = "Jenny Fuller",
                AssigneeAvatarColorKey = "AvatarColor4",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor2"] },
            },
            new WorkItem
            {
                Title = "Add filtering support to ComboBox",
                Description = "Enhance the functionality of the ComboBox component by allowing users to filter and search through options.",
                Assignee = "Ashley Robertson",
                AssigneeAvatarColorKey = "AvatarColor1",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor3"], (Color)App.Current.Resources["AccentColor4"] },
            },
            new WorkItem
            {
                Title = "Create demos for the new filtering feature of ComboBox",
                Description = "Demonstrate basic filtering as the user types in the ComboBox as well as show case-sensitive and case-insensitive options.",
                Assignee = "Layton Buck",
                AssigneeAvatarColorKey = "AvatarColor2",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor2"], (Color)App.Current.Resources["AccentColor3"], (Color)App.Current.Resources["AccentColor4"] },
            },
            new WorkItem
            {
                Title = "Create tests for the new features of ComboBox",
                Description = "Cover the new ComboBox filtering feature with tests.",
                Assignee = "Chester Harvey",
                AssigneeAvatarColorKey = "AvatarColor3",
                Tags = new List<Color>() { (Color)App.Current.Resources["AccentColor2"], (Color)App.Current.Resources["AccentColor4"] },
            },
        };
    }

    public ObservableCollection<WorkItem> InProgressItems { get; set; }

    public ObservableCollection<WorkItem> DoneItems => new ObservableCollection<WorkItem>();
}
