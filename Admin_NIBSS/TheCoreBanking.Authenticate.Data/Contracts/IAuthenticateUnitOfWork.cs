using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IAuthenticateUnitOfWork
    {
        // Save pending changes to the data store.
        void Commit();
        ISectorRepository Sectors { get; }
        IIndustryRepository Industries { get; }

        //Am adding my Repositories from here
        IAspNetModuleRepository Module { get; }
        IBranchInformationRepository Branch { get; }
        ICompanyInformationRepository Company { get; }
        IAspNetSubModuleRepository SubModule { get; }
        IFinanceTransactionRepository FinanceTransaction { get; }
        IFinanceCurrentDateRepository CurrentDate { get; }
        IMutuallyExclusiveRepository MutuallyExclusive { get; }

        IFinanceTransactionTypeRepository FinanceTransactionType { get; }
        IValidateEODRepository validateEOD { get; }
        IEODMSYProcessRepository EODMSYProcess { get; }

        IFinTrakHolsDateRepository FinHols { get; }
    }
}



