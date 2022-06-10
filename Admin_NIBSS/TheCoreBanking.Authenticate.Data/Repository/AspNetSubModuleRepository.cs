using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class AspNetSubModuleRepository : EFRepository<TblAspNetSubModuleRoles>, IAspNetSubModuleRepository
    {
        public AspNetSubModuleRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblAspNetSubModuleRoles> ValidateSubModule(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

    }
}