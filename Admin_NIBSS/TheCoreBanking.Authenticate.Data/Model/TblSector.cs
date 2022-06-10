using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblSector
    {
        public TblSector()
        {
            TblCustomer = new HashSet<TblCustomer>();
            TblIndustry = new HashSet<TblIndustry>();
        }

        public int Sectorid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Isdeleted { get; set; }

        public ICollection<TblCustomer> TblCustomer { get; set; }
        public ICollection<TblIndustry> TblIndustry { get; set; }
    }
}
