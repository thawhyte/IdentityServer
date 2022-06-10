using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;


namespace TheCoreBanking.Authenticate.Data.Repository
{
    public class SectorRepository : EFRepository<TblSector>, ISectorRepository
    {
        public SectorRepository(TheCoreBankingAuthenticateContext context) : base(context) { }
        
        //public IQueryable<TblSector> GetActive()
        //        => DbSet.Where(s => s.Isdeleted == false);

        //public IQueryable<TblSector> GetSingleByNameOrCode(TblSector tblSector)
        //        => DbSet.Where(s => s.Name == tblSector.Name || s.Code == tblSector.Code);

        //public IQueryable<TblSector> GetSingleByName(TblSector tblSector)
        //       => DbSet.Where(s => s.Name == tblSector.Name );

        //public IQueryable<TblSector> GetSingleByCode(TblSector tblSector)
        //       => DbSet.Where(s => s.Code == tblSector.Code);
    }
}
