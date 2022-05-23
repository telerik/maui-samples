using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Examples.NumericInputControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private Gender gender;
    private double age;
    private double weight;
    private double height;
    private double activity;
    private double calories;

    public FirstLookViewModel()
    {
        this.Genders = new ObservableCollection<Gender>
        {
            Gender.Female,
            Gender.Male
        };

        this.CalculateCommand = new Command(this.OnCalculateCalories, this.CanCalculateCalories);
    }

    public ObservableCollection<Gender> Genders { get; private set; }
    public Command CalculateCommand { get; private set; }

    public Gender Gender
    {
        get
        {
            return this.gender;
        }
        set
        {
            this.UpdateValue(ref this.gender, value);
        }
    }

    public double Age
    {
        get
        {
            return this.age;
        }
        set
        {
            this.UpdateValue(ref this.age, value);
            this.CalculateCommand.ChangeCanExecute();
        }
    }

    public double Weight
    {
        get
        {
            return this.weight;
        }
        set
        {
            this.UpdateValue(ref this.weight, value);
            this.CalculateCommand.ChangeCanExecute();
        }
    }

    public double Height
    {
        get
        {
            return this.height;
        }
        set
        {
            this.UpdateValue(ref this.height, value);
            this.CalculateCommand.ChangeCanExecute();
        }
    }

    public double Activity
    {
        get
        {
            return this.activity;
        }
        set
        {
            this.UpdateValue(ref this.activity, value);
        }
    }

    public double Calories
    {
        get
        {
            return this.calories;
        }
        set
        {
            this.UpdateValue(ref this.calories, value);
        }
    }



    private bool CanCalculateCalories()
    {
        bool shouldCalculateCalories = (this.Age > 0 && this.Weight > 0 && this.Height > 0);
        return shouldCalculateCalories;
    }

    private void OnCalculateCalories()
    {
        var bmr = 10 * 0.45 * this.Weight + 6.25 * 30.48 * this.Height - 5 * this.Age;

        switch (this.Gender)
        {
            case Gender.Female:
                bmr -= 161;
                break;
            case Gender.Male:
                bmr += 5;
                break;
        }

        switch (this.Activity)
        {
            case 3:
                this.Calories = bmr * 1.37;
                break;
            case 4:
                this.Calories = bmr * 1.42;
                break;
            case 5:
                this.Calories = bmr * 1.46;
                break;
            case 6:
            case 7:
                this.Calories = bmr * 1.55;
                break;
            default:
                this.Calories = bmr * 1.2;
                break;
        }
    }
}