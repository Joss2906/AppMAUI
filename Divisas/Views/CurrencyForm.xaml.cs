namespace Divisas.Views;
using Divisas.ViewModels;

public partial class CurrencyForm : ContentPage
{
	public CurrencyForm(NewCurrencyViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}