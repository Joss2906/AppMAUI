using Divisas.Views;
namespace Divisas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new Splash());

            MainPage = new AppShell();

            Routing.RegisterRoute(nameof(CurrencyForm), typeof(CurrencyForm));
            Routing.RegisterRoute(nameof(Splash), typeof(Splash));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            Routing.RegisterRoute(nameof(CurrencyList), typeof(CurrencyList));
            Routing.RegisterRoute(nameof(Conversion), typeof(Conversion));

            Shell.Current.GoToAsync("//Splash");

        }
    }
}
