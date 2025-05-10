using System.Collections.ObjectModel;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class ForecastViewModel
{
    public string CityName { get; set; } = "Bucuresti";
    public ObservableCollection<ForecastInfo.ForecastItem> DailyForecasts { get; set; } = new();

    public async Task LoadForecastAsync()
    {
        var service = new WeatherService();
        var forecast = await service.GetForecastAsync(CityName);

        if (forecast?.List == null) return;

        DailyForecasts.Clear();

        var dailyGroups = forecast.List
            .GroupBy(item => DateTimeOffset.FromUnixTimeSeconds(item.Dt).Date)
            .Take(5);

        foreach (var group in dailyGroups)
        {
            var firstItem = group.First();
            DailyForecasts.Add(firstItem);
        }
    }
}
