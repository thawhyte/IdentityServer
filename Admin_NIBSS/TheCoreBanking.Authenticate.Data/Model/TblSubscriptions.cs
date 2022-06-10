using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblSubscriptions
    {
        public int Id { get; set; }
        public int Idm { get; set; }
        public string ProductAlias { get; set; }
        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
        public string OrderId { get; set; }
        public string OrderDetailId { get; set; }
        public string OrderDetailDescription { get; set; }
        public string Amount { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyTelephone { get; set; }
        public string CompanySector { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string ValidityStartDate { get; set; }
        public string ValidityEndDate { get; set; }
        public string OrderDate { get; set; }
    }
}
