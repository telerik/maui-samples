using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace QSF.Examples.EditorControl.FirstLookExample;

public partial class FirstLookView : ContentView
{
	public FirstLookView()
	{
		InitializeComponent();
	}

	private void RadEditor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
#if IOS && !MACCATALYST
		if (e.PropertyName == nameof(RadEditor.IsFocused))
		{
			if (((RadEditor)sender).IsFocused)
			{
				Microsoft.Maui.Platform.KeyboardAutoManagerScroll.Disconnect();
			}
			else
			{
				Microsoft.Maui.Platform.KeyboardAutoManagerScroll.Connect();
			}
		}
#endif
	}
}