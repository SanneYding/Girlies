using Vaerksted.Services;
using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class FakturaView : ContentPage
    {
        public FakturaView()
        {
            var db = Database.Instance;
            InitializeComponent();
            BindingContext = new FakturaViewModel();
        }
    }
}
