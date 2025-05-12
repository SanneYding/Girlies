using Vaerksted.ViewModels;
using Vaerksted.Services;

namespace Vaerksted.Pages
{
    public partial class KalenderView : ContentPage
    {
        // Bruger Dependency Injection til at få adgang til KalenderViewModel
        public KalenderView(KalenderViewModel kalenderViewModel)
        {
            InitializeComponent();
            // Binde KalenderViewModel til denne side
            BindingContext = kalenderViewModel;
        }
    }
}
