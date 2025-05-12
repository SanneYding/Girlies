using System;
using System.Windows.Input;
using Vaerksted.Models;
using Vaerksted.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Vaerksted.ViewModels
{
    public class OpgaveViewModel : ObservableObject
    {
        private readonly Database _database;

        public OpgaveViewModel()
        {
            _database = new Database();
            SaveAsyncCommand = new Command(async () => await SaveAsync(), () => CanSave); // Updated to use CanSave
            CancelAsyncCommand = new Command(Cancel);
        }

        public ICommand SaveAsyncCommand { get; }
        public ICommand CancelAsyncCommand { get; }

        // Opgave properties (bind to XAML fields)
        public string CustomerName { get; set; }  // Changed from Name to CustomerName
        public string CustomerAddress { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarLicense { get; set; }
        public string Work { get; set; }
        public DateTime Date { get; set; }

        // Property to check if SaveAsyncCommand can be activated
        public bool CanSave =>
            !string.IsNullOrEmpty(CustomerName) &&
            !string.IsNullOrEmpty(CustomerAddress) &&
            !string.IsNullOrEmpty(CarMake) &&
            !string.IsNullOrEmpty(CarModel) &&
            !string.IsNullOrEmpty(CarLicense) &&
            !string.IsNullOrEmpty(Work);

        // Save task to add the "Opgave" to the database
        private async Task SaveAsync()
        {
            var opgave = new Opgave
            {
                CustomerName = CustomerName,  // Changed from Name to CustomerName
                CustomerAddress = CustomerAddress,
                CarMake = CarMake,
                CarModel = CarModel,
                CarLicense = CarLicense,
                Work = Work,
                Date = Date
            };

            await _database.AddOpgaveAsync(opgave);
        }

        // Cancel the operation and go back
        private void Cancel()
        {
            // For example, navigate back or clear the form
            // Navigation.PopAsync(); // If you are using .NET MAUI Navigation
        }
    }
}
