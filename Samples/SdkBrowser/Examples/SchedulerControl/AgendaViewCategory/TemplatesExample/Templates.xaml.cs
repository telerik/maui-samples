using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.AgendaViewCategory.TemplatesExample;

public partial class Templates : ContentView
{
	public Templates()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}