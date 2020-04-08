using AlimentosMarfim.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlimentosMarfim.AppDbContext
{
    public class Context : DbContext
    {

        public DbSet<Setor> Setores { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Cargo> Cargos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString: @"server=192.99.253.245;port=3306;database=DbAlimentos;uid=root;password=;");

        }
    }
}
