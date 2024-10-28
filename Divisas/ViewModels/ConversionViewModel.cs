using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Divisas.DataAccess;
using Divisas.DTOs;
using Divisas.Utilities;
using Divisas.Models;
using System.Collections.ObjectModel;

namespace Divisas.ViewModels
{
    public partial class ConversionViewModel : ObservableObject, IQueryAttributable
    {
        private readonly CurrencyDbContext? _dbContext;
        private double finalExchangeRate = 0;

        [ObservableProperty]
        private List<CurrencyDTO> currencyList = new List<CurrencyDTO>();

        [ObservableProperty]
        private CurrencyDTO? fromCurrency;

        [ObservableProperty]
        private CurrencyDTO? toCurrency;

        [ObservableProperty]
        private double amount;

        [ObservableProperty]
        private double convertedAmount;

        [ObservableProperty]
        private string exchangeRateLabel;
        public ConversionViewModel(CurrencyDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Get()));

            WeakReferenceMessenger.Default.Register<CurrencyListUpdatedMessage>(this, (r, m) =>
            {
                CurrencyList = m.Value.ToList();
            });
        }

        public async Task Get()
        {
            var list = await _dbContext.Currencies.ToListAsync();
            
            CurrencyList = list.Select(item => new CurrencyDTO
            {
                Id = item.Id,
                Name = item.Name,
                ExchangeRate = item.ExchangeRate,
                Image = item.Image
            }).ToList();
        }

        [RelayCommand]
        private void Convert()
        {
            if (FromCurrency != null && ToCurrency != null && Amount > 0)
            {
                finalExchangeRate = FromCurrency.ExchangeRate / ToCurrency.ExchangeRate;
                var temp = Amount * (finalExchangeRate);
                ConvertedAmount = Math.Round(temp, 4);

                ExchangeRateLabel = $"1 {FromCurrency.Image} = {Math.Round(finalExchangeRate, 4)} {ToCurrency.Image}";

            }
            else
            {
                ConvertedAmount = 0; // Resetea si falta algún valor
            }
        }

        partial void OnAmountChanged(double value)
        {
            Convert();
        }

        partial void OnToCurrencyChanged(CurrencyDTO? value)
        {
            Convert();
        }

        partial void OnFromCurrencyChanged(CurrencyDTO? value)
        {
            Convert();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }

        
    }
}
