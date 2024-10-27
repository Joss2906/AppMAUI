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

        [ObservableProperty]
        private List<string> currencyList = new List<string>();
        public ConversionViewModel(CurrencyDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Get()));
        }

        public async Task Get()
        {
            var list = await _dbContext.Currencies.ToListAsync();
            if (list.Any())
            {
                foreach (var item in list)
                {
                    CurrencyList.Add(item.Image);
                }
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }

        
    }
}
