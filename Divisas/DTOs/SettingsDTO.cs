using CommunityToolkit.Mvvm.ComponentModel;

namespace Divisas.DTOs
{
    public partial class SettingsDTO : ObservableObject
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string? baseCurrency;
        [ObservableProperty]
        public string? name;
        [ObservableProperty]
        public string? address;
        [ObservableProperty]
        public string? phone;
    }
}
