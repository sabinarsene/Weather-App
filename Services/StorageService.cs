using SQLite;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class StorageService
{
    private SQLiteAsyncConnection _db;

    public async Task InitAsync()
    {
        if (_db != null) return;
        var path = Path.Combine(FileSystem.AppDataDirectory, "locations.db");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<StoredLocation>();
    }

    public async Task SaveLocationAsync(StoredLocation loc)
    {
        await InitAsync();
        await _db.InsertAsync(loc);
    }

    public async Task<List<StoredLocation>> GetSavedLocationsAsync()
    {
        await InitAsync();
        return await _db.Table<StoredLocation>().ToListAsync();
    }

    public async Task DeleteLocationAsync(string cityName)
    {
        await InitAsync();
        var loc = await _db.Table<StoredLocation>().Where(x => x.CityName == cityName).FirstOrDefaultAsync();
        if (loc != null)
            await _db.DeleteAsync(loc);
    }
}
