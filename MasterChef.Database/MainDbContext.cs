using MasterChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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

        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Tag> Tags { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainDbContext(IConfiguration config)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(_config.GetConnectionString("sqlite"));
            //optionsBuilder.UseSqlServer(_config.GetConnectionString("SqlServer"));
            optionsBuilder.UseMySql(_config.GetConnectionString("MySql"), ServerVersion.Create(new Version(), ServerType.MariaDb));
        }

    }
}
