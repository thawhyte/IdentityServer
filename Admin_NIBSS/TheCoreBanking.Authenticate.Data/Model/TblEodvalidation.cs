using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblEodvalidation
    {
        public int Id { get; set; }
        public string DealId { get; set; }
        public DateTime? TransDate { get; set; }
        public string Operation { get; set; }
        public string Officer { get; set; }
        public string CustomerName { get; set; }
    }
}
