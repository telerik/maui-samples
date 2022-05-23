using Microsoft.Maui.Graphics;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.ItemsControlControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public FirstLookViewModel()
        {
            this.Experiences = new ObservableCollection<Experience>()
            {
                new Experience() { Title = "JS Developer", Company = "@ Progress Software", Icon = "dev.png", Color = Color.FromArgb("#800E88F2")},
                new Experience() { Title = ".NET Software Engineer", Company = "@ Progress Software", Icon = "dev.png", Color = Color.FromArgb("#800E88F2")},
                new Experience() { Title = "QA Automation Engineer", Company = "@ Progress Software", Icon = "qa.png", Color = Color.FromArgb("#80FFAC3E")},
                new Experience() { Title = "Android Developer", Company = "@ Progress Software", Icon = "dev.png", Color = Color.FromArgb("#800E88F2")},
                new Experience() { Title = "Junior UX Designer ", Company = "@ Progress Software", Icon = "ux.png", Color = Color.FromArgb("#8056AF51")},
                new Experience() { Title = "Full Stack Developer", Company = "@ Progress Software", Icon = "dev.png", Color = Color.FromArgb("#800E88F2")},
                new Experience() { Title = "QA Automation Engineer", Company = "@ Progress Software", Icon = "qa.png", Color = Color.FromArgb("#80FFAC3E") },
                new Experience() { Title = "Front End Developer", Company = "@ Progress Software", Icon = "dev.png", Color = Color.FromArgb("#800E88F2")},
                new Experience() { Title = "Junior Developer", Company = "@ Progress Software", Icon = "dev.png", Color = Color.FromArgb("#800E88F2")},
                new Experience() { Title = "Senior UX Designer", Company = "@ Progress Software", Icon = "ux.png", Color = Color.FromArgb("#8056AF51") },
            };
        }

        public ObservableCollection<Experience> Experiences { get; set; }
    }
}
