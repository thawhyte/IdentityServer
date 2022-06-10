using System;
using System.Collections.Generic;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TblBankingCadeposit
    {
        public int Id { get; set; }
        public string CustCode { get; set; }
        public string ProductAcctNo { get; set; }
        public string IssuanceAcctNo { get; set; }
        public string AccountName { get; set; }
        public string BranchId { get; set; }
        public string CoyCode { get; set; }
        public string Miscode { get; set; }
        public DateTime? DateCreated { get; set; }
        public string DepositorName { get; set; }
        public string DepositorAddr { get; set; }
        public string DepositorPhone { get; set; }
        public string DepositorSign { get; set; }
        public decimal? AmtDeposited { get; set; }
        public string Remark { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public int? OperationId { get; set; }
        public string AmtDeposited1 { get; set; }
        public string SlipNumber { get; set; }
        public string Nationality { get; set; }
        public string Idtype { get; set; }
        public string Idno { get; set; }
        public DateTime? IddateIssued { get; set; }
        public DateTime? IddateExpiry { get; set; }
        public string FundSource { get; set; }
        public string ChequeBank { get; set; }
        public string BankLocation { get; set; }
        public string ChequeNo { get; set; }
        public string MeansOfPayment { get; set; }
        public decimal? Balance { get; set; }
        public bool? Disapproved { get; set; }
        public bool? SourceCa { get; set; }
        public bool? DestinationCa { get; set; }
        public string TransTime { get; set; }
        public string TillAcct { get; set; }
        public string PrincipalBalGl { get; set; }
        public string CustomerBr { get; set; }
        public int? DepositMode { get; set; }
        public string Narration { get; set; }
        public bool? IsReversed { get; set; }
        public DateTime? ActualDate { get; set; }
        public string UserName { get; set; }
        public bool ChargeStamp { get; set; }
    }
}
