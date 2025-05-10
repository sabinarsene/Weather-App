using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class ForecastPage : ContentPage
{
    private ForecastViewModel ViewModel => BindingContext as ForecastViewModel;

    public ForecastPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadForecastAsync();
    }
}