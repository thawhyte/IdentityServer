using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblState
    {
        public int Stateid { get; set; }
        public string Statename { get; set; }
        public int? Lgaid { get; set; }
        public int? Countryid { get; set; }
        public int? Regionid { get; set; }
        public string Createdby { get; set; }
        public DateTime? Datetimecreated { get; set; }

        public virtual TblCountry Country { get; set; }
    }
}
