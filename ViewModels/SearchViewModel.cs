using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class SearchViewModel : INotifyPropertyChanged
{
    private string _cityName = "";
    private WeatherInfo? _weather;
    private readonly StorageService _storageService = new();

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

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
