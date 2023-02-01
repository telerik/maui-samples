namespace QSF.ViewModels
{
    public class DescriptionViewModel : ViewModelBase
    {
        private const string ExampleHeaderLabelText = "Example Info";
        private const string ControlHeaderLabelText = "Control Info";

        public DescriptionViewModel(string description, string descriptionLabel, bool isExample = false)
        {
            this.Description = description;
            this.DescriptionLabel = descriptionLabel;
            this.HeaderLabel = isExample ? ExampleHeaderLabelText : ControlHeaderLabelText;
        }

        public string HeaderLabel { get; private set; }
        public string DescriptionLabel { get; private set; }
        public string Description { get; private set; }
    }
}
