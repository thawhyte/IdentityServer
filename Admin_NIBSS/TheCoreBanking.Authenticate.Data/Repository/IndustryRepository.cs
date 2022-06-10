using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;


namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class IndustryRepository : EFRepository<TblIndustry>, IIndustryRepository
    {
        public IndustryRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblIndustry> GetDetailed()
            => DbSet.Include(i => i.Sector);

        public IQueryable<TblIndustry> GetActive()
            => DbSet.Where(s => s.Isdeleted == false)
                .Include(i => i.Sector);
    }
}
