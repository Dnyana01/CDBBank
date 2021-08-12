using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDBBank.Models;

namespace CDBBank.Models
{
    public class CDBBankContext : DbContext
    {
        public CDBBankContext(DbContextOptions options) :base(options) { }

        public DbSet<BankUser> BankUsers { get; set; }
        public DbSet<AllUser> AllUsers { get; set; }
        
        public DbSet<NeftRecords> NeftRecords { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<ElectricityBill> ElectricityBills { get; set; }

       // public DbSet<FilterDates> FilterDates { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BankUser>().
                HasIndex(u => new { u.AccountNum, u.Username, u.Mobile }).
                IsUnique();
        }

       // public DbSet<FilterDates> FilterDates { get; set; }
        public DbSet<CDBBank.Models.FD> FD { get; set; }

       // public DbSet<FilterDates> FilterDates { get; set; }
        public DbSet<CDBBank.Models.RD> RD { get; set; }

    }
}
