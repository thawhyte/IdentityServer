using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblCurrency
    {
        public long Id { get; set; }
        public int? CurrCode { get; set; }
        public string CurrName { get; set; }
        public string CurrSymbol { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string CountryCode { get; set; }
        public decimal? AverageRate { get; set; }
    }
}
