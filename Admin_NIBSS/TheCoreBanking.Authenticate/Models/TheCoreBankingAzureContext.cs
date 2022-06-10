using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheCoreBanking.Authenticate.Models
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=10.4.4.197;Database=TheCoreBankingAzure;user id=sa;password=sqluser10$;");
                // optionsBuilder.UseSqlServer(@"Server=10.4.4.197;Database=TheCoreBankingAzure;user id=sa;password=sqluser10$;");
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
