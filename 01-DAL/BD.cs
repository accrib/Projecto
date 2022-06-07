using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _01_DAL
{
    public class BD : DbContext
    {
        public BD(DbContextOptions<BD> options)
            : base(options)
        {
        }

        public DbSet<Empregado> Empregados { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Linha_Fatura> Linhas_Fatura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
