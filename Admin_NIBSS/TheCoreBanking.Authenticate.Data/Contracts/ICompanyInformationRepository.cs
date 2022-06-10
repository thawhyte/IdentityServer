using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface ICompanyInformationRepository : IRepository<TblCompanyInformation>
    {
        IQueryable<TblCompanyInformation> ValidateCompanyInformation(int Id);
    }
}
