using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class FinTrakHolsDateRepository : EFRepository<TblFinTrakHolsDate>, IFinTrakHolsDateRepository
    {
        public FinTrakHolsDateRepository(TheCoreBankingAuthenticateContext context) : base(context) { }
    }
}
