using Vaerksted.ViewModels;
using Vaerksted.Services;

namespace Vaerksted
{
    public partial class OpgaveView : ContentPage
    {
        // Bruger Dependency Injection til at få adgang til OpgaveViewModel
        public OpgaveView(OpgaveViewModel opgaveViewModel)
        {
            InitializeComponent();
            // Binde ViewModel til denne side
            BindingContext = opgaveViewModel;
        }
    }
}
