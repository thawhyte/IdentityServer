using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblBankingApprovalTrack
    {
        public int Id { get; set; }
        public string ProdNo { get; set; }
        public string CustCode { get; set; }
        public string ProdCode { get; set; }
        public string Operation { get; set; }
        public int? OperationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? Approved { get; set; }
        public bool? Disapproved { get; set; }
        public DateTime? DateApproved { get; set; }
        public string ApprovedBy { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
        public bool? Reversed { get; set; }
        public string BatchRef { get; set; }
    }
}
