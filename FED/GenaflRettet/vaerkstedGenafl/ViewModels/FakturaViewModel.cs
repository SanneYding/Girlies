using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vaerkstedGenafl.Models;
using vaerkstedGenafl.Data;
using vaerkstedGenafl.Views;


namespace vaerkstedGenafl.ViewModels
{
    [QueryProperty("OpgaveId", "OpgaveId")]
    public partial class FakturaViewModel : ObservableObject
    {
        [ObservableProperty] private int opgaveId;
        [ObservableProperty] private string mekaniker;
        [ObservableProperty] private string materialerNavn;
        [ObservableProperty] private float materialePris;
        [ObservableProperty] private float timer;
        [ObservableProperty] private float timePris;

        public ObservableCollection<Faktura> Fakturaer { get; set; } = new();
        public ObservableCollection<Materialer> MaterialeListe { get; set; } = new();

        public FakturaViewModel()
        {
            _ = Initialize();
        }

        public async Task Initialize()
        {
            Fakturaer.Clear();
            MaterialeListe.Clear();

            var fakturaer = await Database.GetFakturaer();
            foreach (var f in fakturaer.Where(f => f.OpgaveID == opgaveId))
            {
                Fakturaer.Add(f);
                var materialer = await Database.GetMaterialerByFakturaId(f.ID);
                foreach (var m in materialer)
                    MaterialeListe.Add(m);
            }
        }

        [RelayCommand]
        private async Task TilføjFaktura()
        {
            var faktura = new Faktura
            {
                OpgaveID = opgaveId,
                Mekaniker = mekaniker,
                Timer = timer,
                TimePris = timePris,
                Dato = DateTime.Now
            };

            await Database.SaveFaktura(faktura);
            Fakturaer.Add(faktura);
        }

        [RelayCommand]
        private async Task TilføjMateriale()
        {
            var material = new Materialer
            {
                Navn = materialerNavn,
                Pris = materialePris
            };

            await Database.SaveMaterial(material);
            MaterialeListe.Add(material);
        }

        //[RelayCommand]
        //private async Task TilføjMateriale(Faktura valgtFaktura)
        //{
        //    var material = new Materialer
        //    {
        //        FakturaID = valgtFaktura.ID,
        //        Navn = materialerNavn,
        //        Pris = materialePris
        //    };

        //    await Database.SaveMaterial(material);
        //    MaterialeListe.Add(material);
        //}

        [RelayCommand]
        private async Task Luk()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
