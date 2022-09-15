using Microsoft.Maui.Graphics;
using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.AutoCompleteControl.CustomizationExample
{
    public class JobTitle : ViewModelBase
    {
        private JobType jobType;
        private string name;
        private string tokenImagePath;
        private string suggestionImagePath;
        private Color tokenTextColor;
        private Color backgroundColor;

        private static readonly Dictionary<JobType, string> typeToTokenImage;
        private static readonly Dictionary<JobType, string> typeToSuggestionImage;
        private static readonly Dictionary<JobType, Color> typeToTextColor;
        private static readonly Dictionary<JobType, Color> typeToBackgroundColor;

        static JobTitle()
        {
            typeToTokenImage = new Dictionary<JobType, string>()
            {
                 {JobType.Designer, "uxdesigner_grey.png"},
                 {JobType.Developer, "developer_grey.png"},
                 {JobType.Manager, "productmanager_grey.png"}
            };

            typeToSuggestionImage = new Dictionary<JobType, string>()
            {
                 {JobType.Designer, "uxdesigner.png"},
                 {JobType.Developer, "developer.png"},
                 {JobType.Manager, "productmanager.png"}
            };

            typeToTextColor = new Dictionary<JobType, Color>()
            {
                { JobType.Designer, Color.FromArgb("#8660C5")},
                { JobType.Developer, Color.FromArgb("#04A2AA")},
                { JobType.Manager, Color.FromArgb("#FF9040")}
            };

            typeToBackgroundColor = new Dictionary<JobType, Color>()
            {
                { JobType.Designer, Color.FromArgb("#F6F1F7")},
                { JobType.Developer, Color.FromArgb("#E3F4F5")},
                { JobType.Manager, Color.FromArgb("#FFF4EB")}
            };
        }

        public JobTitle(JobType jobType)
        {
            this.jobType = jobType;
        }

        public JobTitle(JobType jobType, string name) : this(jobType)
        {
            this.name = name;
        }

        public string Name
        {
            get => this.name;
            set => UpdateValue(ref this.name, value);
        }


        public JobType JobType
        {
            get => this.jobType;
            set => UpdateValue(ref this.jobType, value);
        }

        public string TokenImagePath
        {
            get
            {
                if (this.tokenImagePath == null)
                {
                    this.tokenImagePath = typeToTokenImage[this.jobType];
                }

                return this.tokenImagePath;
            }
        }

        public string SuggestionImagePath
        {
            get
            {
                if (this.suggestionImagePath == null)
                {
                    this.suggestionImagePath = typeToSuggestionImage[this.jobType];
                }

                return this.suggestionImagePath;
            }
        }

        public Color TextColor
        {
            get
            {
                if (this.tokenTextColor == null)
                {
                    this.tokenTextColor = typeToTextColor[this.jobType];
                }

                return this.tokenTextColor;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                if (this.backgroundColor == null)
                {
                    this.backgroundColor = typeToBackgroundColor[this.jobType];
                }

                return this.backgroundColor;
            }
        }
    }
}
