using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IFinanceTransactionRepository : IRepository<TblFinanceTransaction>
    {
        IQueryable<TblFinanceTransaction> ValidateFinanceTransaction(int Id);
        int UpdateStaffInformation(int Id);
        int UpdateUnlockInformation(int Id);
    }
}
