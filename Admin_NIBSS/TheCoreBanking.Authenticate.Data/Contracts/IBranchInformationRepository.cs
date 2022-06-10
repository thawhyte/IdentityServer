using System;
using System.Linq;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IBranchInformationRepository : IRepository<TblBranchInformation>
    {
        IQueryable<TblBranchInformation> ValidateBranchInformation(int Id);
    }
}