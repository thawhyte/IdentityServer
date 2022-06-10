using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblMoneyDealDetails
    {
        public int Id { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public decimal? Principal { get; set; }
        public int? Tenor { get; set; }
        public decimal? Rate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public DateTime? TransDate { get; set; }
        public string RefId { get; set; }
        public bool? HasAccount { get; set; }
        public string Broker { get; set; }
        public string DealId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int? MmTypeId { get; set; }
        public int? PdCategoryId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
        public bool? Approved { get; set; }
        public string AprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public bool? Disapproved { get; set; }
        public string ApprovalComment { get; set; }
        public string Upload { get; set; }
        public string BookingTime { get; set; }
        public bool? Booked { get; set; }
        public decimal? OriginalRate { get; set; }
        public int? OperationId { get; set; }
        public string Remark { get; set; }
        public string Unit { get; set; }
        public bool? UnitHeadApproval { get; set; }
        public bool? UnitHeadDisapproval { get; set; }
        public string UnitHeadApprovedBy { get; set; }
        public DateTime? UnitHeadDateApproved { get; set; }
        public bool? Tsuapproval { get; set; }
        public bool? Tsudisapproval { get; set; }
        public string TsuapprovedBy { get; set; }
        public string ApprovingUnit { get; set; }
        public DateTime? DateTsuapproved { get; set; }
        public bool? TsuheadApproval { get; set; }
        public bool? TsuheadDisapproval { get; set; }
        public DateTime? TsuheadDateApproved { get; set; }
        public string RemarkBy { get; set; }
        public string Operation { get; set; }
        public int? OpId { get; set; }
        public string BrokerStaffNo { get; set; }
        public string TsuheadStaffNo { get; set; }
        public bool? SysconApproval { get; set; }
        public bool? SysconDisapproval { get; set; }
        public string SysconRemark { get; set; }
        public string Sysconperson { get; set; }
        public DateTime? SysconDateApproved { get; set; }
        public int? SysconopId { get; set; }
        public bool? Done { get; set; }
    }
}
