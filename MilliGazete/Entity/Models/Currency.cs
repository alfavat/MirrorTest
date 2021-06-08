using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string Symbol { get; set; }
        public string ShortKey { get; set; }
        public double? CurrencyValue { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public double? DailyChangePercent { get; set; }
        public bool DailyChangeStatus { get; set; }
    }
}
