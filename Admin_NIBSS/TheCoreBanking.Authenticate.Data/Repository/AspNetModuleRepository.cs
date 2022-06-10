using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class AspNetModuleRepository : EFRepository<TblAspNetModuleRoles>, IAspNetModuleRepository
    {
        public AspNetModuleRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblAspNetModuleRoles> ValidateModule(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

    }
}