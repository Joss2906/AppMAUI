using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using Divisas.DataAccess;
using Divisas.DTOs;
using Divisas.Utilities;
using Divisas.Models;
using System.Collections.ObjectModel;
using Divisas.Views;

namespace Divisas.ViewModels
{
    public partial class CurrencyListViewModel : ObservableObject
    {
        private readonly CurrencyDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<CurrencyDTO> currencyList = new ObservableCollection<CurrencyDTO>();

        //public CurrencyListViewModel(CurrencyDbContext context)
        //{
        //    _dbContext = context;

        //    MainThread.BeginInvokeOnMainThread(new Action(async () => await Get()));

        //    WeakReferenceMessenger.Default.Register<CurrencyDeliveryMsg>(this, (r, m) =>
        //    {
        //        MessageReceived(m.Value);
        //    });
        //}

        public async Task Get()
        {
            var list = await _dbContext.Currencies.ToListAsync();
            if (list.Any())
            {
                foreach (var item in list)
                {
                    CurrencyList.Add(new CurrencyDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ExchangeRate = item.ExchangeRate,
                        Image = item.Image
                    });
                }
            }
        }

        private void MessageReceived(CurrencyMessage currencyMessage)
        {
            var currencyDto = currencyMessage.Currency;

            if (currencyMessage.isNew)
            {
                CurrencyList.Add(currencyDto);
            }
            else
            {
                var foundItem = CurrencyList
                    .First(e => e.Id == currencyDto.Id);

                foundItem.Name = currencyDto.Name;
                foundItem.ExchangeRate = currencyDto.ExchangeRate;
                foundItem.Image = currencyDto.Image;
            }

        }

        [RelayCommand]
        private async Task Create()
        {
            var uri = $"{nameof(CurrencyList)}?id=0";
            await Shell.Current.GoToAsync(uri);

            //await Shell.Current.Navigation.PushAsync(new CurrencyForm(), false);
        }

        [RelayCommand]
        private async Task Update(CurrencyDTO currencyDto)
        {
            var uri = $"{nameof(CurrencyForm)}?id={currencyDto.Id}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Delete(CurrencyDTO currencyDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "¿Desea eliminar la divisa?", "Si", "No");

            if (answer)
            {
                var foundItem = await _dbContext.Currencies
                    .FirstAsync(e => e.Id == currencyDto.Id);

                _dbContext.Currencies.Remove(foundItem);
                await _dbContext.SaveChangesAsync();
                CurrencyList.Remove(currencyDto);

            }

        }


    }
}