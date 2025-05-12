using Vaerksted.Services;
using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class MainPage : ContentPage
    {
        public MainPage(OpgaveViewModel opgaveViewModel)
        {
            InitializeComponent();

            // Bind the ViewModel to the page
            BindingContext = opgaveViewModel;
        }
    }

}
