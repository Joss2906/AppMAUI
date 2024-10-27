namespace Divisas.Views;
using Divisas.ViewModels;
public partial class Settings : ContentPage
{
	public Settings(SettingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}