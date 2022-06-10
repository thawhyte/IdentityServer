using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;
using Microsoft.Data.SqlClient;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class ValidateEODRepository : EFRepository<TblEodvalidation>, IValidateEODRepository
    {
        public ValidateEODRepository(TheCoreBankingAuthenticateContext context) : base(context) { }


        public decimal SumTotalFTT()
        {

            decimal? TotalDebit;
            decimal? TotalCredit;


            decimal SumDebit;
            decimal sumCredit;
            decimal? AllTotal = 0.00m;
            DateTime EODdate;

            TheCoreBankingAuthenticateContext ValidateContext = new TheCoreBankingAuthenticateContext();

            var AppDate = ValidateContext.TblFinanceCurrentDate.FirstOrDefault();
            EODdate = AppDate.CurrentDate;

            TotalDebit = ValidateContext.TblFinanceTransaction.Where(p => p.Approved == true && p.ValueDate == EODdate).Select(p => p.DebitAmt).Sum();
            TotalCredit = ValidateContext.TblFinanceTransaction.Where(p => p.Approved == true && p.ValueDate == EODdate).Select(p => p.CreditAmt).Sum();

            if (!TotalDebit.HasValue) { SumDebit = 0.00m; } else { SumDebit = Convert.ToDecimal(TotalDebit); }
            if (!TotalCredit.HasValue) { sumCredit = 0.00m; } else { sumCredit = Convert.ToDecimal(TotalCredit); }

            //if (TotalDebit == null) { SumDebit = 0.00m; } else { SumDebit = Convert.ToDecimal(TotalDebit); }
            //if (TotalCredit == null) { sumCredit = 0.00m; } else { sumCredit = Convert.ToDecimal(TotalCredit); }
            //SumDebit = Convert.ToDecimal(TotalDebit);
            //sumCredit = Convert.ToDecimal(TotalCredit);

            AllTotal = sumCredit - SumDebit;
            return AllTotal.Value;
        }

        public bool ValidateEODTransactions(DateTime EODdate)
        {

            int validateLoanDisbursement = 0;
            int transferCountSingle = 0;
            int transferCountMultiple = 0;
            int ValidateCreditOperation = 0;
            int ValidateMM = 0;
            int validateMoneyMarket = 0;
            int validateCaDeposit = 0;
            int validateCaWithdrawal = 0;
            int validateMoneyMaturedDealOperation = 0;
            bool Result = false;



            TheCoreBankingAuthenticateContext ValidateContext = new TheCoreBankingAuthenticateContext();
            validateMoneyMarket = ValidateContext.TblMoneyMarketDeals.Where(p => p.DealApproved == false && p.Disapproved == false).Count();
            validateMoneyMaturedDealOperation = ValidateContext.TblMoneyMaturedDealOperation.Where(p => p.DealApproved == false && p.DealDisapproved == false).Count();
            ValidateMM = ValidateContext.TblMoneyDealDetails.Where(p => p.TsuheadDisapproval == false && p.Tsudisapproval == false && p.SysconDisapproval == false && p.UnitHeadDisapproval == false).Count();
            validateLoanDisbursement = ValidateContext.TblBankingLoanLease.Where(p => p.Approved == false && p.Disbursed == false).Count();
            ValidateCreditOperation = ValidateContext.TblBankingApprovalTrack.Where(p => p.Approved == false && p.Disapproved == false).Count();
            transferCountSingle = ValidateContext.TblSingleFundTransfer.Where(p => p.Approved == false && p.Disapproved == false && p.IsCancel == false && p.PostDate == EODdate).Count();
            transferCountMultiple = ValidateContext.TblMultipleAccountToCreditFundTransfer.Where(p => p.Approved == false && p.Disapproved == false && p.IsCancel == false && p.DateCreated == EODdate).Count();
            validateCaWithdrawal = ValidateContext.TblBankingCawithdrawal.Where(p => p.Approved == false && p.Disapproved == false && p.DateCreated == EODdate).Count();
            validateCaDeposit = ValidateContext.TblBankingCadeposit.Where(p => p.Approved == false && p.Disapproved == false && p.DateCreated == EODdate).Count();
            var validateMoneyMarket2 = new List<TblMoneyMarketDeals>();
            //Validate the records that has been aprroved
            validateMoneyMarket2 = ValidateContext.TblMoneyMarketDeals.Where(p => p.DealApproved == true || p.Disapproved == true).ToList();
            var validateMoneyMaturedDealOperation2 = ValidateContext.TblMoneyMaturedDealOperation.Where(p => p.DealApproved == true || p.DealDisapproved == true).ToList();
            var ValidateMM2 = ValidateContext.TblMoneyDealDetails.Where(p => p.TsuheadDisapproval == false && p.Tsudisapproval == true || p.SysconDisapproval == true && p.UnitHeadDisapproval == false).ToList();
            var validateLoanDisbursement2 = ValidateContext.TblBankingLoanLease.Where(p => p.Approved == true || p.Disbursed == true).ToList();
            var ValidateCreditOperation2 = ValidateContext.TblBankingApprovalTrack.Where(p => p.Approved == true || p.Disapproved == true).ToList();
            var transferCountSingle2 = ValidateContext.TblSingleFundTransfer.Where(p => p.Approved == true || p.Disapproved == true && p.IsCancel == false && p.PostDate == EODdate).ToList();
            var transferCountMultiple2 = ValidateContext.TblMultipleAccountToCreditFundTransfer.Where(p => p.Approved == true || p.Disapproved == true && p.IsCancel == false && p.DateCreated == EODdate).ToList();
            var validateCaWithdrawal2 = ValidateContext.TblBankingCawithdrawal.Where(p => p.Approved == true || p.Disapproved == true && p.DateCreated == EODdate).ToList();
            var validateCaDeposit2 = ValidateContext.TblBankingCadeposit.Where(p => p.Approved == true || p.Disapproved == true && p.DateCreated == EODdate).ToList();

            //Validate EOD
            int checkDepositEOD = 0; int checkMMarketEOD = 0; int checkMMaturedEOD = 0; int checkDisbursementEOD = 0; int checkWithdrawalEOD = 0; int checkMultipleFundEOD = 0; int checkCreditOperationEOD = 0; int checkSingleFundEOD = 0;
            if (validateCaWithdrawal2 != null)
            {
                var checkWithdrawalEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item7 in validateCaWithdrawal2)
                {
                    foreach (var item in checkWithdrawalEOD2)
                    {
                        if (item7.ProductAcctNo == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }
            if (validateMoneyMarket2 != null)
            {
                var checkMMarketEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item1 in validateMoneyMarket2)
                {
                    foreach (var item in checkMMarketEOD2)
                    {
                        if (item1.DealId == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }
            if (validateMoneyMaturedDealOperation2 != null)
            {
                var checkMMaturedEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item2 in validateMoneyMaturedDealOperation2)
                {
                    foreach (var item in checkMMaturedEOD2)
                    {
                        if (item2.DealId == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }
            if (validateLoanDisbursement2 != null)
            {
                var checkDisbursementEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item3 in validateLoanDisbursement2)
                {
                    foreach (var item in checkDisbursementEOD2)
                    {
                        if (item3.ProductAcctNo == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }
            if (ValidateCreditOperation2 != null)
            {
                var checkCreditOperationEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item4 in ValidateCreditOperation2)
                {
                    foreach (var item in checkCreditOperationEOD2)
                    {
                        if (item4.ProdNo == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }
            if (transferCountSingle2 != null)
            {
                var checkSingleFundEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item5 in transferCountSingle2)
                {
                    foreach (var item in checkSingleFundEOD2)
                    {
                        if (item5.AccountDr == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }

            if (transferCountMultiple2 != null)
            {
                var checkMultipleFundEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item6 in transferCountMultiple2)
                {
                    foreach (var item in checkMultipleFundEOD2)
                    {
                        if (item6.AccountNo == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }


            if (validateCaDeposit2 != null)
            {
                var checkDepositEOD2 = ValidateContext.TblEodvalidation.ToList();
                foreach (var item8 in validateCaDeposit2)
                {
                    foreach (var item in checkDepositEOD2)
                    {
                        if (item8.ProductAcctNo == item.DealId)
                        {
                            DeletePendingTransactions(item.Id);
                        }
                    }
                }
            }
            decimal TotalSumOfFtt = 0.00m;

            TotalSumOfFtt = SumTotalFTT();

            if (TotalSumOfFtt != 0.00m)
            {
                Result = true;

                return Result;
            }
            if (validateCaDeposit > 0)
            {
                Result = true;

                return Result;
            }
            else if (checkDepositEOD == null)
            {
                Result = false;
            }
            else
            {
                DeletePendingTransactions(checkDepositEOD);
            }

            if (validateCaWithdrawal > 0)
            {
                Result = true;

                return Result;
            }
            else if (checkWithdrawalEOD == null)
            {
                Result = false;
            }
            else
            {
                DeletePendingTransactions(checkWithdrawalEOD);
            }

            if (validateMoneyMarket > 0)
            {
                Result = true;

                return Result;
            }
            else if (checkMMarketEOD == null)
            {
                Result = false;
            }
            else
            {

                DeletePendingTransactions(checkMMarketEOD);
            }

            if (validateMoneyMaturedDealOperation > 0)
            {
                Result = true;

                return Result;
            }
            else if (checkMMaturedEOD == null)
            {
                Result = false;
            }
            else
            {
                DeletePendingTransactions(checkMMaturedEOD);

            }

            if (ValidateMM > 0)
            {
                Result = true;

                return Result;
            }
            if (validateLoanDisbursement > 0)
            {
                Result = true;

                return Result;
            }
            else if (checkDisbursementEOD == null)
            {
                Result = false;
            }
            else
            {

                DeletePendingTransactions(checkDisbursementEOD);
            }
            if (ValidateCreditOperation > 0)
            {
                Result = true;

                return Result;
            }
            else if (ValidateCreditOperation2 == null)
            {
                Result = false;
            }
            else
            {
                DeletePendingTransactions(checkCreditOperationEOD);
            }

            if (transferCountSingle > 0)
            {
                Result = true;

                return Result;
            }
            else if (checkSingleFundEOD == null)
            {
                Result = false;
            }
            else
            {
                DeletePendingTransactions(checkSingleFundEOD);
            }

            if (transferCountMultiple > 0)
            {
                Result = true;

                return Result;
            }
            else if (transferCountMultiple2 == null)
            {
                Result = false;
            }
            else
            {
                DeletePendingTransactions(checkMultipleFundEOD);
            }

            return Result;
        }

        public int insertPendingTransactions()
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                result = context.Database.ExecuteSqlRaw("dbo.SP_ValidateEOD");
                return result;
            }

        }


        public int DeletePendingTransactions(int Id)
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _ID = new SqlParameter("@ID", Id);
                result = context.Database.ExecuteSqlRaw("dbo.Sp_DeleteValidateEOD @ID", _ID);
                return result;
            }


        }
    }
}

