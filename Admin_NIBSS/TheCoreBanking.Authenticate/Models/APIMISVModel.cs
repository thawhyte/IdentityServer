using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Models
{
    public class APIMISVModel
    {
        public class ItemMis
        {
            public int parentId { get; set; }
            public string customCode { get; set; }
            public string displayName { get; set; }
            public int tenantId { get; set; }
            public int unitTypeId { get; set; }
            public string companyCode { get; set; }
            public string headUser { get; set; }
            public object lastModificationTime { get; set; }
            public object lastModifierUserId { get; set; }
            public DateTime creationTime { get; set; }
            public object creatorUserId { get; set; }
            public int id { get; set; }
        }

        public class ResultMis
        {
            public List<ItemMis> items { get; set; }
        }

        public class RootMis
        {
            public ResultMis result { get; set; }
            public object targetUrl { get; set; }
            public bool success { get; set; }
            public object error { get; set; }
            public bool unAuthorizedRequest { get; set; }
            public bool __abp { get; set; }
        }

    }
}
