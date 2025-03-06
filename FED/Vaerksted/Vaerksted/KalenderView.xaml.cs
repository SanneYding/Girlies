using Vaerksted.Services;
using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class KalenderView : ContentPage
    {
        public KalenderView()
        {
            var db = Database.Instance;
            InitializeComponent();
            BindingContext = new OpgaveViewModel(db);
        }
    }
}
