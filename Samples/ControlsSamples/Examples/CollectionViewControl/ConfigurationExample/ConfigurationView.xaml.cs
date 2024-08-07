using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;

namespace QSF.Examples.CollectionViewControl.ConfigurationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ConfigurationView : ContentView
{
    public ConfigurationView()
    {
        InitializeComponent();
    }

    private void DeleteContact(object sender, TappedEventArgs e)
    {
        if (((View)sender).BindingContext is Contact contact)
        {
            var source = this.collectionView.ItemsSource as ObservableCollection<Contact>;
            source.Remove(contact);
        }
    }

    private void UpdateIsFavoriteContact(object sender, TappedEventArgs e)
    {
        if (((View)sender).BindingContext is Contact contact)
        {
            this.collectionView.EndItemSwipe(true);
            contact.IsFavorite = !contact.IsFavorite;
        }
    }
}