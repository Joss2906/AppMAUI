﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Divisas.Utilities
{
    public class CurrencyDeliveryMsg:ValueChangedMessage<CurrencyMessage>
    {
        public CurrencyDeliveryMsg(CurrencyMessage value) : base(value) {
        
        }
    }
}
