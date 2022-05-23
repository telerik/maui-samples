using QSF.Common;
using QSF.Services;
using Microsoft.Maui.Controls;

namespace QSF.ViewModels
{
    public class ExampleViewModel : PageViewModel
    {
        private object content;
        private Example example;

        public object Content
        {
            get { return this.content; }
            set { this.UpdateValue(ref this.content, value); }
        }

        public Example Example
        {
            get { return this.example; }
            set { this.UpdateValue(ref this.example, value); }
        }

        public void ConfigurateExampleViewModel(Example example)
        {
            this.IsBackVisible = true;
            this.HeaderTitle = example.DisplayName;
            var exampleService = DependencyService.Get<IExampleService>();
            this.Content = exampleService.CreateExample(example);
            this.Example = example;
        }
    }
}
