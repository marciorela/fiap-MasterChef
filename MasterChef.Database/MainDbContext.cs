using MasterChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Database
{
    public class MainDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Receita>? Receitas { get; set; }
        public DbSet<Tag>? Tags { get; set; }

        public MainDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_config.GetConnectionString("sqlite"));
        }

    }
}
