using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.options;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.impl
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

        private readonly IDatabaseStrategy _databaseStrategy;
        // public IDatabaseStrategy _databaseStrategy { get; set; }


        /*public DatabaseContextFactory()
        {
            _databaseStrategy = new SqlServerDatabaseStrategy("Server=127.0.0.1,11433;Database=test_n5_now;User ID=sa;Password=SqlPass2022*;TrustServerCertificate=True;");
        }*/
        /*public DatabaseContextFactory() : this(null)
        {
        }*/


        public DatabaseContextFactory(IDatabaseStrategy databaseStrategy)
        {
            _databaseStrategy = databaseStrategy;
            //Console.WriteLine("Creo contexto");
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            try
            {
                Console.WriteLine($"{args.Length}");
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                _databaseStrategy.ConfigureOptions(optionsBuilder);
                return new ApplicationDbContext(optionsBuilder.Options, _databaseStrategy);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                throw e.InnerException;
            }

        }
    }
}
