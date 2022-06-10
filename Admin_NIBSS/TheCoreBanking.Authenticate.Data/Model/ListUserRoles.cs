using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class ListUserRoles
    {
        public int UserRoleId { get; set; }
        public string LstUsers { get; set; }
        public string LstUserActivities { get; set; }
        public string LstModules { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
