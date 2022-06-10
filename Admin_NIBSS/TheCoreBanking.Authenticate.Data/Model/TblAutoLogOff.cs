using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblAutoLogOff
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public int LogoutTime { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
