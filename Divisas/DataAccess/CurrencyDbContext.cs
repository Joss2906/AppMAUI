using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divisas.Models;
using Divisas.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Divisas.DataAccess
{
    public class CurrencyDbContext: DbContext
    {
        //aqui se setean las tablas de la bd
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbConnection = $"Filename={DBConnection.ReturnRoute("currencies.db")}";
            optionsBuilder.UseSqlite(dbConnection);
        }

        //modelado de la tabla
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(col => col.Id);
                entity.Property(col => col.Id).IsRequired().ValueGeneratedOnAdd(); //autoincremental
            });
        }
    }
}
