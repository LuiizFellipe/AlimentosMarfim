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
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }


        // sobrescrevendo método do DbContext
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // usando banco MySql na nuvem, parâmetro : é passado a string de conexão com banco
            optionsBuilder.UseMySql(connectionString: @"server=192.99.253.245;port=3306;database=DbAlimentos;uid=root;password=;");

        }
    }
}
