using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IEODMSYProcessRepository : IRepository<TblEodvalidation>
    {

        int RunSavingsAndDebitInterest(DateTime AppDate);
        int RunCOT(DateTime AppDate);
        int RunDormantAccount(DateTime AppDate);
        int RunSMSCharges(DateTime AppDate);
        int ValidateValidMeansOfId();
        int ClearCheques();
    }
}
