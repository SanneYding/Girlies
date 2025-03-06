//using CloudKit;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Vaerksted.Services;

namespace Vaerksted.ViewModels
{
    public partial class KalenderViewModel : ObservableObject
    {
        private readonly Database _database;

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Now;

        [ObservableProperty]
        private int id;

        /*[ObservableProperty]
        private string navn = string.Empty;*/

        [ObservableProperty]
        private float pris;

        [ObservableProperty]
        private int antal;

        [ObservableProperty]
        private int fakturaId;

        public float TotalPris => Pris * Antal;

        public string Navn { get; set; }


        partial void OnSelectedDateChanged(DateTime value)
        {
            
            
        }

        private async Task UpdateOpgaveView(DateTime value)
        {
            var Kalender = _database.GetOpgaveByDateAsync(value);
        }

        public KalenderViewModel(Database database)
        {
            _database = database;
            InitializeKalender();
        }


        public async Task GetOpgaveByDate()
        {

        } 


        private async Task InitializeKalender()
        {
            var Kalender = await _database.GetOpgaveAsync();
            foreach (var opgave in Kalender)
            {
                Kalender.Add(opgave);
            }
        }

    }
}
