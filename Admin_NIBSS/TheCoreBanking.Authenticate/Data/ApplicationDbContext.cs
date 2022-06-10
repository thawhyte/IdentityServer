using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheCoreBanking.Authenticate.Models;

namespace TheCoreBanking.Authenticate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TblStaffInformation> TblStaffInformation { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=10.4.4.197;initial catalog=TheCoreBankingAzure;user id=sa;password=sqluser10$", builder =>
                // optionsBuilder.UseSqlServer(@"Server=OLUFEMI-ADEMOLA\SQLEXPRESS;initial catalog=TheCoreBankingAzure;user id=sa;password=sqluser10$", builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
               
                base.OnConfiguring(optionsBuilder);
               
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TblStaffInformation>(entity =>
            {
                entity.ToTable("tbl_StaffInformation", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Age).HasColumnType("datetime");

                entity.Property(e => e.BranchId)
                    .HasColumnName("BranchID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationCode)
            .HasMaxLength(100)
            .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Miscode)
                    .HasColumnName("MISCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinGender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NextOfKinPhone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PcCode)
                    .HasColumnName("pcCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rank)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RelationShip)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StaffNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Staffsignature)
                 .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ImageData)
                .HasColumnName("imageData")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ImageTitle)
                    .HasColumnName("imageTitle")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

        }
    }
}
