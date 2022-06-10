using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TheCoreBanking.Authenticate.Data.Model;

namespace TheCoreBanking.Authenticate.ModelView
{
    public partial class TheCoreBankingAzureContext : DbContext
    {
        public TheCoreBankingAzureContext()
        {
        }

        public TheCoreBankingAzureContext(DbContextOptions<TheCoreBankingAzureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBranchInformation> TblBranchInformation { get; set; }
        public virtual DbSet<TblCompanyInformation> TblCompanyInformation { get; set; }
        public virtual DbSet<TblCountry> TblCountry { get; set; }
        public virtual DbSet<TblCurrency> TblCurrency { get; set; }
        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblDesignation> TblDesignation { get; set; }
        public virtual DbSet<TblRank> TblRank { get; set; }
        public virtual DbSet<TblState> TblState { get; set; }
        public virtual DbSet<TblUnit> TblUnit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=10.4.4.197;Database=TheCoreBankingAzure; user id=sa; password=sqluser10$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBranchInformation>(entity =>
            {
                entity.ToTable("tbl_BranchInformation", "GeneralSetup");

                entity.Property(e => e.BrAddress)
                    .HasColumnName("brAddress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrId)
                    .HasColumnName("brID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrLocation)
                    .HasColumnName("brLocation")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BrManager)
                    .HasColumnName("brManager")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BrName)
                    .HasColumnName("brName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrState)
                    .HasColumnName("brState")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CoyId)
                    .HasColumnName("coyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCompanyInformation>(entity =>
            {
                entity.ToTable("tbl_CompanyInformation", "GeneralSetup");

                entity.Property(e => e.AccountStand)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.AuthorisedShareCapital).HasColumnType("money");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyClass)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyClass)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyId)
                    .HasColumnName("coyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyName)
                    .HasColumnName("coyName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyRegisteredBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfCommencement).HasColumnType("datetime");

                entity.Property(e => e.DateOfIncorporation).HasColumnType("datetime");

                entity.Property(e => e.DateOfRenewalOfRegistration).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EoyprofitAndLossGl)
                    .HasColumnName("EOYProfitAndLossGL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FormerManagersTrustees)
                    .HasColumnName("FormerManagers_Trustees")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionsRegistered)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvestmentObjective).IsUnicode(false);

                entity.Property(e => e.ManagementType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manager)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfRegistrar)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfScheme).IsUnicode(false);

                entity.Property(e => e.NameOfTrustees)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NatureOfBusiness)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrusteesAddress).IsUnicode(false);

                entity.Property(e => e.Webbsite)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCountry>(entity =>
            {
                entity.HasKey(e => e.Countryid)
                    .HasName("PK__tbl_Coun__10D1609F6C4E6CF9");

                entity.ToTable("TBL_COUNTRY", "Customer");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__tbl_Coun__737584F6A66ACCD7")
                    .IsUnique();

                entity.Property(e => e.Countryid).HasColumnName("COUNTRYID");

                entity.Property(e => e.Countrycode)
                    .HasColumnName("COUNTRYCODE")
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblCurrency>(entity =>
            {
                entity.ToTable("tbl_Currency", "GeneralSetup");

                entity.Property(e => e.AverageRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrSymbol)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("decimal(10, 4)");
            });

            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.ToTable("tbl_Department", "GeneralSetup");

                entity.Property(e => e.CoyId)
                    .HasColumnName("CoyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDesignation>(entity =>
            {
                entity.ToTable("tbl_Designation", "GeneralSetup");

                entity.Property(e => e.Designation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRank>(entity =>
            {
                entity.ToTable("tbl_Rank", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrId)
                    .HasColumnName("brId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rank)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblState>(entity =>
            {
                entity.HasKey(e => e.Stateid)
                    .HasName("PK__tbl_Stat__C3BA3B3A2AC2287C");

                entity.ToTable("TBL_STATE", "Customer");

                entity.HasIndex(e => e.Statename)
                    .HasName("UQ__tbl_Stat__737584F619751F56")
                    .IsUnique();

                entity.Property(e => e.Stateid).HasColumnName("STATEID");

                entity.Property(e => e.Countryid).HasColumnName("COUNTRYID");

                entity.Property(e => e.Createdby)
                    .HasColumnName("CREATEDBY")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Datetimecreated)
                    .HasColumnName("DATETIMECREATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lgaid).HasColumnName("LGAID");

                entity.Property(e => e.Regionid).HasColumnName("REGIONID");

                entity.Property(e => e.Statename)
                    .IsRequired()
                    .HasColumnName("STATENAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TblState)
                    .HasForeignKey(d => d.Countryid)
                    .HasConstraintName("FK_TBL_STATE_TBL_COUNTRY");
            });

            modelBuilder.Entity<TblUnit>(entity =>
            {
                entity.ToTable("tbl_Unit", "GeneralSetup");

                entity.Property(e => e.BrCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.HasSequence("GenerateInterestTransID");

            modelBuilder.HasSequence("seqGetNextBatchRef")
                .StartsAt(25000)
                .HasMin(25000);

            modelBuilder.HasSequence("TransactionSequence").HasMin(0);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
