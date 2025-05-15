using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using vaerkstedGenafl.Data;
using vaerkstedGenafl.Models;
using vaerkstedGenafl.ViewModels;

namespace vaerkstedGenafl.ViewModels
{
    public partial class NyOpgaveViewModel : ObservableObject
    {
        [ObservableProperty] private string kundenavn;
        [ObservableProperty] private string adresse;
        [ObservableProperty] private string bilmærke;
        [ObservableProperty] private string bilmodel;
        [ObservableProperty] private string registreringsnummer;
        [ObservableProperty] private DateTime indleveringDato = DateTime.Now;
        [ObservableProperty] private TimeSpan indleveringTid = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string beskrivelse;

        private OpgaveViewModel oviewModel;

        public NyOpgaveViewModel(OpgaveViewModel vm)
        {
            oviewModel = vm;
        }

        public NyOpgaveViewModel() { }

        [RelayCommand]
        private async Task TilføjOpgave()
        {
            var opgave = new Opgave
            {
                KundeNavn = kundenavn,
                Adresse = adresse,
                BilMaerke = bilmærke,
                BilModel = bilmodel,
                Registreringsnummer = registreringsnummer,
                Indlevering = indleveringDato.Date + indleveringTid,
                Beskrivelse = beskrivelse
            };

            await Database.SaveOpgave(opgave);
            oviewModel?.Opgaver.Add(opgave); // Tilføj hvis parent-VM findes
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Annuller()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
