namespace QSF.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel()
    {
        this.HeaderLabel = "Settings";
    }

    public string HeaderLabel { get; private set; }
}
