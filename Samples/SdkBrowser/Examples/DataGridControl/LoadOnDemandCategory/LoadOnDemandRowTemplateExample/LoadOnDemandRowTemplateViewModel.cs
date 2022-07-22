using SDKBrowser.Examples.DataGridControl;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadOnDemandRowTemplateExample
{
    // >> datagrid-loadondemandrowtemplate-viewmodel-csharp
    public class LoadOnDemandRowTemplateViewModel
    {
        public LoadOnDemandRowTemplateViewModel()
        {
            this.Items = new ObservableCollection<Person>();

            for (int i = 0; i < 20; i++)
            {
                var person = new Person { Name = "Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female };
                this.Items.Add(person);
            }
        }

        public ObservableCollection<Person> Items { get; set; }
    }
    // << datagrid-loadondemandrowtemplate-viewmodel-csharp
}