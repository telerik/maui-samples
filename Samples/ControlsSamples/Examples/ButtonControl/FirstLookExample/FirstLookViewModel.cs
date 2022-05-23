using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Windows.Input;

namespace QSF.Examples.ButtonControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public string Name { get; set; } = "First viewmodel";

        private string category;
        private string popularity;
        private string ingredient;
        private string time;

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Popularity
        {
            get
            {
                return this.popularity;
            }
            set
            {
                if (this.popularity != value)
                {
                    this.popularity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Ingredient
        {
            get
            {
                return this.ingredient;
            }
            set
            {
                if (this.ingredient != value)
                {
                    this.ingredient = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Time
        {
            get
            {
                return this.time;
            }
            set
            {
                if (this.time != value)
                {
                    this.time = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand SelectByCategoryCommand { get; private set; }
        public ICommand SelectByPopularityCommand { get; private set; }
        public ICommand SelectByIngredientCommand { get; private set; }
        public ICommand SelectByTimeCommand { get; private set; }

        public FirstLookViewModel()
        {
            this.Category = "<none>";
            this.Popularity = "<none>";
            this.Ingredient = "<none>";
            this.Time = "<none>";

            this.SelectByCategoryCommand = new Command<string>(category => this.Category = category);
            this.SelectByPopularityCommand = new Command<string>(popularity => this.Popularity = popularity);
            this.SelectByIngredientCommand = new Command<string>(ingredient => this.Ingredient = ingredient);
            this.SelectByTimeCommand = new Command<string>(time => this.Time = time);
        }
    }
}
