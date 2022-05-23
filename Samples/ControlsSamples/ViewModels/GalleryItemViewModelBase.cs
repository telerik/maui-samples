namespace QSF.ViewModels
{
    public abstract class GalleryItemViewModelBase : ViewModelBase
    {
        private bool isSelected;
        private string inactiveIcon;
        private string activeIcon;
        private string icon;

        public GalleryItemViewModelBase(string icon, string inactiveIcon, string dataTemplateResourceKey)
        {
            this.activeIcon = icon;
            this.inactiveIcon = inactiveIcon;
            this.DataTemplateResourceKey = dataTemplateResourceKey;
            this.UpdateIcon();
        }

        public string Icon
        {
            get
            {
                return this.icon;
            }
            private set
            {
                this.UpdateValue(ref this.icon, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            internal set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.UpdateIcon();
                    this.OnPropertyChanged();
                }
            }
        }

        public string DataTemplateResourceKey { get; }

        private void UpdateIcon()
        {
            this.Icon = this.IsSelected ? this.activeIcon : this.inactiveIcon;
        }
    }
}