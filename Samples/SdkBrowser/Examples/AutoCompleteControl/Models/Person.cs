namespace SDKBrowserMaui.Examples.AutoCompleteControl.Models
{
    // >> autocomplete-person-businessobject
    public class Person
    {
        public Person() { }

        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    // << autocomplete-person-businessobject
}
