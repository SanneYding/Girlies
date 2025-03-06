using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private string _name = empty;

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

        //Alias Properties for Binding
        //public string KundeNavn => CustomerName;
        //public string BilModel => CarModel;
        //public DateTime Dato => Date;

        public OpgaveViewModel(Database database)
        {
            _database = database;
            InitializeOpgave();
        }

        //public ICommand AddOpgaveAsync => new Command(async () => await AddOpgave());


        [RelayCommand]
        public async Task AddOpgave()
        {
            var opgaver = await _database.GetOpgaveAsync();
            var opgave = new Opgave
            {
                CustomerName = _name,
                CustomerAddress = _customerAddress,
                CarMake = _carMake,
                CarModel = _carModel,
                CarLicense = _carLicense,
                Date = DateTime.Now,
                Work = _work
            };
            await _database.AddOpgaveAsync(opgave);
            
            Name = string.Empty; 
            CustomerAddress = string.Empty;
            CarMake = string.Empty;
            CarModel = string.Empty;
            CarLicense = string.Empty;
            Work = string.Empty;
        }

        private async void InitializeOpgave()
        {
            var opgaver = await _database.GetOpgaveAsync();
            if (opgaver.Count == 0)
            {
                var testOpgave = new Opgave
                {
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

        [ObservableProperty]
        private DateTime _valgtDato = DateTime.Today; // Default to today’s date

        [ObservableProperty]
        private string _opgaveText = string.Empty;

        [RelayCommand]
        public async Task LoadOpgaveByDate()
        {
            var opgaver = await _database.GetOpgaveByDateAsync(ValgtDato);
            //var opgaver = await _database.GetOpgaveAsync();
            OpgaveText = "";

            foreach (var opgave in opgaver)
            {
                OpgaveText += $"Navn: {opgave.CustomerName}\n" +
                             $"Bil: {opgave.CarMake} {opgave.CarModel} ({opgave.CarLicense})\n" +
                             $"Dato: {opgave.Date:yyyy-MM-dd}\n" +
                             $"Arbejdsbeskrivelse: {opgave.Work}\n" +
                             $"ID: {opgave.ID}\n\n\n";
            }
        }
    }
}
