using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void OnOpenLinkClicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://openweathermap.org");
        await Launcher.Default.OpenAsync(uri);
    }
}
