using SQLite;

namespace WeatherApp.Models;

public class StoredLocation
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100)]
    public string CityName { get; set; } = string.Empty;
}
