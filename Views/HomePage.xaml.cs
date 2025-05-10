using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class HomePage : ContentPage
{
    private HomeViewModel ViewModel => BindingContext as HomeViewModel;

    public HomePage()
    {
        InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadWeatherAsync();
    }

    private async void OnReloadClicked(object sender, EventArgs e)
    {
        await ViewModel.LoadWeatherAsync();
    }
}
