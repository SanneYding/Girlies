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
    public partial class OpgaveViewModel : ObservableObject
    {
        public ObservableCollection<Opgave> Opgaver { get; set; } = new();
        public int selectedOpgaveId;

        public OpgaveViewModel()
        {
            _ = Initialize();
        }

        public async Task Initialize()
        {
            var opgaver = await Database.GetOpgaver();

            foreach (var opgave in opgaver)
            {
                Opgaver.Add(opgave);
            }
        }

        public async Task ReloadOpgaver()
        {
            Opgaver.Clear();
            var opgaver = await Database.GetOpgaver();

            foreach (var opgave in opgaver)
            {
                Opgaver.Add(opgave);
            }
        }

        [RelayCommand]
        private async Task GoToAddOpgave()
        {
            await Shell.Current.GoToAsync(nameof(NyOpgavePage));
        }

        [RelayCommand]
        private async Task GoToFaktura(int id)
        {
            selectedOpgaveId = id;
            await Shell.Current.GoToAsync(nameof(FakturaPage));
        }
    }
}
