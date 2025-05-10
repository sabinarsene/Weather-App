using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class SettingsPage : ContentPage
{
    private SettingsViewModel ViewModel => BindingContext as SettingsViewModel;

    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        await ViewModel.SaveSettingsAsync();
        await DisplayAlert("Succes", "Setările au fost salvate!", "OK");
    }
} 