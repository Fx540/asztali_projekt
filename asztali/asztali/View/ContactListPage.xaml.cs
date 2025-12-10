using asztali.Model;

namespace asztali.View;

public partial class ContactListPage : ContentPage
{
    public ContactListPage()
    {
        InitializeComponent();
        this.Appearing += ContactListPage_Appearing;
    }

    private async void ContactListPage_Appearing(object? sender, EventArgs e)
    {
        await LoadContacts();
    }

    private async Task LoadContacts()
    {
        var contacts = await App.Database.GetContactsAsync();
        ContactsCollectionView.ItemsSource = contacts;
        ContactsCollectionView.SelectedItem = null;
    }

    private async void OnAddContactClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ContactDetailsPage(new ContactItem()));
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ContactItem selectedContact)
        {
            await Navigation.PushAsync(new ContactDetailsPage(selectedContact));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is ContactItem contact)
        {
            bool answer = await DisplayAlert("Megerõsítés", $"Biztosan törölni szeretné {contact.Name} névjegyet?", "Igen", "Nem");
            if (answer)
            {
                await App.Database.DeleteContactAsync(contact);
                await LoadContacts();
            }
        }
    }
}
