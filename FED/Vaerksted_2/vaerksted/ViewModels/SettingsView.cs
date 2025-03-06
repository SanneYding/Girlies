using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace vaerksted.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty] private string theme = "Light Mode";

        public SettingsViewModel()
        {
            ChangeThemeCommand = new RelayCommand(ChangeTheme);
        }

        public RelayCommand ChangeThemeCommand { get; }

        private void ChangeTheme()
        {
            Theme = Theme == "Light Mode" ? "Dark Mode" : "Light Mode";
        }
    }
}
