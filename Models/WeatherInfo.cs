namespace WeatherApp.Models;

public class WeatherInfo
{
    public MainInfo Main { get; set; }
    public List<WeatherDescription> Weather { get; set; }
    public string Name { get; set; }

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
