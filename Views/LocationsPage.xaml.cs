using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class LocationsPage : ContentPage
{
    private LocationsViewModel ViewModel => BindingContext as LocationsViewModel;

    public LocationsPage()
    {
        InitializeComponent();
        Loaded += async (s, e) => await ViewModel.LoadLocationsAsync(); // folosim Loaded pentru a fi siguri că UI e gata
    }

    private async void OnReloadClicked(object sender, EventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.LoadLocationsAsync();
            EmptyLabel.IsVisible = ViewModel.Locations.Count == 0;
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is string city)
        {
            await ViewModel.DeleteLocationAsync(city);
            EmptyLabel.IsVisible = ViewModel.Locations.Count == 0;
        }
    }
}
