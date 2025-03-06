using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Vaerksted.Models;
using Vaerksted.Services;

namespace Vaerksted.ViewModels
{
    public partial class FakturaViewModel : ObservableObject
    {
        private readonly Database _database;

        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _mekanikerNavn = string.Empty;

        [ObservableProperty]
        private Opgave _selectedOpgave;

        [ObservableProperty]
        private double _timer;

        [ObservableProperty]
        private float _timepris;

        public ObservableCollection<Materialer> Materialer { get; } = new();

        public float TotalPris => (float)(Timer * Timepris + Materialer.Sum(m => m.TotalPris));

        public ObservableCollection<Opgave> OpgaveListe { get; } = new();

        public FakturaViewModel()
        {
            _database = Database.Instance;
            LoadOpgaverAsync();
        }

        private async void LoadOpgaverAsync()
        {
            var opgaver = await _database.GetOpgaveAsync();
            OpgaveListe.Clear();
            foreach (var opgave in opgaver)
            {
                OpgaveListe.Add(opgave);
            }
        }

        [RelayCommand]
        private void AddMaterialer()
        {
            Materialer.Add(new Materialer { Navn = "Nyt MAteriale", Antal = 1, Pris = 100 });
            OnPropertyChanged(nameof(TotalPris));
        }

        [RelayCommand]
        private async Task SaveFakturaAsync()
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
            
        }
    }
}