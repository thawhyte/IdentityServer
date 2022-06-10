using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class FinanceCurrentDateRepository : EFRepository<TblFinanceCurrentDate>, IFinanceCurrentDateRepository
    {
        public FinanceCurrentDateRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblFinanceCurrentDate> ValidateFinanceCurrentDate(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

    }
}
