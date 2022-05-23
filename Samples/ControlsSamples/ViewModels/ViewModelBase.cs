using Microsoft.Maui.Controls;
using QSF.Services;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace QSF.ViewModels;

public class ViewModelBase : NotifyPropertyChangedBase
{
    public INavigationService NavigationService => DependencyService.Get<INavigationService>();
}
