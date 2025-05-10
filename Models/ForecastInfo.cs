namespace WeatherApp.Models;

public class ForecastInfo
{
    public List<ForecastItem> List { get; set; }

    public class ForecastItem
    {
        public MainInfo Main { get; set; }
        public List<WeatherDescription> Weather { get; set; }
        public long Dt { get; set; } // UNIX timestamp
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
