using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class FinanceTransactionTypeRepository : EFRepository<TblFinanceTransactionType>, IFinanceTransactionTypeRepository
    {
        public FinanceTransactionTypeRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblFinanceTransactionType> ValidateFinanceTransactionType(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

    }
}
