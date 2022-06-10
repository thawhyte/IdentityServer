using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IFinanceCurrentDateRepository : IRepository<TblFinanceCurrentDate>
    {
        IQueryable<TblFinanceCurrentDate> ValidateFinanceCurrentDate(int Id);
    }
}

