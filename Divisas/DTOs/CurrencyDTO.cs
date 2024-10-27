using CommunityToolkit.Mvvm.ComponentModel;

namespace Divisas.DTOs
{
    public partial class CurrencyDTO : ObservableObject
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public double exchangeRate;
        [ObservableProperty]
        public string image;
    }
}
