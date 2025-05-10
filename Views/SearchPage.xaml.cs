using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class SearchPage : ContentPage
{
    private SearchViewModel ViewModel => BindingContext as SearchViewModel;

    public SearchPage()
    {
        InitializeComponent();
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        await ViewModel.LoadWeatherAsync();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        await ViewModel.SaveCityAsync();
        await DisplayAlert("Succes", "Oraș salvat!", "OK");
    }
}
