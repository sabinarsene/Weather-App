using System.Net.Http.Json;
using WeatherApp.Models;
using Microsoft.Maui.Controls;

namespace WeatherApp.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "2e93689a4952903cf99db143f04220b9";
    private const string BaseWeatherUrl = "https://api.openweathermap.org/data/2.5/weather";
    private const string BaseForecastUrl = "https://api.openweathermap.org/data/2.5/forecast";

    public WeatherService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<WeatherInfo?> GetWeatherAsync(string city)
    {
        try
        {
            bool useCelsius = Preferences.Get("use_celsius", true);
            var units = useCelsius ? "metric" : "imperial";
            
            var url = $"{BaseWeatherUrl}?q={city}&units={units}&appid={ApiKey}&lang=ro";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[Weather API Error] {response.StatusCode}");
                return null;
            }

            var weather = await response.Content.ReadFromJsonAsync<WeatherInfo>();
            if (weather != null)
            {
                weather.UsesCelsius = useCelsius;
            }
            return weather;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WeatherService Exception] {ex.Message}");
            return null;
        }
    }

    public async Task<ForecastInfo?> GetForecastAsync(string city)
    {
        try
        {
            bool useCelsius = Preferences.Get("use_celsius", true);
            var units = useCelsius ? "metric" : "imperial";
            
            var url = $"{BaseForecastUrl}?q={city}&units={units}&appid={ApiKey}&lang=ro";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[Forecast API Error] {response.StatusCode}");
                return null;
            }

            var forecast = await response.Content.ReadFromJsonAsync<ForecastInfo>();
            if (forecast != null)
            {
                forecast.UsesCelsius = useCelsius;
            }
            return forecast;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ForecastService Exception] {ex.Message}");
            return null;
        }
    }
}
