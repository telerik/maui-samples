using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QSF.Examples.BottomSheetControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    private Employee selectedEmployee;
    private string bottomSheetState = "Hidden";
    private bool isLoadMoreVisible = true;

    public ThemingViewModel()
    {
        this.Employees = this.LoadEmployees();
        this.SelectEmployeeCommand = new Command<Employee>((e) => this.SelectedEmployee = e);
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
                this.BottomSheetState = "Partial";
            }
        }
    }

    public string BottomSheetState
    {
        get => this.bottomSheetState;
        set => this.UpdateValue(ref this.bottomSheetState, value);
    }

    public ICommand SelectEmployeeCommand { get; }

    private ObservableCollection<Employee> LoadEmployees()
    {
        return new ObservableCollection<Employee>
        {
            new Employee
            {
                Name = "Emily Johnson",
                Image = "person_8.png",
                Email = "emily.johnson@greenleaf.com",
                Phone = "+1 555 987 6543",
                Company = "GreenLeaf",
                Role = "HR Manager",
                Project = "Onboarding Process",
                Location = "San Francisco, PST",
                Notes = "Remote, flexible hours",
                IsFavorite = false,
            },
            new Employee
            {
                Name = "David Barber",
                Image = "person_5.png",
                Email = "david.barber@novatech.com",
                Phone = "+1 555 102 3894",
                Company = "NovaTech Inc.",
                Role = "CTO",
                Project = "API Integration",
                Location = "New York, EST",
                Notes = "Weekly sync, VIP client",
                IsFavorite = true,
            },
            new Employee
            {
                Name = "Isabella Barnes",
                Image = "person_1.png",
                Email = "isabella.barnes@soluna.com",
                Phone = "+34 612 345 678",
                Company = "Soluna Media",
                Role = "Marketing Director",
                Project = "Campaign Redesign",
                Location = "Madrid, CET",
                Notes = "Prefers Zoom calls",
                IsFavorite = false,
            },
            new Employee
            {
                Name = "Peter Wong",
                Image = "person_3.png",
                Email = "peter.wong@nimbus.com",
                Phone = "+65 8123 4567",
                Company = "Nimbus AI",
                Role = "Head of Product",
                Project = "AI Dashboard Upgrade",
                Location = "Singapore",
                Notes = "Responsive via Slack",
                IsFavorite = true,
            },
            new Employee
            {
                Name = "Claire Dubois",
                Image = "person_4.png",
                Email = "claire.dubois@renovia.fr",
                Phone = "+33 1 22 33 44 55",
                Company = "Renovia Group",
                Role = "Senior Consultant",
                Project = "Regulatory Docs Revamp",
                Location = "Paris",
                Notes = "Fluent in French/English",
                IsFavorite = false,
            },
            new Employee
            {
                Name = "Isabelle Kraus",
                Image = "person_1.png",
                Email = "isabelle.kraus@vantage.de",
                Phone = "+49 170 99887766",
                Company = "Vantage Systems",
                Role = "Business Analyst",
                Project = "Market Expansion",
                Location = "Berlin",
                Notes = "Available afternoons CET",
                IsFavorite = false,
            },
            new Employee
            {
                Name = "Carla Ramirez",
                Image = "person_7.png",
                Email = "carla.ramirez@skytech.com",
                Phone = "+82 10-2345-6789",
                Company = "SkyTech",
                Role = "Lead Developer",
                Project = "Mobile App Revamp",
                Location = "Boston",
                Notes = "Prefers email communication",
                IsFavorite = true,
            },
            new Employee
            {
                Name = "Lucas Miller",
                Image = "person_9.png",
                Email = "lucas.miller@techzone.de",
                Phone = "+49 30 123456",
                Company = "TechZone",
                Role = "QA Lead",
                Project = "Testing Automation",
                Location = "Berlin",
                Notes = "Speaks German/English",
                IsFavorite = true,
            },
        };
    }
}