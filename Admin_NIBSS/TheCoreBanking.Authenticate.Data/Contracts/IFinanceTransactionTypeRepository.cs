using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IFinanceTransactionTypeRepository : IRepository<TblFinanceTransactionType>
    {
        IQueryable<TblFinanceTransactionType> ValidateFinanceTransactionType(int Id);
    }
}
