using Divisas.Views;
namespace Divisas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Splash());
        }
    }
}
