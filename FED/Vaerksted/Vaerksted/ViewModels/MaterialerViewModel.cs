using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Vaerksted.ViewModels
{
    public partial class MaterialerViewModel : ObservableObject
    {
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



        public MaterialerViewModel()
        {
        }
    }
}
