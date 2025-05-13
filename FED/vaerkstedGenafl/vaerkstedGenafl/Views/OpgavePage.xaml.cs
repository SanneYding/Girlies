using vaerkstedGenafl.ViewModels;
using vaerkstedGenafl.Data;

namespace vaerkstedGenafl.Views;

public partial class OpgavePage : ContentPage
{
    public OpgavePage(OpgaveViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
