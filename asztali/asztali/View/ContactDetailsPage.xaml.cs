namespace asztali.View;
using asztali.Model;
public partial class ContactDetailsPage : ContentPage
{
    private ContactItem _contact;

    public ContactDetailsPage(ContactItem contact)
    {
        InitializeComponent();
        _contact = contact;

        NameEntry.Text = _contact.Name;
        PhoneEntry.Text = _contact.PhoneNumber;
        EmailEntry.Text = _contact.Email;
        AddressEntry.Text = _contact.Address;

        if (_contact.Id != 0)
        {
            DeleteButton.IsVisible = true;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Hiba", "A név megadása kötelezõ!", "OK");
            return;
        }

        bool answer = await DisplayAlert("Megerõsítés", "Biztosan menteni szeretné a változtatásokat?", "Igen", "Nem");
        if (answer)
        {
            _contact.Name = NameEntry.Text;
            _contact.PhoneNumber = PhoneEntry.Text;
            _contact.Email = EmailEntry.Text;
            _contact.Address = AddressEntry.Text;

            await App.Database.SaveContactAsync(_contact);
            await Navigation.PopAsync();
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Megerõsítés", $"Biztosan törölni szeretné {_contact.Name} névjegyet?", "Igen", "Nem");
        if (answer)
        {
            await App.Database.DeleteContactAsync(_contact);
            await Navigation.PopAsync();
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}