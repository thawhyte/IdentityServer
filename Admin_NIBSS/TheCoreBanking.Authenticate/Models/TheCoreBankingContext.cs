using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TheCoreBanking.Authenticate.Models;
using TheCoreBanking.Data.Models;

namespace TheCoreBanking.Authenticate.Models
{
    public partial class TheCoreBankingContext : DbContext
    {

        public TheCoreBankingContext()
        {
        }

        public TheCoreBankingContext(DbContextOptions<TheCoreBankingContext> options)
               : base(options)
        {
        }
        public virtual DbSet<TblBankingAuditTrail> TblBankingAuditTrail { get; set; }
        public virtual DbSet<TblStaffInformation> TblStaffInformation { get; set; }
        public virtual DbSet<RegisterViewModel> RegisterViewModel { get; set; }
        public virtual DbSet<TblSecurityUsers> TblSecurityUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=10.4.4.197;Database=TheCoreBankingAzure;User Id=sa;Password=sqluser10$;", builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
                base.OnConfiguring(optionsBuilder);
               
            }
           
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            });

            modelBuilder.HasSequence("TransactionSequence").HasMin(0);
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
            });
            modelBuilder.Entity<TblSecurityUsers>(entity =>
            {
                entity.HasKey(e => e.StaffNumber);

                entity.ToTable("tbl_SecurityUsers", "GeneralSetup");

                entity.Property(e => e.StaffNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Casareports).HasColumnName("CASAReports");

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastPasswordChange).HasColumnType("datetime");

                entity.Property(e => e.Miscode)
                    .HasColumnName("MISCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MultiBranch).HasColumnName("multiBranch");

                entity.Property(e => e.MultiCompany).HasColumnName("multiCompany");

                entity.Property(e => e.NextPasswordChange).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StaffNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TmpPassword).HasMaxLength(255);

                entity.Property(e => e.TmpPasswordAnswer).HasMaxLength(255);

                entity.Property(e => e.TmpPasswordQuestion).HasMaxLength(255);
            });






        }
    }
}
