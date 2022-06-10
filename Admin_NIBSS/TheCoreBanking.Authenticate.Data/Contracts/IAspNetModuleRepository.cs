using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IAspNetModuleRepository : IRepository<TblAspNetModuleRoles>
    {
        IQueryable<TblAspNetModuleRoles> ValidateModule(int Id);
    }
}
