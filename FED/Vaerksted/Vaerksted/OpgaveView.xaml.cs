using Vaerksted.Services;
using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class OpgaveView : ContentPage
    {
        public OpgaveView()
        {
            var db = Database.Instance;
            InitializeComponent();
            BindingContext = new OpgaveViewModel(db);
        }
    }
}
