using System;
using System.Collections.Generic;
using System.Text;

namespace TheCoreBanking.Authenticate.Data.Model
{
   public class StartDayModel
    {
        public int Id { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime NextDate { get; set; }
    }
}
