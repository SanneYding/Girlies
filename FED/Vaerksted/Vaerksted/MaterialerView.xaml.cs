using Vaerksted.Services;
using Vaerksted.ViewModels;

namespace Vaerksted
{
    public partial class MaterialerView : ContentPage
    {

        public MaterialerView()
        {
            var db = Database.Instance;
            InitializeComponent();
            BindingContext = new MaterialerViewModel();//db
        }

     
    }

}
