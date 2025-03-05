using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new OpgaveViewModel();
        }

     
    }

}
