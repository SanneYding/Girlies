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
        private int _opgaveId;

        [ObservableProperty]
        private double _timer;

        [ObservableProperty]
        private float _timepris;

        //public ObservableCollection<MaterialerViewModel> Materialer { get; } = [];

        //public float TotalPris => (float)(Timer * Timepris + Materialer.Sum(m => m.TotalPris));

        public FakturaViewModel(Database database)
        {
            _database = database;
        }

        [RelayCommand]
        private async Task SaveFakturaAsync()
        {
            /*var faktura = new Faktura
            {
                // Use _id instead of ID
                ID = _id,
                MekanikerNavn = MekanikerNavn,
                OpgaveID = OpgaveId,
                Timer = Timer,
                Timepris = Timepris,
                Materialer = Materialer.Select(m => new Materialer
                {
                    Navn = m.Navn,
                    Pris = m.Pris,
                    Antal = m.Antal
                }).ToList()
            };

            if (_id == 0)
            {
                await _database.AddFakturaAsync(faktura);
            }
            else
            {
                await _database.UpdateFaktura(faktura);
            }*/
        }
    }
}