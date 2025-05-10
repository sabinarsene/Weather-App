using System.Globalization;

namespace WeatherApp.Converters;

public class IconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var icon = value?.ToString();
        if (string.IsNullOrEmpty(icon)) return null;

        return $"https://openweathermap.org/img/wn/{icon}@2x.png";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
