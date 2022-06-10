using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IIndustryRepository : IRepository<TblIndustry>
    {
        IQueryable<TblIndustry> GetDetailed();

        IQueryable<TblIndustry> GetActive();
    }
}
