using QSF.Common;

namespace QSF.Examples.SideDrawerControl.FirstLookExample
{
    public class Item
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Icon => TelerikControlsIcons.GetIcon(this.Title);
    }
}
