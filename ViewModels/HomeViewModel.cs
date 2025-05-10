using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Models;
using WeatherApp.Services;
using Microsoft.Maui.Storage;

namespace WeatherApp.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
    private string _cityName = "Bucuresti";
    private WeatherInfo? _weather;

    public string CityName
    {
        get => _cityName;
        set { _cityName = value; OnPropertyChanged(); }
    }

    public WeatherInfo? Weather
    {
        get => _weather;
        set { _weather = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public async Task LoadWeatherAsync()
    {
        var service = new WeatherService();

        // Încarcă locația salvată din Preferences
        CityName = Preferences.Get("last_city", "Bucuresti");

        Weather = await service.GetWeatherAsync(CityName);
    }

}
