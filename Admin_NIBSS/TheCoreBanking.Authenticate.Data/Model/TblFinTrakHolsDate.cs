using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblFinTrakHolsDate
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string HolidayType { get; set; }
    }
}
