using System.Globalization;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private bool _isCelsius = true;
    private bool _enableNotifications = false;
    private string _selectedLanguage = "Română";
    private List<string> _availableLanguages = new List<string> { "Română", "English" };
    private readonly IMessaging _messaging;

    public interface IMessaging
    {
        void Send(string message, object data = null);
    }

    public class MessagingCenter : IMessaging
    {
        private static readonly MessagingCenter _instance = new MessagingCenter();
        public static MessagingCenter Instance => _instance;

        public void Send(string message, object data = null)
        {
            Microsoft.Maui.Controls.MessagingCenter.Send(this, message, data);
        }
    }

    public bool IsCelsius
    {
        get => _isCelsius;
        set
        {
            if (SetProperty(ref _isCelsius, value) && value)
            {
                IsFahrenheit = false;
            }
        }
    }

    public bool IsFahrenheit
    {
        get => !_isCelsius;
        set
        {
            if (value)
            {
                IsCelsius = false;
                _isCelsius = false;
                OnPropertyChanged();
            }
        }
    }

    public bool EnableNotifications
    {
        get => _enableNotifications;
        set => SetProperty(ref _enableNotifications, value);
    }

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set => SetProperty(ref _selectedLanguage, value);
    }

    public List<string> AvailableLanguages => _availableLanguages;

    public SettingsViewModel()
    {
        _messaging = MessagingCenter.Instance;
        LoadSettings();
    }

    private void LoadSettings()
    {
        // Încarcă setările din Preferences
        IsCelsius = Preferences.Get("use_celsius", true);
        EnableNotifications = Preferences.Get("enable_notifications", false);
        SelectedLanguage = Preferences.Get("selected_language", "Română");

        // Setează cultura aplicației
        SetAppCulture(SelectedLanguage);
    }

    public async Task SaveSettingsAsync()
    {
        // Salvează setările în Preferences
        bool oldCelsius = Preferences.Get("use_celsius", true);
        string oldLanguage = Preferences.Get("selected_language", "Română");

        Preferences.Set("use_celsius", IsCelsius);
        Preferences.Set("enable_notifications", EnableNotifications);
        Preferences.Set("selected_language", SelectedLanguage);

        bool settingsChanged = oldCelsius != IsCelsius || oldLanguage != SelectedLanguage;

        // Setează cultura aplicației dacă s-a schimbat limba
        if (oldLanguage != SelectedLanguage)
        {
            SetAppCulture(SelectedLanguage);
        }

        // Notifică alte părți ale aplicației că s-au schimbat setările
        if (settingsChanged)
        {
            _messaging.Send("SettingsChanged");
        }

        return;
    }

    private void SetAppCulture(string language)
    {
        // Setează cultura în funcție de limbă
        string cultureName = language == "English" ? "en-US" : "ro-RO";
        
        // Doar pentru debugging, cultura se va seta corect doar la repornirea aplicației
        System.Diagnostics.Debug.WriteLine($"Setare cultură: {cultureName}");
        
        // Cultura trebuie setată la pornirea aplicației într-un proiect real
        CultureInfo culture = new CultureInfo(cultureName);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }
} 