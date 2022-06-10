using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblMoneyMaturedDealOperation
    {
        public int Id { get; set; }
        public string DealId { get; set; }
        public string MmDealId { get; set; }
        public string CpId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? DiscountedValue { get; set; }
        public decimal? MaturityAmount { get; set; }
        public decimal? PrincipalAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TransactionAmount { get; set; }
        public decimal? InterestArchived { get; set; }
        public decimal? PenalRate { get; set; }
        public decimal? PenalAmount { get; set; }
        public decimal? Whtrate { get; set; }
        public decimal? Whtamount { get; set; }
        public int? Tenor { get; set; }
        public int? NewTenor { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? NewInterestRate { get; set; }
        public decimal? EffectiveYield { get; set; }
        public string SettlementAccount { get; set; }
        public int? MDealStatusId { get; set; }
        public string CoyCode { get; set; }
        public string BranchCode { get; set; }
        public string DealCreatedby { get; set; }
        public bool? DealApproved { get; set; }
        public string DealApprovedby { get; set; }
        public bool? DealDisapproved { get; set; }
        public DateTime? DateApproved { get; set; }
        public string Remark { get; set; }
        public int? OperationId { get; set; }
        public string Upload { get; set; }
        public string BatchRef { get; set; }
    }
}
