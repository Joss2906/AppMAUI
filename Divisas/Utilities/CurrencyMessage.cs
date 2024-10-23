using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divisas.DTOs;

namespace Divisas.Utilities
{
    public class CurrencyMessage
    {
        public bool isNew { get; set; } //para saber si es  nuevo o edición
        public CurrencyDTO Currency { get; set; }
    }
}
