using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class CompanyInformationRepository : EFRepository<TblCompanyInformation>, ICompanyInformationRepository
    {
        public CompanyInformationRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblCompanyInformation> ValidateCompanyInformation(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

    }
}
