using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vaerkstedGenafl.ViewModels;

namespace vaerkstedGenafl.Views;

public partial class FakturaPage : ContentPage
{
    public FakturaPage(FakturaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
