using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InfraStructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            
            var account = modelBuilder.Entity<Account>();
            account.HasKey(a => a.Id);
          
            account.Property(b => b.Balance)
                .IsRequired()
                .HasColumnType(nameof(SqlDbType.Money));
            account.Property(b => b.Agency).HasMaxLength(20);
            account.Property(b => b.AccountNumber).HasMaxLength(20);

            modelBuilder.Entity<Transfer>()
                .Property(b => b.EntryId);
            modelBuilder.Entity<Transfer>()
                .Property(b => b.Amount)
                .HasColumnType(nameof(SqlDbType.Money));

            base.OnModelCreating(modelbuilder);
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transfer> Trasnfers { get; set; }
    }
}
