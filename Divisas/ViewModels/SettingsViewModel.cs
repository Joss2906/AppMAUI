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
    public partial class SettingsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly CurrencyDbContext? _dbContext;

        [ObservableProperty]
        private SettingsDTO settingsDto = new SettingsDTO();

        [ObservableProperty]
        private bool loading = false;

        public SettingsViewModel(CurrencyDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await GetData()));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Loading = true;
            Task.Run(async () =>
            {
                var settings = await _dbContext.Settings.FirstOrDefaultAsync();

                if (settings != null)
                {
                    SettingsDto.Id = settings.Id;
                    SettingsDto.BaseCurrency = settings.BaseCurrency;
                    SettingsDto.Name = settings.Name;
                    SettingsDto.Address = settings.Address;
                    SettingsDto.Phone = settings.Phone;
                }
                else
                {
                    SettingsDto.Id = 0;
                }

                MainThread.BeginInvokeOnMainThread(() => Loading = false);
            });
        }

        public async Task GetData()
        {
            var data = await _dbContext.Settings.FirstOrDefaultAsync();
            if (data != null)
            {
                SettingsDto.Id = data.Id;
                SettingsDto.BaseCurrency = data.BaseCurrency;
                SettingsDto.Name = data.Name;
                SettingsDto.Address = data.Address;
                SettingsDto.Phone = data.Phone;
            }
        }

        [RelayCommand]
        public void SaveSettings()
        {
            Loading = true;

            Task.Run(async () =>
            {
                var existingSettings = await _dbContext.Settings.FirstOrDefaultAsync();

                if (existingSettings != null)
                {
                    existingSettings.BaseCurrency = SettingsDto.BaseCurrency;
                    existingSettings.Name = SettingsDto.Name;
                    existingSettings.Address = SettingsDto.Address;
                    existingSettings.Phone = SettingsDto.Phone;

                    _dbContext.Settings.Update(existingSettings);
                }
                else
                {
                    var newSettings = new Settings
                    {
                        BaseCurrency = SettingsDto.BaseCurrency,
                        Name = SettingsDto.Name,
                        Address = SettingsDto.Address,
                        Phone = SettingsDto.Phone
                    };
                    _dbContext.Settings.Add(newSettings);
                    SettingsDto.Id = newSettings.Id; // Actualizar el Id del DTO
                }

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Loading = false;
                });
            });
        }
    }
}
