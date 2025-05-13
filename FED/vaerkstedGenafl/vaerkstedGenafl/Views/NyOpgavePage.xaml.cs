using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vaerkstedGenafl.ViewModels;

namespace vaerkstedGenafl.Views;

public partial class NyOpgavePage : ContentPage
{
    public NyOpgavePage(NyOpgaveViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
