using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Persistence.extensions;
using Persistence.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDatabaseStrategy _databaseStrategy;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDatabaseStrategy databaseStrategy)
          : base(options)
        {
            _databaseStrategy = databaseStrategy;
        }
        public ApplicationDbContext(IDatabaseStrategy databaseStrategy)
        {
            _databaseStrategy = databaseStrategy;
        }

        public ApplicationDbContext() : base()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _databaseStrategy.ConfigureOptions(optionsBuilder);
        }

        public void UseOtherDatabase()
        {
            _databaseStrategy.UseDatabase(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
