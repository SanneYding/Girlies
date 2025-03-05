using Vaerksted.ViewModels;

namespace Vaerksted.Pages;

public partial class FakturaPage : ContentPage
{
	public FakturaPage(FakturaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}