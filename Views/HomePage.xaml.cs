using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class HomePage : ContentPage
{
    private HomeViewModel ViewModel => BindingContext as HomeViewModel;

    public HomePage()
    {
        InitializeComponent();
        Loaded += async (_, _) => await ViewModel.LoadWeatherAsync();
    }

    private async void OnShowWeatherClicked(object sender, EventArgs e)
    {
        await ViewModel.LoadWeatherAsync();
    }
}
