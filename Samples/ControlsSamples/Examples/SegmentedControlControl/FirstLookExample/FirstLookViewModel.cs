using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.SegmentedControlControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private ProfileType selectedProfileType;

    public FirstLookViewModel()
    {
        this.ProfileTypes = new ObservableCollection<ProfileType>
        {
            new ProfileType { Name = "Personal", Description = "For individual use - share basic information, preferences, and connect with others." },
            new ProfileType { Name = "Business", Description = "For companies or organizations - showcase services, contact details, and professional information." },
            new ProfileType { Name = "Education", Description = "For schools and students - highlight courses, achievements and academic details." },
        };

        this.SelectedProfileType = this.ProfileTypes[0];
    }

    public ObservableCollection<ProfileType> ProfileTypes { get; private set; }

    public ProfileType SelectedProfileType
    {
        get => this.selectedProfileType;
        set
        {
            if (this.selectedProfileType == value)
            {
                return;
            }

            this.selectedProfileType = value;
            this.OnPropertyChanged();
            this.OnPropertyChanged(nameof(this.SelectedProfileDescription));
        }
    }

    public string SelectedProfileDescription =>
        this.selectedProfileType != null ? this.selectedProfileType.Description : string.Empty;
}
