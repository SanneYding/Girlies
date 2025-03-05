using Vaerksted.ViewModels;

namespace Vaerksted.Pages;

public partial class Opgaver : ContentPage
{
	public Opgaver(OpgaveViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}


    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}