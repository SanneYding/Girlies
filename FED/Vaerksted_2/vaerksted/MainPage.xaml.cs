using Microsoft.Maui.Controls;
using vaerksted.ViewModels;

namespace vaerksted;

public partial class MainPage : ContentPage
{
    private FormViewModel formViewModel = new FormViewModel();
    private SettingsViewModel settingsViewModel = new SettingsViewModel();

    public MainPage()
    {
        InitializeComponent();

        // Standard ViewModel
        BindingContext = formViewModel;
    }

    // Metode til at skifte mellem ViewModels
    private void SwitchToSettings(object sender, EventArgs e)
    {
        BindingContext = settingsViewModel;
    }

    private void SwitchToForm(object sender, EventArgs e)
    {
        BindingContext = formViewModel;
    }
}