using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;
using TheCoreBanking.Authenticate.Data.Repository;

namespace TheCoreBanking.Authenticate.Data.Helpers
{

    public class RepositoryFactories2
    {
        /// <summary>
        /// Return the runtime Code Camper repository factory functions,
        /// each one is a factory for a repository of a particular type.
        /// </summary>
        /// <remarks>
        /// MODIFY THIS METHOD TO ADD CUSTOM CODE CAMPER FACTORY FUNCTIONS
        /// </remarks>
        private IDictionary<Type, Func<TheCoreBankingAuthenticateContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<TheCoreBankingAuthenticateContext, object>>
                {
                {typeof(IBranchInformationRepository), dbContext => new BranchInformationRepository(dbContext)},
                 {typeof(ICompanyInformationRepository), dbContext => new CompanyInformationRepository(dbContext)},
                     {typeof(ISectorRepository), dbContext => new SectorRepository(dbContext)},
                    {typeof(IIndustryRepository), dbContext => new IndustryRepository(dbContext)},

                  {typeof(IAspNetModuleRepository), dbContext => new AspNetModuleRepository(dbContext)},
                   {typeof(IAspNetSubModuleRepository), dbContext => new AspNetSubModuleRepository(dbContext)},
                     {typeof(IFinanceCurrentDateRepository), dbContext => new FinanceCurrentDateRepository(dbContext)},
                   {typeof(IFinanceTransactionRepository), dbContext => new FinanceTransactionRepository(dbContext)},
                    {typeof(IFinanceTransactionTypeRepository), dbContext => new FinanceTransactionTypeRepository(dbContext)},
                    {typeof(IMutuallyExclusiveRepository), dbContext => new MutuallyExclusiveRepository(dbContext)},
                    {typeof(IValidateEODRepository), dbContext => new ValidateEODRepository(dbContext)},
                    {typeof(IEODMSYProcessRepository), dbContext => new EODMSYProcessRepository(dbContext)},
                      {typeof(IFinTrakHolsDateRepository), dbContext => new FinTrakHolsDateRepository(dbContext)},

            };
        }

        /// <summary>
        /// Constructor that initializes with runtime Code Camper repository factories
        /// </summary>
        public RepositoryFactories2()
        {
           // _repositoryFactories = GetCodeCamperFactories();
            _repositoryFactories = GetFactories();
        }

        /// <summary>
        /// Constructor that initializes with an arbitrary collection of factories
        /// </summary>
        /// <param name="factories">
        /// The repository factory functions for this instance. 
        /// </param>
        /// <remarks>
        /// This ctor is primarily useful for testing this class
        /// </remarks>
        public RepositoryFactories2(IDictionary<Type, Func<TheCoreBankingAuthenticateContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        /// <summary>
        /// Get the repository factory function for the type.
        /// </summary>
        /// <typeparam name="T">Type serving as the repository factory lookup key.</typeparam>
        /// <returns>The repository function if found, else null.</returns>
        /// <remarks>
        /// The type parameter, T, is typically the repository type 
        /// but could be any type (e.g., an entity type)
        /// </remarks>
        public Func<TheCoreBankingAuthenticateContext, object> GetRepositoryFactory<T>()
        {

            Func<TheCoreBankingAuthenticateContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        /// <summary>
        /// Get the factory for <see cref="IRepository{T}"/> where T is an entity type.
        /// </summary>
        /// <typeparam name="T">The root type of the repository, typically an entity type.</typeparam>
        /// <returns>
        /// A factory that creates the <see cref="IRepository{T}"/>, given an EF <see cref="DbContext"/>.
        /// </returns>
        /// <remarks>
        /// Looks first for a custom factory in <see cref="_repositoryFactories"/>.
        /// If not, falls back to the <see cref="DefaultEntityRepositoryFactory{T}"/>.
        /// You can substitute an alternative factory for the default one by adding
        /// a repository factory for type "T" to <see cref="_repositoryFactories"/>.
        /// </remarks>
        public Func<TheCoreBankingAuthenticateContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
        /// </summary>
        /// <typeparam name="T">Type of the repository's root entity</typeparam>
        protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EFRepository<T>(dbContext);
        }

        /// <summary>
        /// Get the dictionary of repository factory functions.
        /// </summary>
        /// <remarks>
        /// A dictionary key is a System.Type, typically a repository type.
        /// A value is a repository factory function
        /// that takes a <see cref="DbContext"/> argument and returns
        /// a repository object. Caller must know how to cast it.
        /// </remarks>
        private readonly IDictionary<Type, Func<TheCoreBankingAuthenticateContext, object>> _repositoryFactories;
    }
}
