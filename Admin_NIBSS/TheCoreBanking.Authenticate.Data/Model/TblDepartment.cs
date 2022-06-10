using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblDepartment
    {
        public long Id { get; set; }
        public string CoyId { get; set; }
        public string Department { get; set; }
        public string Remark { get; set; }
        public string DeptCode { get; set; }
    }
}
