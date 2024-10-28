using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divisas.DTOs;

namespace Divisas.Utilities
{
    public class CurrencyListUpdatedMessage : ValueChangedMessage<ObservableCollection<CurrencyDTO>>
    {
        public CurrencyListUpdatedMessage(ObservableCollection<CurrencyDTO> currencyList) : base(currencyList)
        {
        }
    }

}
