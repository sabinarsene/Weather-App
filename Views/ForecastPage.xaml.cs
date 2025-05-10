using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class ForecastPage : ContentPage
{
    private ForecastViewModel ViewModel => BindingContext as ForecastViewModel;

    public ForecastPage()
    {
        InitializeComponent();
        Loaded += async (_, _) => await ViewModel.LoadForecastAsync();
    }
}
