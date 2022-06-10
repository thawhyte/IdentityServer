using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Models
{
    public class meta
    {
        public string status { get; set; }
        public string message { get; set; }
        public string info { get; set; }
    }
    public class data {
        public string cn { get; set; }
        public string sn { get; set; }
        public string givenName { get; set; }
        public string distinguishedName { get; set; }
        public string instanceType { get; set; }
        public string whenCreated { get; set; }
        public string whenChanged { get; set; }
        public string displayName { get; set; }
        public string uSNCreated { get; set; }
        public string uSNChanged { get; set; }
        public string name { get; set; }
        public string objectGUID { get; set; }
        public string userAccountControl { get; set; }

        public string sAMAccountName { get; set; }
        public string sAMAccountType { get; set; }
        public string userPrincipalName { get; set; }
    }
    public class AdUserModel
    {
        public meta meta { get; set; }
        public data data { get; set; }
    }
}


