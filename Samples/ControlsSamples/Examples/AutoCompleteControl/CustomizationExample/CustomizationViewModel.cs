using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.AutoCompleteControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        public CustomizationViewModel()
        {
            this.JobTitles = new List<JobTitle>()
            {
                new JobTitle(JobType.Designer, "UX Designer"),
                new JobTitle(JobType.Designer, "Graphic Designer"),
                new JobTitle(JobType.Manager, "Product Manager"),
                new JobTitle(JobType.Manager, "Project Manager"),
                new JobTitle(JobType.Developer, ".Net Developer")
            };

            this.Jobs = new List<string>()
            {
                "Software Engineer - .NET Maui",
                "Senior UI/ UX Designer",
                "Product Manager",
                "Junior .NET Software Engineer",
                "Senior Web Marketing Strategist",
                "Senior Full Stack Engineer"
            };
        }

        public List<JobTitle> JobTitles { get; private set; }

        public List<string> Jobs { get; private set; }
    }
}
