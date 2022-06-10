using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IValidateEODRepository : IRepository<TblEodvalidation>
    {
       
        decimal SumTotalFTT();
        bool ValidateEODTransactions(DateTime EODdate);
        int insertPendingTransactions();
        int DeletePendingTransactions(int Id);
    }
}
