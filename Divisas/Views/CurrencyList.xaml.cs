using Divisas.ViewModels;
namespace Divisas.Views;

public partial class CurrencyList : ContentPage
{
    //public CurrencyList()
    //{
    //       BindingContext = new CurrencyListViewModel();
    //       InitializeComponent();

    //}

    public CurrencyList(CurrencyListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}