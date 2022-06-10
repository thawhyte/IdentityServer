using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblCountry
    {
        public TblCountry()
        {
            TblState = new HashSet<TblState>();
        }

        public int Countryid { get; set; }
        public string Name { get; set; }
        public string Countrycode { get; set; }

        public virtual ICollection<TblState> TblState { get; set; }
    }
}
