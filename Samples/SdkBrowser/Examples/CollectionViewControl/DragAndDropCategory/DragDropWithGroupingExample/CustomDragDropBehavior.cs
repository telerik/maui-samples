using System;
using System.Collections;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.CollectionView;

namespace SDKBrowserMaui.Examples.CollectionViewControl.DragAndDropCategory.DragDropWithGroupingExample;

// >> collectionview-customdragdropbehavior
public class CustomDragDropBehavior : CollectionViewDragDropBehavior
{
    public override void Drop(CollectionViewDragDropContext state)
    {
        var collectionView = this.CollectionView;
        if (collectionView == null)
        {
            return;
        }

        var hasGrouping = collectionView.GroupDescriptors.Count > 0;
        if (hasGrouping)
        {
            var dataView = collectionView.GetDataView();

            var dropTargetItem = state.DropTargetItem;
            var draggedItem = state.DraggedItem;
            if (dropTargetItem == draggedItem)
            {
                return;
            }

            var destinationGroup = dataView.GetParentGroup(dropTargetItem);
            if (destinationGroup != null)
            {
                var childItems = destinationGroup.ChildItems;

                int dropIndex = GetIndex(childItems, state.DropTargetItem);
                dropIndex = state.Placement == ItemDropPlacement.After ? dropIndex + 1 : dropIndex;

                var destinationItemsSource = state.DestinationItemsSource;
                var sourceItemsSource = state.SourceItemsSource;

                if (sourceItemsSource != null && destinationItemsSource == sourceItemsSource && dataView.GetParentGroup(draggedItem) == destinationGroup)
                {
                    var removeIndex = GetIndex(childItems, draggedItem);
                    if (dropIndex > removeIndex)
                    {
                        dropIndex = Math.Clamp(dropIndex - 1, 0, childItems.Count - 1);
                    }
                }

                destinationItemsSource.Remove(draggedItem);
                ((DataModel)draggedItem).Country = (string)destinationGroup.Key;
                ((DataModel)draggedItem).Continent = (string)destinationGroup.ParentGroup.Key;
                destinationItemsSource.Insert(dropIndex, draggedItem);
            }
        }
        else
        {
            base.Drop(state);
        }
    }

    public int GetIndex(IEnumerable list, object item)
    {
        int num = -1;
        foreach (object item2 in list)
        {
            num++;
            if (item2.Equals(item))
            {
                return num;
            }
        }

        return ~num;
    }
}
// << collectionview-customdragdropbehavior
