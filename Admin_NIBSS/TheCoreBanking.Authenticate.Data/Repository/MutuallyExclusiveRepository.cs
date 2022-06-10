using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class MutuallyExclusiveRepository : EFRepository<TblMutuallyExclusive>, IMutuallyExclusiveRepository
    {
        public MutuallyExclusiveRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblMutuallyExclusive> ValidateMutuallyExclusive(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

    }
}