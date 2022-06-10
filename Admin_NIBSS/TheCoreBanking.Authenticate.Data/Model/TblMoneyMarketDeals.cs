using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblMoneyMarketDeals
    {
        public string DealId { get; set; }
        public int Id { get; set; }
        public string MmDealId { get; set; }
        public string CpId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal? DiscountedValue { get; set; }
        public decimal? MaturityAmount { get; set; }
        public decimal? PrincipalAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? Discount { get; set; }
        public int? Tenor { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? EffectiveYield { get; set; }
        public string SettlementAccount { get; set; }
        public string TerminateAcctNo { get; set; }
        public int? MDealStatusId { get; set; }
        public string NewDealId { get; set; }
        public string CoyCode { get; set; }
        public string BranchCode { get; set; }
        public string DealCreatedby { get; set; }
        public bool? DealApproved { get; set; }
        public string DealApprovedby { get; set; }
        public string Operation { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string MatDate { get; set; }
        public string StaffNo { get; set; }
        public decimal? AmtCollected { get; set; }
        public decimal? AmtReInvested { get; set; }
        public bool? Lien { get; set; }
        public decimal? LienAmount { get; set; }
        public bool? Credit { get; set; }
        public string GivingCpty { get; set; }
        public DateTime? EventDate { get; set; }
        public bool? PendingDeals { get; set; }
        public string Remark { get; set; }
        public string DealUpload { get; set; }
        public decimal? Charge { get; set; }
        public string PostedBy { get; set; }
        public string CurrentClass { get; set; }
        public bool? Disapproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool? OverrideDealLim { get; set; }
        public bool? OverrideAggrLim { get; set; }
        public bool? OverrideTierLim { get; set; }
        public int? PdTypeId { get; set; }
        public int? OperationId { get; set; }
        public decimal? TaxCharge { get; set; }
        public string PosterCode { get; set; }
        public string ApprovalCode { get; set; }
        public string PdmmTypeId { get; set; }
        public string NewmmDealId { get; set; }
        public string NewpdmmTypeId { get; set; }
        public bool? SendLetter { get; set; }
        public bool? Operated { get; set; }
        public bool? PayDiscount { get; set; }
        public bool? InterBank { get; set; }
        public decimal? StartingAmount { get; set; }
        public string Ref { get; set; }
        public bool? Pledged { get; set; }
        public int? OnlineId { get; set; }
        public DateTime? FictitiousMaturity { get; set; }
        public string Pccode { get; set; }
        public string AccountOfficer { get; set; }
        public string UnitCode { get; set; }
        public bool? Matured { get; set; }
        public bool? AutoRollover { get; set; }
        public int? DaysForRollover { get; set; }
        public int? AutoRolloverType { get; set; }
        public int? OriginalTenor { get; set; }
    }
}
