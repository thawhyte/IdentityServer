using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IAspNetSubModuleRepository : IRepository<TblAspNetSubModuleRoles>
    {
        IQueryable<TblAspNetSubModuleRoles> ValidateSubModule(int Id);
    }
}
