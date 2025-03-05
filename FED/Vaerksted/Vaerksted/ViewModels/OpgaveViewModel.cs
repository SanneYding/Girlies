using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Vaerksted.Models;
using Vaerksted.Services;
//using static Android.Graphics.ImageDecoder;

namespace Vaerksted.ViewModels
{
    public partial class OpgaveViewModel : ObservableObject
    {
        private static readonly string empty = string.Empty;
        private readonly Database _database;

        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _customerName = empty;

        [ObservableProperty]
        private string _customerAddress = empty;

        [ObservableProperty]
        private string _carMake = empty;

        [ObservableProperty]
        private string _carModel = empty;

        [ObservableProperty]
        private string _carLicense = empty;

        [ObservableProperty]
        private DateTime _date = DateTime.Now;

        [ObservableProperty]
        private string _work = empty;

        public ObservableCollection<FakturaViewModel> Fakturaer { get; } = [];

        // Alias Properties for Binding
        public string KundeNavn => CustomerName;
        public string BilModel => CarModel;
        public DateTime Dato => Date;

        public OpgaveViewModel(Database database)
        {
            _database = database;
            InitializeOpgave();
        }

        public OpgaveViewModel()
        {
        }



        public ICommand AddOpgaveAsync => new Command<Opgave>(async (Opgave opgave) => _AddOpgaveAsync(opgave));


        private void _AddOpgaveAsync(Opgave opgave)
        {
            var a = 1;
            a++;
        }

        private async void InitializeOpgave()
        {
            var opgaver = await _database.GetOpgaveAsync();
            if (opgaver.Count == 0)
            {
                var testOpgave = new Opgave
                {
                    ID = 1,
                    CustomerName = "Test Kunde",
                    CustomerAddress = "Testvej 123",
                    CarMake = "Toyota",
                    CarModel = "Corolla",
                    CarLicense = "AB12345",
                    Date = DateTime.Now,
                    Work = "Test reparation"
                };

                await _database.AddOpgaveAsync(testOpgave);
            }
        }
    }
}
