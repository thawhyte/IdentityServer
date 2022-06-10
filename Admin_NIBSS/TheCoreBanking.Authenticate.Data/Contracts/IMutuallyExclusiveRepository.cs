using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IMutuallyExclusiveRepository : IRepository<TblMutuallyExclusive>
    {
        IQueryable<TblMutuallyExclusive> ValidateMutuallyExclusive(int Id);
    }
}
