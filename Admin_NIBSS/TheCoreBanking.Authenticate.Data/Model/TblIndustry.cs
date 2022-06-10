using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblIndustry
    {
        public TblIndustry()
        {
            TblCustomer = new HashSet<TblCustomer>();
        }

        public int Industryid { get; set; }
        public string Name { get; set; }
        public int Sectorid { get; set; }
        public bool Isdeleted { get; set; }

        public TblSector Sector { get; set; }
        public ICollection<TblCustomer> TblCustomer { get; set; }
    }
}
