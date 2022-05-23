using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QSF.ViewModels
{
    public abstract class GalleryExampleViewModelBase : ExampleViewModel
    {
        private ObservableCollection<GalleryItemViewModelBase> galleryItems;
        private ObservableCollection<object> selectedItems;
        public object viewContent;
        private GalleryItemViewModelBase selectedItem;

        public ObservableCollection<GalleryItemViewModelBase> GalleryItems
        {
            get
            {
                if (this.galleryItems == null)
                {
                    this.galleryItems = new ObservableCollection<GalleryItemViewModelBase>(this.GetGalleryItems());
                }

                return this.galleryItems;
            }
        }

        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return this.selectedItems;
            }
            set
            {
                if (this.selectedItems != value)
                {
                    this.OnSelectedItemsCollectionChanging();
                    this.selectedItems = value;
                    this.OnSelectedItemsCollectionChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        public GalleryItemViewModelBase SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            private set
            {
                this.UpdateValue(ref this.selectedItem, value);
            }
        }

        protected abstract IEnumerable<GalleryItemViewModelBase> GetGalleryItems();

        private void OnSelectedItemsCollectionChanging()
        {
            if (this.SelectedItems != null)
            {
                this.SelectedItems.CollectionChanged -= this.SelectedItems_CollectionChanged;
            }
        }

        private void OnSelectedItemsCollectionChanged()
        {
            if (this.SelectedItems != null)
            {
                this.SelectedItems.CollectionChanged += this.SelectedItems_CollectionChanged;

                if (this.SelectedItems != null)
                {
                    if (this.SelectedItems.Count == 0)
                    {
                        this.SelectedItems.Add(this.GalleryItems.First());
                    }

                    this.SelectedItem = (GalleryItemViewModelBase)this.SelectedItems.First();
                }
            }

            this.OnSelectionChanged();
        }

        private void SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnSelectionChanged();
        }

        internal void OnSelectionChanged()
        {
            if (this.SelectedItems != null && this.SelectedItems.Count > 0)
            {
                if (this.GalleryItems != null)
                {
                    foreach (var item in this.GalleryItems)
                    {
                        item.IsSelected = false;
                    }
                }

                this.SelectedItem = (GalleryItemViewModelBase)this.SelectedItems.First();

                this.selectedItem.IsSelected = true;
            }
        }
    }
}