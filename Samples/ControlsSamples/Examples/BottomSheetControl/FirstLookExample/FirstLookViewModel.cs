using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QSF.Examples.BottomSheetControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private Employee selectedEmployee;
    private string bottomSheetState = "Hidden";
    private bool isLoadMoreVisible = true;

    public FirstLookViewModel()
    {
        this.Employees = this.LoadEmployees();
        this.SelectEmployeeCommand = new Command<Employee>((e) => this.SelectedEmployee = e);
        this.CloseBottomSheetCommand = new Command(this.OnCloseBottomSheet);
        this.SelectedEmployee = this.Employees[0];
    }

    public ObservableCollection<Employee> Employees { get; }

    public Employee SelectedEmployee
    {
        get => this.selectedEmployee;
        set
        {
            this.UpdateValue(ref this.selectedEmployee, value);
            if (this.BottomSheetState == "Hidden")
            {
                this.BottomSheetState = "Minimal";
            }
        }
    }

    public string BottomSheetState
    {
        get => this.bottomSheetState;
        set => this.UpdateValue(ref this.bottomSheetState, value);
    }

    public ICommand SelectEmployeeCommand { get; }
    public ICommand CloseBottomSheetCommand { get; }

    private void OnCloseBottomSheet()
    {
        this.BottomSheetState = "Hidden";
    }

    private ObservableCollection<Employee> LoadEmployees()
    {
        return new ObservableCollection<Employee>
        {
            new Employee
            {
                Name = "Amy Alberts",
                Image = "person_1.png",
                Email = "amy.alberts@company.com",
                BusinessGroup = "Technical Support Engineer",
                Department = "Developer Tools",
                Location = "1st Floor, Desk 74",
                Floor = "1st Floor",
                IsFavorite = false
            },
            new Employee
            {
                Name = "David Barber",
                Image = "person_2.png",
                Email = "david.barber@company.com",
                BusinessGroup = "Software Developer",
                Department = "Engineering",
                Location = "2nd Floor, Desk 21",
                Floor = "2nd Floor",
                IsFavorite = true
            },
            new Employee
            {
                Name = "Edward Parker",
                Image = "person_3.png",
                Email = "edward.parker@company.com",
                BusinessGroup = "Product Manager",
                Department = "Product Management",
                Location = "3rd Floor, Desk 15",
                Floor = "3rd Floor",
                IsFavorite = false
            },
            new Employee
            {
                Name = "Isabella Barnes",
                Image = "person_4.png",
                Email = "isabella.barnes@company.com",
                BusinessGroup = "UX Designer",
                Department = "Design Studio",
                Location = "4th Floor, Desk 33",
                Floor = "4th Floor",
                IsFavorite = false
            },
            new Employee
            {
                Name = "Warren Pal",
                Image = "person_5.png",
                Email = "warren.pal@company.com",
                BusinessGroup = "QA Engineer",
                Department = "Quality Assurance",
                Location = "2nd Floor, Desk 18",
                Floor = "2nd Floor",
                IsFavorite = true
            },
            new Employee
            {
                Name = "Ivan Ivanov",
                Image = "person_6.png",
                Email = "ivan.ivanov@company.com",
                BusinessGroup = "DevOps Engineer",
                Department = "Infrastructure",
                Location = "1st Floor, Desk 5",
                Floor = "1st Floor",
                IsFavorite = false
            },
            new Employee
            {
                Name = "Laura Xu",
                Image = "person_7.png",
                Email = "laura.xu@company.com",
                BusinessGroup = "Cloud Architect",
                Department = "Developer Tools",
                Location = "3rd Floor, Desk 27",
                Floor = "3rd Floor",
                IsFavorite = true
            },
            new Employee
            {
                Name = "Natalie Bailey",
                Image = "person_8.png",
                Email = "natalie.bailey@company.com",
                BusinessGroup = "UX Designer",
                Department = "Design Studio",
                Location = "4th Floor, Desk 36",
                Floor = "4th Floor",
                IsFavorite = true
            },
            new Employee
            {
                Name = "Bryan Baker",
                Image = "person_9.png",
                Email = "bryan.baker@company.com",
                BusinessGroup = "DevOps Engineer",
                Department = "Infrastructure",
                Location = "3rd Floor, Desk 8",
                Floor = "3rd Floor",
                IsFavorite = true
            }
        };
    }
}