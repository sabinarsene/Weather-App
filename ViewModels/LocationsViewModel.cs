using System.Collections.ObjectModel;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class LocationsViewModel
{
    public ObservableCollection<StoredLocation> Locations { get; set; } = new();
    private readonly StorageService _storageService = new();

    public async Task LoadLocationsAsync()
    {
        var list = await _storageService.GetLocationsAsync();
        Locations.Clear();
        foreach (var item in list)
            Locations.Add(item);
    }

    public async Task DeleteLocationAsync(StoredLocation loc)
    {
        await _storageService.DeleteLocationAsync(loc);
        await LoadLocationsAsync();
    }
}
