using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface ISectorRepository : IRepository<TblSector>
    {
        //IQueryable<TblSector> GetActive();
        //IQueryable<TblSector> GetSingleByNameOrCode(TblSector tblSector);
        //IQueryable<TblSector> GetSingleByName(TblSector tblSector);
        //IQueryable<TblSector> GetSingleByCode(TblSector tblSector);
    }
}
