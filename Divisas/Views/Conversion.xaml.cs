namespace Divisas.Views;
using Divisas.ViewModels;

public partial class Conversion : ContentPage
{
	public Conversion(ConversionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}