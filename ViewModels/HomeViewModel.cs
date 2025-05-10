using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Models;
using WeatherApp.Services;
using Microsoft.Maui.Storage;

namespace WeatherApp.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private string _cityName = "Bucuresti";
    private WeatherInfo? _weather;
    private string _temperature = "-";
    private string _description = "-";

    public string CityName
    {
        get => _cityName;
        set { SetProperty(ref _cityName, value); }
    }

    public WeatherInfo? Weather
    {
        get => _weather;
        set 
        { 
            if (SetProperty(ref _weather, value))
            {
                // Actualizează proprietățile derivate
                if (value?.Main != null)
                {
                    string unit = value.UsesCelsius ? "°C" : "°F";
                    Temperature = $"{value.Main.Temp}{unit}";
                }
                if (value?.Weather != null && value.Weather.Count > 0)
                {
                    Description = value.Weather[0].Description;
                }
            }
        }
    }

    public string Temperature
    {
        get => _temperature;
        set { SetProperty(ref _temperature, value); }
    }

    public string Description
    {
        get => _description;
        set { SetProperty(ref _description, value); }
    }

    public async Task LoadWeatherAsync()
    {
        var service = new WeatherService();

        // Încarcă locația salvată din Preferences
        CityName = Preferences.Get("last_city", "Bucuresti");

        Weather = await service.GetWeatherAsync(CityName);
        
        // Salvează orașul curent pentru viitoare utilizări
        Preferences.Set("last_city", CityName);
    }
}
