using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class LocationsPage : ContentPage
{
    private LocationsViewModel ViewModel => BindingContext as LocationsViewModel;

    public LocationsPage()
    {
        InitializeComponent();
        Loaded += async (_, _) => await ViewModel.LoadLocationsAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var location = (StoredLocation)button.CommandParameter;
        await ViewModel.DeleteLocationAsync(location);
    }
}
