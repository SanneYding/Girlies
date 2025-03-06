using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;


namespace vaerksted.ViewModels
{
    public partial class FormViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string email;

        public FormViewModel()
        {
            SubmitCommand = new RelayCommand(OnSubmit);
            ClearCommand = new RelayCommand(ClearFields);
        }

        public ICommand SubmitCommand { get; }
        public ICommand ClearCommand { get; }

        private void OnSubmit()
        {
            Application.Current.MainPage.DisplayAlert("Success", "Form submitted!", "OK");
            ClearFields();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Email = string.Empty;
        }
    }
}