using System.Collections.ObjectModel;
using WeatherApp.Services;
using WeatherApp.Models;
using System.Diagnostics;

namespace WeatherApp.ViewModels;

public class ForecastViewModel : BaseViewModel
{
    public class ForecastDisplayItem
    {
        public string DateText { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string Description { get; set; }
    }

    private string _cityName = "Bucuresti";
    public string CityName
    {
        get => _cityName;
        set => SetProperty(ref _cityName, value);
    }

    public ObservableCollection<ForecastDisplayItem> DailyForecasts { get; set; } = new();

    public ForecastViewModel()
    {
        // Ascultă pentru schimbări de setări
        Microsoft.Maui.Controls.MessagingCenter.Subscribe<SettingsViewModel.MessagingCenter>(
            this, "SettingsChanged", async (sender) => await LoadForecastAsync());
    }

    public async Task LoadForecastAsync()
    {
        try
        {
            var service = new WeatherService();
            // Încarcă orașul din preferințe dacă există
            CityName = Preferences.Get("last_city", "Bucuresti");
            
            var forecast = await service.GetForecastAsync(CityName);

            if (forecast?.List == null || forecast.List.Count == 0)
            {
                Debug.WriteLine("Nicio dată de prognoză primită");
                return;
            }

            DailyForecasts.Clear();

            var dailyGroups = forecast.List
                .GroupBy(item => DateTimeOffset.FromUnixTimeSeconds(item.Dt).Date)
                .Take(5);

            int count = 0;
            foreach (var group in dailyGroups)
            {
                var item = group.First();
                var date = DateTimeOffset.FromUnixTimeSeconds(item.Dt).Date.ToString("dd/MM/yyyy");
                string unit = forecast.UsesCelsius ? "°C" : "°F";

                DailyForecasts.Add(new ForecastDisplayItem
                {
                    DateText = date,
                    Temperature = $"Temperatură: {item.Main.Temp}{unit}",
                    Humidity = $"Umiditate: {item.Main.Humidity}%",
                    Description = item.Weather.FirstOrDefault()?.Description ?? "-"
                });
                count++;
            }
            
            Debug.WriteLine($"Prognoza încărcată cu succes. Zile afișate: {count}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Eroare la încărcarea prognozei: {ex.Message}");
        }
    }
}
