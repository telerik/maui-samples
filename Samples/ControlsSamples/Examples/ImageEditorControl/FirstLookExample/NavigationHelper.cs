using Telerik.Maui.Controls;

namespace QSF.Examples.ImageEditorControl.FirstLookExample
{
    internal class NavigationHelper
    {
        public static NavigationHelper Instance { get; private set; }

        static NavigationHelper()
        {
            Instance = new NavigationHelper();
        }

        private NavigationHelper()
        {
        }

        public RadContentView FirstLook { get; set; }

        public void NavigateToPickImageView()
        {
            if (this.FirstLook == null)
            {
                return;
            }

            this.FirstLook.Content = new PickImageView() { BindingContext = new PickImageViewModel() };
        }

        public void NavigateToEditImageView(string imagePath)
        {
            if (this.FirstLook == null)
            {
                return;
            }

            this.FirstLook.Content = new EditImageView() { BindingContext = new EditImageViewModel(imagePath) };
        }
    }
}
