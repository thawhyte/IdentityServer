using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;
using Microsoft.Data.SqlClient;
namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class FinanceTransactionRepository : EFRepository<TblFinanceTransaction>, IFinanceTransactionRepository
    {
        public FinanceTransactionRepository(TheCoreBankingAuthenticateContext context) : base(context) { }

        public IQueryable<TblFinanceTransaction> ValidateFinanceTransaction(int Id)
        {
            return DbSet.Where(mod => mod.Id == Id);
        }

        public int UpdateStaffInformation(int Id)
        {

            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _Id = new SqlParameter("@Id", Id);

                return context.Database.ExecuteSqlRaw("sp_UpdateStaffInformation @Id", _Id);

            }
            //return result;
        }
        public int UpdateUnlockInformation(int Id)
        {

            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _Id = new SqlParameter("@Id", Id);

                return context.Database.ExecuteSqlRaw("sp_UnlockStaffInformation @Id", _Id);

            }
            //return result;
        }

    }
}
