using System.Collections.ObjectModel;
using WeatherApp.Models;
using WeatherApp.Services;
using System.Diagnostics;

namespace WeatherApp.ViewModels;

public class LocationsViewModel : BaseViewModel
{
    private readonly WeatherService _weatherService = new();
    private readonly StorageService _storageService = new();

    public ObservableCollection<StoredLocationDisplay> Locations { get; set; } = new();

    public class StoredLocationDisplay
    {
        public string CityName { get; set; }
        public string Temperature { get; set; }
    }

    public LocationsViewModel()
    {
        // Ascultă pentru schimbări de setări
        Microsoft.Maui.Controls.MessagingCenter.Subscribe<SettingsViewModel.MessagingCenter>(
            this, "SettingsChanged", async (sender) => await LoadLocationsAsync());
    }

    public async Task LoadLocationsAsync()
    {
        try
        {
            await _storageService.InitAsync();
            var saved = await _storageService.GetSavedLocationsAsync();

            Debug.WriteLine($"Locații încărcate: {saved.Count}");

            Locations.Clear();
            foreach (var loc in saved)
            {
                try
                {
                    var weather = await _weatherService.GetWeatherAsync(loc.CityName);
                    
                    string unit = weather?.UsesCelsius == true ? "°C" : "°F";
                    Locations.Add(new StoredLocationDisplay
                    {
                        CityName = loc.CityName,
                        Temperature = weather?.Main?.Temp != null ? $"{weather.Main.Temp}{unit}" : "N/A"
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Eroare la încărcarea vremii pentru {loc.CityName}: {ex.Message}");
                    Locations.Add(new StoredLocationDisplay
                    {
                        CityName = loc.CityName,
                        Temperature = "N/A"
                    });
                }
            }

            Debug.WriteLine($"Locații afișate: {Locations.Count}");
            OnPropertyChanged(nameof(Locations));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Eroare la încărcarea locațiilor: {ex.Message}");
        }
    }

    public async Task DeleteLocationAsync(string cityName)
    {
        await _storageService.DeleteLocationAsync(cityName);
        await LoadLocationsAsync();
    }
}
