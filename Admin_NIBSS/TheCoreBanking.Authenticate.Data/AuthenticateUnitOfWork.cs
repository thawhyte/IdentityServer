using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;
using TheCoreBanking.Authenticate.Data.Helpers;

namespace TheCoreBanking.Authenticate.Data
{
    public class AuthenticateUnitOfWork : IAuthenticateUnitOfWork, IDisposable
    {
        private TheCoreBankingAuthenticateContext DbContext = new TheCoreBankingAuthenticateContext();

        public AuthenticateUnitOfWork(IRepositoryProvider2 repositoryProvider)
        {
            //CreateDbContext();

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        protected IRepositoryProvider2 RepositoryProvider { get; set; }

        // Code Camper repositories

        //public ICustomerAccountTypeRepository CustomerAccountTypes => GetEntityRepository<ICustomerAccountTypeRepository>();
        //public ICustomerRepository Customers => GetEntityRepository<ICustomerRepository>();
        //public IAccountRefereeRepository AccountReferees => GetEntityRepository<IAccountRefereeRepository>();
        public IBranchInformationRepository Branch => GetEntityRepository<IBranchInformationRepository>();
        public ICompanyInformationRepository Company => GetEntityRepository<ICompanyInformationRepository>();
        public IIndustryRepository Industries => GetEntityRepository<IIndustryRepository>();
        public ISectorRepository Sectors => GetEntityRepository<ISectorRepository>();
        public IAspNetModuleRepository Module => GetEntityRepository<IAspNetModuleRepository>();
        public IAspNetSubModuleRepository SubModule => GetEntityRepository<IAspNetSubModuleRepository>();
        public IFinanceCurrentDateRepository CurrentDate => GetEntityRepository<IFinanceCurrentDateRepository>();
        public IMutuallyExclusiveRepository MutuallyExclusive => GetEntityRepository<IMutuallyExclusiveRepository>();
        public IFinanceTransactionRepository FinanceTransaction => GetEntityRepository<IFinanceTransactionRepository>();
        public IFinanceTransactionTypeRepository FinanceTransactionType => GetEntityRepository<IFinanceTransactionTypeRepository>();
        public IValidateEODRepository validateEOD { get { return GetEntityRepository<IValidateEODRepository>(); } }

        public IEODMSYProcessRepository EODMSYProcess { get { return GetEntityRepository<IEODMSYProcessRepository>(); } }
        public IFinTrakHolsDateRepository FinHols { get { return GetEntityRepository<IFinTrakHolsDateRepository>(); } }

        public void Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");

            DbContext.SaveChanges();
        }


        private IRepository<T> GetStandardRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }
        private T GetEntityRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (DbContext != null)
                //{
                //    DbContext.Dispose();
                //}
            }
        }

        #endregion
    }
}
