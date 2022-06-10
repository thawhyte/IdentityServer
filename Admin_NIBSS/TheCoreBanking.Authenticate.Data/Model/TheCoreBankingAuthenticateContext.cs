using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheCoreBanking.Authenticate.Data.Model
{
    public partial class TheCoreBankingAuthenticateContext : DbContext
    {
        public virtual DbSet<TblAutoLogOff> TblAutoLogOff { get; set; }
        public virtual DbSet<vwUsermanagement> vwUsermanagement { get; set; }
        public virtual DbSet<TblBankingAuditTrail> TblBankingAuditTrail { get; set; }
        public virtual DbSet<TblSubscriptions> TblSubscriptions { get; set; }
        public virtual DbSet<AspNetUserRoles> TblAspNetUserRoles { get; set; }
        public virtual DbSet<TblAspNetModuleRoles> TblAspNetModuleRoles { get; set; }
        public virtual DbSet<TblAspNetSubModuleRoles> TblAspNetSubModuleRoles { get; set; }
        public virtual DbSet<ListUserRoles> ListUserRoles { get; set; }
        public virtual DbSet<TblMoneyMarketDeals> TblMoneyMarketDeals { get; set; }
        public virtual DbSet<TblMoneyMaturedDealOperation> TblMoneyMaturedDealOperation { get; set; }
        public virtual DbSet<TblStaffInformation> TblStaffInformation { get; set; }
        public virtual DbSet<TblFinanceTransactionType> TblFinanceTransactionType { get; set; }
        public virtual DbSet<TblFinanceTransaction> TblFinanceTransaction { get; set; }
        public virtual DbSet<TblFinanceCurrentDate> TblFinanceCurrentDate { get; set; }
        public virtual DbSet<TblMutuallyExclusive> TblMutuallyExclusive { get; set; }
        public virtual DbSet<StartDayModel> StartDayModel { get; set; }
        public virtual DbSet<EndofDayModel> EndofDayModel { get; set; }
        public virtual DbSet<TblCasa> TblCasa { get; set; }
        public virtual DbSet<TblEodvalidation> TblEodvalidation { get; set; }
        public virtual DbSet<TblBankingApprovalTrack> TblBankingApprovalTrack { get; set; }
        public virtual DbSet<TblMoneyDealDetails> TblMoneyDealDetails { get; set; }
        public virtual DbSet<TblBankingLoanLease> TblBankingLoanLease { get; set; }
        public virtual DbSet<TblSingleFundTransfer> TblSingleFundTransfer { get; set; }
        public virtual DbSet<TblMultipleAccountToCreditFundTransfer> TblMultipleAccountToCreditFundTransfer { get; set; }
        public virtual DbSet<TblSector> TblSector { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblIndustry> TblIndustry { get; set; }
        public virtual DbSet<TblFinTrakHolsDate> TblFinTrakHolsDate { get; set; }
        public virtual DbSet<TblCompanyInformation> TblCompanyInformation { get; set; }
        public virtual DbSet<TblBranchInformation> TblBranchInformation { get; set; }
        public virtual DbSet<TblBankingCadeposit> TblBankingCadeposit { get; set; }
        public virtual DbSet<TblBankingCawithdrawal> TblBankingCawithdrawal { get; set; }
        public virtual DbSet<TblMisinformation> TblMisinformation { get; set; }
        public virtual DbSet<TblCountry> TblCountry { get; set; }
        public virtual DbSet<TblCurrency> TblCurrency { get; set; }
        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblDesignation> TblDesignation { get; set; }
        public virtual DbSet<TblRank> TblRank { get; set; }
        public virtual DbSet<TblState> TblState { get; set; }
        public virtual DbSet<TblUnit> TblUnit { get; set; }
        public virtual DbSet<TblInformation> TblInformation { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(@"Server=10.4.4.197;Database=TheCoreBankingAzure;user id=finance;password=sqluser10$$", builder =>
                // optionsBuilder.UseSqlServer(@"Server=10.4.4.197;Database=TheCoreBankingAzure;user id=sa;password=sqluser10$", builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
              
                base.OnConfiguring(optionsBuilder);


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblInformation>(entity =>
            {
                entity.ToTable("tbl_Information", "GeneralSetup");

                entity.Property(e => e.UserGame)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
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

            modelBuilder.Entity<TblBankingCawithdrawal>(entity =>
            {
                entity.ToTable("TBL_Banking_CAWithdrawal", "Retail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ActualDate).HasColumnType("datetime");

                entity.Property(e => e.Addresss)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AmtWithdraw).HasColumnType("money");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.BranchId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Chequeno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerBr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DestinationCa).HasColumnName("DestinationCA");

                entity.Property(e => e.FormId)
                    .HasColumnName("FormID")
                    .HasColumnType("char(2)");

                entity.Property(e => e.Miscode)
                    .HasColumnName("MISCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Narration)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.PrincipalBalGl)
                    .HasColumnName("PrincipalBalGL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductAcctNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SlipNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SourceCa).HasColumnName("SourceCA");

                entity.Property(e => e.TillAcct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransTime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UploadDoc)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TblBankingCadeposit>(entity =>
            {
                entity.ToTable("tbl_Banking_CADeposit", "Retail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ActualDate).HasColumnType("datetime");

                entity.Property(e => e.AmtDeposited).HasColumnType("money");

                entity.Property(e => e.AmtDeposited1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.BankLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BranchId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeBank)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerBr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DepositorAddr)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DepositorName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DepositorPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepositorSign)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationCa).HasColumnName("DestinationCA");

                entity.Property(e => e.FundSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IddateExpiry)
                    .HasColumnName("IDDateExpiry")
                    .HasColumnType("date");

                entity.Property(e => e.IddateIssued)
                    .HasColumnName("IDDateIssued")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idno)
                    .HasColumnName("IDNo")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Idtype)
                    .HasColumnName("IDType")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IssuanceAcctNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MeansOfPayment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Miscode)
                    .HasColumnName("MISCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Narration)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.PrincipalBalGl)
                    .HasColumnName("PrincipalBalGL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductAcctNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SlipNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SourceCa).HasColumnName("SourceCA");

                entity.Property(e => e.TillAcct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
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
            modelBuilder.Entity<TblAutoLogOff>(entity =>
            {
                entity.ToTable("tbl_AutoLogOff", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("dateCreated")
                    .HasColumnType("date");

                entity.Property(e => e.LogoutTime).HasColumnName("logoutTime");
            });

            modelBuilder.HasSequence("GenerateInterestTransID");

            modelBuilder.HasSequence("seqGetNextBatchRef")
                .StartsAt(25000)
                .HasMin(25000);

            modelBuilder.HasSequence("TransactionSequence").HasMin(0);
            modelBuilder.Entity<TblMoneyMarketDeals>(entity =>
            {
                entity.ToTable("tbl_MoneyMarketDeals", "MoneyMarket");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountOfficer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CpId)
                    .HasColumnName("cpID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentClass)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DealApprovedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealCreatedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealId)
                    .IsRequired()
                    .HasColumnName("DealID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealUpload).IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.DiscountedValue).HasColumnType("money");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.EffectiveYield).HasColumnType("decimal(18, 13)");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.FictitiousMaturity).HasColumnType("datetime");

                entity.Property(e => e.GivingCpty)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InterestAmount).HasColumnType("money");

                entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MDealStatusId).HasColumnName("mDealStatusID");

                entity.Property(e => e.MatDate)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MaturityAmount).HasColumnType("money");

                entity.Property(e => e.MaturityDate).HasColumnType("datetime");

                entity.Property(e => e.MmDealId)
                    .HasColumnName("mmDealID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewDealId)
                    .HasColumnName("NewDealID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewmmDealId)
                    .HasColumnName("NewmmDealID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NewpdmmTypeId)
                    .HasColumnName("NewpdmmTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OnlineId).HasColumnName("onlineID");

                entity.Property(e => e.Operation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.PayDiscount).HasColumnName("payDiscount");

                entity.Property(e => e.Pccode)
                    .HasColumnName("PCcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PdTypeId).HasColumnName("pdTypeId");

                entity.Property(e => e.PdmmTypeId)
                    .HasColumnName("pdmmTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PosterCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrincipalAmount).HasColumnType("money");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.SendLetter).HasColumnName("sendLetter");

                entity.Property(e => e.SettlementAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.OriginalTenor)
                   .HasMaxLength(50)
                   .IsUnicode(false);
                entity.Property(e => e.TerminateAcctNo)
                   .HasMaxLength(50)
                   .IsUnicode(false);
                entity.Property(e => e.StaffNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TblMoneyMaturedDealOperation>(entity =>
            {
                entity.ToTable("tbl_MoneyMaturedDealOperation", "MoneyMarket");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatchRef)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CpId)
                    .HasColumnName("cpID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DealApprovedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealCreatedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealId)
                    .IsRequired()
                    .HasColumnName("DealID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscountedValue).HasColumnType("money");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.EffectiveYield).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.InterestAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InterestArchived)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MDealStatusId).HasColumnName("mDealStatusID");

                entity.Property(e => e.MaturityAmount).HasColumnType("money");

                entity.Property(e => e.MaturityDate).HasColumnType("datetime");

                entity.Property(e => e.MmDealId)
                    .HasColumnName("mmDealID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NewInterestRate).HasColumnName("newInterestRate");

                entity.Property(e => e.NewTenor)
                    .HasColumnName("newTenor")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.PenalAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PenalRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrincipalAmount).HasColumnType("money");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SettlementAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Upload).IsUnicode(false);

                entity.Property(e => e.Whtamount)
                    .HasColumnName("WHTAmount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Whtrate)
                    .HasColumnName("WHTRate")
                    .HasDefaultValueSql("((0))");
            });
            modelBuilder.Entity<TblMisinformation>(entity =>
            {
                entity.ToTable("tbl_MISInformation", "GeneralSetup");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.MisCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MisName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MisTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentMisCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.HasSequence("GenerateInterestTransID");

            modelBuilder.HasSequence("seqGetNextBatchRef")
                .StartsAt(25000)
                .HasMin(25000);

            modelBuilder.HasSequence("TransactionSequence").HasMin(0);
            modelBuilder.Entity<TblFinTrakHolsDate>(entity =>
            {
                entity.ToTable("tbl_FinTrakHolsDate", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HolidayType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSubscriptions>(entity =>
            {
                entity.ToTable("tbl_Subscriptions", "GeneralSetup");

                entity.Property(e => e.Amount).IsUnicode(false);

                entity.Property(e => e.ApplicationId)
                    .HasColumnName("Application_id")
                    .IsUnicode(false);

                entity.Property(e => e.CompanyAddress)
                    .HasColumnName("company_address")
                    .IsUnicode(false);

                entity.Property(e => e.CompanyDescription)
                    .HasColumnName("Company_description")
                    .IsUnicode(false);

                entity.Property(e => e.CompanyEmail)
                    .HasColumnName("company_email")
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("Company_Id")
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("Company_name")
                    .IsUnicode(false);

                entity.Property(e => e.CompanySector)
                    .HasColumnName("company_sector")
                    .IsUnicode(false);

                entity.Property(e => e.CompanyTelephone)
                    .HasColumnName("company_telephone")
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .IsUnicode(false);

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .IsUnicode(false);

                entity.Property(e => e.OrderDetailDescription)
                    .HasColumnName("order_detail_description")
                    .IsUnicode(false);

                entity.Property(e => e.OrderDetailId)
                    .HasColumnName("Order_Detail_ID")
                    .IsUnicode(false);

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_ID")
                    .IsUnicode(false);

                entity.Property(e => e.ProductAlias)
                    .HasColumnName("Product_alias")
                    .IsUnicode(false);

                entity.Property(e => e.TenantId)
                    .HasColumnName("tenant_id")
                    .IsUnicode(false);

                entity.Property(e => e.ValidityEndDate)
                    .HasColumnName("validity_EndDate")
                    .IsUnicode(false);

                entity.Property(e => e.ValidityStartDate)
                    .HasColumnName("validity_StartDate")
                    .IsUnicode(false);
            });
            modelBuilder.Entity<vwUsermanagement>(entity =>
            {
                entity.ToTable("vw_UserManagment", "dbo");
            });
            modelBuilder.Entity<EndofDayModel>(entity =>
            {
                entity.ToTable("sp_EndOfDay", "dbo");

                entity.Property(e => e.Id);

                entity.Property(e => e.date);

                entity.Property(e => e.ydate);
                entity.Property(e => e.newdate);

                entity.Property(e => e.deals);

                entity.Property(e => e.product);

                entity.Property(e => e.sumproduct);
                entity.Property(e => e.year);

                entity.Property(e => e.CheckDate);
                entity.Property(e => e.iprincipal);

                entity.Property(e => e.interestRate);
                entity.Property(e => e.effectiveDate);

                entity.Property(e => e.countTemp);
                entity.Property(e => e.kounter);

                entity.Property(e => e.DealID);
                entity.Property(e => e.LastEOD);

                entity.Property(e => e.Tenor);
                entity.Property(e => e.iaccruedTodate);

                entity.Property(e => e.startingDate);
                entity.Property(e => e.countDown);

                entity.Property(e => e.BackDate);

                entity.Property(e => e.NextWorkingDate);

                entity.Property(e => e.DayDiff);
                entity.Property(e => e.today);

                entity.Property(e => e.CoyCode);
                entity.Property(e => e.Time);

                entity.Property(e => e.Ref);
                entity.Property(e => e.ErrorMsg);

                entity.Property(e => e.ErrorSp_Name);
                entity.Property(e => e.ErrorLine);
                entity.Property(e => e.ErrorNumber);

                entity.Property(e => e.ErrorState);
            });
            modelBuilder.Entity<StartDayModel>(entity =>
            {
                entity.ToTable("sp_StartOfDay", "dbo");

                entity.Property(e => e.Id);

                entity.Property(e => e.CurrentDate);

                entity.Property(e => e.NextDate);
            });
            modelBuilder.Entity<TblMutuallyExclusive>(entity =>
            {
                entity.ToTable("tbl_MutuallyExclusive", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Endofday).HasColumnName("endofday");

                entity.Property(e => e.Startofday).HasColumnName("startofday");
            });
            modelBuilder.Entity<TblFinanceCurrentDate>(entity =>
            {
                entity.ToTable("tbl_FinanceCurrentDate", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentDate).HasColumnType("DateTime");
            });
            modelBuilder.Entity<TblFinanceTransactionType>(entity =>
            {
                entity.ToTable("tbl_FinanceTransactionType", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TblFinanceTransaction>(entity =>
            {
                entity.ToTable("tbl_FinanceTransaction", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationId)
                    .HasColumnName("ApplicationID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Approved).HasDefaultValueSql("((1))");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditAmt).HasColumnType("money");

                entity.Property(e => e.DebitAmt).HasColumnType("money");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationBranch)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Itemid)
                    .HasColumnName("ITEMID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LcurrencyCode)
                    .HasColumnName("LCurrencyCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Legtype)
                    .HasColumnName("LEGTYPE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Miscode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NonBrAccountId)
                    .HasColumnName("NonBrAccountID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PostedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostingTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SCoyCode)
                    .HasColumnName("sCoyCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sbu)
                    .HasColumnName("SBU")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SourceBranch)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.ValueDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<TblStaffInformation>(entity =>
            {
                entity.ToTable("tbl_StaffInformation", "GeneralSetup");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BranchId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BranchLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comment).HasColumnType("text");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImageData).HasMaxLength(50);

                entity.Property(e => e.ImageTitle)
                    .HasColumnName("imageTitle")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Locked)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Miscode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinGender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PcCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Rank)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.RelationShip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StaffNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                   .HasMaxLength(300)
                   .IsUnicode(false);
            });

            modelBuilder.HasSequence("GenerateInterestTransID");

            modelBuilder.HasSequence("seqGetNextBatchRef")
                .StartsAt(25000)
                .HasMin(25000);

            modelBuilder.HasSequence("TransactionSequence").HasMin(0);
            modelBuilder.Entity<ListUserRoles>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.DateCreated).HasColumnType("DateTime");

                entity.Property(e => e.LstModules)
                    .HasColumnName("lstModules")
                    .IsUnicode(false);

                entity.Property(e => e.LstUserActivities)
                    .HasColumnName("lstUserActivities")
                    .IsUnicode(false);

                entity.Property(e => e.LstUsers)
                    .HasColumnName("lstUsers")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);
            });
            modelBuilder.Entity<TblAspNetModuleRoles>(entity =>
            {
                entity.ToTable("tbl_AspNetModuleRoles","dbo");

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAspNetSubModuleRoles>(entity =>
            {
                entity.ToTable("tbl_AspNetSubModuleRoles", "dbo");

                entity.Property(e => e.ModuleId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.SubmoduleName).HasMaxLength(256);
            });
            modelBuilder.Entity<TblBankingAuditTrail>(entity =>
            {
                entity.ToTable("tbl_BankingAuditTrail", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CmpName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransDate).HasColumnType("datetime");

                entity.Property(e => e.TransTime)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransType).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorCode)
                  .HasMaxLength(50)
                  .IsUnicode(false);
            });

            modelBuilder.HasSequence("TransactionSequence").HasMin(0);
            modelBuilder.Entity<TblCasa>(entity =>
            {
                entity.HasKey(e => e.Casaaccountid);

                entity.ToTable("TBL_CASA", "Customer");

                entity.HasIndex(e => e.Accountnumber)
                    .HasName("IX_TBL_CASA")
                    .IsUnique();

                entity.Property(e => e.Casaaccountid).HasColumnName("CASAACCOUNTID");

                entity.Property(e => e.Accountname)
                    .IsRequired()
                    .HasColumnName("ACCOUNTNAME")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Accountnumber)
                    .IsRequired()
                    .HasColumnName("ACCOUNTNUMBER")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Accountofficerdeptid).HasColumnName("ACCOUNTOFFICERDEPTID");

                entity.Property(e => e.Accountofficerid)
                    .HasColumnName("ACCOUNTOFFICERID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Accountstatusid)
                    .HasColumnName("ACCOUNTSTATUSID")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Actionby)
                    .HasColumnName("ACTIONBY")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Actiondate)
                    .HasColumnName("ACTIONDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Approvalstatusid).HasColumnName("APPROVALSTATUSID");

                entity.Property(e => e.Availablebalance)
                    .HasColumnName("AVAILABLEBALANCE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Branchid).HasColumnName("BRANCHID");

                entity.Property(e => e.Companyid).HasColumnName("COMPANYID");

                entity.Property(e => e.Createdby)
                    .HasColumnName("CREATEDBY")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Currencyid)
                    .HasColumnName("CURRENCYID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Customerid).HasColumnName("CUSTOMERID");

                entity.Property(e => e.Datetimecreated)
                    .HasColumnName("DATETIMECREATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datetimedeleted)
                    .HasColumnName("DATETIMEDELETED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datetimeupdated)
                    .HasColumnName("DATETIMEUPDATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.Deletedby)
                    .HasColumnName("DELETEDBY")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Effectivedate)
                    .HasColumnName("EFFECTIVEDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Hasoverdraft).HasColumnName("HASOVERDRAFT");

                entity.Property(e => e.Interestrate)
                    .HasColumnName("INTERESTRATE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Iscurrentaccount).HasColumnName("ISCURRENTACCOUNT");

                entity.Property(e => e.Lastupdatecomment)
                    .HasColumnName("LASTUPDATECOMMENT")
                    .IsUnicode(false);

                entity.Property(e => e.Lastupdatedby)
                    .HasColumnName("LASTUPDATEDBY")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ledgerbalance)
                    .HasColumnName("LEDGERBALANCE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Mandate)
                    .HasColumnName("MANDATE")
                    .IsUnicode(false);

                entity.Property(e => e.Miscode)
                    .HasColumnName("MISCODE")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Oldproductaccountnumber1)
                    .HasColumnName("OLDPRODUCTACCOUNTNUMBER1")
                    .HasMaxLength(50);

                entity.Property(e => e.Oldproductaccountnumber2)
                    .HasColumnName("OLDPRODUCTACCOUNTNUMBER2")
                    .HasMaxLength(50);

                entity.Property(e => e.Oldproductaccountnumber3)
                    .HasColumnName("OLDPRODUCTACCOUNTNUMBER3")
                    .HasMaxLength(50);

                entity.Property(e => e.Operationid).HasColumnName("OPERATIONID");

                entity.Property(e => e.Overdraftamount)
                    .HasColumnName("OVERDRAFTAMOUNT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Overdraftexpirydate)
                    .HasColumnName("OVERDRAFTEXPIRYDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Overdraftinterestrate)
                    .HasColumnName("OVERDRAFTINTERESTRATE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Postnostatusid).HasColumnName("POSTNOSTATUSID");

                entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

                entity.Property(e => e.Relationshipmanagerid)
                    .HasColumnName("RELATIONSHIPMANAGERID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Relationshipofficerdeptid).HasColumnName("RELATIONSHIPOFFICERDEPTID");

                entity.Property(e => e.Relationshipofficerid)
                    .HasColumnName("RELATIONSHIPOFFICERID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Teammiscode)
                    .HasColumnName("TEAMMISCODE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tenor)
                    .HasColumnName("TENOR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Terminaldate)
                    .HasColumnName("TERMINALDATE")
                    .HasColumnType("date");
            });
            modelBuilder.Entity<TblEodvalidation>(entity =>
            {
                entity.ToTable("TBL_EODValidation","dbo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DealId)
                    .HasColumnName("DealID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Officer)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Operation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TransDate).HasColumnType("date");
            });
            modelBuilder.Entity<TblBankingApprovalTrack>(entity =>
            {
                entity.ToTable("tbl_BankingApprovalTrack", "Credit");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BatchRef)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Operation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.ProdCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProdNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TblMoneyDealDetails>(entity =>
            {
                entity.ToTable("tbl_MoneyDealDetails", "MoneyMarket");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalComment).IsUnicode(false);

                entity.Property(e => e.ApprovingUnit)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AprovedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BookingTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Broker)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrokerStaffNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateTsuapproved)
                    .HasColumnName("DateTSUApproved")
                    .HasColumnType("datetime");

                entity.Property(e => e.DealId)
                    .HasColumnName("DealID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.MaturityDate).HasColumnType("datetime");

                entity.Property(e => e.MmTypeId).HasColumnName("mmTypeID");

                entity.Property(e => e.OpId).HasColumnName("opID");

                entity.Property(e => e.Operation).IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.PdCategoryId).HasColumnName("pdCategoryID");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RefId)
                    .IsRequired()
                    .HasColumnName("RefID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.RemarkBy).IsUnicode(false);

                entity.Property(e => e.SysconApproval).HasColumnName("sysconApproval");

                entity.Property(e => e.SysconDateApproved)
                    .HasColumnName("sysconDateApproved")
                    .HasColumnType("datetime");

                entity.Property(e => e.SysconDisapproval).HasColumnName("sysconDisapproval");

                entity.Property(e => e.SysconRemark)
                    .HasColumnName("sysconRemark")
                    .IsUnicode(false);

                entity.Property(e => e.SysconopId).HasColumnName("sysconopID");

                entity.Property(e => e.Sysconperson)
                    .HasColumnName("sysconperson")
                    .IsUnicode(false);

                entity.Property(e => e.TransDate).HasColumnType("datetime");

                entity.Property(e => e.Tsuapproval).HasColumnName("TSUApproval");

                entity.Property(e => e.TsuapprovedBy)
                    .HasColumnName("TSUApprovedBy")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tsudisapproval).HasColumnName("TSUDisapproval");

                entity.Property(e => e.TsuheadApproval).HasColumnName("TSUHeadApproval");

                entity.Property(e => e.TsuheadDateApproved)
                    .HasColumnName("TSUHeadDateApproved")
                    .HasColumnType("datetime");

                entity.Property(e => e.TsuheadDisapproval).HasColumnName("TSUHeadDisapproval");

                entity.Property(e => e.TsuheadStaffNo)
                    .HasColumnName("TSUHeadStaffNO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UnitHeadApprovedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitHeadDateApproved).HasColumnType("datetime");

                entity.Property(e => e.Upload).IsUnicode(false);
            });
            modelBuilder.Entity<TblMoneyMarketDeals>(entity =>
            {
                entity.ToTable("tbl_MoneyMarketDeals", "MoneyMarket");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountOfficer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CpId)
                    .HasColumnName("cpID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentClass)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DealApprovedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealCreatedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealId)
                    .IsRequired()
                    .HasColumnName("DealID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DealUpload).IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.DiscountedValue).HasColumnType("money");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.EffectiveYield).HasColumnType("decimal(18, 13)");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.FictitiousMaturity).HasColumnType("datetime");

                entity.Property(e => e.GivingCpty)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InterestAmount).HasColumnType("money");

                entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 7)");

                entity.Property(e => e.MDealStatusId).HasColumnName("mDealStatusID");

                entity.Property(e => e.MatDate)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MaturityAmount).HasColumnType("money");

                entity.Property(e => e.MaturityDate).HasColumnType("datetime");

                entity.Property(e => e.MmDealId)
                    .HasColumnName("mmDealID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewDealId)
                    .HasColumnName("NewDealID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewmmDealId)
                    .HasColumnName("NewmmDealID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NewpdmmTypeId)
                    .HasColumnName("NewpdmmTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OnlineId).HasColumnName("onlineID");

                entity.Property(e => e.Operation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.PayDiscount).HasColumnName("payDiscount");

                entity.Property(e => e.Pccode)
                    .HasColumnName("PCcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PdTypeId).HasColumnName("pdTypeId");

                entity.Property(e => e.PdmmTypeId)
                    .HasColumnName("pdmmTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PosterCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrincipalAmount).HasColumnType("money");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.SendLetter).HasColumnName("sendLetter");

                entity.Property(e => e.SettlementAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TblBankingLoanLease>(entity =>
            {
                entity.ToTable("tbl_BankingLoanLease", "Credit");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovalComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveMaintainComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveOpComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BulletFreqName)
                    .HasColumnName("BulletFReqName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BulletName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditMandate).IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentAcct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateOfDisburse).HasColumnType("datetime");

                entity.Property(e => e.DatePaid).HasColumnType("datetime");

                entity.Property(e => e.DisburseComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Disburser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EffectDate).HasColumnType("datetime");

                entity.Property(e => e.FirstpayDate).HasColumnType("datetime");

                entity.Property(e => e.FreqType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IssueOfferletter).HasColumnName("issueOfferletter");

                entity.Property(e => e.Miscode)
                    .HasColumnName("MISCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MyPath)
                    .HasColumnName("myPath")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nhf).HasColumnName("NHF");

                entity.Property(e => e.NoOfPrincipalAddition).HasColumnName("noOfPrincipalAddition");

                entity.Property(e => e.NoOfPrincipalReduction).HasColumnName("noOfPrincipalReduction");

                entity.Property(e => e.Officer1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Officer2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Pccode1)
                    .HasColumnName("PCcode1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pccode2)
                    .HasColumnName("PCcode2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PdTypeId).HasColumnName("pdTypeID");

                entity.Property(e => e.PrincipalFreqType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductAcctNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileLoan).HasColumnName("profileLoan");

                entity.Property(e => e.Ref)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RelationshipManager)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RelationshipManagerDept)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RelationshipOfficer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RelationshipOfficerDept)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Riskass).HasColumnName("riskass");

                entity.Property(e => e.Sbumis)
                    .HasColumnName("SBUMIS")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TerminalDate).HasColumnType("datetime");

                entity.Property(e => e.UpfrontInterest).HasColumnName("upfrontInterest");

                entity.Property(e => e.Upfrontprincipal).HasColumnName("upfrontprincipal");

                entity.Property(e => e.Upload).IsUnicode(false);
            });
            modelBuilder.Entity<TblSingleFundTransfer>(entity =>
            {
                entity.ToTable("tbl_SingleFundTransfer", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountCr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountDr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.NarrationCr)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NarrationDr)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.PostingTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReciepNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TransCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValueDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<TblMultipleAccountToCreditFundTransfer>(entity =>
            {
                entity.ToTable("tbl_MultipleAccountToCredit_FundTransfer", "Finance");

                entity.Property(e => e.AccountNo).IsUnicode(false);

                entity.Property(e => e.AccountNoDr).IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ApprovedBy).IsUnicode(false);

                entity.Property(e => e.BatchName).IsUnicode(false);

                entity.Property(e => e.BrCode).IsUnicode(false);

                entity.Property(e => e.CoyCode).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Narration).IsUnicode(false);

                entity.Property(e => e.ReciepNo).IsUnicode(false);

                entity.Property(e => e.Reference).IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.TransCode).IsUnicode(false);

                entity.Property(e => e.ValueDate).HasColumnType("datetime");
            });

            modelBuilder.HasSequence("seqGetNextBatchRef")
                .StartsAt(25000)
                .HasMin(25000);

            modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        
        //
           modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.Customerid);

                entity.ToTable("TBL_CUSTOMER", "Customer");

                entity.Property(e => e.Customerid).HasColumnName("CUSTOMERID");

                entity.Property(e => e.Accountcreationcomplete).HasColumnName("ACCOUNTCREATIONCOMPLETE");

                entity.Property(e => e.Actedonby)
                    .HasColumnName("ACTEDONBY")
                    .HasMaxLength(150);

                entity.Property(e => e.Annualincomeid).HasColumnName("ANNUALINCOMEID");

                entity.Property(e => e.Approvalstatus).HasColumnName("APPROVALSTATUS");

                entity.Property(e => e.Bankaccountnumber)
                    .HasColumnName("BANKACCOUNTNUMBER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Bankaccountopeneddate)
                    .HasColumnName("BANKACCOUNTOPENEDDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Bankaccountypeid).HasColumnName("BANKACCOUNTYPEID");

                entity.Property(e => e.Bankaddress)
                    .HasColumnName("BANKADDRESS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Bankid).HasColumnName("BANKID");

                entity.Property(e => e.Branchid).HasColumnName("BRANCHID");

                entity.Property(e => e.Businesscategoryid).HasColumnName("BUSINESSCATEGORYID");

                entity.Property(e => e.Businessstartdate)
                    .HasColumnName("BUSINESSSTARTDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Businesswebsite)
                    .HasColumnName("BUSINESSWEBSITE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Createdby).HasColumnName("CREATEDBY");

                entity.Property(e => e.Creationmailsent).HasColumnName("CREATIONMAILSENT");

                entity.Property(e => e.Creditrating)
                    .HasColumnName("CREDITRATING")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Currentemployer)
                    .HasColumnName("CURRENTEMPLOYER")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Customeraccounttypeid).HasColumnName("CUSTOMERACCOUNTTYPEID");

                entity.Property(e => e.Customercode)
                    .IsRequired()
                    .HasColumnName("CUSTOMERCODE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Customersensitivitylevelid).HasColumnName("CUSTOMERSENSITIVITYLEVELID");

                entity.Property(e => e.Dateactedon)
                    .HasColumnName("DATEACTEDON")
                    .HasColumnType("date");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnName("DATEOFBIRTH")
                    .HasColumnType("date");

                entity.Property(e => e.Datetimecreated)
                    .HasColumnName("DATETIMECREATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datetimedeleted)
                    .HasColumnName("DATETIMEDELETED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datetimeupdated)
                    .HasColumnName("DATETIMEUPDATED")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.Deletedby).HasColumnName("DELETEDBY");

                entity.Property(e => e.Educationlevel)
                    .HasColumnName("EDUCATIONLEVEL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Employeddate)
                    .HasColumnName("EMPLOYEDDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Employmentstatus).HasColumnName("EMPLOYMENTSTATUS");

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Firstchilddob)
                    .HasColumnName("FIRSTCHILDDOB")
                    .HasColumnType("date");

                entity.Property(e => e.Firstchildname)
                    .HasColumnName("FIRSTCHILDNAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Foreignrpno)
                    .HasColumnName("FOREIGNRPNO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Genderid).HasColumnName("GENDERID");

                entity.Property(e => e.Hometown)
                    .HasColumnName("HOMETOWN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idexpiry)
                    .HasColumnName("IDEXPIRY")
                    .HasColumnType("date");

                entity.Property(e => e.Idissueauthority)
                    .HasColumnName("IDISSUEAUTHORITY")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idplaceofissue)
                    .HasColumnName("IDPLACEOFISSUE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Industryid).HasColumnName("INDUSTRYID");

                entity.Property(e => e.Institutiontypeid).HasColumnName("INSTITUTIONTYPEID");

                entity.Property(e => e.Ispoliticallyexposed).HasColumnName("ISPOLITICALLYEXPOSED");

                entity.Property(e => e.Lastupdatedby).HasColumnName("LASTUPDATEDBY");

                entity.Property(e => e.Maritalstatusid).HasColumnName("MARITALSTATUSID");

                entity.Property(e => e.Marriagecertificationdate)
                    .HasColumnName("MARRIAGECERTIFICATIONDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Modeofidentificationid).HasColumnName("MODEOFIDENTIFICATIONID");

                entity.Property(e => e.Mothersmaidenname)
                    .HasColumnName("MOTHERSMAIDENNAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Namercparentbody)
                    .HasColumnName("NAMERCPARENTBODY")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Namercrelatedcoys)
                    .HasColumnName("NAMERCRELATEDCOYS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasColumnName("NATIONALITY")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Natureofbusiness)
                    .HasColumnName("NATUREOFBUSINESS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nokaddress)
                    .HasColumnName("NOKADDRESS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nokdob)
                    .HasColumnName("NOKDOB")
                    .HasColumnType("date");

                entity.Property(e => e.Nokemail)
                    .HasColumnName("NOKEMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nokgenderid).HasColumnName("NOKGENDERID");

                entity.Property(e => e.Nokothernames)
                    .HasColumnName("NOKOTHERNAMES")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nokphone)
                    .HasColumnName("NOKPHONE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nokrelationship)
                    .HasColumnName("NOKRELATIONSHIP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Noksurname)
                    .HasColumnName("NOKSURNAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Occupation)
                    .HasColumnName("OCCUPATION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Othernames)
                    .HasColumnName("OTHERNAMES")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Placeofbirth)
                    .HasColumnName("PLACEOFBIRTH")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Pobox)
                    .HasColumnName("POBOX")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Previousemployer)
                    .HasColumnName("PREVIOUSEMPLOYER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rcnumber)
                    .HasColumnName("RCNUMBER")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Regionid).HasColumnName("REGIONID");

                entity.Property(e => e.Relationshipofficerid).HasColumnName("RELATIONSHIPOFFICERID");

                entity.Property(e => e.Scumlnumber)
                    .HasColumnName("SCUMLNUMBER")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sectorid).HasColumnName("SECTORID");

                entity.Property(e => e.Sourceoffundid).HasColumnName("SOURCEOFFUNDID");

                entity.Property(e => e.Spousemail)
                    .HasColumnName("SPOUSEMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Spousenamework)
                    .HasColumnName("SPOUSENAMEWORK")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Spousephone)
                    .HasColumnName("SPOUSEPHONE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Staffnumber)
                    .HasColumnName("STAFFNUMBER")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Stateoforiginid).HasColumnName("STATEOFORIGINID");

                entity.Property(e => e.Stateoriginlgaid).HasColumnName("STATEORIGINLGAID");

                entity.Property(e => e.Surname)
                    .HasColumnName("SURNAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Taxidnumber)
                    .HasColumnName("TAXIDNUMBER")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Titleid).HasColumnName("TITLEID");

                entity.Property(e => e.Weddingdate)
                    .HasColumnName("WEDDINGDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Workaddress)
                    .HasColumnName("WORKADDRESS")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Workcountry).HasColumnName("WORKCOUNTRY");

                entity.Property(e => e.Workphone)
                    .HasColumnName("WORKPHONE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bvn)
                   .HasColumnName("BVN")
                   .HasMaxLength(50);

                entity.Property(e => e.Workstate).HasColumnName("WORKSTATE");

           

            
            });

            modelBuilder.Entity<TblSector>(entity =>
            {
                entity.HasKey(e => e.Sectorid);

                entity.ToTable("TBL_SECTOR", "Customer");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__tbl_Sect__737584F6E31E5F87")
                    .IsUnique();

                entity.Property(e => e.Sectorid).HasColumnName("SECTORID");

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(200);
            });
            modelBuilder.Entity<TblIndustry>(entity =>
            {
                entity.HasKey(e => e.Industryid);

                entity.ToTable("TBL_INDUSTRY", "Customer");

                entity.Property(e => e.Industryid).HasColumnName("INDUSTRYID");

                entity.Property(e => e.Isdeleted).HasColumnName("ISDELETED");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sectorid).HasColumnName("SECTORID");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.TblIndustry)
                    .HasForeignKey(d => d.Sectorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_INDUSTRY_TBL_SECTOR");
            });
        

          
        }
    }
}
