using System.Globalization;

namespace WeatherApp.Converters;

public class TempUnitConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool usesCelsius)
        {
            return usesCelsius ? "°C" : "°F";
        }
        return "°C"; // default
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
} 