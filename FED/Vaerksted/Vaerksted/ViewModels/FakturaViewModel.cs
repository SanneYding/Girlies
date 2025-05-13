using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Vaerksted.Models;
using Vaerksted.Services;
using Microsoft.Maui.Controls; // For INavigation

namespace Vaerksted.ViewModels
{
    public partial class FakturaViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly Database _database = new Database(); // Direkte instans ligesom i dit eksempel

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string mekanikerNavn = string.Empty;

        [ObservableProperty]
        private Opgave selectedOpgave;

        [ObservableProperty]
        private double timer;

        [ObservableProperty]
        private float timepris;

        public ObservableCollection<Materialer> Materialer { get; } = new();
        public ObservableCollection<Opgave> OpgaveListe { get; } = new();

        public float TotalPris => (float)(Timer * Timepris + Materialer.Sum(m => m.TotalPris));

        public FakturaViewModel(INavigation navigation)
        {
            _navigation = navigation;
            LoadOpgaverAsync();
        }

        private async void LoadOpgaverAsync()
        {
            var opgaver = await _database.GetOpgaveListAsync();
            OpgaveListe.Clear();
            foreach (var opgave in opgaver)
            {
                OpgaveListe.Add(opgave);
            }
        }

        [RelayCommand]
        private void AddMaterialer()
        {
            Materialer.Add(new Materialer { Navn = "Nyt Materiale", Antal = 1, Pris = 100 });
            OnPropertyChanged(nameof(TotalPris));
        }

        [RelayCommand]
        public async void Save()
        {
            if (SelectedOpgave == null) return;

            var faktura = new Faktura
            {
                MekanikerNavn = MekanikerNavn,
                OpgaveID = SelectedOpgave.ID,
                Timer = Timer,
                Timepris = Timepris,
                Materialer = Materialer.ToList()
            };

            await _database.AddFakturaAsync(faktura);
            await _navigation.PopAsync();
        }

        [RelayCommand]
        public async void Cancel()
        {
            await _navigation.PopToRootAsync();
        }
    }
}
