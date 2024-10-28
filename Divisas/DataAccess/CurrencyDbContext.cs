using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divisas.Models;
using Divisas.Utilities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Divisas.DataAccess
{
    public class CurrencyDbContext: DbContext
    {
        //aqui se setean las tablas de la bd
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Settings> Settings { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = DBConnection.ReturnRoute("currencies.db");
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("La base de datos no existe en la ruta: " + dbPath);
            }
            else
            {
                Console.WriteLine("La base de datos se encuentra en: " + dbPath);

                // esta linea sirve para eliminar la BD
                //File.Delete(dbPath); 
            }

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