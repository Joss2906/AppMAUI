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


namespace Divisas.ViewModels
{
    public partial class NewCurrencyViewModel : ObservableObject, IQueryAttributable
    {
        private readonly CurrencyDbContext _dbContext;

        [ObservableProperty]
        private CurrencyDTO currencyDto = new CurrencyDTO();

        [ObservableProperty]
        private string pageTitle;

        private int currencyId;

        [ObservableProperty]
        private bool loadingIsVisible = false;

        //public NewCurrencyViewModel(CurrencyDbContext context)
        //{
        //    _dbContext = context;
        //}


        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            currencyId = id;

            if (currencyId == 0)
            {
                PageTitle = "Nueva Divisa";
            }
            else
            {
                PageTitle = "Editar Divisa";
                LoadingIsVisible = true;
                await Task.Run(async () =>
                {
                    var currencyFound = await _dbContext.Currencies.FirstAsync(e => e.Id == currencyId);
                    CurrencyDto.Id = currencyFound.Id;
                    CurrencyDto.Name = currencyFound.Name;
                    CurrencyDto.ExchangeRate = currencyFound.ExchangeRate;
                    CurrencyDto.Image = currencyFound.Image;
             
                    MainThread.BeginInvokeOnMainThread(() => { LoadingIsVisible = false; });
                });
            }


            //throw new NotImplementedException();
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingIsVisible = true;
            CurrencyMessage message = new CurrencyMessage();

            await Task.Run(async () =>
            {
                if (currencyId == 0)
                {
                    var tbCurrency = new Currency
                    {
                        Name = CurrencyDto.Name,
                        ExchangeRate = CurrencyDto.ExchangeRate,
                        Image = CurrencyDto.Image
                    };

                    _dbContext.Currencies.Add(tbCurrency);
                    await _dbContext.SaveChangesAsync();

                    CurrencyDto.Id = tbCurrency.Id;
                    message = new CurrencyMessage()
                    {
                        isNew = true,
                        Currency = CurrencyDto
                    };

                }
                else
                {
                    var currencyFound = await _dbContext.Currencies.FirstAsync(e => e.Id == currencyId);
                    currencyFound.Name = CurrencyDto.Name;
                    currencyFound.ExchangeRate = CurrencyDto.ExchangeRate;
                    currencyFound.Image = CurrencyDto.Image;
                      
                    await _dbContext.SaveChangesAsync();

                    message = new CurrencyMessage()
                    {
                        isNew = false,
                        Currency = CurrencyDto
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingIsVisible = false;
                    WeakReferenceMessenger.Default.Send(new CurrencyDeliveryMsg(message));
                    await Shell.Current.Navigation.PopAsync();
                });

            });
        }
    }
}
