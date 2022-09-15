namespace SDKBrowserMaui.Examples.AutoCompleteControl.Models
{
    // >> autocomplete-client-businessobject
    public class Client
    {
        public Client() { }

        public Client(string name, string imageSource)
        {
            this.Name = name;
            this.ImageSource = imageSource;
        }

        public Client(string name, string email, string imageSource)
        {
            this.Name = name;
            this.Email = email;
            this.ImageSource = imageSource;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageSource { get; set; }
    }
    // << autocomplete-client-businessobject
}
