using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
   public  class EODMSYProcessRepository : EFRepository<TblEodvalidation>, IEODMSYProcessRepository
    {
        public EODMSYProcessRepository(TheCoreBankingAuthenticateContext context) : base(context) { }


        public int RunSavingsAndDebitInterest(DateTime AppDate)
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _AppDate = new SqlParameter("@Date", AppDate);
                result = context.Database.ExecuteSqlRaw("dbo.OptimizedAverageInterestCalculation @Date", _AppDate);
                return result;
            }

        }

        public int RunCOT(DateTime AppDate)
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _AppDate = new SqlParameter("@Date", AppDate);
                result = context.Database.ExecuteSqlRaw("dbo.CotFeeCharge @Date", _AppDate);
                return result;
            }

        }


        public int RunDormantAccount(DateTime AppDate)
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _AppDate = new SqlParameter("@Date", AppDate);
                result = context.Database.ExecuteSqlRaw("dbo.InsertServiceDate @Date", _AppDate);
                return result;
            }

        }

        public int RunSMSCharges(DateTime AppDate)
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _AppDate = new SqlParameter("@Date", AppDate);
                result = context.Database.ExecuteSqlRaw("dbo.sp_EOMSMSDebitAuto @Date", _AppDate);
                return result;
            }

        }

        public int ValidateValidMeansOfId()
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                result = context.Database.ExecuteSqlRaw("dbo.Sp_DateExpiryEOD");
                return result;
            }

        }



        public int ClearCheques()
        {
            int result = 0;
            using (var context = new TheCoreBankingAuthenticateContext())
            {
                result = context.Database.ExecuteSqlRaw("dbo.ChequeClearing");
                return result;
            }

        }


    }
}
