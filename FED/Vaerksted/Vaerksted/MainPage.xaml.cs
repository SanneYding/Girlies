using Vaerksted.Services;
using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var db = Database.Instance;
            InitializeComponent();
            BindingContext = new OpgaveViewModel(db);
        }
    }
}
