using Vaerksted.Services;
using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class FakturaView : ContentPage
    {
        // Bruger Dependency Injection til at få adgang til FakturaViewModel
        public FakturaView(FakturaViewModel fakturaViewModel)
        {
            InitializeComponent();
            // Binde FakturaViewModel til denne side
            BindingContext = fakturaViewModel;
        }
    }
}
