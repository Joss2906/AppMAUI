using Divisas.ViewModels;
using Divisas.Views;
namespace Divisas
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        //public MainPage(CurrencyListViewModel viewModel)
        //{
        //    InitializeComponent();
        //    BindingContext = viewModel;
        //}

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        //private async void OnButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new CurrencyForm());
        //}
    }

}
