using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class BranchInformationRepository : EFRepository<TblBranchInformation>, IBranchInformationRepository
    {
        public BranchInformationRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblBranchInformation> ValidateBranchInformation(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

    }
}
