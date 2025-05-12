using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Vaerksted.Services;

namespace Vaerksted.ViewModels
{
    public partial class KalenderViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly Database _database = new();

        public KalenderViewModel(INavigation navigation)
        {
            _navigation = navigation;
            SelectedDate = DateTime.Today;
        }

        [ObservableProperty]
        private DateTime selectedDate;

        [ObservableProperty]
        private string opgaveText = string.Empty;

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private float pris;

        [ObservableProperty]
        private int antal;

        [ObservableProperty]
        private int fakturaId;

        [ObservableProperty]
        private string navn = string.Empty;

        public float TotalPris => Pris * Antal;

        partial void OnSelectedDateChanged(DateTime value)
        {
            LoadOpgaverForDatoCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadOpgaverForDato()
        {
            var opgaver = await _database.GetOpgaveByDateAsync(SelectedDate);
            OpgaveText = string.Empty;

            foreach (var opgave in opgaver)
            {
                OpgaveText += $"Navn: {opgave.CustomerName}\n" +
                              $"Bil: {opgave.CarMake} {opgave.CarModel} ({opgave.CarLicense})\n" +
                              $"Dato: {opgave.Date:yyyy-MM-dd}\n" +
                              $"Arbejdsbeskrivelse: {opgave.Work}\n" +
                              $"ID: {opgave.ID}\n\n";
            }

            if (string.IsNullOrWhiteSpace(OpgaveText))
            {
                OpgaveText = "Ingen opgaver fundet for denne dato.";
            }
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await _navigation.PopToRootAsync();
        }

        [RelayCommand]
        public async Task Save()
        {
            // Her kunne man tilføje logik til at gemme en opgave eller faktura
            await _navigation.PopAsync();
        }
    }
}
