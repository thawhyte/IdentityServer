using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblMutuallyExclusive
    {
        public int Id { get; set; }
        public bool Endofday { get; set; }
        public bool Startofday { get; set; }
    }
}
