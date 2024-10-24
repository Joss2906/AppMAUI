using Microsoft.Maui.Controls;
namespace Divisas.Views;

public partial class Splash : ContentPage
{
	public Splash()
	{
		InitializeComponent();
	}

    private async void OnGetStartedClicked(object sender, EventArgs e)
    {
        // Navegar a la página principal
        var navigationPage = new NavigationPage(new PageSelector());
        await Navigation.PushAsync(navigationPage);
        Navigation.RemovePage(this);

    }
}