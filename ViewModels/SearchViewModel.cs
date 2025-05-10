using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class SearchViewModel : BaseViewModel
{
    private string _cityName = "";
    private WeatherInfo? _weather;
    private readonly StorageService _storageService = new();

    public string CityName
    {
        get => _cityName;
        set { SetProperty(ref _cityName, value); }
    }

    public WeatherInfo? Weather
    {
        get => _weather;
        set { SetProperty(ref _weather, value); }
    }

    public async Task LoadWeatherAsync()
    {
        var service = new WeatherService();
        Weather = await service.GetWeatherAsync(CityName);
    }

    public async Task SaveCityAsync()
    {
        if (Weather != null)
        {
            await _storageService.SaveLocationAsync(new StoredLocation { CityName = Weather.Name });
            Preferences.Set("last_city", Weather?.Name ?? CityName);
        }
    }
}
